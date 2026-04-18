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

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
        UPDATE Bill
        SET 
            status = 1,
            dateCheckOut = GETDATE(),
            payMethod = @payMethod
        WHERE id = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _idOrder);
                    cmd.Parameters.AddWithValue("@payMethod", "Cash"); // hoặc Bank

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // MỞ FORM BILL
                frmBillToPrint f = new frmBillToPrint(_idOrder);
                f.Show();

                MessageBox.Show("Thanh toán thành công!");
            }

        

        void LoadOrderDetails()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

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
            ISNULL(SUM(f.price * bi.quantity),0) AS totalPrice
        FROM Bill b
        LEFT JOIN BillInfo bi ON b.id = bi.idBill
        LEFT JOIN Food f ON bi.idFood = f.id
        WHERE b.id = @id
        GROUP BY 
            b.id, b.idTable, b.dateCheckIn,
            b.customerName, b.payMethod, b.status";

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
            this.Close();
        }


    }
}
