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
    public partial class frmBillToPrint : Form
    {
        string orderId;
        public frmBillToPrint(string id)
        {
            InitializeComponent();
            orderId = id;

            dgvBill.ReadOnly = true;
            txtTotalPrice.ReadOnly = true;
            txtDiscount.ReadOnly = true;
            txtAmountDue.ReadOnly = true;
            txtTotalPrice.BorderStyle = BorderStyle.None;
            txtDiscount.BorderStyle = BorderStyle.None;
            txtAmountDue.BorderStyle = BorderStyle.None;
            dgvBill.MultiSelect = false;
            dgvBill.RowHeadersVisible = false;
        }

        void LoadBill()
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
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", orderId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);
                    conn.Close();

                    dgvBill.DataSource = dt;

                    
                    dgvBill.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvBill.Columns["price"].DefaultCellStyle.Format = "N0";
                    dgvBill.Columns["totalprice"].DefaultCellStyle.Format = "N0";
                    dgvBill.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    dgvBill.BorderStyle = BorderStyle.None;
                    dgvBill.GridColor = Color.White;
                    dgvBill.BackgroundColor = Color.White;
                    dgvBill.EnableHeadersVisualStyles = false;
                    dgvBill.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    dgvBill.DefaultCellStyle.BackColor = Color.White;
                    dgvBill.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvBill.RowTemplate.Height = 30;
                    dgvBill.RowHeadersVisible = false;


            }
            }
            
        

        void CalculateTotal()
        {
            double total = 0;

            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                if (row.Cells["totalprice"].Value != null)
                {
                    total += Convert.ToDouble(row.Cells["totalprice"].Value);
                }
            }

            double discountAmount = 0;
            double amountDue = total;

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
            string query = @"SELECT discountAmount, finalAmount
                             FROM Bill
                             WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", orderId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["discountAmount"] != DBNull.Value)
                        {
                            discountAmount = Convert.ToDouble(reader["discountAmount"]);
                        }

                        if (reader["finalAmount"] != DBNull.Value)
                        {
                            amountDue = Convert.ToDouble(reader["finalAmount"]);
                        }
                        else
                        {
                            amountDue = total - discountAmount;
                        }
                    }
                }
            }

            txtTotalPrice.Text = total.ToString("N0");
            txtDiscount.Text = discountAmount.ToString("N0");
            txtAmountDue.Text = amountDue.ToString("N0");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void frmBillToPrint_Load(object sender, EventArgs e)
        {
            LoadBill();
            CalculateTotal();
        }
    }
}
