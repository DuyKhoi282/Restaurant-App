using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class frmBillToPrint : Form
    {
        string orderId;
        double discountPercent = 0;
        PrintDocument printDoc = new PrintDocument();
        public frmBillToPrint(string id)
        {
            InitializeComponent();
            orderId = id;

            printDoc.PrintPage += PrintDoc_PrintPage;

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

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 10;
            int left = 10;

            Font titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font normalFont = new Font("Segoe UI", 10);
            Font totalFont = new Font("Segoe UI", 11, FontStyle.Bold);

            // ===== HEADER =====
            e.Graphics.DrawString("THE CHILLS RESTAURANT", titleFont, Brushes.Black, 60, y);
            y += 30;

            e.Graphics.DrawString("Bill ID: " + orderId, normalFont, Brushes.Black, left, y);
            y += 20;

            e.Graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), normalFont, Brushes.Black, left, y);
            y += 20;

            e.Graphics.DrawString("------------------------------------------", normalFont, Brushes.Black, left, y);
            y += 20;

            // ===== TABLE HEADER =====
            e.Graphics.DrawString("Tên món", headerFont, Brushes.Black, left, y);
            e.Graphics.DrawString("SL", headerFont, Brushes.Black, 180, y);
            e.Graphics.DrawString("Tiền", headerFont, Brushes.Black, 230, y);
            y += 20;

            e.Graphics.DrawString("------------------------------------------", normalFont, Brushes.Black, left, y);
            y += 20;

            // ===== DATA =====
            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                if (row.Cells["FoodName"].Value != null)
                {
                    string name = row.Cells["FoodName"].Value.ToString();
                    string qty = row.Cells["quantity"].Value.ToString();
                    string price = Convert.ToDouble(row.Cells["totalprice"].Value).ToString("N0");

                    // Nếu tên dài → xuống dòng
                    if (name.Length > 20)
                    {
                        name = name.Substring(0, 20) + "...";
                    }

                    e.Graphics.DrawString(name, normalFont, Brushes.Black, left, y);
                    e.Graphics.DrawString(qty, normalFont, Brushes.Black, 180, y);
                    e.Graphics.DrawString(price, normalFont, Brushes.Black, 230, y);

                    y += 20;
                }
            }

            y += 10;
            e.Graphics.DrawString("------------------------------------------", normalFont, Brushes.Black, left, y);
            y += 25;

            // ===== TOTAL =====
            e.Graphics.DrawString("Tổng:", normalFont, Brushes.Black, left, y);
            e.Graphics.DrawString(txtTotalPrice.Text, normalFont, Brushes.Black, 230, y);
            y += 20;

            e.Graphics.DrawString("Giảm:", normalFont, Brushes.Black, left, y);
            e.Graphics.DrawString(txtDiscount.Text, normalFont, Brushes.Black, 230, y);
            y += 25;

            e.Graphics.DrawString("Thanh toán:", totalFont, Brushes.Black, left, y);
            e.Graphics.DrawString(txtAmountDue.Text, totalFont, Brushes.Black, 230, y);
            y += 30;

            e.Graphics.DrawString("------------------------------------------", normalFont, Brushes.Black, left, y);
            y += 25;

            // ===== FOOTER =====
            e.Graphics.DrawString("Cảm ơn quý khách!", normalFont, Brushes.Black, 80, y);
        }

        void LoadBill()
        {
            
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

                string query = @"
    SELECT 
    ROW_NUMBER() OVER (ORDER BY f.name) AS STT,
    f.name AS FoodName,
    SUM(bi.quantity) AS quantity,
    CASE WHEN ISNULL(b.isBuffet, 0) = 1 THEN 0 ELSE f.price END AS price,
    SUM(CASE WHEN ISNULL(b.isBuffet, 0) = 1 THEN 0 ELSE f.price * bi.quantity END) AS TotalPrice,
    MAX(ISNULL(b.discountPercent, 0)) AS discountPercent
FROM Bill b
JOIN BillInfo bi ON b.id = bi.idBill
JOIN Food f ON bi.idFood = f.id
WHERE b.id = @id
GROUP BY f.name, f.price, b.isBuffet";

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

                    if (dt.Rows.Count > 0)
                    {
                        discountPercent = Convert.ToDouble(dt.Rows[0]["discountPercent"]);
                    }


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
            bool isBuffetBill = false;
            int buffetGuestCount = 1;
            double discountPercent = 0;
            bool hasFinalAmount = false;

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
            string query = @"SELECT discountAmount, finalAmount, ISNULL(discountPercent, 0) AS discountPercent,
                                    ISNULL(isBuffet, 0) AS isBuffet, ISNULL(buffetGuestCount, 1) AS buffetGuestCount
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
                            hasFinalAmount = true;
                        }
                        else
                        {
                            amountDue = total - discountAmount;
                        }

                        discountPercent = Convert.ToDouble(reader["discountPercent"]);
                        isBuffetBill = Convert.ToInt32(reader["isBuffet"]) == 1;
                        buffetGuestCount = Convert.ToInt32(reader["buffetGuestCount"]);
                    }
                }
            }

            if (isBuffetBill)
            {
                total = 299000 * Math.Max(1, buffetGuestCount);

                if (!hasFinalAmount)
                {
                    if (discountAmount > 0)
                    {
                        amountDue = total - discountAmount;
                    }
                    else if (discountPercent > 0)
                    {
                        amountDue = total * (1 - discountPercent / 100d);
                    }
                    else
                    {
                        amountDue = total;
                    }
                }

                discountAmount = Math.Max(0, total - amountDue);
            }

            amountDue = Math.Max(0, amountDue);

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

        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDoc;
            preview.Width = 500;
            preview.Height = 600;
            preview.PrintPreviewControl.Zoom = 1.5;
            preview.ShowDialog();

            // Nếu muốn in luôn:
            //printDoc.Print();
        }
    }
}
