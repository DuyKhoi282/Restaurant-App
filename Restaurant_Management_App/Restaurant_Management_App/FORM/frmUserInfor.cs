using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            DisplayUserAvatar();
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

        private void DisplayUserAvatar()
        {
            try
            {
                string imageName = UserSession.ImagePath;

                if (!string.IsNullOrEmpty(imageName))
                {
                    string folderPath = GetAvaFolderPath();
                    string fullPath = Path.Combine(folderPath, imageName);

                    if (File.Exists(fullPath))
                    {
                        using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            if (picAvaUser_Detail.Image != null) picAvaUser_Detail.Image.Dispose();
                            picAvaUser_Detail.Image = Image.FromStream(fs);
                        }
                        picAvaUser_Detail.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        // LỖI: Có tên file nhưng không tìm thấy file vật lý trong folder
                        MessageBox.Show("Không tìm thấy file ảnh tại: " + fullPath);
                        picAvaUser_Detail.BackColor = Color.Gray;
                    }
                }
                else
                {
                    // LỖI: ImagePath trong database đang bị trống hoặc NULL
                    picAvaUser_Detail.BackColor = Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
                picAvaUser_Detail.BackColor = Color.DimGray;
            }
        }
        private string GetAvaFolderPath()
        {
            // 1. Lấy thư mục gốc của file .exe
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // 2. Thư mục đích mong muốn
            string folderPath = Path.Combine(baseDir, "Images", "Employees");

            // 3. Nếu chạy trong Visual Studio (Debug), baseDir sẽ nằm sâu trong bin/Debug
            // Ta kiểm tra nếu không thấy thư mục Images ở đó, ta mới tìm ngược lên
            if (!Directory.Exists(folderPath))
            {
                // Thử tìm ngược lên 3 cấp (bin -> Debug -> net...)
                DirectoryInfo parent = Directory.GetParent(baseDir);
                if (parent?.Parent?.Parent != null)
                {
                    string projectFolder = parent.Parent.Parent.FullName;
                    string devPath = Path.Combine(projectFolder, "Images", "Employees");
                    if (Directory.Exists(devPath)) return devPath;
                }
            }

            // 4. Nếu không thấy nữa, tự động tạo thư mục ngay tại nơi chạy file .exe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }
    }
}
