using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;

namespace Restaurant_Management_App
{ 
    public partial class frmRatingService : Form
    {
        string billId;
        List<ucQuestion> danhSachCauHoi = new List<ucQuestion>();
        public frmRatingService(string idOrder)
        {
            InitializeComponent();
            billId = idOrder;

        }
        string connectionString =@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";
        void LoadDanhSachDanhGia()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
SELECT 
    ROW_NUMBER() OVER (ORDER BY MAX(r.CreatedAt) DESC) AS STT,
    r.BillId,
    tf.name AS TenBan,
    AVG(CAST(r.Star AS FLOAT)) AS TrungBinhSao
FROM RatingService r
JOIN Bill b ON r.BillId = b.id
JOIN tableFood tf ON b.idTable = tf.id
GROUP BY r.BillId, tf.name
ORDER BY STT";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                DataTable table = new DataTable();

                adapter.Fill(table);

                dgvRating.DataSource = table;
            }
        }


        void HienThiDanhGiaDaTraLoi(string billId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
SELECT Question, Star
FROM RatingService
WHERE BillId = @BillId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@BillId", billId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                flpQuestion.Controls.Clear();

                while (reader.Read())
                {
                    ucQuestion uc = new ucQuestion();

                    uc.NoiDungCauHoi = reader["Question"].ToString();

                    uc.SetStar(Convert.ToInt32(reader["Star"]));

                    uc.Width = flpQuestion.ClientSize.Width - 25;

                    flpQuestion.Controls.Add(uc);
                }

                conn.Close();
            }
        }


        private void dgvRating_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string selectedBillId =
                    dgvRating.Rows[e.RowIndex].Cells["BillId"].Value.ToString();

                HienThiDanhGiaDaTraLoi(selectedBillId);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HienThiCauHoi()
        {
            // Danh sách nội dung
            string[] danhSach = {
        "1. Chất lượng món ăn thế nào?",
        "2. Thái độ nhân viên phục vụ?",
        "3. Thời gian chờ đợi lên món?",
        "4. Không gian nhà hàng sạch sẽ không?",
        "5. Giá cả có tương xứng chất lượng?"
    };

            flpQuestion.Controls.Clear(); // Xóa sạch trước khi thêm

            danhSachCauHoi.Clear();

            foreach (string cauHoi in danhSach)
            {
                ucQuestion item = new ucQuestion();

                item.NoiDungCauHoi = cauHoi;

                item.Width = flpQuestion.ClientSize.Width - 25;

                flpQuestion.Controls.Add(item);

                danhSachCauHoi.Add(item);
            }
        }

        bool DaDanhGia()
        {
            string connectionString =
        @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query =
                "SELECT COUNT(*) FROM RatingService WHERE BillId = @BillId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@BillId", billId);

                conn.Open();

                int count = (int)cmd.ExecuteScalar();

                conn.Close();

                return count > 0;
            }
        }

        private void frmRatingService_Load(object sender, EventArgs e)
        {
            label2.Text = $"{billId}";
            if (DaDanhGia())
            {
                LoadDanhSachDanhGia();
                dgvRating.CellClick += dgvRating_CellClick;
            }
            else
                HienThiCauHoi();
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString =
@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (ucQuestion question in danhSachCauHoi)
                {
                    if (question.SoSao == 0)
                    {
                        MessageBox.Show("Vui lòng đánh giá đầy đủ tất cả câu hỏi!");
                        return;
                    }

                    string query = @"
INSERT INTO RatingService(BillId, Question, Star)
VALUES(@BillId, @Question, @Star)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@BillId", billId);
                    cmd.Parameters.AddWithValue("@Question", question.NoiDungCauHoi);
                    cmd.Parameters.AddWithValue("@Star", question.SoSao);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cảm ơn bạn đã đánh giá dịch vụ! ⭐");

            Form parent = this.ParentForm;
            if (parent is frmMain main)
            {
                main.LoadForm(new frmOrderDetails(billId));
            }

        }

        private void btnDelAll_Rating_Click(object sender, EventArgs e)
        {
            // Duyệt qua từng UserControl trong FlowLayoutPanel (ví dụ tên là flpReview)
            foreach (Control ctr in flpQuestion.Controls)
            {
                // Kiểm tra xem control đó có đúng là ucQuestion không
                if (ctr is ucQuestion uc)
                {
                    uc.ClearSelection(); // Gọi hàm xóa của từng UserControl
                }
            }

            // (Tùy chọn) Thông báo sau khi đã xóa xong
            // MessageBox.Show("Đã xóa tất cả lựa chọn đánh giá.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form parent = this.ParentForm;
            if (parent is frmMain main)
            {
                main.LoadForm(new frmOrderDetails(billId));
            }
            this.Hide();
        }


    }
}