using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public static class Database
    {
        // Chuỗi kết nối đến cơ sở dữ liệu SQL Server,
        // sử dụng Integrated Security để xác thực bằng tài khoản Windows
        public static string connStr =
        @"Data Source=DESKTOP-OV7KJ7S\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
    }
}
