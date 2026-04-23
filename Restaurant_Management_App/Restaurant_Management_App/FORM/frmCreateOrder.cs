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
        private readonly OrderRepository _repo = new OrderRepository();
        private readonly BuffetService _buffetService = new BuffetService();
        private readonly List<FoodMenuItem> _foods = new List<FoodMenuItem>();
        private readonly Dictionary<int, int> _draftBuffetItems = new Dictionary<int, int>();
        private readonly Dictionary<int, int> _submittedBuffetItems = new Dictionary<int, int>();

        private ComboBox _cbServiceMode;
        private ComboBox _cbBuffetPackage;
        private NumericUpDown _numBuffetGuests;
        private Button _btnBuffetPay;
        private Label _lblBuffetPackage;
        private Label _lblBuffetGuests;

        private bool IsBuffetMode => _cbServiceMode != null && _cbServiceMode.SelectedItem != null && _cbServiceMode.SelectedItem.ToString() == "Buffet";

        public frmCreateOrder()
        {
            InitializeComponent();

            Load += FrmOrder_Load;
            btnClear.Click += BtnClear_Click;
            numDiscount.ValueChanged += (s, e) => CalculateTotal();
            cbTable.SelectedIndexChanged += cbTable_SelectedIndexChanged;

            // Hide discount controls on Create Order screen as requested.
            lblDiscountCaption.Visible = false;
            numDiscount.Visible = false;
            numDiscount.Value = 0;

            SetupBuffetControls();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            _buffetService.EnsureSchema();
            LoadTables();
            LoadCategoryList();
            LoadFoodList();

            if (cbCase.Items.Count == 0) cbCase.Items.AddRange(new[] { "Eat in", "Take away", "Delivery" });
            if (cbPayMethod.Items.Count == 0) cbPayMethod.Items.AddRange(new[] { "Cash", "Credit Card", "E-Wallet" });

            cbCase.SelectedIndex = 0;
            cbPayMethod.SelectedIndex = 0;
            cbOrderType.SelectedIndex = 0;
            LoadOpenBillBySelectedTable();
        }

        private void SetupBuffetControls()
        {
            pnlOrderTop.Height = 150;

            _cbServiceMode = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(515, 96),
                Size = new Size(130, 24)
            };
            _cbServiceMode.Items.AddRange(new object[] { "Normal", "Buffet" });
            _cbServiceMode.SelectedIndex = 0;
            _cbServiceMode.SelectedIndexChanged += ServiceModeChanged;
            pnlOrderTop.Controls.Add(_cbServiceMode);

            Label lblMode = new Label
            {
                Text = "Service",
                Location = new Point(430, 100),
                AutoSize = true
            };
            pnlOrderTop.Controls.Add(lblMode);

            _lblBuffetPackage = new Label { Text = "Buffet gói", Location = new Point(10, 100), AutoSize = true, Visible = false };
            pnlOrderTop.Controls.Add(_lblBuffetPackage);

            _cbBuffetPackage = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(90, 96),
                Size = new Size(90, 24),
                Visible = false
            };
            _cbBuffetPackage.Items.AddRange(new object[] { "299K", "599K" });
            _cbBuffetPackage.SelectedIndex = 0;
            _cbBuffetPackage.SelectedIndexChanged += (s, e) => ApplyMenuFilter();
            pnlOrderTop.Controls.Add(_cbBuffetPackage);

            _lblBuffetGuests = new Label { Text = "Số khách", Location = new Point(190, 100), AutoSize = true, Visible = false };
            pnlOrderTop.Controls.Add(_lblBuffetGuests);

            _numBuffetGuests = new NumericUpDown
            {
                Location = new Point(252, 96),
                Size = new Size(70, 24),
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Visible = false
            };
            pnlOrderTop.Controls.Add(_numBuffetGuests);

            _btnBuffetPay = new Button
            {
                Text = "Thanh toán Buffet",
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(470, 106),
                Size = new Size(140, 32),
                Visible = false
            };
            _btnBuffetPay.FlatAppearance.BorderSize = 0;
            _btnBuffetPay.Click += BtnBuffetPay_Click;
            pnlSummary.Controls.Add(_btnBuffetPay);
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
                MessageBox.Show("Failed to load tables: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryList()
        {
            DataTable categories = Database.Instance.ExecuteQuery("SELECT id, name FROM FoodCategory ORDER BY name");
            DataRow allRow = categories.NewRow();
            allRow["id"] = 0;
            allRow["name"] = "All category";
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
                MessageBox.Show("Failed to load menu: " + ex.Message);
            }
        }

        private void ApplyMenuFilter()
        {
            string searchText = txtSearchFood.Text.Trim().ToLowerInvariant();
            string selectedCategory = (cbCategory.SelectedItem as DataRowView)?["name"]?.ToString() ?? "All category";
            bool allCategory = selectedCategory.Equals("All category", StringComparison.OrdinalIgnoreCase);
            bool isBuffet299 = IsBuffetMode && _cbBuffetPackage.SelectedItem != null && _cbBuffetPackage.SelectedItem.ToString() == "299K";
            bool isBuffet599 = IsBuffetMode && _cbBuffetPackage.SelectedItem != null && _cbBuffetPackage.SelectedItem.ToString() == "599K";

            var filtered = _foods.Where(f =>
                (allCategory || f.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(searchText) || f.Name.ToLowerInvariant().Contains(searchText)) &&
                (!IsBuffetMode || (isBuffet299 && f.Price <= 299000) || (isBuffet599 && f.Price <= 599000)));

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
            if (IsBuffetMode)
            {
                AddFoodToBuffetDraft(foodId);
                return;
            }

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
                    txtOrderNo.Text = FormatBillId(billId);
                }
                else
                {
                    txtOrderNo.Text = FormatBillId(billId);
                }

                Database.Instance.ExecuteNonQuery($"INSERT INTO dbo.BillInfo (idBill, idFood, quantity) VALUES ({billId}, {foodId}, 1)");
                LoadBillDetails(billId);
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message);
            }
        }

        private void AddFoodToBuffetDraft(int foodId)
        {
            if (_draftBuffetItems.ContainsKey(foodId))
            {
                _draftBuffetItems[foodId] += 1;
            }
            else
            {
                _draftBuffetItems[foodId] = 1;
            }

            BindBuffetCartView();
            CalculateTotal();
        }

        private void BindBuffetCartView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("foodId", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("quantity", typeof(int));
            dt.Columns.Add("price", typeof(double));
            dt.Columns.Add("source", typeof(string));

            foreach (KeyValuePair<int, int> item in _submittedBuffetItems)
            {
                FoodMenuItem food = _foods.FirstOrDefault(f => f.Id == item.Key);
                if (food == null) continue;
                dt.Rows.Add(food.Id, food.Name, item.Value, food.Price, "Đã submit");
            }

            foreach (KeyValuePair<int, int> item in _draftBuffetItems)
            {
                FoodMenuItem food = _foods.FirstOrDefault(f => f.Id == item.Key);
                if (food == null) continue;
                dt.Rows.Add(food.Id, food.Name, item.Value, food.Price, "Chờ submit");
            }

            dgvCart.DataSource = dt;
        }

        private void LoadBillDetails(int billId)
        {
            string query = @"SELECT bi.idFood AS foodId, f.name AS name, bi.quantity AS quantity, f.price AS price
                             FROM dbo.BillInfo bi
                             JOIN dbo.Food f ON bi.idFood = f.id
                             WHERE bi.idBill = " + billId;

            dgvCart.DataSource = Database.Instance.ExecuteQuery(query);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            double subtotal = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;

                object priceObj = GetCellValue(row, "colPrice", "price");
                object qtyObj = GetCellValue(row, "colQty", "quantity");

                double price = priceObj == null || priceObj == DBNull.Value ? 0 : Convert.ToDouble(priceObj);
                int qty = qtyObj == null || qtyObj == DBNull.Value ? 0 : Convert.ToInt32(qtyObj);
                subtotal += price * qty;
            }

            double discountPercent = (double)numDiscount.Value;
            double tax = subtotal * 0.07;
            double total = IsBuffetMode
                ? ((_cbBuffetPackage.SelectedItem?.ToString() == "599K" ? 599000 : 299000) * (double)_numBuffetGuests.Value)
                : (subtotal + tax) * (1 - discountPercent / 100);

            lblSubtotalValue.Text = subtotal.ToString("N0") + " VNĐ";
            lblTaxValue.Text = IsBuffetMode ? "0 VNĐ" : tax.ToString("N0") + " VNĐ";
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

            if (IsBuffetMode)
            {
                RemoveOneBuffetItem(e.RowIndex);
                return;
            }

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

        private void RemoveOneBuffetItem(int rowIndex)
        {
            try
            {
                object foodIdObj = dgvCart.Rows[rowIndex].Cells["FoodID"].Value;
                if (foodIdObj == null) return;

                int foodId = Convert.ToInt32(foodIdObj);

                if (_draftBuffetItems.ContainsKey(foodId) && _draftBuffetItems[foodId] > 0)
                {
                    _draftBuffetItems[foodId] -= 1;
                    if (_draftBuffetItems[foodId] <= 0) _draftBuffetItems.Remove(foodId);
                }
                else if (_submittedBuffetItems.ContainsKey(foodId) && _submittedBuffetItems[foodId] > 0)
                {
                    _submittedBuffetItems[foodId] -= 1;
                    if (_submittedBuffetItems[foodId] <= 0) _submittedBuffetItems.Remove(foodId);
                }

                BindBuffetCartView();
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa món buffet: " + ex.Message);
            }
        }

        private void numDiscount_ValueChanged_1(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void btnCheckout_Click_1(object sender, EventArgs e)
        {
            if (IsBuffetMode)
            {
                SubmitBuffetBatch(false);
                return;
            }

            try
            {
                if (cbTable.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước khi lưu đơn!");
                    return;
                }

                int tableId = (int)cbTable.SelectedValue;
                int billId = GetOpenBillIdByTable(tableId);
                if (billId == 0)
                {
                    MessageBox.Show("Bàn này chưa có món nào để lưu.", "Thông báo");
                    return;
                }

                string updateBill = $@"UPDATE dbo.Bill
                                       SET customerName = N'{EscapeSqlValue(txtCustomerName.Text)}',
                                           caseName = N'{EscapeSqlValue(cbCase.Text)}',
                                           payMethod = N'{EscapeSqlValue(cbPayMethod.Text)}',
                                           kitchenStatus = CASE
                                               WHEN kitchenStatus IS NULL OR kitchenStatus = N'Draft' THEN N'Pending'
                                               ELSE kitchenStatus
                                           END
                                       WHERE id = {billId}";
                Database.Instance.ExecuteNonQuery(updateBill);

                txtOrderNo.Text = FormatBillId(billId);
                MessageBox.Show($"Đã lưu order bàn {cbTable.Text}. Bàn vẫn mở để gọi thêm món cho đến khi thanh toán ở Order Management.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu đơn: " + ex.Message);
            }
        }

        private void SubmitBuffetBatch(bool silentWhenEmpty = false)
        {
            if (_draftBuffetItems.Count == 0)
            {
                if (!silentWhenEmpty)
                {
                    MessageBox.Show("Chưa có món buffet nào để submit.");
                }
                return;
            }

            foreach (KeyValuePair<int, int> item in _draftBuffetItems)
            {
                if (_submittedBuffetItems.ContainsKey(item.Key))
                {
                    _submittedBuffetItems[item.Key] += item.Value;
                }
                else
                {
                    _submittedBuffetItems[item.Key] = item.Value;
                }
            }

            _draftBuffetItems.Clear();
            BindBuffetCartView();
            CalculateTotal();
            if (!silentWhenEmpty)
            {
                MessageBox.Show("Đã submit đợt gọi món buffet.");
            }
        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOpenBillBySelectedTable();
        }

        private void ServiceModeChanged(object sender, EventArgs e)
        {
            bool buffet = IsBuffetMode;
            _lblBuffetPackage.Visible = buffet;
            _cbBuffetPackage.Visible = buffet;
            _lblBuffetGuests.Visible = buffet;
            _numBuffetGuests.Visible = buffet;
            _btnBuffetPay.Visible = buffet;

            cbCase.Enabled = !buffet;
            cbPayMethod.Enabled = !buffet;
            txtNote.Enabled = !buffet;
            btnCheckout.Text = buffet ? "Submit Buffet" : "Submit";

            _draftBuffetItems.Clear();
            _submittedBuffetItems.Clear();
            ApplyMenuFilter();
            LoadOpenBillBySelectedTable();
        }

        private void LoadOpenBillBySelectedTable()
        {
            if (cbTable.SelectedValue == null) return;

            int tableId;
            if (!int.TryParse(cbTable.SelectedValue.ToString(), out tableId)) return;

            if (IsBuffetMode)
            {
                EnsureBuffetTableAccount(tableId);
            }

            string query = $@"SELECT TOP 1 id, customerName, caseName, payMethod
                              FROM Bill
                              WHERE idTable = {tableId}
                                AND status = 0
                              ORDER BY id DESC";
            DataTable dt = Database.Instance.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
            {
                txtOrderNo.Text = "";
                txtCustomerName.Text = IsBuffetMode ? $"Table {tableId}" : "";
                if (cbCase.Items.Count > 0) cbCase.SelectedIndex = 0;
                if (cbPayMethod.Items.Count > 0) cbPayMethod.SelectedIndex = 0;
                dgvCart.DataSource = null;
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

            if (IsBuffetMode)
            {
                _submittedBuffetItems.Clear();
                _draftBuffetItems.Clear();
                LoadBuffetSubmittedFromBill(billId);
                BindBuffetCartView();
            }
            else
            {
                LoadBillDetails(billId);
            }
        }

        private void LoadBuffetSubmittedFromBill(int billId)
        {
            DataTable dt = Database.Instance.ExecuteQuery($@"SELECT idFood, SUM(quantity) quantity
                                                             FROM BillInfo
                                                             WHERE idBill = {billId}
                                                             GROUP BY idFood");
            foreach (DataRow row in dt.Rows)
            {
                int foodId = Convert.ToInt32(row["idFood"]);
                int qty = Convert.ToInt32(row["quantity"]);
                _submittedBuffetItems[foodId] = qty;
            }
        }

        private void BtnBuffetPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsBuffetMode) return;
                if (cbTable.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn.");
                    return;
                }

                SubmitBuffetBatch(true);

                if (_submittedBuffetItems.Count == 0)
                {
                    MessageBox.Show("Không có món buffet để thanh toán.");
                    return;
                }

                int tableId = (int)cbTable.SelectedValue;
                int billId = GetOpenBillIdByTable(tableId);
                if (billId == 0)
                {
                    object result = Database.Instance.ExecuteScalar($@"INSERT INTO dbo.Bill (idTable, customerName, caseName, payMethod, status, kitchenStatus)
                                                                       VALUES ({tableId}, N'{EscapeSqlValue(txtCustomerName.Text)}', N'Buffet {_cbBuffetPackage.Text}', N'Cash', 0, N'Ready');
                                                                       SELECT SCOPE_IDENTITY();");
                    billId = Convert.ToInt32(result);
                }
                else
                {
                    Database.Instance.ExecuteNonQuery($@"UPDATE dbo.Bill
                                                         SET customerName = N'{EscapeSqlValue(txtCustomerName.Text)}',
                                                             caseName = N'Buffet {_cbBuffetPackage.Text}',
                                                             payMethod = N'Cash',
                                                             kitchenStatus = N'Ready'
                                                         WHERE id = {billId}");
                    Database.Instance.ExecuteNonQuery($"DELETE dbo.BillInfo WHERE idBill = {billId}");
                }

                foreach (KeyValuePair<int, int> item in _submittedBuffetItems)
                {
                    Database.Instance.ExecuteNonQuery($@"INSERT INTO dbo.BillInfo (idBill, idFood, quantity)
                                                         VALUES ({billId}, {item.Key}, {item.Value})");
                }

                EnsureBuffetTableAccount(tableId);

                txtOrderNo.Text = FormatBillId(billId);
                _submittedBuffetItems.Clear();
                _draftBuffetItems.Clear();
                BindBuffetCartView();
                CalculateTotal();

                MessageBox.Show("Đã thanh toán buffet, món đã chuyển sang Order Management và trạng thái Ready.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán buffet: " + ex.Message);
            }
        }

        private string EnsureBuffetTableAccount(int tableId)
        {
            string userName = tableId.ToString();
            Database.Instance.ExecuteNonQuery($@"
IF NOT EXISTS (SELECT 1 FROM dbo.BuffetCustomerAccount WHERE userName = N'{userName}')
BEGIN
    INSERT INTO dbo.BuffetCustomerAccount(userName, [password], fullName)
    VALUES (N'{userName}', N'123', N'Table {tableId}')
END
ELSE
BEGIN
    UPDATE dbo.BuffetCustomerAccount
    SET [password] = N'123', fullName = N'Table {tableId}'
    WHERE userName = N'{userName}'
END");
            return userName;
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
