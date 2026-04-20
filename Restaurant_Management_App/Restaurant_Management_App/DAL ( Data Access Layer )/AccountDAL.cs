using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public class AccountDAL
    {
        public AccountDTO Login(string userId, string password) // hàm login được gọi ở frmLogin
        {
            string query = @"
        SELECT a.UserId, a.fullName, r.RoleName
        FROM Account a
        JOIN Role r ON a.RoleId = r.Id
        WHERE a.UserId = @userId AND a.Password = @password";

            // Thực thi truy vấn và lấy dữ liệu
            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { userId, password });

            if (dt.Rows.Count == 0) // không có tài khoản nào phù hợp với userId và password đã nhập
                return null;

            DataRow row = dt.Rows[0]; // Lấy dòng dữ liệu đầu tiên (vì userId là duy nhất nên chỉ có một dòng)

            return new AccountDTO() // Trả về một đối tượng AccountDTO chứa thông tin người dùng đã tìm thấy 
            {
                UserId = row["userId"].ToString(),
                FullName = row["fullName"].ToString(),
                RoleName = row["RoleName"].ToString()
            };
        }

        public AccountDTO GetUserById(string userId) // Hàm lấy thông tin chi tiết của người dùng đã được đăng nhập
        {
            string query = @"
    SELECT 
        a.userId,
        a.fullName,
        a.phone,
        a.email,
        (a.address + ' ' + a.ward + ' ' + a.district + ' ' + a.city) AS fullAddress,
        a.birthday,
        a.salary,
        r.RoleName
    FROM Account a
    JOIN Role r ON a.RoleId = r.Id
    WHERE a.UserId = @userId";

            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { userId }); // truy vấn đối tượng người dùng theo userId

            if (dt.Rows.Count == 0) // không tìm thấy 
                return null;

            DataRow row = dt.Rows[0]; // đối tượng phù hợp với userId

            return new AccountDTO() // Trả về một đối tượng AccountDTO chứa thông tin chi tiết của người dùng
            {
                UserId = row["userId"]?.ToString(),
                FullName = row["fullName"]?.ToString(),
                Phone = row["phone"]?.ToString(),
                Email = row["email"]?.ToString(),
                Address = row["fullAddress"]?.ToString(),
                Birthday = row["birthday"]?.ToString(),
                RoleName = row["RoleName"]?.ToString()
            };
        }

        //HIỂN THỊ USERID LÊN CBX Ở FORM CHANGE PASSWORD HISTORY
        public DataTable GetListAccountForCbo()
        {
            // Chỉ lấy 2 cột cần thiết để ComboBox hoạt động mượt mà
            string query = "SELECT userId, fullname FROM Account ORDER BY fullname ASC";
            return Database.Instance.ExecuteQuery(query);
        }

        public DataTable GetAllUsers() // Hàm lấy danh sách tất cả người dùng trong hệ thống - các tài khoản đã được tạo 
        {
            string query = @"
    SELECT 
        a.RoleId,
        a.userId,   
        a.password,
        a.fullname,
        a.phone,
        a.email,
        a.address,
        a.ward,
        a.district,
        a.city,
        a.birthday,
        a.salary,
        r.RoleName
    FROM Account a
    JOIN Role r ON a.RoleId = r.Id
    WHERE a.isDeleted = 0";

            return Database.Instance.ExecuteQuery(query); //
        }

        public DataTable GetCityList()
        {
            return Database.Instance.ExecuteQuery("SELECT * FROM city");
        }

        public DataTable GetDistrictListByCity(string cityId)
        {
            string query = "SELECT * FROM district WHERE cityId = @id";            
            return Database.Instance.ExecuteQuery(query, new object[] { cityId });
        }

        public DataTable GetWardListByDistrict(string districtId)
        {
            string query = "SELECT * FROM ward WHERE districtId = @id";
            return Database.Instance.ExecuteQuery(query, new object[] { districtId });
        }

        public bool UpdateAccount(string password, string userId, string fullname, DateTime birthday, string phone, string address, string ward, string district, string city, decimal salary, string email, int roleId)
        {
            string queryUpdate = "UPDATE Account SET password = @pass , fullname = @f , birthday = @b , phone = @ph , address = @a , ward = @w , district = @d , city = @c , salary = @s , email = @e , RoleId = @r WHERE userId = @id";

            // Mảng tham số: Đếm đủ 12 và đúng thứ tự dấu @ từ trái qua phải
            object[] tempParams = new object[] {
        password,   // 1. @pass
        fullname,   // 2. @f
        birthday,   // 3. @b
        phone,      // 4. @ph
        address,    // 5. @a
        ward,       // 6. @w
        district,   // 7. @d
        city,       // 8. @c
        salary,     // 9. @s
        email,      // 10. @e
        roleId,     // 11. @r
        userId      // 12. @id
    };

            try
            {
                return Database.Instance.ExecuteNonQuery(queryUpdate, tempParams) > 0;
            }
            catch (Exception ex)
            {
                // Hiện lỗi này để biết chính xác SQL đang nhận được gì
                MessageBox.Show("Lỗi thực thi SQL: " + ex.Message);
                return false;
            }
        }

        //==============================================
        // Change Password - Đổi mật khẩu
        //==============================================
        // Kiểm tra mật khẩu cũ
        public bool CheckOldPassword(string userId, string oldPass)
        {
            string query = "SELECT COUNT(*) FROM Account WHERE userId = @id AND Password = @pass";
            DataTable result = Database.Instance.ExecuteQuery(query, new object[] { userId, oldPass });

            // Kiểm tra xem DataTable có dòng nào không trước khi truy cập Rows[0]
            if (result.Rows.Count > 0)
            {
                return (int)result.Rows[0][0] > 0;
            }
            return false;
        }

        // Đổi mật khẩu và lưu lịch sử
        public bool ChangePassword(string userId, string oldPass, string newPass)
        {
            // 1. Cập nhật bảng Account
            string q1 = "UPDATE Account SET Password = @new WHERE userId = @id";
            Database.Instance.ExecuteNonQuery(q1, new object[] { newPass, userId });

            // 2. Ghi lịch sử (Sử dụng UserSession.UserId để biết ai là người thực hiện)
            string q2 = "INSERT INTO PasswordHistory ( userId , oldPassword , newPassword , changedBy ) VALUES ( @id , @old , @new , @by )";
            int result = Database.Instance.ExecuteNonQuery(q2, new object[] { userId, oldPass, newPass, UserSession.UserId });

            return result > 0;
        }

        public bool AdminUpdateAccount(string targetUserId, string newPass)
        {
            // Lấy mật khẩu cũ để làm cột oldPassword trong lịch sử
            string oldPass = GetCurrentPassword(targetUserId);

            // 1. Cập nhật mật khẩu mới vào bảng Account
            string queryUpdate = "UPDATE Account SET Password = @new WHERE userId = @id";
            int r1 = Database.Instance.ExecuteNonQuery(queryUpdate, new object[] { newPass, targetUserId });

            // 2. Ghi log vào bảng PasswordHistory
            // changedBy = UserSession.UserId (Đây là ID của Admin đang cầm máy)
            string queryHistory = "INSERT INTO PasswordHistory ( userId , oldPassword , newPassword , changedBy ) " +
                                  "VALUES ( @id , @old , @new , @admin )";

            int r2 = Database.Instance.ExecuteNonQuery(queryHistory, new object[] { targetUserId, oldPass, newPass, UserSession.UserId });

            return r1 > 0 && r2 > 0;
        }

        // Hàm phụ để lấy mật khẩu hiện tại (phục vụ việc ghi log)
        public string GetCurrentPassword(string userId)
        {
            string query = "SELECT Password FROM Account WHERE userId = @id";
            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { userId });
            return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
        }

        // Lấy lịch sử đổi mật khẩu (có thể lọc theo userId nếu muốn)
        public DataTable GetPasswordHistory(string userId = "")
        {
            string query = @"SELECT ph.Id,a.fullname ,a. userId, ph.oldPassword, ph.newPassword, ph.changeDate, ph.changedBy 
                     FROM PasswordHistory ph 
                     JOIN Account a ON ph.userId = a.userId ";

            // Nếu có userId thì thêm điều kiện lọc
            if (!string.IsNullOrEmpty(userId))
            {
                query += " WHERE ph.userId = @id ";
                query += " ORDER BY ph.changeDate DESC";
                return Database.Instance.ExecuteQuery(query, new object[] { userId });
            }

            // Nếu không có thì lấy tất cả
            query += " ORDER BY ph.changeDate DESC";
            return Database.Instance.ExecuteQuery(query);
        }

        public bool DeleteAccount(string userId)
        {
            // Thay vì DELETE, chúng ta UPDATE cột isDeleted lên 1
            string query = "UPDATE Account SET isDeleted = 1 WHERE userId = @id";

            int result = Database.Instance.ExecuteNonQuery(query, new object[] { userId });

            return result > 0;
        }
    }
}