using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    public class frmCustomerCaring : Form
    {
        private readonly LoyaltyService _loyaltyService = new LoyaltyService();
        private readonly BuffetService _buffetService = new BuffetService();

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

        // Buffet controls
        private readonly TextBox _txtCustomerUser = new TextBox();
        private readonly TextBox _txtCustomerPass = new TextBox();
        private readonly TextBox _txtCustomerName = new TextBox();
        private readonly NumericUpDown _numBillId = new NumericUpDown();
        private readonly NumericUpDown _numGuestCount = new NumericUpDown();
        private readonly ComboBox _cbPackage = new ComboBox();
        private readonly Button _btnStartBuffet = new Button();

        private readonly TextBox _txtLoginUser = new TextBox();
        private readonly TextBox _txtLoginPass = new TextBox();
        private readonly Button _btnLoginBuffet = new Button();
        private readonly Label _lblSessionInfo = new Label();
        private readonly Label _lblBuffetTotal = new Label();
        private readonly DataGridView _dgvBuffetMenu = new DataGridView();
        private readonly Button _btnSubmitBatch = new Button();

        private readonly DataGridView _dgvBuffetHistory = new DataGridView();
        private readonly DataGridView _dgvActiveSessions = new DataGridView();
        private readonly Button _btnMarkServed = new Button();

        private int _currentSessionId = 0;

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
            try
            {
                _buffetService.EnsureSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khởi tạo dữ liệu buffet: " + ex.Message);
            }

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
            TabPage tabBuffet = new TabPage("Buffet tự order");
            tabCustomer.BackColor = Color.White;
            tabPromotion.BackColor = Color.White;
            tabBuffet.BackColor = Color.White;

            BuildCustomerTab(tabCustomer, baseFont, titleFont, primaryRed);
            BuildPromotionTab(tabPromotion, baseFont, titleFont, primaryRed, lightRedBackground);
            BuildBuffetTab(tabBuffet, baseFont, titleFont, primaryRed, lightRedBackground);

            _tabControl.TabPages.Add(tabCustomer);
            _tabControl.TabPages.Add(tabPromotion);
            _tabControl.TabPages.Add(tabBuffet);
            Controls.Add(_tabControl);
        }

        private void BuildCustomerTab(TabPage tabCustomer, Font baseFont, Font titleFont, Color primaryRed)
        {
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
        }

        private void BuildPromotionTab(TabPage tabPromotion, Font baseFont, Font titleFont, Color primaryRed, Color lightRedBackground)
        {
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
        }

        private void BuildBuffetTab(TabPage tabBuffet, Font baseFont, Font titleFont, Color primaryRed, Color lightRedBackground)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 105));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            Panel pnlStaff = new Panel { Dock = DockStyle.Fill, BackColor = lightRedBackground, Padding = new Padding(8) };
            Panel pnlLogin = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke, Padding = new Padding(8) };

            Label lblStaffTitle = new Label { Text = "Thiết lập buffet bởi nhân viên (khóa hạn mức 299K/599K)", Left = 8, Top = 6, Width = 620, Font = titleFont, ForeColor = primaryRed };
            Label lblCusUser = new Label { Text = "User KH", Left = 8, Top = 38, Width = 65, Font = baseFont };
            _txtCustomerUser.SetBounds(75, 35, 120, 26);
            Label lblCusPass = new Label { Text = "Pass KH", Left = 205, Top = 38, Width = 65, Font = baseFont };
            _txtCustomerPass.SetBounds(270, 35, 120, 26);
            Label lblCusName = new Label { Text = "Tên KH", Left = 400, Top = 38, Width = 60, Font = baseFont };
            _txtCustomerName.SetBounds(460, 35, 130, 26);

            Label lblBill = new Label { Text = "Bill ID", Left = 8, Top = 72, Width = 65, Font = baseFont };
            _numBillId.SetBounds(75, 69, 80, 26);
            _numBillId.Maximum = 1000000;
            Label lblPackage = new Label { Text = "Gói", Left = 165, Top = 72, Width = 35, Font = baseFont };
            _cbPackage.SetBounds(200, 69, 80, 26);
            _cbPackage.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbPackage.Items.AddRange(new object[] { "299K", "599K" });
            _cbPackage.SelectedIndex = 0;
            Label lblGuest = new Label { Text = "Số khách", Left = 290, Top = 72, Width = 62, Font = baseFont };
            _numGuestCount.SetBounds(352, 69, 70, 26);
            _numGuestCount.Minimum = 1;
            _numGuestCount.Maximum = 100;
            _numGuestCount.Value = 1;

            _btnStartBuffet.Text = "Tạo phiên buffet";
            _btnStartBuffet.SetBounds(435, 66, 155, 30);
            _btnStartBuffet.BackColor = primaryRed;
            _btnStartBuffet.ForeColor = Color.White;
            _btnStartBuffet.FlatStyle = FlatStyle.Flat;
            _btnStartBuffet.FlatAppearance.BorderSize = 0;
            _btnStartBuffet.Font = titleFont;
            _btnStartBuffet.Click += BtnStartBuffet_Click;

            pnlStaff.Controls.AddRange(new Control[]
            {
                lblStaffTitle, lblCusUser, _txtCustomerUser, lblCusPass, _txtCustomerPass,
                lblCusName, _txtCustomerName, lblBill, _numBillId, lblPackage, _cbPackage,
                lblGuest, _numGuestCount, _btnStartBuffet
            });

            Label lblLoginTitle = new Label { Text = "Khách hàng đăng nhập trên thiết bị để tự order", Left = 8, Top = 6, Width = 500, Font = titleFont, ForeColor = primaryRed };
            Label lblLoginUser = new Label { Text = "User", Left = 8, Top = 40, Width = 40, Font = baseFont };
            _txtLoginUser.SetBounds(50, 37, 120, 26);
            Label lblLoginPass = new Label { Text = "Pass", Left = 178, Top = 40, Width = 40, Font = baseFont };
            _txtLoginPass.SetBounds(220, 37, 120, 26);
            _txtLoginPass.PasswordChar = '*';
            _btnLoginBuffet.Text = "Đăng nhập buffet";
            _btnLoginBuffet.SetBounds(352, 34, 145, 30);
            _btnLoginBuffet.BackColor = primaryRed;
            _btnLoginBuffet.ForeColor = Color.White;
            _btnLoginBuffet.FlatStyle = FlatStyle.Flat;
            _btnLoginBuffet.FlatAppearance.BorderSize = 0;
            _btnLoginBuffet.Click += BtnLoginBuffet_Click;

            _lblSessionInfo.SetBounds(8, 72, 520, 22);
            _lblSessionInfo.Font = baseFont;
            _lblSessionInfo.Text = "Chưa đăng nhập phiên buffet.";

            _lblBuffetTotal.SetBounds(540, 72, 260, 22);
            _lblBuffetTotal.Font = titleFont;
            _lblBuffetTotal.ForeColor = primaryRed;
            _lblBuffetTotal.TextAlign = ContentAlignment.MiddleRight;
            _lblBuffetTotal.Text = "Tạm tính buffet: 0 VNĐ";

            pnlLogin.Controls.AddRange(new Control[]
            {
                lblLoginTitle, lblLoginUser, _txtLoginUser, lblLoginPass, _txtLoginPass,
                _btnLoginBuffet, _lblSessionInfo, _lblBuffetTotal
            });

            _dgvBuffetMenu.Dock = DockStyle.Fill;
            _dgvBuffetMenu.AllowUserToAddRows = false;
            _dgvBuffetMenu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvBuffetMenu.BackgroundColor = Color.White;
            _dgvBuffetMenu.ColumnHeadersDefaultCellStyle.BackColor = primaryRed;
            _dgvBuffetMenu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _dgvBuffetMenu.EnableHeadersVisualStyles = false;

            _btnSubmitBatch.Text = "Submit đợt gọi món";
            _btnSubmitBatch.Dock = DockStyle.Bottom;
            _btnSubmitBatch.Height = 36;
            _btnSubmitBatch.BackColor = primaryRed;
            _btnSubmitBatch.ForeColor = Color.White;
            _btnSubmitBatch.FlatStyle = FlatStyle.Flat;
            _btnSubmitBatch.FlatAppearance.BorderSize = 0;
            _btnSubmitBatch.Click += BtnSubmitBatch_Click;

            Panel pnlMenu = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            Label lblMenu = new Label { Text = "Menu buffet trong hạn mức đã khóa", Dock = DockStyle.Top, Height = 24, Font = titleFont, ForeColor = primaryRed };
            pnlMenu.Controls.Add(_dgvBuffetMenu);
            pnlMenu.Controls.Add(_btnSubmitBatch);
            pnlMenu.Controls.Add(lblMenu);

            _dgvBuffetHistory.Dock = DockStyle.Fill;
            _dgvBuffetHistory.ReadOnly = true;
            _dgvBuffetHistory.AllowUserToAddRows = false;
            _dgvBuffetHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvBuffetHistory.BackgroundColor = Color.White;

            _dgvActiveSessions.Dock = DockStyle.Right;
            _dgvActiveSessions.Width = 360;
            _dgvActiveSessions.ReadOnly = true;
            _dgvActiveSessions.AllowUserToAddRows = false;
            _dgvActiveSessions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _btnMarkServed.Text = "Nhân viên xác nhận đã lên món (đợt chọn)";
            _btnMarkServed.Dock = DockStyle.Bottom;
            _btnMarkServed.Height = 36;
            _btnMarkServed.BackColor = primaryRed;
            _btnMarkServed.ForeColor = Color.White;
            _btnMarkServed.FlatStyle = FlatStyle.Flat;
            _btnMarkServed.FlatAppearance.BorderSize = 0;
            _btnMarkServed.Click += BtnMarkServed_Click;

            Panel pnlHistory = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            Label lblHistory = new Label { Text = "Lịch sử submit theo đợt", Dock = DockStyle.Top, Height = 24, Font = titleFont, ForeColor = primaryRed };
            pnlHistory.Controls.Add(_dgvBuffetHistory);
            pnlHistory.Controls.Add(_dgvActiveSessions);
            pnlHistory.Controls.Add(_btnMarkServed);
            pnlHistory.Controls.Add(lblHistory);

            layout.Controls.Add(pnlStaff, 0, 0);
            layout.Controls.Add(pnlLogin, 0, 1);
            layout.Controls.Add(pnlMenu, 0, 2);
            layout.Controls.Add(pnlHistory, 0, 3);
            tabBuffet.Controls.Add(layout);
        }

        private void BtnStartBuffet_Click(object sender, EventArgs e)
        {
            try
            {
                int billId = Convert.ToInt32(_numBillId.Value);
                if (billId <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Bill ID hợp lệ.");
                    return;
                }

                int sessionId = _buffetService.CreateOrStartSession(
                    _txtCustomerUser.Text.Trim(),
                    _txtCustomerPass.Text.Trim(),
                    _txtCustomerName.Text.Trim(),
                    billId,
                    _cbPackage.Text,
                    (int)_numGuestCount.Value,
                    UserSession.UserId);

                MessageBox.Show($"Đã tạo phiên buffet #{sessionId}. Hạn mức {_cbPackage.Text} đã khóa tới khi thanh toán.");
                ReloadBuffetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo phiên buffet: " + ex.Message);
            }
        }

        private void BtnLoginBuffet_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow session = _buffetService.LoginCustomer(_txtLoginUser.Text.Trim(), _txtLoginPass.Text.Trim());
                if (session == null)
                {
                    MessageBox.Show("Sai tài khoản/mật khẩu hoặc chưa có phiên buffet active.");
                    return;
                }

                _currentSessionId = Convert.ToInt32(session["sessionId"]);
                string packageCode = Convert.ToString(session["packageCode"]);
                int guestCount = Convert.ToInt32(session["guestCount"]);
                _lblSessionInfo.Text = $"Phiên #{_currentSessionId} | Bill {session["billId"]} | Gói {packageCode} | {guestCount} khách";
                _lblBuffetTotal.Text = "Tạm tính buffet: " + _buffetService.GetEstimatedBuffetTotal(_currentSessionId).ToString("N0") + " VNĐ";

                LoadBuffetMenu();
                LoadBuffetHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập buffet: " + ex.Message);
            }
        }

        private void LoadBuffetMenu()
        {
            if (_currentSessionId <= 0) return;

            DataTable menu = _buffetService.GetBuffetMenuBySession(_currentSessionId);
            _dgvBuffetMenu.DataSource = menu;

            if (!_dgvBuffetMenu.Columns.Contains("quantity")) return;
            _dgvBuffetMenu.Columns["foodId"].ReadOnly = true;
            _dgvBuffetMenu.Columns["name"].ReadOnly = true;
            _dgvBuffetMenu.Columns["price"].ReadOnly = true;
            _dgvBuffetMenu.Columns["quantity"].ReadOnly = false;
            _dgvBuffetMenu.Columns["quantity"].HeaderText = "Qty gọi";
        }

        private void BtnSubmitBatch_Click(object sender, EventArgs e)
        {
            if (_currentSessionId <= 0)
            {
                MessageBox.Show("Vui lòng đăng nhập buffet trước khi submit.");
                return;
            }

            try
            {
                Dictionary<int, int> orders = new Dictionary<int, int>();
                foreach (DataGridViewRow row in _dgvBuffetMenu.Rows)
                {
                    if (row.IsNewRow) continue;

                    int foodId = Convert.ToInt32(row.Cells["foodId"].Value);
                    int qty = 0;
                    int.TryParse(Convert.ToString(row.Cells["quantity"].Value), out qty);
                    if (qty > 0)
                    {
                        orders[foodId] = qty;
                    }
                }

                int batchId = _buffetService.SubmitBatch(_currentSessionId, orders);
                MessageBox.Show($"Đã submit đợt gọi món #{batchId}.");
                LoadBuffetMenu();
                LoadBuffetHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi submit đợt gọi món: " + ex.Message);
            }
        }

        private void BtnMarkServed_Click(object sender, EventArgs e)
        {
            if (_dgvBuffetHistory.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng trong lịch sử đợt gọi món.");
                return;
            }

            try
            {
                int batchId = Convert.ToInt32(_dgvBuffetHistory.CurrentRow.Cells["batchId"].Value);
                _buffetService.MarkBatchServed(batchId);
                LoadBuffetHistory();
                MessageBox.Show("Đã cập nhật trạng thái: đã lên món.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật trạng thái: " + ex.Message);
            }
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
            ReloadBuffetData();
        }

        private void ReloadBuffetData()
        {
            _dgvActiveSessions.DataSource = _buffetService.GetActiveSessions();
            if (_currentSessionId > 0)
            {
                LoadBuffetHistory();
            }
        }

        private void LoadBuffetHistory()
        {
            if (_currentSessionId <= 0) return;
            _dgvBuffetHistory.DataSource = _buffetService.GetBatchHistory(_currentSessionId);
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
