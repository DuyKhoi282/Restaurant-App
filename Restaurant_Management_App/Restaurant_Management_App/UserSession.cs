using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public static class UserSession // biến toàn cục giúp lưu dữ liệu của user đã đăng nhập để sử dụng ở các form khác nhau trong ứng dụng
    {
        public static string UserId { get; set; }
        public static string FullName { get; set; }
        public static string RoleName { get; set; } // Để phân quyền ẩn/hiện nút
        public static string ImagePath { get; set; }
        public static int IsDeleted { get; set; }
    }
}
