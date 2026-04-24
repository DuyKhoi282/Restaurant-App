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

            flowLayoutPanel1.Controls.Clear(); // Xóa sạch trước khi thêm

            danhSachCauHoi.Clear();

            foreach (string cauHoi in danhSach)
            {
                ucQuestion item = new ucQuestion();

                item.NoiDungCauHoi = cauHoi;

                item.Width = flowLayoutPanel1.ClientSize.Width - 25;

                flowLayoutPanel1.Controls.Add(item);

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
            if (DaDanhGia())
            {
                MessageBox.Show("Hóa đơn này đã được đánh giá rồi!");
                Form parent = this.ParentForm;
                if (parent is frmMain main)
                {
                    main.LoadForm(new frmOrderDetails(billId));
                }

            }else
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
    }
}