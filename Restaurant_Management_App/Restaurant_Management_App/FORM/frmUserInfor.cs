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
    public partial class frmUserInfor : Form
    {
        public frmUserInfor()
        {
            InitializeComponent();
        }


        // Hàm này sẽ được gọi khi form được tải lên,
        // nó sẽ gọi hàm LoadUserInfo để lấy thông tin người dùng và hiển thị lên giao diện
        private void frmUserInfor_Load(object sender, EventArgs e) 
        {
            LoadUserInfo();
        }


        // Hàm này sẽ lấy thông tin chi tiết của người dùng đã đăng nhập từ database thông qua AccountDAL
        void LoadUserInfo()
        {
            AccountDAL dao = new AccountDAL();

            var user = dao.GetUserById(UserSession.UserId);

            if (user == null)
            {
                MessageBox.Show("Không tìm thấy thông tin user");
                return;
            }

            BindUserToUI(user); //Hiển thị thông tin người dùng lên giao diện
        }


        //gán thông tin người dùng từ đối tượng AccountDTO vào giao diện để hiển thị chi tiết thông tin người dùng
        void BindUserToUI(AccountDTO user)
        {
            lblUserId_Detail.Text = user.UserId;
            lblFullname_Detail.Text = user.FullName;
            lblRole_Detail.Text = user.RoleName;
            lblPhone_Detail.Text = user.Phone;
            lblEmail_Detail.Text = user.Email;
            lblAddress_Detail.Text = user.Address;
            lblBirthday_Detail.Text = user.Birthday;
        }

        private void btnBack_InfoUser_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
