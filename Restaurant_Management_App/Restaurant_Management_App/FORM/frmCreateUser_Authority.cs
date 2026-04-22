using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Restaurant_Management_App
{
    public partial class frmCreateUser_Authority : Form
    {
        private string selectedImagePath = "";
        private string oldImagePath = "";
        string imageName = "";
        string tempPath = "";
        private int employeeId = 0; // ID nhân viên nếu là mode Edit
        private bool isBinding = false; // Biến cờ ngăn chặn sự kiện "đánh nhau"

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

            DataTable dtCity = AccountDAL.GetCityList();
            cbxCity_CUA.DataSource = dtCity;
            cbxCity_CUA.DisplayMember = "cityName";
            cbxCity_CUA.ValueMember = "cityId";

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
            // Kiểm tra xem cột có tồn tại không rồi ẩn đi
            if (dgvAccount.Columns["RoleId"] != null)
            {
                dgvAccount.Columns["RoleId"].Visible = false;
            }
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
            string salary = txtSalary_CUA.Text.Trim();            // Lấy RoleId từ ComboBox
            int RoleId = Convert.ToInt32(cbxRole_CUA.SelectedValue);

            if (string.IsNullOrWhiteSpace(userId) ||
                   string.IsNullOrWhiteSpace(password) ||
                   string.IsNullOrWhiteSpace(fullname) ||
                   string.IsNullOrWhiteSpace(phone) ||
                   string.IsNullOrWhiteSpace(email) ||
                   string.IsNullOrWhiteSpace(address) ||
                   string.IsNullOrWhiteSpace(ward) ||
                   string.IsNullOrWhiteSpace(city) ||
                   string.IsNullOrWhiteSpace(district) ||
                   string.IsNullOrWhiteSpace(salary) ||
                   cbxRole_CUA.SelectedValue == null || // Kiểm tra chưa chọn Quyền
                   string.IsNullOrEmpty(selectedImagePath)) // Kiểm tra chưa chọn ảnh
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tất cả thông tin và chọn ảnh đại diện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- TRONG btnCreate_CUA_Click ---

            // 2. Mở kết nối và gán tham số (Bắt chước cách UpdateAccount gán tham số)
            using (SqlConnection conn = new SqlConnection(Database.connStr))
            {
                conn.Open();               
                string checkQuery = "SELECT COUNT(*) FROM Account WHERE UserId = @u";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);               
                checkCmd.Parameters.AddWithValue("@u", userId);
                if ((int)checkCmd.ExecuteScalar() > 0)
                {
                    MessageBox.Show("UserID này đã tồn tại, vui lòng chọn ID khác!");
                    return;
                }

                // 4. BÂY GIỜ MỚI GIẢI PHÓNG VÀ LƯU ẢNH
                if (picAvaUser_CUA.Image != null)
                {
                    picAvaUser_CUA.Image.Dispose();
                    picAvaUser_CUA.Image = null;
                }

                string fileNameForDB = SaveUserImage(userId, selectedImagePath);

                string query = @"INSERT INTO Account 
    (userId, password, RoleId, fullname, phone, email, birthday, address, ward, district, city, salary, imagePath) 
    VALUES (@u, @p, @RoleId, @f, @ph, @e, @b, @a, @w, @d, @c, @s, @img)";
                SqlCommand cmd = new SqlCommand(query, conn);
                // Gán các tham số thông thường
                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@RoleId", RoleId);
                cmd.Parameters.AddWithValue("@f", fullname);
                cmd.Parameters.AddWithValue("@ph", phone);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@b", birthdate);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.Parameters.AddWithValue("@w", ward);
                cmd.Parameters.AddWithValue("@d", district);
                cmd.Parameters.AddWithValue("@c", city);
                cmd.Parameters.AddWithValue("@s", salary);

                cmd.Parameters.AddWithValue("@img", (object)fileNameForDB ?? DBNull.Value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tạo tài khoản thành công!");

                // Reset biến sau khi thành công
                selectedImagePath = "";
                LoadAccountList();
            }
        }

        // click vào 1 dòng sẽ hiện thị thông tin chi tiết của tài khoản đó lên các TextBox, ComboBox và DateTimePicker bên dưới 
        // để dễ dàng chỉnh sửa hoặc xem thông tin chi tiết
        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e) 
        {

            btnClear_CUA_Click(null, null);
            cbxCity_CUA.Enabled = true;
            cbxDistrict_CUA.Enabled = true;
            cbxWard_CUA.Enabled = true;
            if (e.RowIndex >= 0)
            {   
                isBinding = true;

                try
                {
                    DataGridViewRow row = dgvAccount.Rows[e.RowIndex];
                    object rawValue = row.Cells["imagePath"].Value;
                    MessageBox.Show("Dữ liệu ảnh trong Grid là: '" + (rawValue ?? "NULL") + "'");
                    // 1. ĐỔ TEXTBOX (Giữ nguyên)
                    txtUserId_CUA.Text = row.Cells["userId"].Value?.ToString();
                    txtPassword_CUA.Text = row.Cells["password"].Value?.ToString();
                    txtFullname_CUA.Text = row.Cells["fullname"].Value?.ToString();
                    txtPhone_CUA.Text = row.Cells["phone"].Value?.ToString();
                    txtEmail_CUA.Text = row.Cells["email"].Value?.ToString();
                    txtAddress_CUA.Text = row.Cells["address"].Value?.ToString();
                    txtSalary_CUA.Text = row.Cells["salary"].Value?.ToString();

                    // --- Lấy dữ liệu City từ Grid (Khử lỗi null và khoảng trắng) ---
                    string cityName = row.Cells["city"].Value?.ToString().Trim();
                    string distName = row.Cells["district"].Value?.ToString().Trim();
                    string wardName = row.Cells["ward"].Value?.ToString().Trim();

                    // 1. Gán City
                    cbxCity_CUA.Text = cityName;

                    // 2. Nạp và gán District dựa trên City vừa chọn
                    if (cbxCity_CUA.SelectedValue != null)
                    {
                        cbxDistrict_CUA.DataSource = AccountDAL.GetDistrictListByCity(cbxCity_CUA.SelectedValue.ToString());
                        cbxDistrict_CUA.DisplayMember = "districtName";
                        cbxDistrict_CUA.ValueMember = "districtId";
                        cbxDistrict_CUA.Text = distName; // Gán tên quận sau khi đã có DataSource
                    }

                    // 3. Nạp và gán Ward dựa trên District vừa chọn
                    if (cbxDistrict_CUA.SelectedValue != null)
                    {
                        cbxWard_CUA.DataSource = AccountDAL.GetWardListByDistrict(cbxDistrict_CUA.SelectedValue.ToString());
                        cbxWard_CUA.DisplayMember = "wardName";
                        cbxWard_CUA.ValueMember = "wardId";
                        cbxWard_CUA.Text = wardName; // Gán tên phường
                    }
                        //  XỬ LÝ ROLE & BIRTHDAY (Giữ nguyên)
                        if (row.Cells["RoleId"].Value != null) cbxRole_CUA.SelectedValue = row.Cells["RoleId"].Value;
                        if (row.Cells["birthday"].Value != null && row.Cells["birthday"].Value != DBNull.Value)
                        dtpBirthday_CUA.Value = Convert.ToDateTime(row.Cells["birthday"].Value);

                    //  XỬ LÝ ẢNH (Giữ nguyên đoạn FileStream đã chạy được của bạn)
                    HandleImageDisplay(row.Cells["imagePath"].Value?.ToString()?.Trim());
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    isBinding = false;
                }
            }
        }

        // Hàm phụ để code CellClick nhìn gọn hơn
        private void HandleImageDisplay(string imageName)
        {
            try
            {
                if (picAvaUser_CUA.Image != null) picAvaUser_CUA.Image.Dispose();
                oldImagePath = imageName;
                if (!string.IsNullOrEmpty(imageName))
                {
                    string fullPath = Path.Combine(GetAvaFolderPath(), imageName);
                    if (File.Exists(fullPath))
                    {
                        using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            picAvaUser_CUA.Image = Image.FromStream(fs);
                            Console.WriteLine(imageName);
                        }
                        picAvaUser_CUA.SizeMode = PictureBoxSizeMode.Zoom;
                        picAvaUser_CUA.BackColor = Color.Transparent;
                    }
                    else
                    {
                        picAvaUser_CUA.BackColor = Color.Gray;
                    }
                }
            }
            catch { picAvaUser_CUA.BackColor = Color.Red; }
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
            if (isBinding) return; // Quan trọng: Tránh xung đột khi click Grid

            if (cbxCity_CUA.SelectedValue != null && cbxCity_CUA.SelectedIndex != -1)
            {
                try
                {
                    // 1. Lấy ID của thành phố đang chọn
                    string cityId = cbxCity_CUA.SelectedValue.ToString();

                    // 2. Mở khóa và tải dữ liệu cho District
                    cbxDistrict_CUA.Enabled = true;
                    DataTable dt = AccountDAL.GetDistrictListByCity(cityId);

                    cbxDistrict_CUA.DataSource = dt;
                    cbxDistrict_CUA.DisplayMember = "districtName";
                    cbxDistrict_CUA.ValueMember = "districtId";

                    // 3. Reset District và Ward về trạng thái chưa chọn
                    cbxDistrict_CUA.SelectedIndex = -1;
                    cbxWard_CUA.DataSource = null;
                    cbxWard_CUA.Enabled = false;
                }
                catch { }
            }
        }

        private void cbxDistrict_CUA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isBinding) return;

            if (cbxDistrict_CUA.SelectedValue != null && cbxDistrict_CUA.SelectedIndex != -1)
            {
                try
                {
                    // 1. Lấy ID của Quận đang chọn
                    string districtId = cbxDistrict_CUA.SelectedValue.ToString();

                    // 2. Mở khóa và tải dữ liệu cho Ward
                    cbxWard_CUA.Enabled = true;
                    DataTable dt = AccountDAL.GetWardListByDistrict(districtId);

                    cbxWard_CUA.DataSource = dt;
                    cbxWard_CUA.DisplayMember = "wardName";
                    cbxWard_CUA.ValueMember = "wardId";

                    cbxWard_CUA.SelectedIndex = -1;
                }
                catch { }
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

            if (picAvaUser_CUA.Image != null)
            {
                picAvaUser_CUA.Image.Dispose(); // Giải phóng bộ nhớ
                picAvaUser_CUA.Image = null;    // Xóa hình
            }

            // Reset lại màu nền mặc định (nếu trước đó bạn để màu đỏ/xám để test)
            picAvaUser_CUA.BackColor = Color.LightGray;

            // Reset biến lưu đường dẫn ảnh cũ (nếu có dùng)
            oldImagePath = "";

            txtFullname_CUA.Focus(); // Đưa con trỏ chuột về ô tên
        }

        private void btnUpdate_CUA_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(txtUserId_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtPassword_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtFullname_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtPhone_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtAddress_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtSalary_CUA.Text) ||
                string.IsNullOrWhiteSpace(txtEmail_CUA.Text) ||
                cbxRole_CUA.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cá nhân và chọn Quyền hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxCity_CUA.SelectedIndex == -1 ||
                cbxDistrict_CUA.SelectedIndex == -1 ||
                cbxWard_CUA.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Tỉnh, Quận và Phường!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Lấy thông tin văn bản từ giao diện
                string userId = txtUserId_CUA.Text.Trim();
                string password = txtPassword_CUA.Text.Trim();
                string fullname = txtFullname_CUA.Text.Trim();
                string email = txtEmail_CUA.Text.Trim();
                string phone = txtPhone_CUA.Text.Trim();
                string address = txtAddress_CUA.Text.Trim();
                string ward = cbxWard_CUA.Text;
                string district = cbxDistrict_CUA.Text;
                string city = cbxCity_CUA.Text;
                DateTime birthday = dtpBirthday_CUA.Value;
                int RoleId = Convert.ToInt32(cbxRole_CUA.SelectedValue);

                if (!decimal.TryParse(txtSalary_CUA.Text.Trim(), out decimal salary))
                {
                    MessageBox.Show("Lương phải là số hợp lệ!");
                    return;
                }

                // --- 3. XỬ LÝ ẢNH (VỊ TRÍ CHỈNH SỬA Ở ĐÂY) ---
                string finalImageName = "";

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    // Nếu người dùng vừa click vào PictureBox và chọn ảnh mới (biến này gán ở sự kiện Click PictureBox)
                    finalImageName = selectedImagePath;
                }
                else
                {
                    // Nếu không chọn ảnh mới, dùng lại tên ảnh cũ đã lấy từ CellClick (biến này gán ở dgvAccount_CellClick)
                    finalImageName = oldImagePath;
                }

                // 4. Xử lý lịch sử mật khẩu (Nếu đổi pass)
                // Kiểm tra mật khẩu (Sử dụng tên biến khác để tránh lỗi scope)
                string oldPassForHistory = AccountDAL.GetCurrentPassword(userId);

                if (password != oldPassForHistory)
                {
                    // 1. Dùng tên biến rõ ràng @u, @old...
                    string queryHistory = "INSERT INTO PasswordHistory (userId, oldPassword, newPassword, changedBy) " +
                                          "VALUES (@u, @old, @new, @by)";
    
                    // 2. Tạo Command trực tiếp tại đây để tránh lỗi mapping của lớp Database
                    using (SqlConnection conn = new SqlConnection(Database.connStr))
                    {
                        conn.Open();
                        SqlCommand cmdH = new SqlCommand(queryHistory, conn);

                        string currentPwd = password.ToString(); 
                        string oldPwd = oldPassForHistory.ToString();

                        cmdH.Parameters.AddWithValue("@u", userId);
                        cmdH.Parameters.AddWithValue("@old", oldPwd);
                        cmdH.Parameters.AddWithValue("@new", currentPwd);
                        cmdH.Parameters.AddWithValue("@by", UserSession.UserId);
                        cmdH.ExecuteNonQuery();
                    }
                }

                // 5. Gọi hàm Update từ DAL (Truyền finalImageName vào cuối cùng)
                // Lưu ý: Đảm bảo hàm UpdateAccount trong lớp AccountDAL có nhận tham số cuối là string
                bool isUpdate = AccountDAL.UpdateAccount(password, userId, fullname, birthday, phone, address, ward, district, city, salary, email, RoleId, finalImageName);

                if (isUpdate)
                {
                    MessageBox.Show("Cập nhật thông tin và Ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại danh sách để cập nhật dữ liệu mới lên Grid
                    LoadAccountList();

                    // QUAN TRỌNG: Sau khi cập nhật xong, hãy gán ảnh hiện tại thành ảnh cũ
                    oldImagePath = finalImageName;
                    selectedImagePath = ""; // Reset biến chọn ảnh mới
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSearchId_CUA_Click(object sender, EventArgs e)
        {
            // 1. Lấy mã ID cần tìm từ Textbox
            string searchId = txtSearchId_CUA.Text.Trim();

            if (string.IsNullOrEmpty(searchId))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool found = false;

            // 2. Duyệt qua tất cả các dòng trong DataGridView để Reset màu trước khi tìm mới
            // (Giúp bảng không bị lem nhem màu từ những lần search trước)
            foreach (DataGridViewRow row in dgvAccount.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White; // Hoặc màu mặc định của bảng bạn
            }

            // 3. Bắt đầu vòng lặp tìm kiếm
            foreach (DataGridViewRow row in dgvAccount.Rows)
            {
                // Kiểm tra xem ô userId có dữ liệu không và so sánh (không phân biệt hoa thường)
                if (row.Cells["userId"].Value != null &&
                    row.Cells["userId"].Value.ToString().Equals(searchId, StringComparison.OrdinalIgnoreCase))
                {
                    // --- A. TÔ MÀU VÀ CHỌN DÒNG ---
                    row.DefaultCellStyle.BackColor = Color.Gold; // Tô màu vàng nổi bật
                    row.Selected = true; // Bôi xanh (Select) dòng đó

                    // Đặt ô đầu tiên của dòng này làm CurrentCell để hệ thống tiêu điểm vào đây
                    dgvAccount.CurrentCell = row.Cells[0];

                    // --- B. TỰ ĐỘNG CUỘN (AUTO SCROLL) ---
                    // Lệnh này cực kỳ an toàn cho dù Form to hay nhỏ, ít hay nhiều dòng.
                    // Nó sẽ cố gắng đưa dòng tìm thấy lên vị trí đầu tiên nhìn thấy được của DGV.
                    dgvAccount.FirstDisplayedScrollingRowIndex = row.Index;

                    found = true;
                    break; // Tìm thấy rồi thì thoát vòng lặp ngay
                }
            }

            // 4. Thông báo nếu không tìm thấy
            if (!found)
            {
                MessageBox.Show($"Không tìm thấy nhân viên có mã: {searchId}", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_CUA_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn nhân viên nào trên Grid chưa
            if (dgvAccount.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa khỏi danh sách!");
                return;
            }

            // 2. Lấy ID nhân viên từ dòng đang chọn
            string userId = dgvAccount.SelectedRows[0].Cells["userId"].Value.ToString();
            string fullName = dgvAccount.SelectedRows[0].Cells["fullname"].Value.ToString();

            // 3. Hỏi xác nhận (Tránh bấm nhầm)
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn ẩn tài khoản của nhân viên [{fullName}] không?\n(Dữ liệu sẽ không bị xóa vĩnh viễn)",
                                                   "Xác nhận xóa",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 4. Gọi hàm xóa từ DAL
                if (AccountDAL.DeleteAccount(userId))
                {
                    MessageBox.Show("Đã ẩn tài khoản thành công!");

                    // 5. Load lại danh sách để nhân viên đó biến mất khỏi DataGridView
                    LoadAccountList();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện xóa.");
                }
            }
        }


        private void picAvaUser_CUA_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // QUAN TRỌNG: Phải gán đường dẫn vào biến này
                selectedImagePath = ofd.FileName;

                // Hiển thị tạm lên PictureBox để người dùng xem
                picAvaUser_CUA.Image = Image.FromFile(ofd.FileName);
                picAvaUser_CUA.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private string SaveUserImage(string userId, string sourcePath)
        {
            try
            {
                // 1. Kiểm tra nếu không có đường dẫn ảnh nguồn hoặc file không tồn tại
                if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
                {
                    return null;
                }

                // 2. Lấy đường dẫn thư mục lưu trữ (Images/Employees)
                string folderPath = GetAvaFolderPath();

                // 3. Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // 4. Xây dựng tên file mới: [UserId].[Phần_mở_rộng] (Ví dụ: NV01.jpg)
                string extension = Path.GetExtension(sourcePath);
                string fileName = userId + extension;
                string destPath = Path.Combine(folderPath, fileName);

                // 5. XỬ LÝ QUAN TRỌNG: Nếu file đích đã tồn tại, phải giải phóng PictureBox trước khi ghi đè
                if (File.Exists(destPath))
                {
                    // Giải phóng ảnh đang hiển thị để tránh lỗi "The process cannot access the file..."
                    if (picAvaUser_CUA.Image != null)
                    {
                        picAvaUser_CUA.Image.Dispose();
                        picAvaUser_CUA.Image = null;
                    }

                    // Ép hệ thống dọn dẹp bộ nhớ đệm để nhả file ngay lập tức
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                // 6. Thực hiện Copy file từ máy tính vào thư mục dự án (true = cho phép ghi đè)
                File.Copy(sourcePath, destPath, true);

                // 7. TRẢ VỀ TÊN FILE (Chỉ trả về tên file, không trả về toàn bộ đường dẫn để lưu vào DB cho nhẹ)
                return fileName;
            }
            catch (Exception ex)
            {
                // Hiện thông báo lỗi chi tiết để debug
                MessageBox.Show("Lỗi khi lưu file vật lý: " + ex.Message, "Lỗi Lưu Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string GetAvaFolderPath()
        {
            // 1. Lấy thư mục gốc của file .exe
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // 2. Thư mục đích mong muốn
            string folderPath = Path.Combine(baseDir, "Images", "Employees");

            // 3. Nếu chạy trong Visual Studio (Debug), baseDir sẽ nằm sâu trong bin/Debug
            // Ta kiểm tra nếu không thấy thư mục Images ở đó, ta mới tìm ngược lên
            if (!Directory.Exists(folderPath))
            {
                // Thử tìm ngược lên 3 cấp (bin -> Debug -> net...)
                DirectoryInfo parent = Directory.GetParent(baseDir);
                if (parent?.Parent?.Parent != null)
                {
                    string projectFolder = parent.Parent.Parent.FullName;
                    string devPath = Path.Combine(projectFolder, "Images", "Employees");
                    if (Directory.Exists(devPath)) return devPath;
                }
            }

            // 4. Nếu không thấy nữa, tự động tạo thư mục ngay tại nơi chạy file .exe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        // Hàm lấy đường dẫn gốc của Project để lưu ảnh 
        private string GetProjectPath(string relativePath)
        {
            // Lấy đường dẫn từ bin/Debug ngược lên thư mục gốc Project
            string exePath = Application.StartupPath;
            string projectPath = Directory.GetParent(exePath).Parent.FullName;
            return Path.Combine(projectPath, relativePath);
        }

        private void SaveEmployee()
        {
            string query = employeeId == 0
                ? "INSERT INTO Employee(Name, ImagePath) VALUES(@name, @image)"
                : "UPDATE Employee SET Name=@name, ImagePath=@image WHERE Id=@id";

            using (SqlConnection conn = new SqlConnection(Database.connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", txtFullname_CUA.Text);
                cmd.Parameters.AddWithValue("@image", selectedImagePath); // Lưu đường dẫn này
                if (employeeId > 0) cmd.Parameters.AddWithValue("@id", employeeId);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã lưu nhân viên thành công!");
            }
        }
    }    
}
