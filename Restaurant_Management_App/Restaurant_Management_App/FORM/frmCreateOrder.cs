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
            btnHold.Click += BtnHold_Click;
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
            DialogResult result = MessageBox.Show($"Tổng tiền là {totalMoney}. Xác nhận thanh toán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                    MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Làm mới màn hình để đón khách tiếp theo
                    ResetForm();

                    // 6. (Tùy chọn) Load lại danh sách bàn nếu bạn có sơ đồ bàn ở Form khác
                    // LoadTable(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thanh toán: " + ex.Message);
                }
            }
        }

        // Gom nhóm việc clear giao diện vào 1 hàm cho sạch code
        void ResetForm()
        {
            txtOrderNo.Clear();
            txtCustomerName.Clear();
            dgvCart.DataSource = null; // Xóa giỏ hàng
            numDiscount.Value = 0;     // Reset giảm giá về 0
            CalculateTotal();          // Tính lại tổng tiền (sẽ về 0)
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            // Hỏi trước khi xóa sạch
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn làm mới toàn bộ hóa đơn hiện tại không?",
                "Xác nhận làm mới",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        private void BtnHold_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order has been saved as 'Unpaid'. You can close the form.");
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
        private void AddFoodToBill()
        {
            if (cbTable.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước!", "Thông báo");
                return;
            }
            if (cbTable.SelectedValue == null) return;
            int tableId = (int)cbTable.SelectedValue;

            // BƯỚC 1: Tìm hóa đơn cũ (status = 0)
            int billId = BillDAL.Instance.GetUncheckBillIDByTableID(tableId);

            if (billId == -1) // Nếu bàn trống
            {
                // BƯỚC 2: Lấy thông tin từ giao diện truyền vào hàm Insert
                string customer = txtCustomerName.Text;
                string cases = cbCase.Text;         // Dining/Takeaway...
                string method = cbPayMethod.Text;   // Cash/Card...

                BillDAL.Instance.InsertBill(tableId, customer, cases, method);
                billId = BillDAL.Instance.GetMaxIDBill();
            }

            // BƯỚC 3: Ghi món vào chi tiết hóa đơn (BillInfo)
            // BƯỚC 3: Ghi món vào chi tiết hóa đơn (BillInfo)
             if (dgvMenu.CurrentRow == null) return;

            // Dựa vào hình image_f3fa6d, cột chứa số ID (1, 2, 3...) nằm ở vị trí số 3 (tính từ 0)
            // Bạn dùng index để lấy cho CHÍNH XÁC, không sợ trùng tên cột
            int foodId = Convert.ToInt32(dgvMenu.CurrentRow.Cells[3].Value);

            int quantity = 1; // Mặc định mỗi lần click là thêm 1

            // Kiểm tra xem billId có hợp lệ không trước khi chèn
            if (billId != -1)
            {
                try
                {
                    BillInfoDAL.Instance.InsertBillInfo(billId, foodId, quantity);

                    // BƯỚC 4: Load lại giỏ hàng và tính tiền
                    LoadBillDetails(billId);
                    txtOrderNo.Text = billId.ToString();
                    CalculateTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm món: " + ex.Message);
                }
            }
        }

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
            // Kiểm tra nếu bấm đúng vào cột nút X
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "colDelete")
            {
                // Lấy ID món ăn từ cột FoodId (cột đầu tiên trong hình image_f33411.png của bạn)
                int foodId = (int)dgvCart.Rows[e.RowIndex].Cells["FoodId"].Value;
                int billId = int.Parse(txtOrderNo.Text);

                if (MessageBox.Show("Bạn muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Lệnh xóa trong SQL
                    string query = $"DELETE dbo.BillInfo WHERE idBill = {billId} AND idFood = {foodId}";
                    Database.Instance.ExecuteNonQuery(query);

                    // Nạp lại giỏ hàng và tính lại tiền
                    LoadBillDetails(billId);
                    CalculateTotal();
                }
            }
        }

        private void numDiscount_ValueChanged_1(object sender, EventArgs e)
        {
            CalculateTotal(); // Chỉnh % giảm giá là tiền tổng tự nhảy theo
        }

        private void AddFootToBill(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Chống click vào tiêu đề cột
            if (e.RowIndex < 0) return;

            try
            {
                // 2. Kiểm tra chọn bàn
                if (cbTable.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int tableId = (int)cbTable.SelectedValue;

                // 3. Lấy foodId từ cột số 3 (Cột 'id' chứa số 1, 2, 3... trong hình image_f3fa6d)
                // Mình dùng TryParse để nếu ô đó trống hoặc lỗi thì chương trình không bị văng
                if (dgvMenu.Rows[e.RowIndex].Cells[3].Value == null ||
                    string.IsNullOrEmpty(dgvMenu.Rows[e.RowIndex].Cells[3].Value.ToString()))
                {
                    MessageBox.Show("Không tìm thấy mã món ăn ở dòng này!", "Lỗi dữ liệu");
                    return;
                }

                int foodId = Convert.ToInt32(dgvMenu.Rows[e.RowIndex].Cells[3].Value);

                // 4. Tìm hoặc tạo Bill
                int billId = BillDAL.Instance.GetUncheckBillIDByTableID(tableId);

                if (billId == -1)
                {
                    // Truyền đủ 4 tham số như BillDAL đã sửa
                    BillDAL.Instance.InsertBill(tableId, txtCustomerName.Text, cbCase.Text, cbPayMethod.Text);
                    billId = BillDAL.Instance.GetMaxIDBill();
                }

                // 5. Chèn vào BillInfo (Chi tiết hóa đơn)
                // Nếu dòng này vẫn báo lỗi Foreign Key, tức là số foodId lấy ra không tồn tại trong bảng Food ở SQL
                BillInfoDAL.Instance.InsertBillInfo(billId, foodId, 1);

                // 6. Cập nhật giao diện
                txtOrderNo.Text = billId.ToString();
                LoadBillDetails(billId);
                CalculateTotal();
            }
            catch (Exception ex)
            {
                // Hiện lỗi chi tiết để bạn dễ debug
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}