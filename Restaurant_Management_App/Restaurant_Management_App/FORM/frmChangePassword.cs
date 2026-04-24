using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class frmChangePassword : Form
    {
        // 
        AccountDAL accountDAL = new AccountDAL();
        public frmChangePassword()
        {
            InitializeComponent();
        }


        private void btnUpdate_ChangePassword_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu
            string currentId = UserSession.UserId; // Giả sử lấy từ thông tin đăng nhập
            string oldPass = txtCurrentPass.Text.Trim();
            string newPass = txtNewPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            // 2. Kiểm tra bỏ trống
            if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass) || string.IsNullOrWhiteSpace(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // 3. Kiểm tra mật khẩu mới và nhập lại có khớp không
            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại không trùng khớp!");
                return;
            }

            // 4. Kiểm tra mật khẩu cũ có đúng với DB không
            if (!accountDAL.CheckOldPassword(currentId, oldPass))
            {
                MessageBox.Show("Mật khẩu hiện tại không chính xác!");
                return;
            }

            // 5. Thực hiện đổi
            if (accountDAL.ChangePassword(currentId, oldPass, newPass))
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
                this.Close(); // Đóng form
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!");
            }
        }

        private void btnBack_ResetPassword_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
