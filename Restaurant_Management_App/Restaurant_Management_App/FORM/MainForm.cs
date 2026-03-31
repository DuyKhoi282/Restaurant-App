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
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            //Form có viền và có thể thay đổi kích thước(resize)
            this.FormBorderStyle = FormBorderStyle.Sizable;
            //Form sẽ giới hạn kích thước tối thiểu là 1300x700 để đảm bảo giao diện không bị quá nhỏ
            this.MinimumSize = new Size(1300, 700);

            this.Resize += MainForm_Resize;
            this.Shown += MainForm_Shown;
        }

        // Biến lưu tỉ lệ giữa kích thước ban đầu của form và kích thước hiện tại để giữ cho panelLogin luôn ở giữa form khi form được resize
        double widthRatio = 0.55;   // panel chiếm 25% chiều ngang
        double heightRatio = 0.80;  // panel chiếm 35% chiều dọc

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            tlpSidebar.Width = Math.Max(180, this.Width / 6);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            MainForm_Resize(null, null);
        }
    }
}
