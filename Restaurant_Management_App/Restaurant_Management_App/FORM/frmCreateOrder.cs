using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Restaurant_Management_App;

namespace Restaurant_Management_App.FORM
{
    public partial class frmCreateOrder : Form
    {
        private const double BuffetFixedPrice = 299000;
        private readonly OrderRepository _repo = new OrderRepository();
        private readonly List<FoodMenuItem> _foods = new List<FoodMenuItem>();
        private bool _isBuffetLocked;
        private bool _isBuffetGuestCountLocked;
        private string _lastOrderType = "Không buffet";
        private Label lblBuffetGuestCount;
        private NumericUpDown numBuffetGuestCount;
        private Button btnIncreaseBuffetGuestCount;
        private Button btnRequestEditOrderInfo;
        private Button btnSaveOrderInfo;
        private bool _isOrderInfoLocked;

        public frmCreateOrder()
        {
            InitializeComponent();

            Load += FrmOrder_Load;
            btnClear.Click += BtnClear_Click;
            numDiscount.ValueChanged += (s, e) => CalculateTotal();
            cbTable.SelectedIndexChanged += cbTable_SelectedIndexChanged;
            cbOrderType.SelectedIndexChanged += HandleOrderTypeChangedBuffetRule2026;
            cbCase.SelectedIndexChanged += HandleCaseSelectionChangedBuffetRule;

            // Hide discount controls on Create Order screen as requested.
            lblDiscountCaption.Visible = false;
            numDiscount.Visible = false;
            numDiscount.Value = 0;
            ApplyCreateOrderThemeStyling();
            ApplyHiddenBuffetInputsState();
            InitializeBuffetGuestCountInput();
            InitializeOrderInfoWorkflowButtons();
            EnsureBuffetSchema();
        }

        private void InitializeBuffetGuestCountInput()
        {
            lblBuffetGuestCount = new Label
            {
                AutoSize = true,
                Name = "lblBuffetGuestCount",
                Text = "Số người buffet",
                Location = new Point(410, 112),
                Visible = false
            };

            numBuffetGuestCount = new NumericUpDown
            {
                Name = "numBuffetGuestCount",
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Size = new Size(90, 22),
                Location = new Point(525, 108),
                TextAlign = HorizontalAlignment.Right,
                Visible = false,
                Enabled = false
            };

            btnIncreaseBuffetGuestCount = new Button
            {
                Name = "btnIncreaseBuffetGuestCount",
                Text = "+1 khách",
                Size = new Size(80, 24),
                Location = new Point(525, 136),
                Visible = false,
                Enabled = false
            };

            numBuffetGuestCount.ValueChanged += NumBuffetGuestCount_ValueChanged;
            btnIncreaseBuffetGuestCount.Click += BtnIncreaseBuffetGuestCount_Click;

            pnlOrderTop.Controls.Add(lblBuffetGuestCount);
            pnlOrderTop.Controls.Add(numBuffetGuestCount);
            pnlOrderTop.Controls.Add(btnIncreaseBuffetGuestCount);
        }

        private void InitializeOrderInfoWorkflowButtons()
        {
            btnRequestEditOrderInfo = new Button
            {
                Name = "btnRequestEditOrderInfo",
                Text = "Yêu cầu chỉnh sửa",
                Size = new Size(130, 32),
                Location = new Point(250, 12),
                Visible = false
            };

            btnSaveOrderInfo = new Button
            {
                Name = "btnSaveOrderInfo",
                Text = "Lưu",
                Size = new Size(60, 32),
                Location = new Point(250, 12),
                Visible = false
            };

            btnRequestEditOrderInfo.Click += BtnRequestEditOrderInfo_Click;
            btnSaveOrderInfo.Click += BtnSaveOrderInfo_Click;

            pnlSummary.Controls.Add(btnRequestEditOrderInfo);
            pnlSummary.Controls.Add(btnSaveOrderInfo);
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            LoadTables();
            LoadCategoryList();
            LoadFoodList();

            if (cbCase.Items.Count == 0) cbCase.Items.AddRange(new[] { "Tại quán", "Mang đi" });
            if (cbPayMethod.Items.Count == 0) cbPayMethod.Items.AddRange(new[] { "Tiền mặt", "Thẻ", "Ví điện tử" });
            if (cbOrderType.Items.Count == 0) cbOrderType.Items.AddRange(new[] { "Không buffet", "Buffet" });

            cbCase.SelectedIndex = 0;
            cbPayMethod.SelectedIndex = 0;
            cbOrderType.SelectedItem = "Không buffet";
            LoadOpenBillBySelectedTable();
        }

        private void LoadTables()
        {
            try
            {
                var tables = _repo.GetTableList();
                cbTable.DataSource = tables;
                cbTable.DisplayMember = "Item2";
                cbTable.ValueMember = "Item1";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryList()
        {
            DataTable categories = Database.Instance.ExecuteQuery("SELECT id, name FROM FoodCategory ORDER BY name");
            DataRow allRow = categories.NewRow();
            allRow["id"] = 0;
            allRow["name"] = "Tất cả danh mục";
            categories.Rows.InsertAt(allRow, 0);

            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "name";
            cbCategory.ValueMember = "id";
        }

        private void LoadFoodList()
        {
            try
            {
                string query = @"SELECT f.id, f.name, c.name [Category], f.price, f.image
                                 FROM Food f
                                 JOIN FoodCategory c ON f.idCategory = c.id
                                 WHERE f.status = N'Available'";

                DataTable data = Database.Instance.ExecuteQuery(query);
                _foods.Clear();

                foreach (DataRow row in data.Rows)
                {
                    _foods.Add(new FoodMenuItem
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString(),
                        Category = row["Category"].ToString(),
                        Price = Convert.ToDouble(row["price"]),
                        ImagePath = row["image"] == DBNull.Value ? string.Empty : row["image"].ToString()
                    });
                }

                ApplyMenuFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thực đơn: " + ex.Message);
            }
        }

        private void ApplyMenuFilter()
        {
            string searchText = txtSearchFood.Text.Trim().ToLowerInvariant();
            string selectedCategory = (cbCategory.SelectedItem as DataRowView)?["name"]?.ToString() ?? "Tất cả danh mục";
            bool allCategory = selectedCategory.Equals("Tất cả danh mục", StringComparison.OrdinalIgnoreCase);

            var filtered = _foods.Where(f =>
                (allCategory || f.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchText) || f.Name.ToLowerInvariant().Contains(searchText)));

            RenderFoodCards(filtered);
        }

        private void RenderFoodCards(IEnumerable<FoodMenuItem> foods)
        {
            flpMenu.SuspendLayout();
            flpMenu.Controls.Clear();

            foreach (FoodMenuItem food in foods)
            {
                flpMenu.Controls.Add(CreateFoodCard(food));
            }

            flpMenu.ResumeLayout();
        }

        private Panel CreateFoodCard(FoodMenuItem food)
        {
            Panel card = new Panel
            {
                Width = 180,
                Height = 220,
                Margin = new Padding(8),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = food.Id
            };

            PictureBox pic = new PictureBox
            {
                Dock = DockStyle.Top,
                Height = 130,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Gainsboro
            };

            if (!string.IsNullOrWhiteSpace(food.ImagePath))
            {
                string fullPath = ResolveFoodImagePath(food.ImagePath);
                if (File.Exists(fullPath))
                {
                    pic.Image = Image.FromFile(fullPath);
                }
            }

            Label lblName = new Label
            {
                Dock = DockStyle.Top,
                Height = 46,
                Padding = new Padding(6, 8, 6, 0),
                Text = food.Name,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                AutoEllipsis = true
            };

            Label lblPrice = new Label
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(6, 2, 6, 6),
                Text = $"{food.Price:N0} VNĐ",
                ForeColor = Color.Firebrick,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.TopLeft
            };

            card.Controls.Add(lblPrice);
            card.Controls.Add(lblName);
            card.Controls.Add(pic);

            card.Click += FoodCard_Click;
            pic.Click += FoodCard_Click;
            lblName.Click += FoodCard_Click;
            lblPrice.Click += FoodCard_Click;

            return card;
        }

        private void FoodCard_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null) return;

            Control owner = c as Panel ?? c.Parent;
            if (owner?.Tag == null) return;

            AddFoodToBill(Convert.ToInt32(owner.Tag));
        }

        private void AddFoodToBill(int foodId)
        {
            if (foodId <= 0) return;

            try
            {
                int tableId = (int)cbTable.SelectedValue;
                int billId = GetOpenBillIdByTable(tableId);

                if (billId == 0)
                {
                    string queryInsertBill = $@"INSERT INTO dbo.Bill (idTable, customerName, caseName, payMethod, status, kitchenStatus)
                                               VALUES ({tableId}, N'{EscapeSqlValue(txtCustomerName.Text)}', N'{EscapeSqlValue(cbCase.Text)}', N'{EscapeSqlValue(cbPayMethod.Text)}', 0, N'Draft');
                                               SELECT SCOPE_IDENTITY();";

                    object result = Database.Instance.ExecuteScalar(queryInsertBill);

                    billId = Convert.ToInt32(result);
                    UpdateBillMetadata(billId);
                    txtOrderNo.Text = FormatBillId(billId);
                }
                else
                {
                    UpdateBillMetadata(billId);
                    txtOrderNo.Text = FormatBillId(billId);
                }

                LockBuffetGuestCountIfNeeded();

                Database.Instance.ExecuteNonQuery($"INSERT INTO dbo.BillInfo (idBill, idFood, quantity) VALUES ({billId}, {foodId}, 1)");
                LoadBillDetails(billId);
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message);
            }
        }

        private void LoadBillDetails(int billId)
        {
            string query = @"SELECT bi.idFood AS foodId, f.name AS name, bi.quantity AS quantity,
                                    CASE WHEN ISNULL(b.isBuffet, 0) = 1 THEN 0 ELSE f.price END AS price,
                                    CASE WHEN ISNULL(b.kitchenStatus, N'Pending') = N'Ready'
                                         THEN N'Đã lên món'
                                         ELSE N'Đang chờ'
                                    END AS foodStatus
                             FROM dbo.BillInfo bi
                             JOIN dbo.Food f ON bi.idFood = f.id
                             JOIN dbo.Bill b ON b.id = bi.idBill
                             WHERE bi.idBill = " + billId;

            dgvCart.DataSource = Database.Instance.ExecuteQuery(query);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            bool isBuffet = cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase);

            double subtotal = 0;
            if (isBuffet)
            {
                int guestCount = (int)numBuffetGuestCount.Value;
                subtotal = BuffetFixedPrice * guestCount;
            }
            else
            {
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.IsNewRow) continue;

                    object priceObj = GetCellValue(row, "colPrice", "price");
                    object qtyObj = GetCellValue(row, "colQty", "quantity");

                    double price = priceObj == null || priceObj == DBNull.Value ? 0 : Convert.ToDouble(priceObj);
                    int qty = qtyObj == null || qtyObj == DBNull.Value ? 0 : Convert.ToInt32(qtyObj);
                    subtotal += price * qty;
                }
            }

            double discountPercent = (double)numDiscount.Value;
            double tax = isBuffet ? 0 : subtotal * 0.07;
            double total = isBuffet
                ? subtotal
                : (subtotal + tax) * (1 - discountPercent / 100);

            lblSubtotalValue.Text = subtotal.ToString("N0") + " VNĐ";
            lblTaxValue.Text = tax.ToString("N0") + " VNĐ";
            lblTotalValue.Text = total.ToString("N0") + " VNĐ";
        }

        private object GetCellValue(DataGridViewRow row, params string[] preferredColumnNames)
        {
            foreach (string columnName in preferredColumnNames)
            {
                if (string.IsNullOrWhiteSpace(columnName)) continue;
                if (dgvCart.Columns.Contains(columnName))
                {
                    return row.Cells[columnName].Value;
                }
            }

            return null;
        }

        private void ResetForm()
        {
            txtCustomerName.Text = "";
            txtOrderNo.Text = "";

            if (cbTable.Items.Count > 0) cbTable.SelectedIndex = 0;
            if (cbCase.Items.Count > 0) cbCase.SelectedIndex = 0;
            if (cbPayMethod.Items.Count > 0) cbPayMethod.SelectedIndex = 0;

            dgvCart.DataSource = null;
            numBuffetGuestCount.Value = 1;
            numBuffetGuestCount.Enabled = false;
            numBuffetGuestCount.Visible = false;
            lblBuffetGuestCount.Visible = false;
            btnIncreaseBuffetGuestCount.Visible = false;
            btnIncreaseBuffetGuestCount.Enabled = false;
            _isBuffetGuestCountLocked = false;
            ApplyOrderInfoLockState(false);
            lblTotalValue.Text = "0 VNĐ";
            txtCustomerName.Focus();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                MessageBox.Show("Không có hóa đơn nào để xóa!", "Thông báo");
                return;
            }

            int billId = int.Parse(txtOrderNo.Text);
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn XÓA TẤT CẢ món ăn của hóa đơn {txtOrderNo.Text} không?",
                                                  "Cảnh báo xóa dữ liệu",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                int check = Database.Instance.ExecuteNonQuery("DELETE dbo.BillInfo WHERE idBill = " + billId);
                if (check >= 0)
                {
                    MessageBox.Show("Đã xóa sạch các món trong đơn hàng!", "Thành công");
                    LoadBillDetails(billId);
                    CalculateTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategory.SelectedValue == null) return;
            ApplyMenuFilter();
        }

        private void txtSearchFood_TextChanged(object sender, EventArgs e)
        {
            ApplyMenuFilter();
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvCart.Columns[e.ColumnIndex].Name != "colDelete") return;

            try
            {
                object foodIdObj = dgvCart.Rows[e.RowIndex].Cells["FoodID"].Value;
                if (foodIdObj == null) return;

                int foodId = Convert.ToInt32(foodIdObj);
                int billId = int.Parse(txtOrderNo.Text.Replace("HD", ""));

                if (MessageBox.Show("Bạn muốn xóa món này khỏi đơn hàng?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable billInfoRows = Database.Instance.ExecuteQuery(
                        $"SELECT TOP 1 id, quantity FROM dbo.BillInfo WHERE idBill = {billId} AND idFood = {foodId} ORDER BY id");

                    if (billInfoRows.Rows.Count == 0) return;

                    int billInfoId = Convert.ToInt32(billInfoRows.Rows[0]["id"]);
                    int currentQuantity = Convert.ToInt32(billInfoRows.Rows[0]["quantity"]);

                    int result = currentQuantity > 1
                        ? Database.Instance.ExecuteNonQuery($"UPDATE dbo.BillInfo SET quantity = quantity - 1 WHERE id = {billInfoId}")
                        : Database.Instance.ExecuteNonQuery($"DELETE dbo.BillInfo WHERE id = {billInfoId}");

                    if (result > 0)
                    {
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

        private void numDiscount_ValueChanged_1(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void btnCheckout_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cbTable.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước khi gửi món!");
                    return;
                }

                int tableId = (int)cbTable.SelectedValue;
                int billId = GetOpenBillIdByTable(tableId);
                if (billId == 0)
                {
                    MessageBox.Show("Bàn này chưa có món nào để gửi.", "Thông báo");
                    return;
                }

                string updateBill = $@"UPDATE dbo.Bill
                                       SET customerName = N'{EscapeSqlValue(txtCustomerName.Text)}',
                                           caseName = N'{EscapeSqlValue(cbCase.Text)}',
                                           payMethod = N'{EscapeSqlValue(cbPayMethod.Text)}',
                                           note = N'{EscapeSqlValue(BuildBillNote())}',
                                           diningStatus = N'Đang dùng bữa',
                                           orderInfoLocked = 1,
                                           kitchenStatus = CASE
                                               WHEN kitchenStatus IS NULL OR kitchenStatus = N'Draft' THEN N'Pending'
                                               ELSE kitchenStatus
                                           END
                                       WHERE id = {billId}";
                Database.Instance.ExecuteNonQuery(updateBill);
                ApplyOrderInfoLockState(true);

                txtOrderNo.Text = FormatBillId(billId);
                MessageBox.Show($"Đã xác nhận đơn và chuyển trạng thái 'Đang dùng bữa' cho bàn {cbTable.Text}.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi món: " + ex.Message);
            }
        }

        private void BtnRequestEditOrderInfo_Click(object sender, EventArgs e)
        {
            if (!_isOrderInfoLocked) return;

            string pin = PromptForInput("Nhập mật khẩu/Mã PIN nhân viên để mở khóa:", "Xác thực chỉnh sửa");
            if (string.IsNullOrWhiteSpace(pin))
            {
                return;
            }

            string query = $@"SELECT TOP 1 userId
                              FROM Account
                              WHERE password = N'{EscapeSqlValue(pin)}'
                                AND isDeleted = 0";
            DataTable dt = Database.Instance.ExecuteQuery(query);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Mật khẩu/PIN không đúng.", "Thông báo");
                return;
            }

            Database.Instance.ExecuteNonQuery($"UPDATE dbo.Bill SET orderInfoLocked = 0 WHERE id = {GetOpenBillIdByTable((int)cbTable.SelectedValue)}");
            ApplyOrderInfoLockState(false);
            MessageBox.Show("Đã mở khóa thông tin đơn. Vui lòng chỉnh sửa và bấm Lưu.", "Thông báo");
        }

        private void BtnSaveOrderInfo_Click(object sender, EventArgs e)
        {
            if (_isOrderInfoLocked || cbTable.SelectedValue == null) return;

            int billId = GetOpenBillIdByTable((int)cbTable.SelectedValue);
            if (billId == 0) return;

            UpdateBillMetadata(billId);
            Database.Instance.ExecuteNonQuery($"UPDATE dbo.Bill SET orderInfoLocked = 1 WHERE id = {billId}");
            ApplyOrderInfoLockState(true);
            MessageBox.Show("Đã lưu thay đổi và khóa lại thông tin đơn hàng.", "Thông báo");
        }

        private void BtnBuffetLogin_Click(object sender, EventArgs e)
        {
            if (!cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Vui lòng chọn loại đơn Buffet trước.", "Thông báo");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBuffetAccount.Text) || string.IsNullOrWhiteSpace(txtBuffetPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu Buffet.", "Thông báo");
                return;
            }

            string query = $@"SELECT TOP 1 userId, fullname
                              FROM Account
                              WHERE userId = N'{EscapeSqlValue(txtBuffetAccount.Text.Trim())}'
                                AND password = N'{EscapeSqlValue(txtBuffetPassword.Text)}'
                                AND isDeleted = 0";
            DataTable dt = Database.Instance.ExecuteQuery(query);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Đăng nhập Buffet không thành công. Vui lòng kiểm tra lại tài khoản trong cơ sở dữ liệu.", "Thông báo");
                return;
            }

            txtCustomerName.Text = dt.Rows[0]["fullname"].ToString();
            cbCase.SelectedItem = "Buffet";
            MessageBox.Show("Đăng nhập Buffet thành công. Bạn có thể chọn món và gửi từng đợt.", "Thông báo");
        }

        private void HandleSendPaymentRequestClick2026(object sender, EventArgs e)
        {
            if (cbTable.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bàn.", "Thông báo");
                return;
            }

            int tableId = (int)cbTable.SelectedValue;
            int billId = GetOpenBillIdByTable(tableId);
            if (billId == 0)
            {
                MessageBox.Show("Không có đơn mở để yêu cầu thanh toán.", "Thông báo");
                return;
            }

            string query = $"UPDATE dbo.Bill SET kitchenStatus = N'ReadyToPay' WHERE id = {billId}";
            Database.Instance.ExecuteNonQuery(query);
            MessageBox.Show("Đã gửi yêu cầu thanh toán sang Order Management.", "Thông báo");
            LoadBillDetails(billId);
        }

        private void HandleOrderTypeChangedBuffetRule2026(object sender, EventArgs e)
        {
            bool isBuffet = cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase);

            if (_isBuffetLocked && !isBuffet)
            {
                cbOrderType.SelectedItem = "Buffet";
                MessageBox.Show("Đơn Buffet đã được khóa, không thể chuyển sang Không buffet.", "Thông báo");
                return;
            }

            if (isBuffet && cbCase.Text.Equals("Mang đi", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Hình thức Mang đi không áp dụng loại đơn Buffet.", "Thông báo");
                cbOrderType.SelectedItem = _lastOrderType;
                return;
            }

            if (isBuffet)
            {
                _isBuffetLocked = true;
                lblBuffetGuestCount.Visible = true;
                numBuffetGuestCount.Visible = true;
                numBuffetGuestCount.Enabled = !_isBuffetGuestCountLocked;
                btnIncreaseBuffetGuestCount.Visible = false;
                btnIncreaseBuffetGuestCount.Enabled = false;
            }
            else
            {
                lblBuffetGuestCount.Visible = false;
                numBuffetGuestCount.Visible = false;
                numBuffetGuestCount.Enabled = false;
                btnIncreaseBuffetGuestCount.Visible = false;
                btnIncreaseBuffetGuestCount.Enabled = false;
                if (!_isBuffetLocked)
                {
                    _isBuffetGuestCountLocked = false;
                    numBuffetGuestCount.Value = 1;
                }
            }

            _lastOrderType = cbOrderType.Text;

            if (!string.IsNullOrWhiteSpace(txtOrderNo.Text) && int.TryParse(txtOrderNo.Text, out int billId))
            {
                UpdateBillMetadata(billId);
            }
            CalculateTotal();
        }

        private void HandleCaseSelectionChangedBuffetRule(object sender, EventArgs e)
        {
            if (cbCase.Text.Equals("Mang đi", StringComparison.OrdinalIgnoreCase) && _isBuffetLocked)
            {
                MessageBox.Show("Đơn Buffet chỉ áp dụng cho hình thức Tại quán.", "Thông báo");
                cbCase.SelectedItem = "Tại quán";
                return;
            }

            if (cbCase.Text.Equals("Mang đi", StringComparison.OrdinalIgnoreCase) &&
                cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Mang đi không thể chọn Buffet. Hệ thống sẽ chuyển về Không buffet.", "Thông báo");
                cbOrderType.SelectedItem = "Không buffet";
            }
        }

        private void ApplyHiddenBuffetInputsState()
        {
            lblBuffetAccount.Visible = false;
            txtBuffetAccount.Visible = false;
            lblBuffetPassword.Visible = false;
            txtBuffetPassword.Visible = false;
            btnBuffetLogin.Visible = false;
            txtCustomerName.ReadOnly = false;
        }

        private void ApplyOrderInfoLockState(bool isLocked)
        {
            _isOrderInfoLocked = isLocked;

            txtCustomerName.ReadOnly = isLocked;
            cbTable.Enabled = !isLocked;
            cbOrderType.Enabled = !isLocked;
            cbCase.Enabled = !isLocked;
            cbPayMethod.Enabled = !isLocked;

            if (cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase))
            {
                numBuffetGuestCount.Enabled = !isLocked && !_isBuffetGuestCountLocked;
                btnIncreaseBuffetGuestCount.Enabled = !isLocked && btnIncreaseBuffetGuestCount.Visible;
            }
            else
            {
                numBuffetGuestCount.Enabled = false;
                btnIncreaseBuffetGuestCount.Enabled = false;
            }

            btnCheckout.Text = isLocked ? "Đã xác nhận" : "Xác nhận";
            btnRequestEditOrderInfo.Visible = isLocked;
            btnSaveOrderInfo.Visible = !isLocked;
        }

        private string PromptForInput(string message, string title)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 380;
                prompt.Height = 170;
                prompt.Text = title;
                Label textLabel = new Label { Left = 12, Top = 14, Text = message, Width = 340 };
                TextBox inputBox = new TextBox { Left = 12, Top = 44, Width = 340, PasswordChar = '*' };
                Button confirmation = new Button { Text = "OK", Left = 272, Width = 80, Top = 78, DialogResult = DialogResult.OK };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
            }
        }

        private void NumBuffetGuestCount_ValueChanged(object sender, EventArgs e)
        {
            if (!cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (!_isBuffetGuestCountLocked)
            {
                if (!string.IsNullOrWhiteSpace(txtOrderNo.Text) && int.TryParse(txtOrderNo.Text, out int billId))
                {
                    UpdateBillMetadata(billId);
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtOrderNo.Text) && int.TryParse(txtOrderNo.Text, out int lockedBillId))
            {
                // Đồng bộ lại dữ liệu với bill đã khóa để tránh chỉnh sửa trái luồng.
                UpdateBillMetadata(lockedBillId);
            }

            CalculateTotal();
        }

        private void LockBuffetGuestCountIfNeeded()
        {
            if (!cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase) || _isBuffetGuestCountLocked)
            {
                return;
            }

            _isBuffetGuestCountLocked = true;
            numBuffetGuestCount.Enabled = false;
            btnIncreaseBuffetGuestCount.Visible = false;
            btnIncreaseBuffetGuestCount.Enabled = false;
        }

        private void BtnIncreaseBuffetGuestCount_Click(object sender, EventArgs e)
        {
            if (!cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (numBuffetGuestCount.Value >= numBuffetGuestCount.Maximum)
            {
                MessageBox.Show("Đã đạt số khách buffet tối đa.", "Thông báo");
                return;
            }

            numBuffetGuestCount.Value += 1;

            if (!string.IsNullOrWhiteSpace(txtOrderNo.Text) && int.TryParse(txtOrderNo.Text, out int billId))
            {
                UpdateBillMetadata(billId);
            }

            CalculateTotal();
            MessageBox.Show("Đã thêm 1 khách buffet và cập nhật tổng tiền.", "Thông báo");
        }

        private void ApplyCreateOrderThemeStyling()
        {
            Color primary = Color.FromArgb(158, 27, 27);
            pnlMenu.BackColor = Color.FromArgb(255, 245, 245);
            pnlOrder.BackColor = Color.FromArgb(255, 245, 245);
            lblTitle.ForeColor = primary;
            lblMenu.ForeColor = primary;
            btnCheckout.BackColor = primary;
            btnClear.BackColor = Color.FromArgb(233, 236, 239);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderColor = primary;
            btnClear.FlatAppearance.BorderSize = 1;
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = primary;
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 215, 218);
            dgvCart.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOpenBillBySelectedTable();
        }

        private void LoadOpenBillBySelectedTable()
        {
            if (cbTable.SelectedValue == null) return;

            int tableId;
            if (!int.TryParse(cbTable.SelectedValue.ToString(), out tableId)) return;

            string query = $@"SELECT TOP 1 id, customerName, caseName, payMethod, note,
                                     ISNULL(isBuffet, 0) AS isBuffet,
                                     ISNULL(buffetGuestCount, 1) AS buffetGuestCount,
                                     ISNULL(orderInfoLocked, 0) AS orderInfoLocked
                              FROM Bill
                              WHERE idTable = {tableId}
                                AND status = 0
                              ORDER BY id DESC";
            DataTable dt = Database.Instance.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
            {
                txtOrderNo.Text = "";
                txtCustomerName.Text = "";
                txtNote.Text = "";
                if (cbCase.Items.Count > 0) cbCase.SelectedIndex = 0;
                if (cbPayMethod.Items.Count > 0) cbPayMethod.SelectedIndex = 0;
                cbOrderType.SelectedItem = "Không buffet";
                _isBuffetLocked = false;
                _isBuffetGuestCountLocked = false;
                _lastOrderType = "Không buffet";
                numBuffetGuestCount.Value = 1;
                numBuffetGuestCount.Enabled = false;
                numBuffetGuestCount.Visible = false;
                lblBuffetGuestCount.Visible = false;
                btnIncreaseBuffetGuestCount.Visible = false;
                btnIncreaseBuffetGuestCount.Enabled = false;
                dgvCart.DataSource = null;
                ApplyOrderInfoLockState(false);
                CalculateTotal();
                return;
            }

            DataRow row = dt.Rows[0];
            int billId = Convert.ToInt32(row["id"]);
            txtOrderNo.Text = FormatBillId(billId);
            txtCustomerName.Text = row["customerName"] == DBNull.Value ? "" : row["customerName"].ToString();

            string caseName = row["caseName"] == DBNull.Value ? "" : row["caseName"].ToString();
            if (!string.IsNullOrWhiteSpace(caseName) && cbCase.Items.Contains(caseName))
                cbCase.SelectedItem = caseName;

            string payMethod = row["payMethod"] == DBNull.Value ? "" : row["payMethod"].ToString();
            if (!string.IsNullOrWhiteSpace(payMethod) && cbPayMethod.Items.Contains(payMethod))
                cbPayMethod.SelectedItem = payMethod;

            string billNote = row["note"] == DBNull.Value ? "" : row["note"].ToString();
            txtNote.Text = billNote;
            bool isBuffetBill = Convert.ToInt32(row["isBuffet"]) == 1;
            int buffetGuestCount = Convert.ToInt32(row["buffetGuestCount"]);
            if (isBuffetBill)
            {
                cbOrderType.SelectedItem = "Buffet";
                _isBuffetLocked = true;
                _isBuffetGuestCountLocked = true;
                _lastOrderType = "Buffet";
                numBuffetGuestCount.Value = buffetGuestCount <= 0 ? 1 : buffetGuestCount;
                numBuffetGuestCount.Visible = true;
                lblBuffetGuestCount.Visible = true;
                numBuffetGuestCount.Enabled = false;
                btnIncreaseBuffetGuestCount.Visible = false;
                btnIncreaseBuffetGuestCount.Enabled = false;
            }
            else
            {
                cbOrderType.SelectedItem = "Không buffet";
                _isBuffetLocked = false;
                _isBuffetGuestCountLocked = false;
                _lastOrderType = "Không buffet";
                numBuffetGuestCount.Value = 1;
                numBuffetGuestCount.Visible = false;
                lblBuffetGuestCount.Visible = false;
                numBuffetGuestCount.Enabled = false;
                btnIncreaseBuffetGuestCount.Visible = false;
                btnIncreaseBuffetGuestCount.Enabled = false;
            }

            bool isInfoLocked = Convert.ToInt32(row["orderInfoLocked"]) == 1;
            ApplyOrderInfoLockState(isInfoLocked);

            LoadBillDetails(billId);
        }

        private void UpdateBillMetadata(int billId)
        {
            if (billId <= 0) return;

            string query = $@"UPDATE dbo.Bill
                              SET customerName = N'{EscapeSqlValue(txtCustomerName.Text)}',
                                  caseName = N'{EscapeSqlValue(cbCase.Text)}',
                                  payMethod = N'{EscapeSqlValue(cbPayMethod.Text)}',
                                  note = N'{EscapeSqlValue(BuildBillNote())}',
                                  isBuffet = {(cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase) ? 1 : 0)},
                                  buffetGuestCount = {(cbOrderType.Text.Equals("Buffet", StringComparison.OrdinalIgnoreCase) ? (int)numBuffetGuestCount.Value : 0)}
                              WHERE id = {billId}";
            Database.Instance.ExecuteNonQuery(query);
        }

        private string BuildBillNote()
        {
            return (txtNote.Text ?? string.Empty).Trim();
        }

        private void EnsureBuffetSchema()
        {
            Database.Instance.ExecuteNonQuery(@"
IF COL_LENGTH('dbo.Bill', 'isBuffet') IS NULL
BEGIN
    ALTER TABLE dbo.Bill ADD isBuffet BIT NOT NULL CONSTRAINT DF_Bill_isBuffet DEFAULT(0);
END");
            Database.Instance.ExecuteNonQuery(@"
IF COL_LENGTH('dbo.Bill', 'buffetGuestCount') IS NULL
BEGIN
    ALTER TABLE dbo.Bill ADD buffetGuestCount INT NOT NULL CONSTRAINT DF_Bill_buffetGuestCount DEFAULT(1);
END");
            Database.Instance.ExecuteNonQuery(@"
IF COL_LENGTH('dbo.Bill', 'orderInfoLocked') IS NULL
BEGIN
    ALTER TABLE dbo.Bill ADD orderInfoLocked BIT NOT NULL CONSTRAINT DF_Bill_orderInfoLocked DEFAULT(0);
END");
            Database.Instance.ExecuteNonQuery(@"
IF COL_LENGTH('dbo.Bill', 'diningStatus') IS NULL
BEGIN
    ALTER TABLE dbo.Bill ADD diningStatus NVARCHAR(50) NOT NULL CONSTRAINT DF_Bill_diningStatus DEFAULT(N'Chuẩn bị');
END");
        }

        private int GetOpenBillIdByTable(int tableId)
        {
            object result = Database.Instance.ExecuteScalar($@"SELECT TOP 1 id
                                                               FROM Bill
                                                               WHERE idTable = {tableId}
                                                                 AND status = 0
                                                               ORDER BY id DESC");
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        private string FormatBillId(int id)
        {
            return id.ToString("D4");
        }

        private static string EscapeSqlValue(string input)
        {
            return (input ?? string.Empty).Replace("'", "''");
        }

        private static string ResolveFoodImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return string.Empty;
            }

            if (Path.IsPathRooted(imagePath))
            {
                return imagePath;
            }

            string startupPath = Application.StartupPath;
            string directPath = Path.Combine(startupPath, imagePath);
            if (File.Exists(directPath))
            {
                return directPath;
            }

            string projectPath = startupPath;
            for (int i = 0; i < 4; i++)
            {
                DirectoryInfo parent = Directory.GetParent(projectPath);
                if (parent == null) break;
                projectPath = parent.FullName;

                string candidate = Path.Combine(projectPath, imagePath);
                if (File.Exists(candidate))
                {
                    return candidate;
                }
            }

            return directPath;
        }

        private sealed class FoodMenuItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public string ImagePath { get; set; }
        }
    }
}
