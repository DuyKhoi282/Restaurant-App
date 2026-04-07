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
                MessageBox.Show("Failed to load menu: " + ex.Message); //alooooooooo
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
            DialogResult result = MessageBox.Show("Tạo đơn thành công:>!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                    //MessageBox.Show("Tạo đơn thành công:>!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            try
            {
                // 1. Lấy thông tin từ giao diện
                // Nếu txtOrderNo trống, hãy mặc định lấy MaxID + 1
                if (string.IsNullOrEmpty(txtOrderNo.Text))
                {
                    int nextId = BillDAL.Instance.GetMaxIDBill() + 1;
                    txtOrderNo.Text = FormatBillId(nextId);
                }

                int billId = int.Parse(txtOrderNo.Text);
                int tableId = (int)cbTable.SelectedValue;
                int foodId = -1;

                // Lấy foodId từ dgvMenu
                if (dgvMenu.Columns.Contains("id"))
                    foodId = Convert.ToInt32(dgvMenu.Rows[e.RowIndex].Cells["id"].Value);
                else if (dgvMenu.Columns.Contains("colMenuId"))
                    foodId = Convert.ToInt32(dgvMenu.Rows[e.RowIndex].Cells["colMenuId"].Value);

                if (foodId <= 0) return;

                // 2. KIỂM TRA & TẠO BILL (Nếu chưa có trong SQL)
                // Vì bạn bỏ IDENTITY nên chúng ta phải tự INSERT mã billId này vào
                string checkBill = $"SELECT COUNT(*) FROM dbo.Bill WHERE id = {billId}";
                int exists = (int)Database.Instance.ExecuteScalar(checkBill);

                if (exists == 0)
                {
                    // Chèn Bill mới (Parent)
                    string queryInsertBill = @"INSERT INTO dbo.Bill (id, idTable, customerName, caseName, payMethod, status) 
                                       VALUES ( @idBill , @idTable , @custName , @case , @pay , 0 )";
                    Database.Instance.ExecuteNonQuery(queryInsertBill, new object[] {
                billId, tableId, txtCustomerName.Text, cbCase.Text, cbPayMethod.Text
            });
                }

                // 3. Tăng số lượng món nếu đã tồn tại trong giỏ hoặc thêm mới
                // Ở đây tôi gọi InsertBillInfo (bạn nên sửa DAL để dùng tên cột 'quantity')
                string queryInsertInfo = $"INSERT INTO dbo.BillInfo (idBill, idFood, quantity) VALUES ({billId}, {foodId}, 1)";
                Database.Instance.ExecuteNonQuery(queryInsertInfo);

                // 4. Cập nhật giao diện
                LoadBillDetails(billId);
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message);
            }
        }

        private void btnCheckout_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrderNo.Text))
                {
                    MessageBox.Show("Vui lòng thêm món trước khi lưu đơn!");
                    return;
                }

                int billId = int.Parse(txtOrderNo.Text);

                // 1. Cập nhật lại thông tin Bill (Trường hợp nhân viên đổi tên khách hoặc phương thức thanh toán sau khi đã chọn món)
                string updateBill = @"UPDATE dbo.Bill SET customerName = @name , caseName = @case , payMethod = @pay 
                              WHERE id = @id";
                Database.Instance.ExecuteNonQuery(updateBill, new object[] {
            txtCustomerName.Text, cbCase.Text, cbPayMethod.Text, billId
        });

                // 2. Vì món ăn đã được INSERT từng cái ở hàm AddFootToBill bên trên rồi, 
                // ở nút Submit này bạn chỉ cần chốt đơn hoặc cập nhật Status nếu muốn thanh toán luôn.

                MessageBox.Show($"Lưu đơn hàng {txtOrderNo.Text} thành công!", "Thông báo");

                // 3. Reset form để nhập đơn tiếp theo
                ResetForm();

                // Gợi ý ID tiếp theo
                int nextId = BillDAL.Instance.GetMaxIDBill() + 1;
                txtOrderNo.Text = FormatBillId(nextId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu đơn: " + ex.Message);
            }
        }
        private string FormatBillId(int id)
        {
            // "D4" nghĩa là Decimal với 4 chữ số, tự động bù số 0 ở trước
            return id.ToString("D4");
        }

    }
}