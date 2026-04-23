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
    public partial class frmOrderDetails : Form
    {
        string _idOrder;
        string currentRole;

        public frmOrderDetails(string idOrder)
        {
            InitializeComponent();
            _idOrder = idOrder;

            dgvFoodDetails.ReadOnly = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (txtStatus.Text == "Paid")
            {
                // Đã thanh toán → chỉ mở lại hóa đơn
                frmBillToPrint f = new frmBillToPrint(_idOrder);
                f.Show();
                return;
            }

            if (txtStatusOrders.Text != "Ready")
            {
                MessageBox.Show("Đơn hàng chưa hoàn thành, không thể thanh toán");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Xác nhận thanh toán đơn này?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                PayOrder();
                LoadOrderDetails(); // reload lại để update status
            }
        }

        

        void PayOrder()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
            int billId = Convert.ToInt32(_idOrder);
            LoyaltyService loyaltyService = new LoyaltyService();

            decimal originalAmount = loyaltyService.GetBillTotal(billId);
            LoyaltyService.PromotionMatch promo = SelectPromotionForPayment(loyaltyService, txtCustomerName.Text);
            if (promo == null && !string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                // null nghĩa là user chọn "Không áp dụng" hoặc không đủ điều kiện
            }

            double discountPercent = promo == null ? 0 : promo.DiscountPercent;
            decimal discountAmount = originalAmount * (decimal)(discountPercent / 100d);
            decimal finalAmount = originalAmount - discountAmount;
            int pointsUsed = promo == null ? 0 : promo.MinPoints;
            int? promotionId = promo == null ? (int?)null : promo.PromotionId;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        UPDATE Bill
        SET 
            status = 1,
            dateCheckOut = GETDATE(),
            payMethod = @payMethod,
            discountPercent = @discountPercent,
            discountAmount = @discountAmount,
            finalAmount = @finalAmount,
            idPromotion = @idPromotion
        WHERE id = @id AND status = 0";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idOrder);
                cmd.Parameters.AddWithValue("@payMethod", "Cash"); // hoặc Bank
                cmd.Parameters.AddWithValue("@discountPercent", discountPercent);
                cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
                cmd.Parameters.AddWithValue("@finalAmount", finalAmount);
                cmd.Parameters.AddWithValue("@idPromotion", (object)promotionId ?? DBNull.Value);

                conn.Open();
                int affected = cmd.ExecuteNonQuery();
                conn.Close();

                if (affected > 0)
                {
                    loyaltyService.ApplyPaymentAndPoints(billId, txtCustomerName.Text, finalAmount, pointsUsed, promotionId);
                }
            }

            // MỞ FORM BILL
            frmBillToPrint f = new frmBillToPrint(_idOrder);
            f.Show();

            if (promo == null)
            {
                MessageBox.Show($"Thanh toán thành công!\nTổng bill: {originalAmount:N0} VNĐ");
            }
            else
            {
                MessageBox.Show($"Thanh toán thành công!\nÁp dụng CTKM: {promo.PromotionName} (-{discountPercent}%)\nGiảm: {discountAmount:N0} VNĐ\nCần thanh toán: {finalAmount:N0} VNĐ");
            }
        }

        private LoyaltyService.PromotionMatch SelectPromotionForPayment(LoyaltyService loyaltyService, string customerName)
        {
            DataTable promos = loyaltyService.GetEligiblePromotionsForCustomer(customerName);
            if (promos.Rows.Count == 0)
            {
                return null;
            }

            LoyaltyService.PromotionMatch autoPromo = loyaltyService.GetBestPromotionForCustomer(customerName);

            using (Form picker = new Form())
            {
                picker.Text = "Chọn khuyến mãi";
                picker.StartPosition = FormStartPosition.CenterParent;
                picker.FormBorderStyle = FormBorderStyle.FixedDialog;
                picker.MinimizeBox = false;
                picker.MaximizeBox = false;
                picker.ClientSize = new Size(460, 150);

                Label lbl = new Label
                {
                    Left = 12,
                    Top = 20,
                    Width = 420,
                    Text = "Chọn chương trình khuyến mãi khi thanh toán:"
                };

                ComboBox cbPromo = new ComboBox
                {
                    Left = 12,
                    Top = 50,
                    Width = 430,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cbPromo.Items.Add("Tự động chọn tốt nhất");
                cbPromo.Items.Add("Không áp dụng khuyến mãi");

                foreach (DataRow row in promos.Rows)
                {
                    cbPromo.Items.Add($"{row["promoName"]} (-{row["discountPercent"]}%, cần {row["minPoints"]} điểm) [ID:{row["id"]}]");
                }
                cbPromo.SelectedIndex = 0;

                Button btnOk = new Button
                {
                    Text = "OK",
                    Left = 272,
                    Width = 80,
                    Top = 95,
                    DialogResult = DialogResult.OK
                };
                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    Left = 362,
                    Width = 80,
                    Top = 95,
                    DialogResult = DialogResult.Cancel
                };

                picker.Controls.Add(lbl);
                picker.Controls.Add(cbPromo);
                picker.Controls.Add(btnOk);
                picker.Controls.Add(btnCancel);
                picker.AcceptButton = btnOk;
                picker.CancelButton = btnCancel;

                DialogResult result = picker.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    return autoPromo;
                }

                if (cbPromo.SelectedIndex == 0)
                {
                    return autoPromo;
                }
                if (cbPromo.SelectedIndex == 1)
                {
                    return null;
                }

                int promoRowIndex = cbPromo.SelectedIndex - 2;
                DataRow selected = promos.Rows[promoRowIndex];
                return new LoyaltyService.PromotionMatch
                {
                    PromotionId = Convert.ToInt32(selected["id"]),
                    PromotionName = selected["promoName"].ToString(),
                    MinPoints = Convert.ToInt32(selected["minPoints"]),
                    DiscountPercent = Convert.ToDouble(selected["discountPercent"])
                };
            }
        }

        

        void LoadOrderDetails()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
            // Đảm bảo schema loyalty/discount đã được tạo trước khi query các cột mới trên Bill
            LoyaltyService loyaltyService = new LoyaltyService();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            b.id ,
            b.idTable,
            
            CONVERT(DATE, b.dateCheckIn) AS [date],
            CONVERT(TIME, b.dateCheckIn) AS [time],
            b.customerName,
            b.payMethod,
            CASE 
                WHEN b.status = 0 THEN 'Unpaid'
                ELSE 'Paid'
            END AS status,
            CASE 
                WHEN b.finalAmount IS NULL THEN 
                    CASE WHEN ISNULL(b.isBuffet,0) = 1 THEN 299000
                         ELSE ISNULL(SUM(f.price * bi.quantity),0)
                    END
                ELSE b.finalAmount
            END AS totalPrice,
            ISNULL(b.kitchenStatus, 'Pending') AS kitchenStatus
        FROM Bill b
        LEFT JOIN BillInfo bi ON b.id = bi.idBill
        LEFT JOIN Food f ON bi.idFood = f.id
        WHERE b.id = @id
        GROUP BY 
            b.id, b.idTable, b.dateCheckIn,
            b.customerName, b.payMethod, b.status, b.kitchenStatus, b.finalAmount";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idOrder);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                dgvFoodDetails.BackgroundColor = Color.White;
                dgvFoodDetails.BorderStyle = BorderStyle.None;
                dgvFoodDetails.RowHeadersVisible = false;
                dgvFoodDetails.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgvFoodDetails.BorderStyle = BorderStyle.None;
                dgvFoodDetails.GridColor = Color.White;

                if (reader.Read())
                {

                    txtIdOrder.Text = reader["id"].ToString();
                    txtIdTable.Text = reader["idTable"].ToString(); 
                    gbxListDetails.Text = reader["id"].ToString();
                    txtDate.Text = reader["date"].ToString();
                    txtTime.Text = reader["time"].ToString();
                    txtCustomerName.Text = reader["customerName"].ToString();
                    txtPayMethod.Text = reader["payMethod"].ToString();
                    txtStatus.Text = reader["status"].ToString();
                    txtTotalPrice.Text = reader["totalPrice"].ToString();

                    string kitchenStatus = reader["kitchenStatus"].ToString();
                    txtStatusOrders.Text = kitchenStatus; // bạn cần thêm textbox

                    if (txtStatus.Text == "Paid")
                    {
                        btnPay.Text = "View Bill";
                    }
                    else
                    {
                        btnPay.Text = "Pay";
                    }
                }

                conn.Close();
            }

            // Load danh sách món
            LoadFoodList();
            btnPay.Enabled = (txtStatusOrders.Text == "Ready" || txtStatus.Text == "Paid");
        }

        void LoadFoodList()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

            string query = @"
    SELECT 
    ROW_NUMBER() OVER (ORDER BY f.name) AS STT,
    f.name AS FoodName,
    SUM(bi.quantity) AS quantity,
    f.price,
    SUM(f.price * bi.quantity) AS TotalPrice
FROM Bill b
JOIN BillInfo bi ON b.id = bi.idBill
JOIN Food f ON bi.idFood = f.id
WHERE b.id = @id
GROUP BY f.name, f.price";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _idOrder);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFoodDetails.DataSource = dt;

                dgvFoodDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Đẹp hơn
                dgvFoodDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvFoodDetails.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dgvFoodDetails.RowTemplate.Height = 30;

                // Không cho resize lung tung
                dgvFoodDetails.AllowUserToResizeRows = false;
                dgvFoodDetails.AllowUserToResizeColumns = false;

                // Full chiều cao
                dgvFoodDetails.Dock = DockStyle.Fill;

                dgvFoodDetails.Columns["STT"].FillWeight = 10;
                dgvFoodDetails.Columns["FoodName"].FillWeight = 40;
                dgvFoodDetails.Columns["quantity"].FillWeight = 15;
                dgvFoodDetails.Columns["price"].FillWeight = 15;
                dgvFoodDetails.Columns["TotalPrice"].FillWeight = 20;

                dgvFoodDetails.Columns["price"].DefaultCellStyle.Format = "N0";
                dgvFoodDetails.Columns["TotalPrice"].DefaultCellStyle.Format = "N0";

                dgvFoodDetails.AllowUserToAddRows = false;
            }

            
            }

        

        private void frmOrderDetails_Load(object sender, EventArgs e)
        {
            LoadOrderDetails();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form parent = this.ParentForm;
            if (parent is frmMain main)
            {
                main.LoadForm(new frmOrderManegement(currentRole));
            }
        }


    }
}
