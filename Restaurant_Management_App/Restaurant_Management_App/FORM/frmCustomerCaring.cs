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
            _tabControl.Dock = DockStyle.Fill;

            TabPage tabCustomer = new TabPage("Tích điểm khách hàng");
            TabPage tabPromotion = new TabPage("Chương trình khuyến mãi");

            _dgvCustomers.Dock = DockStyle.Fill;
            _dgvCustomers.ReadOnly = true;
            _dgvCustomers.AllowUserToAddRows = false;
            _dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabCustomer.Controls.Add(_dgvCustomers);

            Panel pnlPromoTop = new Panel { Dock = DockStyle.Top, Height = 120 };
            _dgvPromotions.Dock = DockStyle.Fill;
            _dgvPromotions.ReadOnly = true;
            _dgvPromotions.AllowUserToAddRows = false;
            _dgvPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Label lblName = new Label { Text = "Tên CTKM", Left = 10, Top = 12, Width = 80 };
            _txtPromoName.SetBounds(95, 8, 180, 24);

            Label lblDesc = new Label { Text = "Mô tả", Left = 290, Top = 12, Width = 40 };
            _txtPromoDesc.SetBounds(335, 8, 250, 24);

            Label lblStart = new Label { Text = "Bắt đầu", Left = 10, Top = 48, Width = 80 };
            _dtStart.SetBounds(95, 44, 180, 24);

            Label lblEnd = new Label { Text = "Kết thúc", Left = 290, Top = 48, Width = 60 };
            _dtEnd.SetBounds(355, 44, 180, 24);

            Label lblMinPoints = new Label { Text = "Điểm tối thiểu", Left = 10, Top = 84, Width = 80 };
            _numMinPoints.SetBounds(95, 80, 100, 24);
            _numMinPoints.Maximum = 100000;

            Label lblDiscount = new Label { Text = "Giảm (%)", Left = 220, Top = 84, Width = 60 };
            _numDiscount.SetBounds(285, 80, 100, 24);
            _numDiscount.Maximum = 100;

            _btnCreatePromo.Text = "Tạo CTKM";
            _btnCreatePromo.SetBounds(420, 78, 115, 28);
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
        }
    }
}
