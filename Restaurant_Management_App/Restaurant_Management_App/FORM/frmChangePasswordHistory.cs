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
    public partial class frmChangePasswordHistory : Form
    {
        AccountDAL accountDAL = new AccountDAL();
        private string targetUserId; // Biến lưu ID được truyền sang

        public frmChangePasswordHistory(string userId = "")
        {
            InitializeComponent();
            this.targetUserId = userId; // Gán ID nhận được vào biến tạm
        }
       

        void LoadHistory()
        {
            // Gọi hàm DAL với biến targetUserId
            dgvHistoryChangePass.DataSource = accountDAL.GetPasswordHistory(targetUserId);

            //if (dgvHistoryChangePass.Columns["Id"] != null) dgvHistoryChangePass.Columns["Id"].Visible = false; // Ẩn ID vì user không cần xem

            //dgvHistoryChangePass.Columns["fullname"].HeaderText = "Tài Khoản";
            dgvHistoryChangePass.Columns["oldPassword"].HeaderText = "Mật Khẩu Cũ";
            dgvHistoryChangePass.Columns["newPassword"].HeaderText = "Mật Khẩu Mới";

            // Định dạng Ngày tháng (Rất quan trọng)
            dgvHistoryChangePass.Columns["changeDate"].HeaderText = "Ngày Thay Đổi";
            dgvHistoryChangePass.Columns["changeDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dgvHistoryChangePass.Columns["changedBy"].HeaderText = "Người Thực Hiện";

            // Căn giữa dữ liệu cho cột Ngày tháng và Người thực hiện
            dgvHistoryChangePass.Columns["changeDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistoryChangePass.Columns["changedBy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            /// Hoặc nếu muốn cột tự giãn theo nội dung dài nhất bên trong nó:
            dgvHistoryChangePass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void frmChangePasswordHistory_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }
    }
}
