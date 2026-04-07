using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Restaurant_Management_App; // Đảm bảo có namespace này để gọi BillDAL, Database

namespace Restaurant_Management_App.FORM
{
    public partial class frmCreateOrder : Form
    {
        private readonly OrderRepository _repo = new OrderRepository();

        public frmCreateOrder()
        {
            InitializeComponent();

            // Đăng ký các sự kiện
            this.Load += FrmOrder_Load;
            btnCheckout.Click += BtnCheckout_Click;
            btnClear.Click += BtnClear_Click;

            // Đăng ký thêm sự kiện cho numDiscount để tự tính lại tiền khi đổi giảm giá
            numDiscount.ValueChanged += (s, e) => CalculateTotal();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            LoadTables();    // Load danh sách bàn
            LoadFoodList();  // Load danh sách món ăn

            // Khởi tạo ComboBox Case và PayMethod nếu chưa có dữ liệu trong Design
            if (cbCase.Items.Count == 0) cbCase.Items.AddRange(new string[] { "Eat in", "Take away", "Delivery" });
            if (cbPayMethod.Items.Count == 0) cbPayMethod.Items.AddRange(new string[] { "Cash", "Credit Card", "E-Wallet" });

            cbCase.SelectedIndex = 0;
            cbPayMethod.SelectedIndex = 0;
        }

        private void LoadTables()
        {
            try
            {
                // Sử dụng Repo hiện tại của bạn
                var tables = _repo.GetTableList();
                cbTable.DataSource = tables;
                cbTable.DisplayMember = "Item2"; // Tên bàn
                cbTable.ValueMember = "Item1";   // ID bàn
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tables: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HÀM LOAD DANH SÁCH MÓN ĂN (BỔ SUNG) ---
        void LoadFoodList()
        {
            try
            {
                // 1. Thêm điều kiện lọc chỉ lấy món 'Available'
                // Những món 'Out of stock' sẽ không được lấy lên DataTable
                string query = @"SELECT f.id, f.name, c.name [Category], f.price, f.status 
                         FROM Food f 
                         JOIN FoodCategory c ON f.idCategory = c.id 
                         WHERE f.status = N'Available'";

                DataTable data = Database.Instance.ExecuteQuery(query);
                dgvMenu.DataSource = data;

                // 2. Mapping và ẩn cột ID để giao diện chuyên nghiệp hơn
                if (dgvMenu.Columns.Contains("id"))
                {
                    dgvMenu.Columns["id"].Name = "colMenuId";
                    dgvMenu.Columns["colMenuId"].Visible = false; // Ẩn ID đi, nhân viên không cần xem số này
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load menu: " + ex.Message);
            }
        }

        private void dgvMenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Giả sử đây là chỗ bạn đổ dữ liệu vào dgvMenu
            DataTable dt = Database.Instance.ExecuteQuery("SELECT * FROM Food");
            dgvMenu.DataSource = dt;

            // QUAN TRỌNG: Gán ID vào thuộc tính DataPropertyName của cột ID
            if (dgvMenu.Columns.Contains("colId")) // "colId" là tên (Name) bạn đặt trong Edit Columns
            {
                dgvMenu.Columns["colId"].DataPropertyName = "id";
            }
        }

        void LoadBillDetails(int billId)
        {
            // Truy vấn lấy dữ liệu chi tiết hóa đơn
            string query = @"SELECT f.name AS [Item], bi.quantity AS [Qty], f.price AS [Unit price], (f.price * bi.quantity) AS [Total] 
                             FROM dbo.BillInfo bi 
                             JOIN dbo.Food f ON bi.idFood = f.id 
                             WHERE bi.idBill = " + billId;

            DataTable data = Database.Instance.ExecuteQuery(query);
            dgvCart.DataSource = data;

            // Cập nhật lại tổng tiền
            CalculateTotal();
        }

        void CalculateTotal()
        {
            double subtotal = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Total"].Value != null)
                    subtotal += Convert.ToDouble(row.Cells["Total"].Value);
            }

            // Lấy % giảm giá từ NumericUpDown
            double discountPercent = (double)numDiscount.Value;
            double tax = subtotal * 0.07; // Thuế 7%

            // Công thức: Tổng = (Tạm tính + Thuế) - Số tiền giảm
            double total = (subtotal + tax) * (1 - discountPercent / 100);

            lblSubtotalValue.Text = subtotal.ToString("N0") + " VNĐ";
            lblTaxValue.Text = tax.ToString("N0") + " VNĐ";
            lblTotalValue.Text = total.ToString("N0") + " VNĐ";
        }

        // Bắt sự kiện khi nhân viên thay đổi con số giảm giá
        private void numDiscount_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        // --- CÁC NÚT CHỨC NĂNG DÙNG REPOSITORY ---
        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có hóa đơn để thanh toán không
            if (string.IsNullOrWhiteSpace(txtOrderNo.Text))
            {
                MessageBox.Show("Không có hóa đơn nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Xác nhận thanh toán
            string totalMoney = lblTotalValue.Text;
            DialogResult result = MessageBox.Show($"Tổng tiền là {totalMoney}. Xác nhận tạo đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int billId = int.Parse(txtOrderNo.Text);
                    int tableId = (int)cbTable.SelectedValue;

                    // 3. Gọi DAL để cập nhật Bill thành status = 1 (Đã thanh toán)
                    BillDAL.Instance.CheckOut(billId);

                    // 4. Cập nhật bàn ăn về trạng thái 'Trống'
                    // Bạn có thể viết hàm này trong TableDAL hoặc chạy trực tiếp qua Database
                    string updateTableQuery = $"UPDATE tableFood SET status = N'Trống' WHERE id = {tableId}";
                    Database.Instance.ExecuteNonQuery(updateTableQuery);

                    MessageBox.Show("Tạo đơn thành công:>!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Làm mới màn hình để đón khách tiếp theo
                    ResetForm();

                    // 6. (Tùy chọn) Load lại danh sách bàn nếu bạn có sơ đồ bàn ở Form khác
                    // LoadTable(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tạo đơn thất bại :< " + ex.Message);
                }
            }
        }

        // Gom nhóm việc clear giao diện vào 1 hàm cho sạch code
        private void ResetForm()
        {
            // 1. Xóa tên khách hàng và mã đơn
            txtCustomerName.Text = "";
            txtOrderNo.Text = ""; // Hoặc gán lại "0000"

            // 2. Đưa các ComboBox về lựa chọn mặc định
            if (cbTable.Items.Count > 0) cbTable.SelectedIndex = 0;
            if (cbCase.Items.Count > 0) cbCase.SelectedIndex = 0;
            if (cbPayMethod.Items.Count > 0) cbPayMethod.SelectedIndex = 0;

            // 3. Xóa sạch danh sách món trong giỏ hàng (dgvCart)
            // Nếu dgvCart dùng DataSource:
            dgvCart.DataSource = null;
            // Nếu dgvCart nạp thủ công từng dòng:
            // dgvCart.Rows.Clear();

            // 4. Reset tổng tiền về 0
            lblTotalValue.Text = "0 VNĐ";

            // 5. Đưa con trỏ chuột về ô tên khách hàng để nhập đơn mới cho nhanh
            txtCustomerName.Focus();
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có mã hóa đơn (Order No) chưa
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                MessageBox.Show("Không có hóa đơn nào để xóa!", "Thông báo");
                return;
            }

            // Lấy ID hóa đơn từ ô nhập liệu (vì bạn đã format 0001 nên cần ép kiểu lại)
            int billId = int.Parse(txtOrderNo.Text);

            // 2. Hỏi xác nhận để tránh bấm nhầm
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn XÓA TẤT CẢ món ăn của hóa đơn {txtOrderNo.Text} không?",
                                                "Cảnh báo xóa dữ liệu",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 3. Thực hiện lệnh xóa trong SQL
                    // Xóa tất cả các dòng trong BillInfo có mã idBill này
                    string query = "DELETE dbo.BillInfo WHERE idBill = " + billId;
                    int check = Database.Instance.ExecuteNonQuery(query);

                    if (check >= 0)
                    {
                        MessageBox.Show("Đã xóa sạch các món trong đơn hàng!", "Thành công");

                        // 4. Cập nhật lại giao diện
                        LoadBillDetails(billId); // Nạp lại giỏ hàng (lúc này sẽ trống)
                        CalculateTotal();        // Tính lại tổng tiền (sẽ về 0)
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategory = (int)cbCategory.SelectedValue;
            LoadFoodListByCategory(idCategory);
        }

        void LoadFoodListByCategory(int id)
        {
            // Nếu chọn "Tất cả" (giả sử id = 0) thì hiện hết, còn không thì lọc theo idCategory
            string query = id == 0
                ? "SELECT id, name, price FROM Food"
                : "SELECT id, name, price FROM Food WHERE idCategory = " + id;

            dgvMenu.DataSource = Database.Instance.ExecuteQuery(query);
        }
        // Giả sử đây là hàm xử lý khi nhân viên click chọn món
        

        private void txtSearchFood_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchFood.Text;
            // Lọc những món có tên chứa từ khóa và phải còn hàng (Available)
            string query = string.Format("SELECT f.id, f.name, c.name [Category], f.price " +
                                         "FROM Food f JOIN FoodCategory c ON f.idCategory = c.id " +
                                         "WHERE f.status = N'Available' AND f.name LIKE N'%{0}%'", searchText);

            dgvMenu.DataSource = Database.Instance.ExecuteQuery(query);
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra RowIndex để tránh click vào tiêu đề
            if (e.RowIndex < 0) return;

            // 2. Kiểm tra tên cột - Hãy đảm bảo Name trong Edit Columns là "colDelete"
            if (dgvCart.Columns[e.ColumnIndex].Name == "colDelete")
            {
                try
                {
                    // Lấy FoodId an toàn hơn
                    object foodIdObj = dgvCart.Rows[e.RowIndex].Cells["FoodId"].Value;
                    if (foodIdObj == null) return;

                    int foodId = Convert.ToInt32(foodIdObj);

                    // Lấy BillId (Xử lý trường hợp bạn có format 0001)
                    // Dùng hàm thay thế nếu mã đơn có chứa chữ như "HD0001"
                    string rawBillId = txtOrderNo.Text.Replace("HD", "");
                    int billId = int.Parse(rawBillId);

                    if (MessageBox.Show("Bạn muốn xóa món này khỏi đơn hàng?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // 3. Lệnh xóa trong SQL
                        // Lưu ý: Nếu cột trong SQL của bạn là 'quantity' thay vì 'count', hãy kiểm tra lại nhé
                        string query = $"DELETE dbo.BillInfo WHERE idBill = {billId} AND idFood = {foodId}";

                        int result = Database.Instance.ExecuteNonQuery(query);

                        if (result > 0)
                        {
                            // 4. Cập nhật giao diện
                            LoadBillDetails(billId);
                            CalculateTotal();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa món: " + ex.Message, "Thông báo lỗi");
                }
            }
        }

        private void numDiscount_ValueChanged_1(object sender, EventArgs e)
        {
            CalculateTotal(); // Chỉnh % giảm giá là tiền tổng tự nhảy theo
        }

        private void AddFootToBill(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Khai báo biến ở đầu để tránh lỗi "không tìm thấy billId"
            int billId = -1;

            try
            {
                // 1. Lấy mã món ăn từ cột "id"
                int foodId = -1;
                if (dgvMenu.Columns.Contains("id"))
                    foodId = Convert.ToInt32(dgvMenu.Rows[e.RowIndex].Cells["id"].Value);
                else if (dgvMenu.Columns.Contains("colId"))
                    foodId = Convert.ToInt32(dgvMenu.Rows[e.RowIndex].Cells["colId"].Value);

                if (foodId <= 0) return;

                // 2. Kiểm tra bàn
                if (cbTable.SelectedValue == null) return;
                int tableId = (int)cbTable.SelectedValue;

                // 3. Tìm hoặc tạo hóa đơn mới
                billId = BillDAL.Instance.GetUncheckBillIDByTableID(tableId);

                if (billId == -1)
                {
                    BillDAL.Instance.InsertBill(tableId, txtCustomerName.Text, cbCase.Text, cbPayMethod.Text);
                    billId = BillDAL.Instance.GetMaxIDBill();
                }

                // 4. Lưu món vào BillInfo
                BillInfoDAL.Instance.InsertBillInfo(billId, foodId, 1);

                // 5. Cập nhật giao diện (Load giỏ hàng + Format mã đơn)
                LoadBillDetails(billId);

                // GỌI HÀM FORMAT TẠI ĐÂY
                txtOrderNo.Text = FormatBillId(billId);

                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnCheckout_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra mã hóa đơn
                if (string.IsNullOrEmpty(txtOrderNo.Text))
                {
                    MessageBox.Show("Vui lòng thêm món trước khi Submit!", "Thông báo");
                    return;
                }

                int billId = int.Parse(txtOrderNo.Text);

                // 2. Xóa dữ liệu cũ trong BillInfo để lưu mới (Tránh trùng lặp)
                string deleteOldInfo = "DELETE dbo.BillInfo WHERE idBill = " + billId;
                Database.Instance.ExecuteNonQuery(deleteOldInfo);

                // 3. Lưu danh sách món từ dgvCart vào SQL
                int countSuccess = 0;
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.Cells["FoodId"].Value != null)
                    {
                        int foodId = Convert.ToInt32(row.Cells["FoodId"].Value);
                        int quantity = Convert.ToInt32(row.Cells["Qty"].Value);

                        string query = $"INSERT INTO dbo.BillInfo (idBill, idFood, count) VALUES ({billId}, {foodId}, {quantity})";
                        Database.Instance.ExecuteNonQuery(query);
                        countSuccess++;
                    }
                }

                // 4. Xử lý sau khi thành công
                if (countSuccess > 0)
                {
                    MessageBox.Show($"Đã gửi {countSuccess} món vào hệ thống!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- BẮT ĐẦU ĐOẠN BỔ SUNG Ở ĐÂY ---

                    // Làm sạch giỏ hàng trên màn hình sau khi đã gửi xong
                    dgvCart.DataSource = null;
                    if (dgvCart.Rows.Count > 0) dgvCart.Rows.Clear(); // Nếu dùng nạp tay thì dùng dòng này

                    // Cập nhật lại tổng tiền về 0
                    CalculateTotal();

                    // Dự báo mã đơn tiếp theo cho nhân viên biết
                    int nextBillId = BillDAL.Instance.GetMaxIDBill() + 1;
                    txtOrderNo.Text = FormatBillId(nextBillId);

                    // Xóa tên khách hàng để nhập đơn mới
                    txtCustomerName.Text = "";
                    txtCustomerName.Focus();

                    // --- KẾT THÚC ĐOẠN BỔ SUNG ---
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi đơn: " + ex.Message);
            }
        }
        private string FormatBillId(int id)
        {
            // "D4" nghĩa là Decimal với 4 chữ số, tự động bù số 0 ở trước
            return id.ToString("D4");
        }

    }
}