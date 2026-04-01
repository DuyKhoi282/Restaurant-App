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

            // Set nền panel trong suốt  
            panelLogin.BackColor = Color.FromArgb(130, 255, 255, 255);



            this.Resize += LoginForm_Resize;
            this.Shown += LoginForm_Shown;
        }

        // Biến lưu tỉ lệ giữa kích thước ban đầu của form và kích thước hiện tại để giữ cho panelLogin luôn ở giữa form khi form được resize
        double widthRatio = 0.55;   // panel chiếm 25% chiều ngang
        double heightRatio = 0.80;  // panel chiếm 35% chiều dọc



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

        //kết nối với database 
        SqlConnection conn = new SqlConnection(Database.connStr);

        private int CheckLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(Database.connStr))
            {
                string query = "SELECT type FROM account WHERE username=@user AND password=@pass";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result); // 0 hoặc 1

                return 2;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int type = CheckLogin(txtUsername.Text, txtPassword.Text);

            if (type == 0 || type == 1)
            {
                string role = (type == 1) ? "Admin" : "Staff";

                MessageBox.Show("Đăng nhập thành công! Role: " + role);

                frmMain main = new frmMain(role);
               
                // Khi MainForm đóng → đóng luôn app
                main.FormClosed += (s, args) => this.Close();

                main.Show();
                this.Hide();
            }
            else
            {
                if(txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }
                else
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
