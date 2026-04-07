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
        }

        void LoadBill()
        {
            
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

                string query = @"
    SELECT 
        ROW_NUMBER() OVER (ORDER BY f.name) AS numr,
        f.name,
        bi.quantity,
        f.price,
        (bi.quantity * f.price) AS totalprice
    FROM Bill b
    JOIN BillInfo bi ON b.id = bi.idBill
    JOIN Food f ON bi.idFood = f.id
    WHERE b.id = @id";

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

            txtTotalPrice.Text = total.ToString();
            txtDiscount.Text = "0";

            double amountDue = total - 0;
            txtAmountDue.Text = amountDue.ToString();
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
