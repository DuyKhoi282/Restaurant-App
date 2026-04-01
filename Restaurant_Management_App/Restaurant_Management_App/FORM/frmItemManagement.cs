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
            SetupDataGridView();
            loadFoodItems();
            addButtonColumns();
            FormatGridColumns();
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
        private void addButtonColumns()//Hàm này dùng để thêm cột chứa nút Edit và Delete vào DataGridView
        {
            if (dgvFood.Columns["colEdit"] == null)
            {
                if (dgvFood.Columns["colEdit"] == null)//Kiểm tra nếu cột Edit chưa tồn tại thì mới thêm vào để tránh việc thêm nhiều cột Edit khi load lại dữ liệu
                {
                    DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                    btnEdit.Name = "colEdit";
                    btnEdit.HeaderText = "";
                    btnEdit.Text = "Edit";
                    btnEdit.UseColumnTextForButtonValue = true;
                    dgvFood.Columns.Add(btnEdit);
                }

                if (dgvFood.Columns["colDelete"] == null)//Kiểm tra nếu cột Delete chưa tồn tại thì mới thêm vào để tránh việc thêm nhiều cột Delete khi load lại dữ liệu
                {
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    btnDelete.Name = "colDelete";
                    btnDelete.HeaderText = "";
                    btnDelete.Text = "Delete";
                    btnDelete.UseColumnTextForButtonValue = true;
                    dgvFood.Columns.Add(btnDelete);
                }
            }
        }
        private void loadFoodItems()//Hàm này dùng để load dữ liệu món ăn vào DataGridView, hiện tại đang là dữ liệu giả để test giao diện
        {
            string query = "SELECT id, name, price FROM Food";

            SqlDataAdapter adapter = new SqlDataAdapter(query, Database.connStr);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dgvFood.DataSource = table;
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
                loadFoodItems();
                return;
            }

            string query = @"SELECT id, name, price FROM Food WHERE name COLLATE Latin1_General_CI_AI LIKE @key";//Hàm giúp không phân biệt chữ hoa chữ thường và dấu khi tìm kiếm món ăn theo tên

            SqlDataAdapter adapter = new SqlDataAdapter(query, Database.connStr);
            adapter.SelectCommand.Parameters.AddWithValue("@key", "%" + keyword + "%");//Thêm tham số tìm kiếm vào câu truy vấn để tránh lỗi SQL Injection

            DataTable table = new DataTable();
            adapter.Fill(table);//Đổ dữ liệu tìm kiếm vào DataTable

            dgvFood.DataSource = table;//Hiển thị kết quả tìm kiếm lên DataGridView

            addButtonColumns();
        }
    }
}

