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
        "5. Giá cả có tương xứng chất lượng?",
        "6. Quy trình đặt bàn có thuận tiện không?",
        "7. Sự tiếp đón của lễ tân có nhiệt tình không?",
        "8. Thực đơn có đa dạng món ăn không?",
        "9. Độ tươi ngon của nguyên liệu thế nào?",
        "10. Cách trình bày món ăn có hấp dẫn không?",
        "11. Nhà vệ sinh có sạch sẽ không?",
        "12. Ánh sáng và nhiệt độ phòng có thoải mái không?",
        "13. Vị trí bãi đổ xe có thuận tiện không?",
        "14. Bạn có sẵn lòng giới thiệu cho bạn bè không?",
        "15. Khả năng bạn quay lại nhà hàng là bao nhiêu?"
    };
            flpQuestion.Controls.Clear(); // Xóa sạch trước khi thêm
            ucQuestionHeader header = new ucQuestionHeader();
            header.Width = flpQuestion.Width - 25;
            flpQuestion.Controls.Add(header);

           

            danhSachCauHoi.Clear();

            foreach (string cauHoi in danhSach)
            {
                ucQuestion item = new ucQuestion();

                item.NoiDungCauHoi = cauHoi;

                item.Width = flpQuestion.ClientSize.Width - 25;

                flpQuestion.Controls.Add(item);

                danhSachCauHoi.Add(item);
                item.Width = flpQuestion.ClientSize.Width - 25;
                item.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
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
            MessageBox.Show("Đã xóa tất cả lựa chọn đánh giá.");
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