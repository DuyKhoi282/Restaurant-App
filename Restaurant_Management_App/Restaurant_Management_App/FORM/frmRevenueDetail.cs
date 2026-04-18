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
    }
}
