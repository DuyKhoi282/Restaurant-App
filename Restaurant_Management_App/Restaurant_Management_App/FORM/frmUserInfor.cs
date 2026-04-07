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

        private void frmUserInfor_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
        }

        void LoadUserInfo()
        {
            AccountDAL dao = new AccountDAL();

            var user = dao.GetUserById(UserSession.UserId);

            if (user == null)
            {
                MessageBox.Show("Không tìm thấy thông tin user");
                return;
            }

            BindUserToUI(user);
        }

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
