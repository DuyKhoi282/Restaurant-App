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
using System.Windows.Forms.DataVisualization.Charting;

namespace Restaurant_Management_App.FORM
{
    public partial class frmRevenueDetail : Form
    {
        ReportType reportType;
        Chart chartRevenue;
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
            
            panelChart.Visible = false;
            dtpFromDate.Value = DateTime.Now.AddDays(-7);
            dtpToDate.Value = DateTime.Now;
            dgvRevenue.Dock = DockStyle.Fill;

            chartRevenue = new Chart();
            chartRevenue.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea();
            chartRevenue.ChartAreas.Add(chartArea);

            //Add vào panel
            panelChart.Controls.Add(chartRevenue);
            LoadRevenue();
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
            LoadChart(dt);
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
            LoadChart(dt);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadRevenue();
        }
        void StyleDataGridView()//định dạng lại datagridview cho đẹp hơn
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

                        int colCount = dgvRevenue.Columns.Count;
                        int rowCount = dgvRevenue.Rows.Count;

                        if (dgvRevenue.Columns.Count == 0)//nếu không có cột nào thì không export
                        {
                            MessageBox.Show("Không có dữ liệu để export!");
                            return;
                        }
                        // ===== TITLE =====
                        ws.Cells["A1"].Value = "BÁO CÁO";
                        ws.Cells["A1"].Style.Font.Size = 16;
                        ws.Cells["A1"].Style.Font.Bold = true;
                        ws.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Cells[1, 1, 1, colCount].Merge = true;

                        // ===== HEADER =====
                        for (int i = 0; i < colCount; i++)
                        {
                            ws.Cells[2, i + 1].Value = dgvRevenue.Columns[i].HeaderText;
                        }

                        using (var range = ws.Cells[2, 1, 2, colCount])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 14;
                            range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.Firebrick);
                            range.Style.Font.Color.SetColor(Color.White);
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }

                        // ===== DATA =====
                        for (int i = 0; i < rowCount; i++)
                        {
                            if (dgvRevenue.Rows[i].IsNewRow) continue;
                            for (int j = 0; j < colCount; j++)
                            {
                                object value = dgvRevenue.Rows[i].Cells[j].Value;
                                var cell = ws.Cells[i + 3, j + 1];

                                if (value is DateTime dt)
                                {
                                    cell.Value = dt;
                                    cell.Style.Numberformat.Format = "dd/MM/yyyy";
                                }
                                else
                                {
                                    cell.Value = value;
                                }

                                cell.Style.Font.Size = 14;
                            }

                            if (i % 2 == 0)
                            {
                                var range = ws.Cells[i + 3, 1, i + 3, colCount];

                                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            }
                        }

                        int totalRow = rowCount + 3;

                        // ===== TOP FOOD =====
                        if (dgvRevenue.Columns.Contains("TotalSold"))
                        {
                            string topFood = dgvRevenue.Rows[0].Cells["FoodName"].Value.ToString();
                            int sold = Convert.ToInt32(dgvRevenue.Rows[0].Cells["TotalSold"].Value);

                            ws.Cells[totalRow, 1].Value = $"TOP 1: {topFood} ({sold} món)";
                            ws.Cells[totalRow, 1, totalRow, colCount].Merge = true;
                            ws.Cells[totalRow, 1].Style.Font.Bold = true;
                            ws.Cells[totalRow, 1].Style.Font.Size = 14;
                            ws.Cells[totalRow, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }

                        // ===== TOTAL MONEY =====
                        if (dgvRevenue.Columns.Contains("TotalAmount"))
                        {
                            decimal total = 0;

                            foreach (DataGridViewRow row in dgvRevenue.Rows)
                            {
                                if (row.IsNewRow) continue;

                                total += Convert.ToDecimal(row.Cells["TotalAmount"].Value);
                            }

                            ws.Cells[totalRow, colCount - 1].Value = "TỔNG DOANH THU:";
                            ws.Cells[totalRow, colCount].Value = total;

                            ws.Cells[totalRow, colCount].Style.Numberformat.Format = "#,##0";
                            ws.Cells[totalRow, colCount].Style.Font.Bold = true;
                        }

                        // ===== BORDER =====
                        var tableRange = ws.Cells[2, 1, totalRow, colCount];
                        tableRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        tableRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        tableRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        tableRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        tableRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        // ===== FORMAT TIỀN =====
                        if (dgvRevenue.Columns.Contains("TotalAmount"))
                        {
                            var moneyCol = ws.Cells[3, colCount, totalRow, colCount];
                            moneyCol.Style.Numberformat.Format = "#,##0";
                            moneyCol.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        }

                        // ===== AUTO FIT =====
                        ws.Cells.AutoFitColumns();

                        package.Save();
                    }

                    MessageBox.Show("Export thành công!");
                }
            }
        }
        void LoadChart(DataTable dt)
        {
            chartRevenue.Series.Clear();
            chartRevenue.Titles.Clear();

            Series series = new Series();

            if (reportType == ReportType.Month)
            {
                series.ChartType = SeriesChartType.Column;
                series.Name = "Doanh thu";

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY("T" + row["Month"], row["TotalAmount"]);
                }
                chartRevenue.Titles.Add("DOANH THU THEO THÁNG");

                series.Color = Color.SeaGreen;
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "N0";
            }
            else if (reportType == ReportType.TopFood)
            {
                series.ChartType = SeriesChartType.Column;
                series.Name = "Top Food";

                int maxValue = 0;
                int maxIndex = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int value = Convert.ToInt32(dt.Rows[i]["TotalSold"]);
                    if (value > maxValue)
                    {
                        maxValue = value;
                        maxIndex = i;
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string name = dt.Rows[i]["FoodName"].ToString();
                    int sold = Convert.ToInt32(dt.Rows[i]["TotalSold"]);

                    int index = series.Points.AddXY(name, sold);

                    //highlight top 1
                    if (i == maxIndex)
                    {
                        series.Points[index].Color = Color.OrangeRed;
                        series.Points[index].Label = "🔥 " + sold;
                    }
                    else
                    {
                        series.Points[index].Color = Color.SteelBlue;
                    }
                }
                chartRevenue.Titles.Add("TOP MÓN BÁN CHẠY");

                series.IsValueShownAsLabel = true;
            }

            chartRevenue.ChartAreas[0].AxisX.Interval = 1;
            chartRevenue.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartRevenue.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            chartRevenue.Series.Add(series);
        }
        void LoadRevenue()
        {
            chartRevenue.Visible = true;

            switch (reportType)
            {
                case ReportType.Date:
                    panelChart.Visible = false;
                    LoadRevenueByDate();
                    break;

                case ReportType.Month:
                    panelChart.Visible = true;
                    LoadRevenueByMonth();
                    break;

                case ReportType.TopFood:
                    panelChart.Visible = true;
                    LoadTopFood();
                    break;

            }
        }

        private void dgvRevenue_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (reportType != ReportType.Date)
                return;

            if (e.RowIndex >= 0)
            {
                int billID = Convert.ToInt32(dgvRevenue.Rows[e.RowIndex].Cells[0].Value);

                new frmBillToPrint(billID.ToString()).ShowDialog();
            }
        }
    }
}
    
