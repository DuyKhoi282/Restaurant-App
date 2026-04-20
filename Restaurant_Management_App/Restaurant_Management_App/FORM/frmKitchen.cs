using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    public partial class frmKitchen : Form
    {
        public frmKitchen()
        {
            InitializeComponent();

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

            LoadKitchen();
            StartAutoRefresh();
        }

        // ================= LOAD DANH SÁCH ĐƠN =================
        void LoadKitchen()
        {
            string query = @"
                SELECT id AS [Order ID], customerName AS [Customer], caseName AS [Type], kitchenStatus AS [Status]
                FROM Bill
                WHERE status = 0
                ORDER BY dateCheckIn DESC";

            dgvKitchen.DataSource = Database.Instance.ExecuteQuery(query);

            PaintStatus();
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
    }
}