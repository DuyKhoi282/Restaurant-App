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


        // Hàm này chỉ lo việc làm đẹp cái bảng
        void FormatDataGridView()
        {
            if (dgvHistoryChangePass.Columns.Count > 0)
            {
                if (dgvHistoryChangePass.Columns["Id"] != null) dgvHistoryChangePass.Columns["Id"].Visible = false;
                dgvHistoryChangePass.Columns["fullname"].HeaderText = "Họ tên";
                dgvHistoryChangePass.Columns["userId"].HeaderText = "Mã Nhân Viên";
                dgvHistoryChangePass.Columns["oldPassword"].HeaderText = "Mật Khẩu Cũ";
                dgvHistoryChangePass.Columns["newPassword"].HeaderText = "Mật Khẩu Mới";
                dgvHistoryChangePass.Columns["changeDate"].HeaderText = "Ngày Thay Đổi";
                dgvHistoryChangePass.Columns["changeDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvHistoryChangePass.Columns["changedBy"].HeaderText = "Người Thực Hiện";
                dgvHistoryChangePass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Hàm Load dữ liệu ban đầu
        void LoadHistory(string id = "")
        {
            dgvHistoryChangePass.DataSource = accountDAL.GetPasswordHistory(id);
            FormatDataGridView();
        }

        private void frmChangePasswordHistory_Load(object sender, EventArgs e)
        {
            LoadUserListToComboBox(); // Đổ danh sách vào ComboBox

            // Nếu Form được mở kèm theo một ID cụ thể (từ Form Profile)
            if (!string.IsNullOrEmpty(targetUserId))
            {
                cbxIdUser_ChangePass.SelectedValue = targetUserId;
                LoadHistory(targetUserId);
            }
            else
            {
                LoadHistory(""); // Mặc định load hết hoặc để trống
            }
        }

        void LoadUserListToComboBox()
        {
            // 1. Lấy dữ liệu thực tế từ Database
            DataTable dt = accountDAL.GetListAccountForCbo();

            // 2. Tạo một dòng mới hoàn toàn thủ công
            DataRow dr = dt.NewRow();
            dr["userId"] = "ALL";       // Giá trị hiển thị và cũng là giá trị ẩn
            dr["fullname"] = "Tất cả";  // Tên bổ trợ (nếu cần)

            // 3. Chèn dòng "ALL" này vào vị trí đầu bảng (vị trí 0)
            dt.Rows.InsertAt(dr, 0);

            // 4. Gán vào ComboBox
            cbxIdUser_ChangePass.DataSource = dt;
            cbxIdUser_ChangePass.DisplayMember = "userId"; // ComboBox sẽ hiện chữ "ALL", "NV001", "NV002"...
            cbxIdUser_ChangePass.ValueMember = "userId";

            // 5. Mặc định chọn dòng "ALL" khi vừa mở Form
            cbxIdUser_ChangePass.SelectedIndex = 0;
        }

        private void cbxIdUser_ChangePass_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Đảm bảo có giá trị được chọn
            if (cbxIdUser_ChangePass.SelectedValue != null)
            {
                string selectedId = cbxIdUser_ChangePass.SelectedValue.ToString();

                if (selectedId == "ALL")
                {
                    // Nếu chọn ALL, ta truyền chuỗi rỗng "" vào hàm DAL 
                    // Hàm GetPasswordHistory của bạn đã code: nếu là "" thì SELECT ALL
                    dgvHistoryChangePass.DataSource = accountDAL.GetPasswordHistory("");
                }
                else
                {
                    // Nếu chọn mã nhân viên cụ thể (NV001, NV002...)
                    dgvHistoryChangePass.DataSource = accountDAL.GetPasswordHistory(selectedId);
                }

                // Làm đẹp lại các cột sau khi nạp dữ liệu mới
                FormatDataGridView();
            }
        }
    }
}
