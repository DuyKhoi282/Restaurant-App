using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class frmRatingService : Form
    {
        public frmRatingService()
        {
            InitializeComponent();
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

            foreach (string cauHoi in danhSach)
            {
                // Khởi tạo instance từ class bạn vừa gửi code
                ucQuestion item = new ucQuestion();

                // Gán nội dung thông qua Property NoiDungCauHoi bạn đã viết
                item.NoiDungCauHoi = cauHoi;

                // Căn chỉnh độ rộng để không bị tràn thanh cuộn
                item.Width = flowLayoutPanel1.ClientSize.Width - 25;

                // Thêm vào khung chứa
                flowLayoutPanel1.Controls.Add(item);
            }
        }

        private void frmRatingService_Load(object sender, EventArgs e)
        {
            HienThiCauHoi();
        }
    }
}
