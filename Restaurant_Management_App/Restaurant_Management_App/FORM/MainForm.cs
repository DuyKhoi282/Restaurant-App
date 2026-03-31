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
    public partial class MainForm : Form
    {
        string role;
        public MainForm(string role)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            //Form có viền và có thể thay đổi kích thước(resize)
            this.FormBorderStyle = FormBorderStyle.Sizable;
            //Form sẽ giới hạn kích thước tối thiểu là 1300x700 để đảm bảo giao diện không bị quá nhỏ
            this.MinimumSize = new Size(1300, 700);

            this.Resize += MainForm_Resize;
            this.Shown += MainForm_Shown;

            // Set quyền truy cập dựa trên vai trò
            //Admin có tất cả quyền 
            //Staff chỉ có quyền tạo đơn, quản lí đơn, chăm sóc khách hàng.
            this.role=role;
            if (role == "Admin")
            {
                btnStaffMNG.Visible = true;
                btnRevenueMNG.Visible = true;
                btnItemMNG.Visible = true;
            }
            else
            {
                btnStaffMNG.Visible = false;
                btnRevenueMNG.Visible = false;
                btnItemMNG.Visible = false;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            tlpSidebar.Width = Math.Max(180, this.Width / 6);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            MainForm_Resize(null, null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Khi main form đóng, sẽ đóng luôn ứng dụng
            Application.Exit();
        }
    }
}
