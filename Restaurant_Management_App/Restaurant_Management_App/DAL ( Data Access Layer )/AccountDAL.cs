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
        public AccountDTO Login(string userId, string password)
        {
            string query = @"
        SELECT a.UserId, a.fullName, r.RoleName
        FROM Account a
        JOIN Role r ON a.RoleId = r.Id
        WHERE a.UserId = @userId AND a.Password = @password";

            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { userId, password });

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new AccountDTO()
            {
                UserId = row["userId"].ToString(),
                FullName = row["fullName"].ToString(),
                RoleName = row["RoleName"].ToString()
            };
        }

        public AccountDTO GetUserById(string userId)
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

            DataTable dt = Database.Instance.ExecuteQuery(query, new object[] { userId });

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new AccountDTO()
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

        public DataTable GetAllUsers()
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

            return Database.Instance.ExecuteQuery(query);
        }
    }
}