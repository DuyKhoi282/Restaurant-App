using System;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    public class frmCustomerCaring : Form
    {
        private readonly LoyaltyService _loyaltyService = new LoyaltyService();

        private readonly TabControl _tabControl = new TabControl();
        private readonly DataGridView _dgvCustomers = new DataGridView();
        private readonly DataGridView _dgvPromotions = new DataGridView();

        private readonly TextBox _txtPromoName = new TextBox();
        private readonly TextBox _txtPromoDesc = new TextBox();
        private readonly NumericUpDown _numMinPoints = new NumericUpDown();
        private readonly NumericUpDown _numDiscount = new NumericUpDown();
        private readonly DateTimePicker _dtStart = new DateTimePicker();
        private readonly DateTimePicker _dtEnd = new DateTimePicker();
        private readonly Button _btnCreatePromo = new Button();

        public frmCustomerCaring()
        {
            Text = "Customer Caring";
            Dock = DockStyle.Fill;
            FormBorderStyle = FormBorderStyle.None;

            BuildUi();
            Load += FrmCustomerCaring_Load;
        }

        private void FrmCustomerCaring_Load(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void BuildUi()
        {
            Font baseFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            Font titleFont = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            Color primaryRed = Color.FromArgb(200, 35, 51);
            Color lightRedBackground = Color.FromArgb(255, 244, 245);

            BackColor = lightRedBackground;
            _tabControl.Font = baseFont;
            _tabControl.Dock = DockStyle.Fill;

            TabPage tabCustomer = new TabPage("Tích điểm khách hàng");
            TabPage tabPromotion = new TabPage("Chương trình khuyến mãi");
            tabCustomer.BackColor = Color.White;
            tabPromotion.BackColor = Color.White;

            _dgvCustomers.Dock = DockStyle.Fill;
            _dgvCustomers.ReadOnly = true;
            _dgvCustomers.AllowUserToAddRows = false;
            _dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvCustomers.BackgroundColor = Color.White;
            _dgvCustomers.DefaultCellStyle.Font = baseFont;
            _dgvCustomers.ColumnHeadersDefaultCellStyle.Font = titleFont;
            _dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = primaryRed;
            _dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _dgvCustomers.EnableHeadersVisualStyles = false;
            tabCustomer.Controls.Add(_dgvCustomers);

            Panel pnlPromoTop = new Panel { Dock = DockStyle.Top, Height = 130, BackColor = lightRedBackground };
            _dgvPromotions.Dock = DockStyle.Fill;
            _dgvPromotions.ReadOnly = true;
            _dgvPromotions.AllowUserToAddRows = false;
            _dgvPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvPromotions.BackgroundColor = Color.White;
            _dgvPromotions.DefaultCellStyle.Font = baseFont;
            _dgvPromotions.ColumnHeadersDefaultCellStyle.Font = titleFont;
            _dgvPromotions.ColumnHeadersDefaultCellStyle.BackColor = primaryRed;
            _dgvPromotions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _dgvPromotions.EnableHeadersVisualStyles = false;

            Label lblName = new Label { Text = "Tên CTKM", Left = 10, Top = 12, Width = 90, Font = titleFont, ForeColor = primaryRed };
            _txtPromoName.SetBounds(105, 8, 180, 28);
            _txtPromoName.Font = baseFont;

            Label lblDesc = new Label { Text = "Mô tả", Left = 295, Top = 12, Width = 50, Font = titleFont, ForeColor = primaryRed };
            _txtPromoDesc.SetBounds(350, 8, 250, 28);
            _txtPromoDesc.Font = baseFont;

            Label lblStart = new Label { Text = "Bắt đầu", Left = 10, Top = 52, Width = 90, Font = titleFont, ForeColor = primaryRed };
            _dtStart.SetBounds(105, 48, 180, 28);
            _dtStart.Font = baseFont;

            Label lblEnd = new Label { Text = "Kết thúc", Left = 295, Top = 52, Width = 70, Font = titleFont, ForeColor = primaryRed };
            _dtEnd.SetBounds(370, 48, 180, 28);
            _dtEnd.Font = baseFont;

            Label lblMinPoints = new Label { Text = "Điểm tối thiểu", Left = 10, Top = 92, Width = 100, Font = titleFont, ForeColor = primaryRed };
            _numMinPoints.SetBounds(115, 88, 100, 28);
            _numMinPoints.Maximum = 100000;
            _numMinPoints.Font = baseFont;

            Label lblDiscount = new Label { Text = "Giảm (%)", Left = 235, Top = 92, Width = 70, Font = titleFont, ForeColor = primaryRed };
            _numDiscount.SetBounds(310, 88, 100, 28);
            _numDiscount.Maximum = 100;
            _numDiscount.Font = baseFont;

            _btnCreatePromo.Text = "Tạo CTKM";
            _btnCreatePromo.SetBounds(430, 86, 130, 32);
            _btnCreatePromo.Font = titleFont;
            _btnCreatePromo.BackColor = primaryRed;
            _btnCreatePromo.ForeColor = Color.White;
            _btnCreatePromo.FlatStyle = FlatStyle.Flat;
            _btnCreatePromo.FlatAppearance.BorderSize = 0;
            _btnCreatePromo.Click += BtnCreatePromo_Click;

            pnlPromoTop.Controls.AddRange(new Control[]
            {
                lblName, _txtPromoName, lblDesc, _txtPromoDesc,
                lblStart, _dtStart, lblEnd, _dtEnd,
                lblMinPoints, _numMinPoints, lblDiscount, _numDiscount,
                _btnCreatePromo
            });

            tabPromotion.Controls.Add(_dgvPromotions);
            tabPromotion.Controls.Add(pnlPromoTop);

            _tabControl.TabPages.Add(tabCustomer);
            _tabControl.TabPages.Add(tabPromotion);
            Controls.Add(_tabControl);
        }

        private void BtnCreatePromo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtPromoName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chương trình.");
                return;
            }

            if (_dtEnd.Value <= _dtStart.Value)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu.");
                return;
            }

            _loyaltyService.CreatePromotion(
                _txtPromoName.Text.Trim(),
                _txtPromoDesc.Text.Trim(),
                _dtStart.Value,
                _dtEnd.Value,
                (int)_numMinPoints.Value,
                (double)_numDiscount.Value);

            MessageBox.Show("Tạo chương trình khuyến mãi thành công!");
            _txtPromoName.Clear();
            _txtPromoDesc.Clear();
            _numMinPoints.Value = 0;
            _numDiscount.Value = 0;
            ReloadData();
        }

        private void ReloadData()
        {
            _dgvCustomers.DataSource = _loyaltyService.GetCustomerPoints();
            _dgvPromotions.DataSource = _loyaltyService.GetPromotions();
            ApplyVietnameseColumnHeaders();
        }

        private void ApplyVietnameseColumnHeaders()
        {
            if (_dgvCustomers.Columns.Contains("id")) _dgvCustomers.Columns["id"].HeaderText = "Mã KH";
            if (_dgvCustomers.Columns.Contains("customerName")) _dgvCustomers.Columns["customerName"].HeaderText = "Tên khách hàng";
            if (_dgvCustomers.Columns.Contains("phone"))
            {
                _dgvCustomers.Columns["phone"].HeaderText = "Số điện thoại";
                _dgvCustomers.Columns["phone"].Visible = false;
            }
            if (_dgvCustomers.Columns.Contains("points")) _dgvCustomers.Columns["points"].HeaderText = "Điểm tích lũy";
            if (_dgvCustomers.Columns.Contains("memberTier")) _dgvCustomers.Columns["memberTier"].HeaderText = "Hạng thành viên";
            if (_dgvCustomers.Columns.Contains("totalSpent")) _dgvCustomers.Columns["totalSpent"].HeaderText = "Tổng chi tiêu";
            if (_dgvCustomers.Columns.Contains("updatedAt")) _dgvCustomers.Columns["updatedAt"].HeaderText = "Cập nhật lần cuối";

            if (_dgvPromotions.Columns.Contains("id")) _dgvPromotions.Columns["id"].HeaderText = "Mã CTKM";
            if (_dgvPromotions.Columns.Contains("promoName")) _dgvPromotions.Columns["promoName"].HeaderText = "Tên CTKM";
            if (_dgvPromotions.Columns.Contains("description")) _dgvPromotions.Columns["description"].HeaderText = "Mô tả";
            if (_dgvPromotions.Columns.Contains("startDate")) _dgvPromotions.Columns["startDate"].HeaderText = "Ngày bắt đầu";
            if (_dgvPromotions.Columns.Contains("endDate")) _dgvPromotions.Columns["endDate"].HeaderText = "Ngày kết thúc";
            if (_dgvPromotions.Columns.Contains("minPoints")) _dgvPromotions.Columns["minPoints"].HeaderText = "Điểm tối thiểu";
            if (_dgvPromotions.Columns.Contains("discountPercent")) _dgvPromotions.Columns["discountPercent"].HeaderText = "Giảm (%)";
            if (_dgvPromotions.Columns.Contains("isActive")) _dgvPromotions.Columns["isActive"].HeaderText = "Đang áp dụng";
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmCustomerCaring
            // 
            this.ClientSize = new System.Drawing.Size(958, 611);
            this.Name = "frmCustomerCaring";
            this.ResumeLayout(false);

        }

    }
}
