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
                LoadAccountList();
            }
        }

        // click vào 1 dòng sẽ hiện thị thông tin chi tiết của tài khoản đó lên các TextBox, ComboBox và DateTimePicker bên dưới 
        // để dễ dàng chỉnh sửa hoặc xem thông tin chi tiết
        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAccount.Rows[e.RowIndex];

                // --- 1. ĐỔ DỮ LIỆU VÀO TEXTBOX ---
                txtUserId_CUA.Text = row.Cells["userId"].Value?.ToString();
                txtPassword_CUA.Text = row.Cells["password"].Value?.ToString();
                txtFullname_CUA.Text = row.Cells["fullname"].Value?.ToString();
                txtPhone_CUA.Text = row.Cells["phone"].Value?.ToString();
                txtEmail_CUA.Text = row.Cells["email"].Value?.ToString();
                txtAddress_CUA.Text = row.Cells["address"].Value?.ToString();
                txtSalary_CUA.Text = row.Cells["salary"].Value?.ToString();

                // --- 2. ĐỔ DỮ LIỆU VÀO COMBOBOX ĐỊA CHỈ ---
                cbxCity_CUA.Text = row.Cells["city"].Value?.ToString();
                cbxDistrict_CUA.Enabled = true;
                cbxWard_CUA.Enabled = true;

                if (cbxCity_CUA.SelectedValue != null)
                {
                    string cityId = cbxCity_CUA.SelectedValue.ToString();
                    cbxDistrict_CUA.DataSource = AccountDAL.GetDistrictListByCity(cityId);
                    cbxDistrict_CUA.DisplayMember = "districtName";
                    cbxDistrict_CUA.ValueMember = "districtId";
                    cbxDistrict_CUA.Text = row.Cells["district"].Value?.ToString().Trim();
                }

                if (cbxDistrict_CUA.SelectedValue != null)
                {
                    string districtId = cbxDistrict_CUA.SelectedValue.ToString();
                    cbxWard_CUA.DataSource = AccountDAL.GetWardListByDistrict(districtId);
                    cbxWard_CUA.DisplayMember = "wardName";
                    cbxWard_CUA.ValueMember = "wardId";
                    cbxWard_CUA.Text = row.Cells["ward"].Value?.ToString().Trim();
                }

                // --- 3. XỬ LÝ ROLE ---
                if (row.Cells["RoleId"].Value != null)
                {
                    cbxRole_CUA.SelectedValue = row.Cells["RoleId"].Value;
                }

                // --- 4. XỬ LÝ DATETIMEPICKER ---
                if (row.Cells["birthday"].Value != null && row.Cells["birthday"].Value != DBNull.Value)
                {
                    dtpBirthday_CUA.Value = Convert.ToDateTime(row.Cells["birthday"].Value);
                }

                // --- 5. BỔ SUNG: XỬ LÝ HIỂN THỊ ẢNH (PICTUREBOX) ---
                // --- TRONG HÀM dgvAccount_CellClick ---
                picAvaUser_CUA.BackColor = Color.Red;
                try
                {
                    // 1. Reset trạng thái
                    picAvaUser_CUA.BackColor = Color.Red; // Giữ để biết code có chạy qua đây
                    if (picAvaUser_CUA.Image != null)
                    {
                        picAvaUser_CUA.Image.Dispose();
                        picAvaUser_CUA.Image = null;
                    }

                    // 2. Lấy tên file và xóa khoảng trắng (Trim)
                    string imageName = row.Cells["imagePath"].Value?.ToString().Trim();
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        string fullPath = Path.Combine(GetAvaFolderPath(), imageName);

                        if (File.Exists(fullPath))
                        {
                            // Xóa ảnh cũ
                            if (picAvaUser_CUA.Image != null) picAvaUser_CUA.Image.Dispose();

                            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                            {
                                picAvaUser_CUA.Image = Image.FromStream(fs);
                            }
                            picAvaUser_CUA.SizeMode = PictureBoxSizeMode.Zoom;
                            picAvaUser_CUA.BackColor = Color.Transparent;
                            picAvaUser_CUA.Invalidate(); // Ép PictureBox vẽ lại
                        }
                        else
                        {
                            picAvaUser_CUA.BackColor = Color.Gray; // Đổi sang màu xám nếu không tìm thấy file
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Nếu nạp ảnh bị lỗi (định dạng sai, file hỏng), nó sẽ hiện thông báo ở đây
                    MessageBox.Show("Lỗi tại PictureBox: " + ex.Message);
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
                int roleId = Convert.ToInt32(cbxRole_CUA.SelectedValue);

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
                string currentPassInDb = AccountDAL.GetCurrentPassword(userId);
                if (password != currentPassInDb)
                {
                    string queryHistory = "INSERT INTO PasswordHistory (userId, oldPassword, newPassword, changedBy) " +
                                          "VALUES (@id, @old, @new, @by)";
                    Database.Instance.ExecuteNonQuery(queryHistory, new object[] { userId, currentPassInDb, password, UserSession.UserId });
                }

                // 5. Gọi hàm Update từ DAL (Truyền finalImageName vào cuối cùng)
                // Lưu ý: Đảm bảo hàm UpdateAccount trong lớp AccountDAL có nhận tham số cuối là string
                bool isUpdate = AccountDAL.UpdateAccount(password, userId, fullname, birthday, phone, address, ward, district, city, salary, email, roleId, finalImageName);

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
            using(OpenFileDialog ofd = new OpenFileDialog())
    {
                // Chỉ cho phép chọn các định dạng ảnh phổ biến
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                ofd.Title = "Chọn ảnh đại diện nhân viên";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 1. Tạo tên file duy nhất bằng GUID để tránh trùng lặp
                        string extension = Path.GetExtension(ofd.FileName);
                        string fileName = Guid.NewGuid().ToString() + extension;

                        // 2. Lấy đường dẫn thư mục lưu trữ
                        string folderPath = GetAvaFolderPath();
                        string destPath = Path.Combine(folderPath, fileName);

                        // 3. Sao chép file vào thư mục dự án
                        File.Copy(ofd.FileName, destPath, true);

                        // 4. Cập nhật các biến trạng thái để nút Update có thể sử dụng
                        selectedImagePath = fileName; // Lưu tên file mới để tí nữa lưu vào DB

                        // 5. Hiển thị ảnh lên PictureBox ngay lập tức
                        // Dùng cách nạp từ Byte để không giữ bản quyền file (tránh lỗi khóa file)
                        byte[] buffer = File.ReadAllBytes(destPath);
                        using (MemoryStream ms = new MemoryStream(buffer))
                        {
                            if (picAvaUser_CUA.Image != null) picAvaUser_CUA.Image.Dispose();
                            picAvaUser_CUA.Image = Image.FromStream(ms);
                        }

                        picAvaUser_CUA.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể nạp ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private string SaveUserImage(string userId, string path)
        {
            // Nếu không chọn ảnh mới (path trống), lấy lại tên ảnh cũ từ DB
            if (string.IsNullOrEmpty(path))
            {
                return AccountDAL.GetCurrentImagePath(userId);
            }

            try
            {
                string folder = GetAvaFolderPath(); // Hàm lấy đường dẫn thư mục JPG/Ava
                string extension = Path.GetExtension(path);
                string fileName = userId + extension;
                string fullSavePath = Path.Combine(folder, fileName);

                // Giải phóng ảnh trong PictureBox nếu đang hiển thị để tránh lỗi "File in use"
                if (picAvaUser_CUA.Image != null)
                {
                    picAvaUser_CUA.Image.Dispose();
                    picAvaUser_CUA.Image = null;
                }

                File.Copy(path, fullSavePath, true);
                return fileName;
            }
            catch (Exception ex)
            {
                return "";
            }           
        }

        private string GetAvaFolderPath()
        {
            // Lấy đường dẫn thư mục đang chạy file .exe (thường là bin/Debug)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Tìm ngược lên để thoát khỏi bin/Debug để vào thư mục Images của dự án
            // Thử dùng đường dẫn tương đối để an toàn hơn
            string projectFolder = Directory.GetParent(baseDirectory).Parent.Parent.FullName;

            // Kết hợp với đường dẫn chính xác đến thư mục có chữ "s" (Employees)
            string folderPath = Path.Combine(projectFolder, "Images", "Employees");

            // Nếu vẫn không thấy, hãy thử kiểm tra thư mục không có chữ "s"
            if (!Directory.Exists(folderPath))
            {
                folderPath = Path.Combine(projectFolder, "Images", "Employee");
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
