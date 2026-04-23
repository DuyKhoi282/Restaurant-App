using System.Windows.Forms;

namespace Restaurant_Management_App
{
    partial class frmOrderManegement
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Panel topPanel;
        private FlowLayoutPanel panelButtons;
        private DataGridView dtgvOrderMagagement;

        private Button btnReload;
        private Button btnStatusOrder;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnStatusOrder = new System.Windows.Forms.Button();
            this.dtgvOrderMagagement = new System.Windows.Forms.DataGridView();
            this.topPanel.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvOrderMagagement)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1280, 70);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "📋 QUẢN LÝ ĐƠN HÀNG";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.topPanel.Controls.Add(this.panelButtons);
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1280, 140);
            this.topPanel.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.panelButtons.Controls.Add(this.btnReload);
            this.panelButtons.Controls.Add(this.btnStatusOrder);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 70);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelButtons.Size = new System.Drawing.Size(1280, 70);
            this.panelButtons.TabIndex = 0;
            this.panelButtons.WrapContents = false;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.Location = new System.Drawing.Point(35, 20);
            this.btnReload.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(180, 60);
            this.btnReload.TabIndex = 0;
            this.btnReload.Text = "🔄 Tải lại";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnStatusOrder
            // 
            this.btnStatusOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnStatusOrder.FlatAppearance.BorderSize = 0;
            this.btnStatusOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatusOrder.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnStatusOrder.ForeColor = System.Drawing.Color.White;
            this.btnStatusOrder.Location = new System.Drawing.Point(245, 20);
            this.btnStatusOrder.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.btnStatusOrder.Name = "btnStatusOrder";
            this.btnStatusOrder.Size = new System.Drawing.Size(180, 60);
            this.btnStatusOrder.TabIndex = 1;
            this.btnStatusOrder.Text = "📊 Trạng thái";
            this.btnStatusOrder.UseVisualStyleBackColor = false;
            this.btnStatusOrder.Click += new System.EventHandler(this.btnStatusOrder_Click);
            // 
            // dtgvOrderMagagement
            // 
            this.dtgvOrderMagagement.AllowUserToAddRows = false;
            this.dtgvOrderMagagement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvOrderMagagement.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 15F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvOrderMagagement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvOrderMagagement.ColumnHeadersHeight = 55;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 15F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvOrderMagagement.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvOrderMagagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvOrderMagagement.EnableHeadersVisualStyles = false;
            this.dtgvOrderMagagement.Location = new System.Drawing.Point(0, 140);
            this.dtgvOrderMagagement.Name = "dtgvOrderMagagement";
            this.dtgvOrderMagagement.RowHeadersVisible = false;
            this.dtgvOrderMagagement.RowHeadersWidth = 51;
            this.dtgvOrderMagagement.RowTemplate.Height = 50;
            this.dtgvOrderMagagement.Size = new System.Drawing.Size(1280, 560);
            this.dtgvOrderMagagement.TabIndex = 0;
            this.dtgvOrderMagagement.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvOrderMagagement_CellClick);
            // 
            // frmOrderManegement
            // 
            this.ClientSize = new System.Drawing.Size(1280, 700);
            this.Controls.Add(this.dtgvOrderMagagement);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.Name = "frmOrderManegement";
            this.Text = "Order Management";
            this.Load += new System.EventHandler(this.frmOrderManegement_Load);
            this.topPanel.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvOrderMagagement)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

