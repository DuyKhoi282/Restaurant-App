using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            //Form ban đầu mở ra sẽ ở trạng thái phóng to(maximize)
            this.WindowState = FormWindowState.Maximized;
            //Form có viền và có thể thay đổi kích thước(resize)
            this.FormBorderStyle = FormBorderStyle.Sizable;
            //Form sẽ giới hạn kích thước tối thiểu là 1300x700 để đảm bảo giao diện không bị quá nhỏ
            this.MinimumSize = new Size(1300, 700);

            // Set nền panelLogin trong suốt  
            panelLogin.BackColor = Color.FromArgb(130, 255, 255, 255);


            // Khi form thay đổi kích thước sẽ gọi hàm LoginForm_Resize để điều chỉnh kích thước 
            this.Resize += LoginForm_Resize;
            // Khi form được hiển thị sẽ gọi hàm LoginForm_Shown 
            this.Shown += LoginForm_Shown;
        }

        // Biến lưu tỉ lệ giữa kích thước ban đầu của form và kích thước hiện tại để giữ cho panelLogin luôn ở giữa form khi form được resize
        double widthRatio = 0.55;   // panel chiếm 55% chiều ngang
        double heightRatio = 0.80;  // panel chiếm 80% chiều dọc



        // fix lỗi lệch khi maximize + lấy tỉ lệ chuẩn
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            // gọi resize lần đầu
            LoginForm_Resize(null, null);
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            int newWidth = (int)(this.ClientSize.Width * widthRatio);
            int newHeight = (int)(this.ClientSize.Height * heightRatio);

            // Giới hạn kích thước
            newWidth = Math.Max(350, Math.Min(500, newWidth));
            newHeight = Math.Max(250, Math.Min(400, newHeight));

            panelLogin.Size = new Size(newWidth, newHeight);

            // Căn giữa
            panelLogin.Left = (this.ClientSize.Width - panelLogin.Width) / 2;
            panelLogin.Top = (this.ClientSize.Height - panelLogin.Height) / 2;
        }

        //Đã set hình background và set stretch trong properties

        //Đã set thêm TableLayoutPanel vào bên trong PanelLogin 
        //Setting Properties Dock = Fill  | ColumnCount = 1
        //                   RowCount = 6 | Padding = 20
        // Đã setting Rows 

            private void btnLogin_Click(object sender, EventArgs e)
        {
            // lấy dữ liệu từ 2 textbox, dùng hàm Trim() để loại bỏ khoảng trắng thừa ở đầu và cuối
            string userId  = txtUserId.Text.Trim();
            string password = txtPassword.Text.Trim();

            AccountDAL dao = new AccountDAL(); // dao giúp tách biệt logic winform và logic truy cập dữ liệu, giúp code dễ bảo trì hơn
            var user = dao.Login(userId, password); // var giúp tự động suy luận kiểu dữ liệu trả về từ hàm Login

            if (user != null)
            {
                MessageBox.Show("Đăng nhập thành công!");

                // lưu trạng thái đăng nhập của người dùng vào UserSession để sử dụng trong các form khác
                UserSession.UserId = user.UserId;           

                frmMain main = new frmMain(user.RoleName);
                main.Show();
                this.Hide();
            }
            else if ( userId == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
            else 
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }
        

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUserId.Focus();
        }
    }
}
