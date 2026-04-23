using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public class AccountDTO //đối tượng dùng để truyền dữ liệu người dùng
    {
        public int RoleId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Birthday { get; set; }
        public decimal  Salary { get; set; }
        public int IsDeleted {  get; set; }
        public string ImagePath { get; set; }
    }
}
