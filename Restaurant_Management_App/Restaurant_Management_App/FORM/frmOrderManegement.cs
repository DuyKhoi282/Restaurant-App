using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Restaurant_Management_App
{
    public partial class frmOrderManegement : Form
    {
        public frmOrderManegement()
        {
            InitializeComponent();

        }

        void LoadOrderList()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            ROW_NUMBER() OVER (ORDER BY b.id DESC) AS STT,
            b.id,
            CONVERT(DATE, b.dateCheckIn) AS [date],
            CONVERT(TIME, b.dateCheckIn) AS [time],
            b.idTable,
            SUM(f.price * bi.quantity) AS totalPrice,
            b.customerName,
            CASE 
                WHEN b.status = 0 THEN 'Unpaid'
                ELSE 'Paid'
            END AS status
        FROM Bill b
        LEFT JOIN BillInfo bi ON b.id = bi.idBill
        LEFT JOIN Food f ON bi.idFood = f.id
        GROUP BY 
            b.id,
            b.dateCheckIn,
            b.idTable,
            b.customerName,
            b.status
        ORDER BY b.id DESC";
            

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dtgvOrderMagagement.DataSource = dt; // DataGridView
            dtgvOrderMagagement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvOrderMagagement.DefaultCellStyle.SelectionBackColor = Color.Blue;
            dtgvOrderMagagement.DefaultCellStyle.SelectionForeColor = Color.White;

            }
        }

        private void frmOrderManegement_Load(object sender, EventArgs e)
        {
            LoadOrderList();
            

            
        }

        void LoadBestSeller()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

            string query = @"
    SELECT TOP 10
        f.name AS FoodName,
        SUM(bi.quantity) AS TotalSold,
        SUM(f.price * bi.quantity) AS TotalRevenue
    FROM BillInfo bi
    JOIN Food f ON bi.idFood = f.id
    JOIN Bill b ON b.id = bi.idBill
    WHERE b.status = 1
    GROUP BY f.name
    ORDER BY TotalSold DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    conn.Open();
                    adapter.Fill(dt);

                    dtgvOrderMagagement.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }


        

        private void dtgvOrderMagagement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =  dtgvOrderMagagement.Rows[e.RowIndex];

                string idOrder = row.Cells["id"].Value.ToString();


                // Mở form chi tiết
                frmOrderDetails f = new frmOrderDetails(idOrder);
                f.ShowDialog();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadOrderList();
        }

        private void btnBSeller_Click(object sender, EventArgs e)
        {
            LoadBestSeller();
        }
    }
}
