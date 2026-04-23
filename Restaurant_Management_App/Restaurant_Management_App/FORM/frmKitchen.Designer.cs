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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPending = new System.Windows.Forms.Label();
            this.lblCooking = new System.Windows.Forms.Label();
            this.lblReady = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dgvKitchen = new System.Windows.Forms.DataGridView();
            this.panelRight = new System.Windows.Forms.Panel();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCooking = new System.Windows.Forms.Button();
            this.btnReady = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlStatus.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchen)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.tableLayoutPanel1.SetColumnSpan(this.lblTitle, 2);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(994, 90);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🍽 KITCHEN ORDERS";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanel1.SetColumnSpan(this.pnlStatus, 2);
            this.pnlStatus.Controls.Add(this.lblPending);
            this.pnlStatus.Controls.Add(this.lblCooking);
            this.pnlStatus.Controls.Add(this.lblReady);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatus.Location = new System.Drawing.Point(3, 93);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(10);
            this.pnlStatus.Size = new System.Drawing.Size(994, 54);
            this.pnlStatus.TabIndex = 1;
            // 
            // lblPending
            // 
            this.lblPending.AutoSize = true;
            this.lblPending.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPending.Location = new System.Drawing.Point(30, 20);
            this.lblPending.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.lblPending.Name = "lblPending";
            this.lblPending.Size = new System.Drawing.Size(104, 23);
            this.lblPending.TabIndex = 0;
            this.lblPending.Text = "🟡 Pending";
            // 
            // lblCooking
            // 
            this.lblCooking.AutoSize = true;
            this.lblCooking.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCooking.Location = new System.Drawing.Point(174, 20);
            this.lblCooking.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.lblCooking.Name = "lblCooking";
            this.lblCooking.Size = new System.Drawing.Size(106, 23);
            this.lblCooking.TabIndex = 1;
            this.lblCooking.Text = "🔵 Cooking";
            // 
            // lblReady
            // 
            this.lblReady.AutoSize = true;
            this.lblReady.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReady.Location = new System.Drawing.Point(320, 20);
            this.lblReady.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.lblReady.Name = "lblReady";
            this.lblReady.Size = new System.Drawing.Size(88, 23);
            this.lblReady.TabIndex = 2;
            this.lblReady.Text = "🟢 Ready";
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.dgvKitchen);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(3, 153);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeft.Size = new System.Drawing.Size(494, 324);
            this.panelLeft.TabIndex = 2;
            // 
            // dgvKitchen
            // 
            this.dgvKitchen.ColumnHeadersHeight = 29;
            this.dgvKitchen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKitchen.Location = new System.Drawing.Point(10, 10);
            this.dgvKitchen.Name = "dgvKitchen";
            this.dgvKitchen.ReadOnly = true;
            this.dgvKitchen.RowHeadersWidth = 51;
            this.dgvKitchen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKitchen.Size = new System.Drawing.Size(474, 304);
            this.dgvKitchen.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.dgvDetail);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(503, 153);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(10);
            this.panelRight.Size = new System.Drawing.Size(494, 324);
            this.panelRight.TabIndex = 3;
            // 
            // dgvDetail
            // 
            this.dgvDetail.ColumnHeadersHeight = 29;
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(10, 10);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersWidth = 51;
            this.dgvDetail.Size = new System.Drawing.Size(474, 304);
            this.dgvDetail.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlStatus, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelLeft, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelRight, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelButtons, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panelButtons, 2);
            this.panelButtons.Controls.Add(this.btnCooking);
            this.panelButtons.Controls.Add(this.btnReady);
            this.panelButtons.Controls.Add(this.btnBack);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(3, 483);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(10);
            this.panelButtons.Size = new System.Drawing.Size(994, 114);
            this.panelButtons.TabIndex = 4;
            // 
            // btnCooking
            // 
            this.btnCooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCooking.ForeColor = System.Drawing.Color.White;
            this.btnCooking.Location = new System.Drawing.Point(13, 13);
            this.btnCooking.Name = "btnCooking";
            this.btnCooking.Size = new System.Drawing.Size(150, 45);
            this.btnCooking.TabIndex = 0;
            this.btnCooking.Text = "🍳 Cooking";
            this.btnCooking.UseVisualStyleBackColor = false;
            // 
            // btnReady
            // 
            this.btnReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(35)))), ((int)(((byte)(51)))));
            this.btnReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReady.ForeColor = System.Drawing.Color.White;
            this.btnReady.Location = new System.Drawing.Point(169, 13);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(150, 45);
            this.btnReady.TabIndex = 1;
            this.btnReady.Text = "✅ Ready";
            this.btnReady.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Gray;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(325, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(150, 45);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "⬅ Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmKitchen
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmKitchen";
            this.Text = "Kitchen";
            this.Load += new System.EventHandler(this.FrmKitchen_Load);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchen)).EndInit();
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

