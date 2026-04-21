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
        private readonly List<FoodMenuItem> _foods = new List<FoodMenuItem>();

        public frmCreateOrder()
        {
            InitializeComponent();

            Load += FrmOrder_Load;
            btnClear.Click += BtnClear_Click;
            numDiscount.ValueChanged += (s, e) => CalculateTotal();
            cbTable.SelectedIndexChanged += cbTable_SelectedIndexChanged;
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            LoadTables();
            LoadCategoryList();
            LoadFoodList();

            if (cbCase.Items.Count == 0) cbCase.Items.AddRange(new[] { "Eat in", "Take away", "Delivery" });
            if (cbPayMethod.Items.Count == 0) cbPayMethod.Items.AddRange(new[] { "Cash", "Credit Card", "E-Wallet" });

            cbCase.SelectedIndex = 0;
            cbPayMethod.SelectedIndex = 0;
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
                    string queryInsertBill = $@"INSERT INTO dbo.Bill (idTable, customerName, caseName, payMethod, status)
                                               VALUES ({tableId}, N'{EscapeSqlValue(txtCustomerName.Text)}', N'{EscapeSqlValue(cbCase.Text)}', N'{EscapeSqlValue(cbPayMethod.Text)}', 0);
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

                object priceObj = row.Cells["colPrice"].Value;
                object qtyObj = row.Cells["colQty"].Value;

                double price = priceObj == null || priceObj == DBNull.Value ? 0 : Convert.ToDouble(priceObj);
                int qty = qtyObj == null || qtyObj == DBNull.Value ? 0 : Convert.ToInt32(qtyObj);
                subtotal += price * qty;
            }

            double discountPercent = (double)numDiscount.Value;
            double tax = subtotal * 0.07;
            double total = (subtotal + tax) * (1 - discountPercent / 100);

            lblSubtotalValue.Text = subtotal.ToString("N0") + " VNĐ";
            lblTaxValue.Text = tax.ToString("N0") + " VNĐ";
            lblTotalValue.Text = total.ToString("N0") + " VNĐ";
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

            try
            {
                object foodIdObj = dgvCart.Rows[e.RowIndex].Cells["FoodID"].Value;
                if (foodIdObj == null) return;

                int foodId = Convert.ToInt32(foodIdObj);
                int billId = int.Parse(txtOrderNo.Text.Replace("HD", ""));

                if (MessageBox.Show("Bạn muốn xóa món này khỏi đơn hàng?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = Database.Instance.ExecuteNonQuery($"DELETE dbo.BillInfo WHERE idBill = {billId} AND idFood = {foodId}");
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
                                           payMethod = N'{EscapeSqlValue(cbPayMethod.Text)}'
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

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOpenBillBySelectedTable();
        }

        private void LoadOpenBillBySelectedTable()
        {
            if (cbTable.SelectedValue == null) return;

            int tableId;
            if (!int.TryParse(cbTable.SelectedValue.ToString(), out tableId)) return;

            string query = $@"SELECT TOP 1 id, customerName, caseName, payMethod
                              FROM Bill
                              WHERE idTable = {tableId} AND status = 0
                              ORDER BY id DESC";
            DataTable dt = Database.Instance.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
            {
                txtOrderNo.Text = "";
                txtCustomerName.Text = "";
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

            LoadBillDetails(billId);
        }

        private int GetOpenBillIdByTable(int tableId)
        {
            object result = Database.Instance.ExecuteScalar($@"SELECT TOP 1 id
                                                               FROM Bill
                                                               WHERE idTable = {tableId} AND status = 0
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
