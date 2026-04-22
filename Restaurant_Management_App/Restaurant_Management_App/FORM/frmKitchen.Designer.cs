using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    partial class frmKitchen
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private FlowLayoutPanel pnlStatus;
        private Label lblPending;
        private Label lblCooking;
        private Label lblReady;

        private Panel panelLeft;
        private Panel panelRight;

        private DataGridView dgvKitchen;
        private DataGridView dgvDetail;

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel panelButtons;

        private Button btnCooking;
        private Button btnReady;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.pnlStatus = new FlowLayoutPanel();
            this.lblPending = new Label();
            this.lblCooking = new Label();
            this.lblReady = new Label();

            this.panelLeft = new Panel();
            this.panelRight = new Panel();

            this.dgvKitchen = new DataGridView();
            this.dgvDetail = new DataGridView();

            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panelButtons = new FlowLayoutPanel();

            this.btnCooking = new Button();
            this.btnReady = new Button();
            this.btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();

            this.pnlStatus.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // ===== TITLE =====
            this.lblTitle.Text = "🍽 KITCHEN ORDERS";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(192, 0, 0); // đỏ đậm

            // ===== STATUS =====
            this.pnlStatus.Dock = DockStyle.Fill;
            this.pnlStatus.FlowDirection = FlowDirection.LeftToRight;
            this.pnlStatus.Padding = new Padding(10);
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(255, 230, 230);

            this.pnlStatus.Controls.Add(this.lblPending);
            this.pnlStatus.Controls.Add(this.lblCooking);
            this.pnlStatus.Controls.Add(this.lblReady);

            this.lblPending.Text = "🟡 Pending";
            this.lblPending.AutoSize = true;
            this.lblPending.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPending.Margin = new Padding(20, 10, 20, 10);

            this.lblCooking.Text = "🔵 Cooking";
            this.lblCooking.AutoSize = true;
            this.lblCooking.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCooking.Margin = new Padding(20, 10, 20, 10);

            this.lblReady.Text = "🟢 Ready";
            this.lblReady.AutoSize = true;
            this.lblReady.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReady.Margin = new Padding(20, 10, 20, 10);

            // ===== LEFT PANEL =====
            this.panelLeft.Dock = DockStyle.Fill;
            this.panelLeft.Padding = new Padding(10);
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.dgvKitchen);

            this.dgvKitchen.Dock = DockStyle.Fill;
            this.dgvKitchen.ReadOnly = true;
            this.dgvKitchen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ===== RIGHT PANEL =====
            this.panelRight.Dock = DockStyle.Fill;
            this.panelRight.Padding = new Padding(10);
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.dgvDetail);

            this.dgvDetail.Dock = DockStyle.Fill;
            this.dgvDetail.ReadOnly = true;

            // ===== BUTTON PANEL =====
            this.panelButtons.Dock = DockStyle.Fill;
            this.panelButtons.FlowDirection = FlowDirection.LeftToRight;
            this.panelButtons.Padding = new Padding(10);
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);

            this.panelButtons.Controls.Add(this.btnCooking);
            this.panelButtons.Controls.Add(this.btnReady);
            this.panelButtons.Controls.Add(this.btnBack);

            // Cooking button
            this.btnCooking.Text = "🍳 Cooking";
            this.btnCooking.Width = 150;
            this.btnCooking.Height = 45;
            this.btnCooking.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnCooking.ForeColor = System.Drawing.Color.White;
            this.btnCooking.FlatStyle = FlatStyle.Flat;

            // Ready button
            this.btnReady.Text = "✅ Ready";
            this.btnReady.Width = 150;
            this.btnReady.Height = 45;
            this.btnReady.BackColor = System.Drawing.Color.FromArgb(200, 35, 51);
            this.btnReady.ForeColor = System.Drawing.Color.White;
            this.btnReady.FlatStyle = FlatStyle.Flat;

            // Back button
            this.btnBack.Text = "⬅ Back";
            this.btnBack.Width = 150;
            this.btnBack.Height = 45;
            this.btnBack.BackColor = System.Drawing.Color.Gray;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // ===== TABLE =====
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.RowCount = 4;

            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.SetColumnSpan(this.lblTitle, 2);

            this.tableLayoutPanel1.Controls.Add(this.pnlStatus, 0, 1);
            this.tableLayoutPanel1.SetColumnSpan(this.pnlStatus, 2);

            this.tableLayoutPanel1.Controls.Add(this.panelLeft, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelRight, 1, 2);

            this.tableLayoutPanel1.Controls.Add(this.panelButtons, 0, 3);
            this.tableLayoutPanel1.SetColumnSpan(this.panelButtons, 2);

            // ===== FORM =====
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Text = "Kitchen";
            this.BackColor = System.Drawing.Color.FromArgb(255, 245, 245);

            this.Load += new System.EventHandler(this.FrmKitchen_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();

            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();

            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);

            this.panelButtons.ResumeLayout(false);

            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

