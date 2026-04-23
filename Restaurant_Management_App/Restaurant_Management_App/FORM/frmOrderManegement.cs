using Restaurant_Management_App.FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurant_Management_App
{
    public partial class frmOrderManegement : Form
    {
        string currentRole;
        Timer autoTimer;

        public frmOrderManegement(string role)
        {
            InitializeComponent();
            currentRole = role;
        }

        // ================= LOAD =================
        private void frmOrderManegement_Load(object sender, EventArgs e)
        {
            LoadOrderList();
            InitAutoRefresh();

            if (currentRole == "Staff")
                btnStatusOrder.Visible = false;

            // 🎨 Style button đỏ đô
            StyleButton(btnReload, Color.FromArgb(158, 27, 27));
            StyleButton(btnStatusOrder, Color.FromArgb(158, 27, 27));

            StyleDataGridView();
        }

        // ================= STYLE BUTTON =================
        private void StyleButton(Button btn, Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(198, 40, 40);
            btn.MouseLeave += (s, e) => btn.BackColor = color;
        }

        // ================= STYLE GRID =================
        void StyleDataGridView()
        {
            dtgvOrderMagagement.EnableHeadersVisualStyles = false;

            dtgvOrderMagagement.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(158, 27, 27);
            dtgvOrderMagagement.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvOrderMagagement.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            dtgvOrderMagagement.ColumnHeadersHeight = 55;

            dtgvOrderMagagement.DefaultCellStyle.Font = new Font("Segoe UI", 14F);
            dtgvOrderMagagement.DefaultCellStyle.SelectionBackColor = Color.FromArgb(158, 27, 27);
            dtgvOrderMagagement.DefaultCellStyle.SelectionForeColor = Color.White;

            dtgvOrderMagagement.GridColor = Color.FromArgb(158, 27, 27);
            dtgvOrderMagagement.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        // ================= LOAD DATA =================
        void LoadOrderList()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY b.id DESC) AS STT,
                    b.id,
                    CONVERT(DATE, b.dateCheckIn) AS [date],
                    CONVERT(TIME, b.dateCheckIn) AS [time],
                    b.idTable,
                    SUM(f.price * bi.quantity) AS totalPrice,
                    b.customerName,
                    CASE WHEN b.status = 0 THEN 'Unpaid' ELSE 'Paid' END AS status,
                    ISNULL(b.kitchenStatus, 'Pending') AS [Kitchen Status]
                FROM Bill b
                LEFT JOIN BillInfo bi ON b.id = bi.idBill
                LEFT JOIN Food f ON bi.idFood = f.id
                WHERE b.status = 1 
                   OR (b.status = 0 AND ISNULL(b.kitchenStatus, 'Pending') <> 'Draft')
                GROUP BY b.id, b.dateCheckIn, b.idTable, b.customerName, b.status, b.kitchenStatus
                ORDER BY b.id DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dtgvOrderMagagement.DataSource = dt;

                PaintKitchenStatus(); // chỉ tô ô status

                dtgvOrderMagagement.Columns["id"].HeaderText = "Mã đơn";
                dtgvOrderMagagement.Columns["date"].HeaderText = "Ngày";
                dtgvOrderMagagement.Columns["time"].HeaderText = "Giờ";
                dtgvOrderMagagement.Columns["idTable"].HeaderText = "Số bàn";
                dtgvOrderMagagement.Columns["CustomerName"].HeaderText = "Tên Khách Hàng";
                dtgvOrderMagagement.Columns["status"].HeaderText = "Trạng thái thanh toán";
                dtgvOrderMagagement.Columns["Kitchen Status"].HeaderText = "Trang thái món";
            }
        }

        // ================= AUTO REFRESH =================
        void InitAutoRefresh()
        {
            autoTimer = new Timer();
            autoTimer.Interval = 5000;
            autoTimer.Tick += (s, e) => LoadOrderList();
            autoTimer.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            autoTimer?.Stop();
            autoTimer?.Dispose();
            base.OnFormClosed(e);
        }

        // ================= TÔ MÀU STATUS =================
        void PaintKitchenStatus()
        {
            foreach (DataGridViewRow row in dtgvOrderMagagement.Rows)
            {
                if (row.Cells["Kitchen Status"].Value == null) continue;

                string status = row.Cells["Kitchen Status"].Value.ToString();

                DataGridViewCell cell = row.Cells["Kitchen Status"];

                if (status == "Pending")
                {
                    cell.Style.BackColor = Color.FromArgb(255, 235, 59); // vàng
                    cell.Style.ForeColor = Color.Black;
                }
                else if (status == "Cooking")
                {
                    cell.Style.BackColor = Color.FromArgb(255, 152, 0); // cam
                    cell.Style.ForeColor = Color.White;
                }
                else if (status == "Ready")
                {
                    cell.Style.BackColor = Color.FromArgb(76, 175, 80); // xanh
                    cell.Style.ForeColor = Color.White;
                }
            }
        }

        // ================= EVENTS =================
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadOrderList();
        }

        private void btnStatusOrder_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is frmMain main)
                main.LoadForm(new frmKitchen(currentRole));
        }

        private void dtgvOrderMagagement_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                string id = dtgvOrderMagagement.Rows[e.RowIndex].Cells["id"].Value.ToString();

                if (this.ParentForm is frmMain main)
                    main.LoadForm(new frmOrderDetails(id));
            }
        }
    }
}