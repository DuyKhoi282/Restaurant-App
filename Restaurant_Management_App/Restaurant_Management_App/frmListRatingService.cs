using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class frmListRatingService : Form
    {
        public frmListRatingService()
        {
            InitializeComponent();
        }

        string connectionString =
@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

        private void frmListRatingService_Load(object sender, EventArgs e)
        {
            LoadDanhSachDanhGia();
        }

        void LoadDanhSachDanhGia()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
SELECT 
    BillId,
    COUNT(*) AS SoCauHoi,
    AVG(CAST(Star AS FLOAT)) AS TrungBinhSao,
    MAX(CreatedAt) AS NgayDanhGia
FROM RatingService
GROUP BY BillId
ORDER BY NgayDanhGia DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                DataTable table = new DataTable();

                adapter.Fill(table);

                dgvRating.DataSource = table;
            }
        }

    }
}