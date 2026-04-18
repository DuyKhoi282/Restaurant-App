using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class frmCreateUser_Authority : Form
    {
        AccountDAL AccountDAL = new AccountDAL();
        public frmCreateUser_Authority()
        {
            InitializeComponent();
            LoadCity();
        }

        private void frmCreateUser_Authority_Load(object sender, EventArgs e)//
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

            LoadAccountList(); //load ds tài khoản lên DataGridView

            dgvAccount.RowPostPaint += dgvAccount_RowPostPaint_1; //số thứ tự mỗi dòng trong dgv
         
            cbxCity_CUA.SelectedIndex = -1; // Để trống lúc đầu
            cbxRole_CUA.SelectedIndex = -1;
            cbxDistrict_CUA.Enabled = false;
            cbxWard_CUA.Enabled = false;
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
            decimal salary = txtSalary_CUA.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSalary_CUA.Text.Trim());
            // Lấy RoleId từ ComboBox
            int roleId = Convert.ToInt32(cbxRole_CUA.SelectedValue);

            using (SqlConnection conn = new SqlConnection(Database.connStr))
            {
                conn.Open();// Mở kết nối đến cơ sở dữ liệu

                string query = @"INSERT INTO Account
        (userId, password, RoleId, fullname, phone, email, birthday, address, ward, district, city, salary)
        VALUES (@u, @p, @r, @f, @ph, @e, @b,@a,@w,@d,@c,@s)"; // Truy vấn SQL để chèn một tài khoản mới vào bảng Account
                SqlCommand cmd = new SqlCommand(query, conn);// Tạo SqlCommand để thực thi truy vấn

                cmd.Parameters.AddWithValue("@u", userId);// Thêm tham số @u với giá trị username vào SqlCommand
                cmd.Parameters.AddWithValue("@p", password);          
                cmd.Parameters.AddWithValue("@r", roleId);
                cmd.Parameters.AddWithValue("@f", fullname);
                cmd.Parameters.AddWithValue("@ph", phone);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@b", birthdate);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.Parameters.AddWithValue("@w", ward);
                cmd.Parameters.AddWithValue("@d", district);
                cmd.Parameters.AddWithValue("@c", city);          
                cmd.Parameters.AddWithValue("@s", salary);

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

        // click vào 1 dòng sẽ hiện thị thông tin chi tiết của tài khoản đó lên các TextBox, ComboBox và DateTimePicker bên dưới 
        // để dễ dàng chỉnh sửa hoặc xem thông tin chi tiết
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
                txtSalary_CUA.Text = row.Cells["salary"].Value?.ToString();

                // COMBOBOX
                
                cbxCity_CUA.Text = row.Cells["city"].Value?.ToString();

                cbxDistrict_CUA.Enabled = true; // Bật ComboBox quận/huyện
                cbxWard_CUA.Enabled = true; // Bật ComboBox phường/xã

                if (cbxCity_CUA.SelectedValue != null)
                {
                    string cityId = cbxCity_CUA.SelectedValue.ToString();
                    cbxDistrict_CUA.DataSource = AccountDAL.GetDistrictListByCity(cityId);
                    cbxDistrict_CUA.DisplayMember = "districtName";
                    cbxDistrict_CUA.ValueMember = "districtId";

                    // Sau khi có danh sách Quận mới gán Text cho Quận
                    cbxDistrict_CUA.Text = row.Cells["district"].Value.ToString().Trim();
                }
               
                if (cbxDistrict_CUA.SelectedValue != null)
                {
                    string districtId = cbxDistrict_CUA.SelectedValue.ToString();
                    cbxWard_CUA.DataSource = AccountDAL.GetWardListByDistrict(districtId);
                    cbxWard_CUA.DisplayMember = "wardName";
                    cbxWard_CUA.ValueMember = "wardId";

                    // Cuối cùng mới gán Text cho Phường
                    cbxWard_CUA.Text = row.Cells["ward"].Value.ToString().Trim();
                }

                cbxRole_CUA.Text = row.Cells["RoleName"].Value?.ToString();

                // DATETIMEPICKER
                if (row.Cells["birthday"].Value != DBNull.Value)
                {
                    dtpBirthday_CUA.Value = Convert.ToDateTime(row.Cells["birthday"].Value);
                }                           
            }
        }
    

        // số thứ tự lên header của dgv
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

        private void cbxCity_CUA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCity_CUA.SelectedValue != null && cbxCity_CUA.Focused)
            {
                string cityId = cbxCity_CUA.SelectedValue.ToString();
                cbxDistrict_CUA.DataSource = AccountDAL.GetDistrictListByCity(cityId);
                cbxDistrict_CUA.DisplayMember = "districtName";
                cbxDistrict_CUA.ValueMember = "districtId";

                cbxWard_CUA.DataSource = null; // Reset phường khi đổi thành phố
                cbxDistrict_CUA.Enabled = true; // Bật ComboBox quận/huyện
                cbxWard_CUA.Enabled = false; // Tạm thời tắt ComboBox phường/xã cho đến khi chọn quận/huyện
            }
        }

        private void cbxDistrict_CUA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDistrict_CUA.SelectedValue != null && cbxDistrict_CUA.Focused)
            {
                string districtId = cbxDistrict_CUA.SelectedValue.ToString();
                cbxWard_CUA.DataSource = AccountDAL.GetWardListByDistrict(districtId);
                cbxWard_CUA.DisplayMember = "wardName";
                cbxWard_CUA.ValueMember = "wardId";

                
                cbxWard_CUA.Enabled = true; // Bật ComboBox phường/xã
            }
        }

        private void LoadCity()
        {
            DataTable dt = AccountDAL.GetCityList();
            cbxCity_CUA.DataSource = dt;
            cbxCity_CUA.DisplayMember = "cityName"; // Tên hiển thị
            cbxCity_CUA.ValueMember = "cityId";     // Giá trị ngầm (79, 01...)
            cbxCity_CUA.SelectedIndex = -1;         // Để trống lúc đầu
        }

        private void btnClear_CUA_Click(object sender, EventArgs e)
        {
            // Reset các TextBox
            txtFullname_CUA.Clear();
            txtEmail_CUA.Clear();
            txtPhone_CUA.Clear();
            txtAddress_CUA.Clear();
            txtSalary_CUA.Clear();
            txtUserId_CUA.Clear();
            txtPassword_CUA.Clear();

            // Reset DateTimePicker (đưa về ngày hiện tại)
            dtpBirthday_CUA.Value = DateTime.Now;

            // Reset các ComboBox địa chỉ
            cbxCity_CUA.SelectedIndex = -1;
            cbxDistrict_CUA.DataSource = null;
            cbxWard_CUA.DataSource = null;
            cbxRole_CUA.SelectedIndex = -1;

            // Khóa lại các ComboBox cấp dưới
            cbxDistrict_CUA.Enabled = false;
            cbxWard_CUA.Enabled = false;

            // Cho phép nhập lại UserId (nếu trước đó bạn khóa để tránh sửa ID)
            txtUserId_CUA.ReadOnly = false;

            txtFullname_CUA.Focus(); // Đưa con trỏ chuột về ô tên
        }

        private void btnUpdate_CUA_Click(object sender, EventArgs e)
        {
            string userId = txtUserId_CUA.Text.Trim();
            string password = txtPassword_CUA.Text.Trim();
            // Kiểm tra dữ liệu đầu vào cơ bản
            // 1. Kiểm tra các ô văn bản (TextBox)
            if (string.IsNullOrWhiteSpace(txtUserId_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtPassword_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtFullname_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtPhone_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtAddress_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtSalary_CUA.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tất cả các thông tin cá nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại không thực hiện Update nữa
            }

            // 2. Kiểm tra các ô chọn địa chỉ (ComboBox)
            // Vì bạn đã set DropDownStyle = DropDownList, nên kiểm tra SelectedIndex hoặc Text
            if (cbxCity_CUA.SelectedIndex == -1 ||
                cbxDistrict_CUA.SelectedIndex == -1 ||
                cbxWard_CUA.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Tỉnh, Quận và Phường!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin từ giao diện
            string fullname = txtFullname_CUA.Text.Trim();
            string phone = txtPhone_CUA.Text.Trim();
            string address = txtAddress_CUA.Text.Trim();
            string ward = cbxWard_CUA.Text;         // Lấy chữ không dấu
            string district = cbxDistrict_CUA.Text; // Lấy chữ không dấu
            string city = cbxCity_CUA.Text;         // Lấy chữ không dấu
            double salary = double.Parse(txtSalary_CUA.Text);
            DateTime birthday = dtpBirthday_CUA.Value;

            // Gọi hàm Update từ DAL
            
            bool isUpdate = AccountDAL.UpdateAccount(password, userId, fullname, birthday, phone, address, ward, district, city, salary);

            if (isUpdate)
            {
                MessageBox.Show("Cap nhat thong tin thanh cong!");
                // Gọi hàm load lại DataGridView ở Form cha nếu cần
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Co loi xay ra khi cap nhat!");
            }
        }

        private void btnHistoryChangePass_Click(object sender, EventArgs e)
        {
            string userId = txtUserId_CUA.Text.Trim();

            // Nếu txtUserId có dữ liệu, truyền nó qua Form History
            // Nếu trống, truyền chuỗi rỗng để Form History lấy tất cả
            frmChangePasswordHistory f = new frmChangePasswordHistory(userId);

            f.ShowDialog(); // Mở Form lịch sử dưới dạng hội thoại
        }
    }    
}
