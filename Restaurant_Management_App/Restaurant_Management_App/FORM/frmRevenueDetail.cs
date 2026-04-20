using Restaurant_Management_App.DAL;
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
using OfficeOpenXml;
using System.IO;

namespace Restaurant_Management_App.FORM
{
    public partial class frmRevenueDetail : Form
    {
        ReportType reportType;
        public enum ReportType//phân biệt loại báo cáo nào đang được hiển thị
        {
            Date,
            Month,
            TopFood
        }
        public frmRevenueDetail(ReportType type)
        {
            InitializeComponent();
            reportType = type;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRevenueDetail_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now.AddDays(-7);
            dtpToDate.Value = DateTime.Now;

            switch (reportType)
            {
                case ReportType.Date:
                    lblTitle.Text = "Doanh thu theo ngày";
                    dtpFromDate.Visible = true;
                    dtpToDate.Visible = true;
                    btnThongKe.Visible = true;
                    break;

                case ReportType.Month:
                    lblTitle.Text = "Doanh thu theo tháng";
                    dtpFromDate.Visible = false;
                    dtpToDate.Visible = false;
                    btnThongKe.Visible = true;
                    break;

                case ReportType.TopFood:
                    lblTitle.Text = "Món bán chạy";
                    dtpFromDate.Visible = false;
                    dtpToDate.Visible = false;
                    btnThongKe.Visible = true;
                    break;
            }
        }
        void LoadRevenueByDate()
        {
            string query = @"
        SELECT 
            b.id AS BillID,
            b.DateCheckOut,
            SUM(bi.quantity * f.price) AS TotalAmount
        FROM Bill b
        JOIN BillInfo bi ON b.id = bi.idBill
        JOIN Food f ON bi.idFood = f.id
        WHERE 
            b.status = 1
            AND b.DateCheckOut BETWEEN @fromDate AND @toDate
        GROUP BY 
            b.id, b.DateCheckOut
        ORDER BY 
            b.DateCheckOut DESC";

            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@fromDate", dtpFromDate.Value),
        new SqlParameter("@toDate", dtpToDate.Value)
            };

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, param);

            dgvRevenue.DataSource = dt;

            // ===== FORMAT =====
            dgvRevenue.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
            dgvRevenue.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // ===== TỔNG TIỀN =====
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["TotalAmount"]);
            }

            lblTotalRevenue.Text = "Tổng doanh thu: " + total.ToString("N0") + " VND";
            dgvRevenue.DataSource = dt;
            StyleDataGridView();
        }
        void LoadRevenueByMonth()
        {
            string query = @"
        SELECT 
            YEAR(b.DateCheckOut) AS Year,
            MONTH(b.DateCheckOut) AS Month,
            SUM(bi.quantity * f.price) AS TotalAmount
        FROM Bill b
        JOIN BillInfo bi ON b.id = bi.idBill
        JOIN Food f ON bi.idFood = f.id
        WHERE b.status = 1
        GROUP BY 
            YEAR(b.DateCheckOut),
            MONTH(b.DateCheckOut)
        ORDER BY 
            Year DESC, Month DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            dgvRevenue.DataSource = dt;

            // ===== FORMAT =====
            dgvRevenue.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
            dgvRevenue.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // ===== TỔNG TIỀN =====
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["TotalAmount"]);
            }

            lblTotalRevenue.Text = "Tổng doanh thu: " + total.ToString("N0") + " VND";
            dgvRevenue.DataSource = dt;
            StyleDataGridView();
        }
        void LoadTopFood()
        {
            string query = @"
        SELECT 
            f.name AS FoodName,
            SUM(bi.quantity) AS TotalSold
        FROM BillInfo bi
        JOIN Food f ON bi.idFood = f.id
        JOIN Bill b ON bi.idBill = b.id
        WHERE b.status = 1
        GROUP BY f.name
        ORDER BY TotalSold DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            dgvRevenue.DataSource = dt;

            // ===== FORMAT =====
            dgvRevenue.Columns["TotalSold"].DefaultCellStyle.Format = "N0";
            dgvRevenue.Columns["TotalSold"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // ===== LABEL =====
            lblTotalRevenue.Text = "Top món bán chạy";
            dgvRevenue.DataSource = dt;
            StyleDataGridView();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            switch (reportType)
            {
                case ReportType.Date:
                    LoadRevenueByDate();
                    break;

                case ReportType.Month:
                    LoadRevenueByMonth();
                    break;

                case ReportType.TopFood:
                    LoadTopFood();
                    break;
            }

        }
        void StyleDataGridView()
        {
            dgvRevenue.Font = new Font("Segoe UI", 10);

            dgvRevenue.EnableHeadersVisualStyles = false;
            dgvRevenue.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvRevenue.ColumnHeadersDefaultCellStyle.BackColor = Color.Firebrick;
            dgvRevenue.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvRevenue.RowTemplate.Height = 30;

            if (dgvRevenue.Columns.Contains("TotalAmount"))
            {
                dgvRevenue.Columns["TotalAmount"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvRevenue.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExcelPackage.License.SetNonCommercialPersonal("DuyKhoi");

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file = new FileInfo(sfd.FileName);

                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        var ws = package.Workbook.Worksheets.Add("Revenue");

                        // Header
                        for (int i = 0; i < dgvRevenue.Columns.Count; i++)
                        {
                            ws.Cells[1, i + 1].Value = dgvRevenue.Columns[i].HeaderText;
                        }

                        // Data
                        for (int i = 0; i < dgvRevenue.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvRevenue.Columns.Count; j++)
                            {
                                ws.Cells[i + 2, j + 1].Value = dgvRevenue.Rows[i].Cells[j].Value;
                            }
                        }

                        ws.Cells.AutoFitColumns();

                        package.Save();
                    }

                    MessageBox.Show("Export thành công!");
                }
            }
        }
    }
}
