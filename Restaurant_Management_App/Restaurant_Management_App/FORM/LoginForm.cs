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
    public partial class LoginForm : Form
    {
        public LoginForm()
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

    }
}
