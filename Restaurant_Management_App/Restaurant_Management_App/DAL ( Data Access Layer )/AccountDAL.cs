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
        a.UserId,        
        a.FullName,
        a.Phone,
        a.Email,
        r.RoleName
    FROM Account a
    JOIN Role r ON a.RoleId = r.Id";

            return Database.Instance.ExecuteQuery(query); //
        }
    }
}