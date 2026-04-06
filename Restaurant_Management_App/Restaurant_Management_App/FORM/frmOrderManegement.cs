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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void LoadOrderList()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_GetOrderList", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                dtgvOrderMagagement.DataSource = dt; // DataGridView
                
            }
        }

        private void AddCheckButtonColumn()
        {
            // Kiểm tra nếu cột đã tồn tại thì không thêm nữa
            if (dtgvOrderMagagement.Columns["btnCheck"] == null)
            {
                DataGridViewButtonColumn btnCheck = new DataGridViewButtonColumn();
                btnCheck.Name = "btnCheck";
                btnCheck.HeaderText = "ACTION"; // Tiêu đề cột
                btnCheck.Text = "Check";
                btnCheck.UseColumnTextForButtonValue = true; // Hiển thị chữ 'Check' trên tất cả các hàng

                // Tùy chỉnh giao diện nút một chút cho chuyên nghiệp
                btnCheck.FlatStyle = FlatStyle.Flat;
                btnCheck.DefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
                btnCheck.DefaultCellStyle.ForeColor = Color.Black;

                dtgvOrderMagagement.Columns.Add(btnCheck);
            }
        }

        private void frmOrderManegement_Load(object sender, EventArgs e)
        {
            LoadOrderList();
            AddCheckButtonColumn();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LoadOrderList();
        }
    }
}
