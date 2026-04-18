using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DataTable GetAllUsers() // Hàm lấy danh sách tất cả người dùng trong hệ thống - các tài khoản đã được tạo 
        {
            string query = @"
    SELECT 
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
    JOIN Role r ON a.RoleId = r.Id";

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

        public bool UpdateAccount(string password, string  userId, string fullname, DateTime birthday, string phone, string address, string ward, string district, string city, double salary)
        {
            // Câu lệnh SQL (Không cần chữ N trước tham số vì dữ liệu đã là không dấu)
            string query = "UPDATE Account SET password = @pass , fullname = @name , birthday = @birth , phone = @p , address = @addr , ward = @w , district = @d , city = @c , salary = @s WHERE userId = @id";

            // Truyền mảng tham số vào
            object[] parameter = new object[] { password, fullname, birthday, phone, address, ward, district, city, salary, userId };

            int result = Database.Instance.ExecuteNonQuery(query, parameter);

            return result > 0;
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
            // Câu lệnh 1: Cập nhật mật khẩu mới
            string queryUpdate = "UPDATE Account SET Password = @new WHERE userId = @id";

            // Câu lệnh 2: Lưu lịch sử
            string queryHistory = "INSERT INTO PasswordHistory (userId, oldPassword, newPassword, changedBy) VALUES ( @id , @old , @new , @change )";

            // Thực thi (Lưu ý: Bạn nên viết một hàm ExecuteTransaction trong lớp Database để chạy cả 2 câu này)
            int r1 = Database.Instance.ExecuteNonQuery(queryUpdate, new object[] { newPass, userId });
            int r2 = Database.Instance.ExecuteNonQuery(queryHistory, new object[] { userId, oldPass, newPass, userId });

            
            return r1 > 0 && r2 > 0;
        }

        // Hàm dành cho Admin: Đổi mật khẩu mà không cần mật khẩu cũ
        public bool AdminUpdateAccount(string userId, string newPass, string adminId)
        {
            // 1. Cập nhật mật khẩu mới (và các thông tin khác nếu cần)
            string queryUpdate = "UPDATE Account SET Password = @new WHERE userId = @id";

            // 2. Lấy mật khẩu hiện tại trước khi đổi để lưu vào lịch sử (optional)
            // Nếu bạn muốn cột oldPassword trong lịch sử có giá trị thật
            string currentPass = GetCurrentPassword(userId);

            // 3. Lưu lịch sử
            string queryHistory = @"INSERT INTO PasswordHistory (userId, oldPassword, newPassword, changedBy) 
                            VALUES ( @id , @old , @new , @admin )";

            int r1 = Database.Instance.ExecuteNonQuery(queryUpdate, new object[] { newPass, userId });
            int r2 = Database.Instance.ExecuteNonQuery(queryHistory, new object[] { userId, currentPass, newPass, adminId });

            return r1 > 0 && r2 > 0;
        }

        // Hàm phụ để lấy mật khẩu hiện tại (phục vụ việc ghi log)
        private string GetCurrentPassword(string userId)
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
    }
}