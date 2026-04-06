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

namespace Restaurant_Management_App.FORM
{
    public partial class frmFoodDetail : Form
    {
        private int foodId = 0; // Biến này sẽ lưu trữ ID của món ăn đang được hiển thị chi tiết
        public frmFoodDetail(int id)
        {
            InitializeComponent();
            foodId = id;// Khi khởi tạo form, truyền vào ID của món ăn để hiển thị chi tiết
        }
        public frmFoodDetail()
        {
            InitializeComponent();
        }

        private void frmFoodDetail_Load(object sender, EventArgs e)
        {
            InitForm();
        }
        private void InitForm()//Khởi tạo dữ liệu ban đầu cho form
        {
            LoadCategory();
            LoadStatus();
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;//Chỉ cho phép chọn trạng thái từ danh sách, không cho nhập tự do

            if (foodId > 0)//Nếu có ID -> thì là edit món ăn, ngược lại là thêm món ăn mới
            {
                LoadFoodById(foodId);
            }
        }
        private void LoadCategory()//Hàm này dùng để load danh sách các loại món ăn vào ComboBox
        {
            string query = "SELECT id, name FROM FoodCategory";

            using (SqlConnection conn = new SqlConnection(Database.connStr))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);// Điền dữ liệu từ DB vào DataTable

                // Thêm một dòng đặc biệt để đại diện cho việc thêm mới loại món ăn
                DataRow newRow = table.NewRow();
                newRow["id"] = 0;
                newRow["name"] = "+ Add new...";
                table.Rows.InsertAt(newRow, 0);

                cmbCategory.DataSource = table;// Gán DB vào ComboBox
                cmbCategory.DisplayMember = "name";// Hiển thị tên loại món ăn
                cmbCategory.ValueMember = "id";// Giá trị thực sự dùng (ID) khi save
            }
        }
        private void LoadStatus()//Khởi tạo trạng thái món ăn (cố định)
        {
            cmbStatus.Items.Clear();//tránh bị add trùng khi load lại form

            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Out of stock");
        }
        private void LoadFoodById(int id)//Load dữ liệu từ DB khi chỉnh sửa
        {
            string query = "SELECT * FROM Food WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(Database.connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())//Nếu tìm thấy món ăn với ID đã cho
                    {
                        //Đổ dữ liệu vào form
                        txtName.Text = reader["name"].ToString();
                        txtPrice.Text = reader["price"].ToString();
                        //Chọn đúng category & status trong ComboBox
                        cmbCategory.SelectedValue = reader["idCategory"];//Chọn loại món ăn trong ComboBox
                        string status = reader["status"].ToString();

                        if (status == "Available")
                        {
                            cmbStatus.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbStatus.SelectedIndex = 1;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())//Kiểm tra dữ liệu đầu vào trước khi lưu
            {
                return;
            }

            if (foodId == 0)
            {
                InsertFood();//thêm mới
            }
            else
            {
                UpdateFood();//cập nhật món ăn
            }

            //Hiển thị thông báo lưu thành công và đóng form
            this.DialogResult = MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK ? DialogResult.OK : DialogResult.None;
            this.Close();
        }
        private bool ValidateInput()//Kiểm tra dữ liệu nhập vào
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại món ăn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái món ăn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;//Nếu tất cả dữ liệu hợp lệ
        }
        private void InsertFood()//Thêm mới món ăn vào DB
        {
            string query = @"INSERT INTO Food(name, idCategory, price, status)
                     VALUES(@name, @category, @price, @status)";
            using (SqlConnection conn = new SqlConnection(Database.connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                BindParams(cmd);//Gán giá trị từ form

                cmd.ExecuteNonQuery();//Thực thi câu lệnh SQL
            }
        }
        private void UpdateFood()//Cập nhật món ăn trong DB
        {
            string query = @"UPDATE Food
                     SET name=@name, idCategory=@category, price=@price, status=@status
                     WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(Database.connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                BindParams(cmd);//Gán giá trị từ form
                cmd.Parameters.AddWithValue("@id", foodId);//Thêm tham số ID để xác định món ăn cần cập nhật
                cmd.ExecuteNonQuery();//Thực thi câu lệnh SQL
            }
        }
        private void BindParams(SqlCommand cmd)//Gán dữ liệu từ form vào câu SQL (dùng chung cho Insert/Update)
        {
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());

            //Lấy id của category đã chọn
            cmd.Parameters.AddWithValue("@category", (int)cmbCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@price", float.Parse(txtPrice.Text));
            cmd.Parameters.AddWithValue("@status",cmbStatus.SelectedIndex == 0 ? "Available" : "Out of stock");
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)//Hàm này sẽ được gọi khi người dùng chọn một loại món ăn khác trong ComboBox
        {
            if (cmbCategory.SelectedValue != null &&
        cmbCategory.SelectedValue.ToString() == "0")
            {
                string name = ShowInputDialog("Thêm Category", "Nhập tên loại:");

                if (!string.IsNullOrWhiteSpace(name))
                {
                    InsertCategory(name);

                    MessageBox.Show("Thêm thành công!");

                    LoadCategory();

                    cmbCategory.SelectedIndex = cmbCategory.Items.Count - 1;
                }
            }
        }
        private void InsertCategory(string name)//Thêm loại món ăn mới vào DB
        {
            string query = "INSERT INTO FoodCategory(name) VALUES(@name)";

            using (SqlConnection conn = new SqlConnection(Database.connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
        }
        private string ShowInputDialog(string title, string prompt)//Hàm này sẽ hiển thị một hộp thoại để người dùng nhập tên loại món ăn mới
        {
            Form form = new Form();
            form.Text = title;
            form.Width = 300;
            form.Height = 150;
            form.StartPosition = FormStartPosition.CenterParent;

            Label label = new Label() { Left = 10, Top = 10, Text = prompt, Width = 260 };
            TextBox textBox = new TextBox() { Left = 10, Top = 40, Width = 260 };

            Button buttonOk = new Button() { Text = "OK", Left = 110, Width = 70, Top = 70 };
            buttonOk.DialogResult = DialogResult.OK;

            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(buttonOk);

            form.AcceptButton = buttonOk;

            return form.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
    
}
