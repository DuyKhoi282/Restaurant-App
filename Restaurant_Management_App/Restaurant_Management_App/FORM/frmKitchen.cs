using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    public partial class frmKitchen : Form
    {
        string currentRole;

        public frmKitchen(String role)
        {
            InitializeComponent();

            currentRole = role;
            this.Load += FrmKitchen_Load;
            dgvKitchen.CellClick += DgvKitchen_CellClick;
            btnCooking.Click += BtnCooking_Click;
            btnReady.Click += BtnReady_Click;
        }

        private void FrmKitchen_Load(object sender, EventArgs e)
        {
            dgvKitchen.Dock = DockStyle.Fill;
            dgvDetail.Dock = DockStyle.Fill;

            dgvKitchen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvKitchen.BackgroundColor = Color.White;
            dgvDetail.BackgroundColor = Color.White;

            dgvKitchen.BorderStyle = BorderStyle.None;
            dgvDetail.BorderStyle = BorderStyle.None;

            dgvKitchen.AllowUserToAddRows = false;
            dgvDetail.AllowUserToAddRows = false;

            dgvKitchen.RowHeadersVisible = false;
            dgvDetail.RowHeadersVisible = false;

            this.Font = new Font("Segoe UI", 15F);

            // TITLE
            lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);

            // STATUS
            lblPending.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblCooking.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblReady.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            // GRID
            StyleGrid(dgvKitchen);
            StyleGrid(dgvDetail);

            // BUTTON
            StyleButton(btnCooking, Color.FromArgb(220, 53, 69));
            StyleButton(btnReady, Color.FromArgb(200, 35, 51));
            StyleButton(btnBack, Color.Gray);

            LoadKitchen();
            StartAutoRefresh();

            if (currentRole == "Chef")
            {
                btnBack.Visible = false;
            }
        }

        private void StyleButton(Button btn, Color color)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btn.Height = 60;
            btn.Width = 180;
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;

            // 🔥 QUAN TRỌNG: Header không bị đè
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 55; // >= row height

            // FONT
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 15F);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            // MÀU
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(158, 27, 27);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // ROW
            dgv.RowTemplate.Height = 50;

            // 🔥 FIX khoảng cách
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // tránh chữ bị dính
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 15F);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            dgv.RowTemplate.Height = 50;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(158, 27, 27);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        // ================= LOAD DANH SÁCH ĐƠN =================
        void LoadKitchen()
        {
            int selectedId = GetSelectedBillId();

            string query = @"
                SELECT id AS [Order ID], customerName AS [Customer], caseName AS [Type], kitchenStatus AS [Status]
                FROM Bill
                WHERE status = 0
                  AND ISNULL(kitchenStatus, N'Pending') <> N'Draft'
                ORDER BY dateCheckIn DESC";

            dgvKitchen.DataSource = Database.Instance.ExecuteQuery(query);

            PaintStatus();

            if (selectedId > 0)
            {
                foreach (DataGridViewRow row in dgvKitchen.Rows)
                {
                    if (Convert.ToInt32(row.Cells["Order ID"].Value) == selectedId)
                    {
                        row.Selected = true;
                        dgvKitchen.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }

        // ================= LOAD CHI TIẾT =================
        void LoadDetail(int billId)
        {
            string query = @"
                SELECT f.name AS [Food], bi.quantity AS [Qty]
                FROM BillInfo bi
                JOIN Food f ON bi.idFood = f.id
                WHERE bi.idBill = " + billId;

            dgvDetail.DataSource = Database.Instance.ExecuteQuery(query);
        }

        // ================= CLICK CHỌN ĐƠN =================
        private void DgvKitchen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int billId = Convert.ToInt32(dgvKitchen.Rows[e.RowIndex].Cells["Order ID"].Value);
            LoadDetail(billId);
        }

        // ================= LẤY BILL ID =================
        int GetSelectedBillId()
        {
            if (dgvKitchen.CurrentRow == null) return -1;

            return Convert.ToInt32(dgvKitchen.CurrentRow.Cells["Order ID"].Value);
        }

        // ================= NÚT ĐANG LÀM =================
        private void BtnCooking_Click(object sender, EventArgs e)
        {
            int billId = GetSelectedBillId();
            if (billId < 0) return;

            string query = $"UPDATE Bill SET kitchenStatus = N'Cooking' WHERE id = {billId}";
            Database.Instance.ExecuteNonQuery(query);

            LoadKitchen();
        }

        // ================= NÚT HOÀN THÀNH =================
        private void BtnReady_Click(object sender, EventArgs e)
        {
            int billId = GetSelectedBillId();
            if (billId < 0) return;

            string query = $"UPDATE Bill SET kitchenStatus = N'Ready' WHERE id = {billId}";
            Database.Instance.ExecuteNonQuery(query);

            LoadKitchen();
        }

        // ================= TÔ MÀU =================
        void PaintStatus()
        {
            foreach (DataGridViewRow row in dgvKitchen.Rows)
            {
                if (row.Cells["Status"].Value == null) continue;

                string status = row.Cells["Status"].Value.ToString();

                if (status == "Pending")
                    row.DefaultCellStyle.BackColor = Color.LightYellow;

                if (status == "Cooking")
                    row.DefaultCellStyle.BackColor = Color.Orange;

                if (status == "Ready")
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        // ================= AUTO REFRESH =================
        void StartAutoRefresh()
        {
            Timer t = new Timer();
            t.Interval = 3000; // 3s
            t.Tick += (s, e) => LoadKitchen();
            t.Start();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form parent = this.ParentForm;
            if (parent is frmMain main)
            {
                main.LoadForm(new frmOrderManegement(currentRole));
            }
        }
    }
}
