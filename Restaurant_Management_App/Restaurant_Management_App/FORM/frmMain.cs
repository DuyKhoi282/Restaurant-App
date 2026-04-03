using Restaurant_Management_App.FORM;
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
    public partial class frmMain : Form
    {
        string role;
        public frmMain(string role)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            //Form có viền và có thể thay đổi kích thước(resize)
            this.FormBorderStyle = FormBorderStyle.Sizable;
            //Form sẽ giới hạn kích thước tối thiểu là 1300x700 để đảm bảo giao diện không bị quá nhỏ
            this.MinimumSize = new Size(1300, 700);

            // Khi form thay đổi kích thước sẽ gọi hàm MainForm_Resize để điều chỉnh kích thước 
            this.Resize += MainForm_Resize;
            // Khi form được hiển thị sẽ gọi hàm MainForm_Shown để đảm bảo kích thước sidebar chính xác
            this.Shown += MainForm_Shown;

            // Set quyền truy cập dựa trên vai trò
            //Admin có tất cả quyền 
            //Staff chỉ có quyền tạo đơn, quản lí đơn, chăm sóc khách hàng.
            this.role=role;
            if (role == "Admin")
            {
                btnStaffMNG.Visible = true;
                btnRevenueMNG.Visible = true;
                btnItemManagement.Visible = true;
            }
            else
            {
                btnStaffMNG.Visible = false;
                btnRevenueMNG.Visible = false;
                btnItemManagement.Visible = false;
            }
        }
        
        private void MainForm_Resize(object sender, EventArgs e)//Hàm này dùng để tự động điều chỉnh kích thước của sidebar khi form thay đổi kích thước
        {
            tlpSidebar.Width = Math.Max(180, this.Width / 6); // Đặt chiều rộng của sidebar bằng 1/6 chiều rộng của form, nhưng không nhỏ hơn 180px
        }

        private void MainForm_Shown(object sender, EventArgs e)//Hàm này dùng để gọi hàm resize lần đầu khi form được hiển thị để đảm bảo sidebar có kích thước phù hợp ngay từ đầu
        {
            MainForm_Resize(null, null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)//Hàm này dùng để đảm bảo khi main form đóng thì sẽ đóng luôn ứng dụng
        {
            // Khi main form đóng, sẽ đóng luôn ứng dụng
            Application.Exit();
        }

        // Nút đăng xuất sẽ quay về frmLogin
        private void btnSignout_Click(object sender, EventArgs e)
        {
            frmLogin LoginForm = new frmLogin();
            LoginForm.Show();
            this.Hide();
        }
        private void LoadForm(Form frm)//Hàm này dùng để load form con vào panel chinh
        {
            pnlMain.Controls.Clear(); // Xóa form cũ nếu có

            frm.TopLevel = false; // Đặt form con không phải là top-level
            frm.FormBorderStyle = FormBorderStyle.None; // Loại bỏ border của form con
            frm.Dock = DockStyle.Fill; // Đặt form con chiếm toàn bộ panel

            pnlMain.Controls.Add(frm); // Thêm form con vào panel
            frm.Show(); // Hiển thị form con
        }

        private void btnItemMNG_Click(object sender, EventArgs e)
        {
            LoadForm(new frmItemManagement());
        }

        private void btnRevenueMNG_Click(object sender, EventArgs e)
        {
            LoadForm(new frmMenuReveneu());

        }

        private void btnOrderMNG_Click(object sender, EventArgs e)
        {
            LoadForm(new frmOrderManegement());
        }
    }
}
