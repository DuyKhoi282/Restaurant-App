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
    public partial class frmCreateUser_Authority : Form
    {
        public frmCreateUser_Authority()
        {
            InitializeComponent();
        }

        private void frmCreateUser_Authority_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Database.connStr)) // Kết nối đến cơ sở dữ liệu
            {
                conn.Open(); // Mở kết nối

                string query = "SELECT Id, RoleName FROM Role"; // Truy vấn để lấy danh sách quyền từ bảng Role
                SqlDataAdapter da = new SqlDataAdapter(query, conn);// Tạo SqlDataAdapter để thực thi truy vấn và điền dữ liệu vào DataTable
                DataTable dt = new DataTable();// Tạo DataTable để lưu trữ dữ liệu truy vấn
                da.Fill(dt);// Điền dữ liệu vào DataTable

                cbxRole_CUA.DataSource = dt;// Thiết lập DataSource cho ComboBox để hiển thị danh sách quyền
                cbxRole_CUA.DisplayMember = "RoleName";// Thiết lập DisplayMember để hiển thị tên quyền trong ComboBox
                cbxRole_CUA.ValueMember = "Id";// Thiết lập ValueMember để lấy giá trị Id của quyền khi chọn trong ComboBox
            }

            LoadAccountList();

            dgvAccount.RowPostPaint += dgvAccount_RowPostPaint_1;
        }

        void LoadAccountList()
        {
            AccountDAL dao = new AccountDAL();
            dgvAccount.DataSource = dao.GetAllUsers();
            dgvAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccount.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;            
        }

        private void btnCreate_CUA_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox và ComboBox
            string userId = txtUserId_CUA.Text.Trim();// Lấy tên đăng nhập từ TextBox và loại bỏ khoảng trắng ở đầu và cuối
            string password = txtPassword_CUA.Text.Trim();
            string fullname = txtFullname_CUA.Text.Trim();
            string phone = txtPhone_CUA.Text.Trim();
            string email = txtEmail_CUA.Text.Trim();
            DateTime birthdate = dtpBirthday_CUA.Value;
            string address = txtAddress_CUA.Text.Trim();
            string ward = cbxWard_CUA.Text.Trim();
            string city = cbxCity_CUA.Text.Trim();
            string district = cbxDistrict_CUA.Text.Trim();
            string displayname = cbxRole_CUA.Text.Trim();
            // Lấy RoleId từ ComboBox
            int roleId = Convert.ToInt32(cbxRole_CUA.SelectedValue);

            using (SqlConnection conn = new SqlConnection(Database.connStr))
            {
                conn.Open();// Mở kết nối đến cơ sở dữ liệu

                string query = @"INSERT INTO Account
        (userId, password, displayName, RoleId, fullname, phone, email, birthday, address, ward, district, city)
        VALUES (@u, @p,@di, @r, @f, @ph, @e, @b,@a,@w,@d,@c)"; // Truy vấn SQL để chèn một tài khoản mới vào bảng Account
                SqlCommand cmd = new SqlCommand(query, conn);// Tạo SqlCommand để thực thi truy vấn

                cmd.Parameters.AddWithValue("@u", userId);// Thêm tham số @u với giá trị username vào SqlCommand
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@di", displayname);
                cmd.Parameters.AddWithValue("@r", roleId);
                cmd.Parameters.AddWithValue("@f", fullname);
                cmd.Parameters.AddWithValue("@ph", phone);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@b", birthdate);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.Parameters.AddWithValue("@w", ward);
                cmd.Parameters.AddWithValue("@d", district);
                cmd.Parameters.AddWithValue("@c", city);               

                // check tài khoản đã tồn tại chưa 
                string checkQuery = "SELECT COUNT(*) FROM Account WHERE UserId = @u";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@u", userId);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("UserID đã tồn tại!");
                    return;
                }

                if (userId == "" || password == "" )
                {
                    MessageBox.Show("Nhập đầy đủ thông tin!");
                    return;
                }

                cmd.ExecuteNonQuery();// Thực thi truy vấn để chèn dữ liệu vào cơ sở dữ liệu

                MessageBox.Show("Tạo tài khoản thành công!");
            }
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAccount.Rows[e.RowIndex];

                // TEXTBOX
                txtUserId_CUA.Text = row.Cells["userId"].Value?.ToString();
                txtPassword_CUA.Text = row.Cells["password"].Value?.ToString();
                txtFullname_CUA.Text = row.Cells["fullname"].Value?.ToString();
                txtPhone_CUA.Text = row.Cells["phone"].Value?.ToString();
                txtEmail_CUA.Text = row.Cells["email"].Value?.ToString();
                txtAddress_CUA.Text = row.Cells["address"].Value?.ToString();

                // COMBOBOX
                cbxWard_CUA.Text = row.Cells["ward"].Value?.ToString();
                cbxDistrict_CUA.Text = row.Cells["district"].Value?.ToString();
                cbxCity_CUA.Text = row.Cells["city"].Value?.ToString();

                // DATETIMEPICKER
                if (row.Cells["birthday"].Value != DBNull.Value)
                {
                    dtpBirthday_CUA.Value = Convert.ToDateTime(row.Cells["birthday"].Value);
                }
            }
        }
    

        private void dgvAccount_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Thiết lập số thứ tự
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            // Định dạng font và căn lề cho số thứ tự
            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Xác định vị trí để vẽ (vùng Header của dòng hiện tại)
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);

            // Vẽ số thứ tự lên Header bằng Graphics (không làm thay đổi dữ liệu nên không gây lặp)
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btnBack_CUA_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }    
}
