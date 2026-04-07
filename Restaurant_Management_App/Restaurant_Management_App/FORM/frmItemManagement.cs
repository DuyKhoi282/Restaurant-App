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
    public partial class frmItemManagement : Form
    {
        public frmItemManagement()
        {
            InitializeComponent();
        }

        private void frmItemManagement_Load(object sender, EventArgs e)
        {
            dgvFood.CellClick += dgvFood_CellClick;
            dgvFood.CellFormatting += dgvFood_CellFormatting;
            SetupDataGridView();
            LoadFoodItems();
        }
        private void SetupDataGridView()//Hàm này dùng để thiết lập giao diện cho DataGridView, bao gồm màu nền, màu chữ, kiểu đường viền, v.v.
        {

            dgvFood.Dock = DockStyle.Fill;
            dgvFood.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFood.MultiSelect = false;
            dgvFood.ReadOnly = true;
            dgvFood.AllowUserToAddRows = false;
            dgvFood.RowHeadersVisible = false;
            // Header style
            dgvFood.EnableHeadersVisualStyles = false;
            dgvFood.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvFood.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvFood.ColumnHeadersHeight = 40;
            // Row style
            dgvFood.RowTemplate.Height = 35;
            dgvFood.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvFood.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvFood.DefaultCellStyle.SelectionForeColor = Color.White;
            // Border
            dgvFood.BorderStyle = BorderStyle.None;
            dgvFood.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFood.GridColor = Color.LightGray;
        }
        private void AddButtonColumns()//Hàm này dùng để thêm cột chứa nút Edit và Delete vào DataGridView
        {
            if (dgvFood.Columns["colEdit"] == null)
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.Name = "colEdit";
                btnEdit.HeaderText = "";
                btnEdit.Text = "Edit";
                btnEdit.UseColumnTextForButtonValue = true;
                dgvFood.Columns.Add(btnEdit);
            }

            if (dgvFood.Columns["colDelete"] == null)
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "colDelete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "Delete";
                btnDelete.UseColumnTextForButtonValue = true;
                dgvFood.Columns.Add(btnDelete);
            }
        }
        private void LoadFoodItems()//Hàm này dùng để load dữ liệu món ăn vào DataGridView, hiện tại đang là dữ liệu giả để test giao diện
        {
            string query =
@"
SELECT f.id, 
       f.name, 
       c.name AS category, 
       f.price,
       f.status
FROM Food f
JOIN FoodCategory c ON f.idCategory = c.id";

            SqlDataAdapter adapter = new SqlDataAdapter(query, Database.connStr);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dgvFood.DataSource = table;

            AddButtonColumns();
            FormatGridColumns();
        }

        private void FormatGridColumns()//Định dạng lại kích thước của các cột trong DataGridView, bao gồm cột id, cột Edit và cột Delete 
        {
            dgvFood.Columns["id"].Width = 50;

            dgvFood.Columns["colEdit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvFood.Columns["colDelete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgvFood.Columns["colEdit"].Width = 80;
            dgvFood.Columns["colDelete"].Width = 80;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)//Tìm kiếm món ăn theo tên khi người dùng nhập vào ô tìm kiếm
        {
            string keyword = txtSearch.Text.Trim();

            if (keyword == "")//Nếu ô tìm kiếm trống thì load lại tất cả món ăn
            {
                LoadFoodItems();
                return;
            }

            string query = @"
SELECT f.id, 
       f.name, 
       c.name AS category, 
       f.price,
       f.status
FROM Food f
JOIN FoodCategory c ON f.idCategory = c.id
WHERE f.name COLLATE Latin1_General_CI_AI LIKE @key"; ;//Hàm giúp không phân biệt chữ hoa chữ thường và dấu khi tìm kiếm món ăn theo tên

            SqlDataAdapter adapter = new SqlDataAdapter(query, Database.connStr);
            adapter.SelectCommand.Parameters.AddWithValue("@key", "%" + keyword + "%");//Thêm tham số tìm kiếm vào câu truy vấn để tránh lỗi SQL Injection

            DataTable table = new DataTable();
            adapter.Fill(table);//Đổ dữ liệu tìm kiếm vào DataTable

            dgvFood.DataSource = table;//Hiển thị kết quả tìm kiếm lên DataGridView

            AddButtonColumns();
            FormatGridColumns();
            dgvFood.Refresh();
        }

        private void btnAddCate_Click(object sender, EventArgs e)//Mở form thêm món ăn mới khi người dùng click vào nút Add
        {
            frmFoodDetail f = new frmFoodDetail(); // form add

            if (f.ShowDialog() == DialogResult.OK)//
            {
                LoadFoodItems(); // reload lại grid
            }
        }
        private void dgvFood_CellClick(object sender, DataGridViewCellEventArgs e)//Xử lý sự kiện khi click vào các nút Edit/Delete trong DataGridView
        {
            if (e.RowIndex < 0) return;

            string columnName = dgvFood.Columns[e.ColumnIndex].Name;

            int id = Convert.ToInt32(dgvFood.Rows[e.RowIndex].Cells["id"].Value);

            if (columnName == "colEdit")//nút Edit
            {
                frmFoodDetail f = new frmFoodDetail(id);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadFoodItems();
                }
            }

            if (columnName == "colDelete")//nút Delete
            {
                DialogResult result = MessageBox.Show("Xóa món này?", "Confirm", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    DeleteFood(id);
                    LoadFoodItems();
                }
            }
        }
        private void DeleteFood(int id)//Xóa món ăn
        {
            using (SqlConnection conn = new SqlConnection(Database.connStr))
            {
                conn.Open();
                //Check bill chưa thanh toán
                string checkQuery = @"
            SELECT COUNT(*) 
            FROM BillInfo bi
            JOIN Bill b ON bi.idBill = b.id
            WHERE bi.idFood = @id AND b.status = 0";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@id", id);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)//nếu có món này trong hóa đơn chưa thanh toán
                {
                    MessageBox.Show("Món này đang nằm trong hóa đơn chưa thanh toán, không thể xóa!");
                    return;
                }

                // nếu không có thì mới xóa
                string deleteQuery = "DELETE FROM Food WHERE id = @id";
                SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }



        

        private void dgvFood_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)//Chỉnh màu cho status
        {
            if (dgvFood.Columns[e.ColumnIndex].Name == "status")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();

                    if (status == "Available")
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else if (status == "Out of stock")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }
    }
}

