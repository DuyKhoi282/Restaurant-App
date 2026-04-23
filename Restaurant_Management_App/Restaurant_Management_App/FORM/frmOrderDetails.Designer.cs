using System.Windows.Forms;

namespace Restaurant_Management_App
{
    partial class frmOrderDetails
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbxListDetails;
        private System.Windows.Forms.DataGridView dgvFoodDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        private System.Windows.Forms.Label lbIdOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;

        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Button btnBack;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxListDetails = new System.Windows.Forms.GroupBox();
            this.dgvFoodDetails = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbIdOrder = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblIdOrder = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblIdTable = new System.Windows.Forms.Label();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPayMethod = new System.Windows.Forms.Label();
            this.lblStatusOrders = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbxListDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoodDetails)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 6);
            this.panel1.Controls.Add(this.gbxListDetails);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1394, 529);
            this.panel1.TabIndex = 0;
            // 
            // gbxListDetails
            // 
            this.gbxListDetails.BackColor = System.Drawing.Color.White;
            this.gbxListDetails.Controls.Add(this.dgvFoodDetails);
            this.gbxListDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxListDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbxListDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.gbxListDetails.Location = new System.Drawing.Point(0, 0);
            this.gbxListDetails.Name = "gbxListDetails";
            this.gbxListDetails.Size = new System.Drawing.Size(1394, 529);
            this.gbxListDetails.TabIndex = 0;
            this.gbxListDetails.TabStop = false;
            this.gbxListDetails.Text = "Chi tiết đơn";
            // 
            // dgvFoodDetails
            // 
            this.dgvFoodDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvFoodDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFoodDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFoodDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFoodDetails.ColumnHeadersHeight = 29;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFoodDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFoodDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFoodDetails.EnableHeadersVisualStyles = false;
            this.dgvFoodDetails.Location = new System.Drawing.Point(3, 26);
            this.dgvFoodDetails.Name = "dgvFoodDetails";
            this.dgvFoodDetails.RowHeadersVisible = false;
            this.dgvFoodDetails.RowHeadersWidth = 51;
            this.dgvFoodDetails.RowTemplate.Height = 30;
            this.dgvFoodDetails.Size = new System.Drawing.Size(1388, 500);
            this.dgvFoodDetails.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.85714F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.85714F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.21428F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbIdOrder, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnBack, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPay, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.button1, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblIdOrder, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblIdTable, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalPrice, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCustomerName, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPayMethod, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblStatusOrders, 5, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.44444F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55556F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.333333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.55556F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1400, 900);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbIdOrder
            // 
            this.lbIdOrder.AutoSize = true;
            this.lbIdOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbIdOrder.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIdOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lbIdOrder.Location = new System.Drawing.Point(3, 535);
            this.lbIdOrder.Name = "lbIdOrder";
            this.lbIdOrder.Size = new System.Drawing.Size(134, 31);
            this.lbIdOrder.TabIndex = 1;
            this.lbIdOrder.Text = "Mã đơn:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label4.Location = new System.Drawing.Point(463, 535);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mã bàn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label2.Location = new System.Drawing.Point(3, 630);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "Ngày:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label5.Location = new System.Drawing.Point(463, 630);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 31);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tổng tiền:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label8.Location = new System.Drawing.Point(926, 630);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(176, 31);
            this.label8.TabIndex = 11;
            this.label8.Text = "Phương thức:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label3.Location = new System.Drawing.Point(3, 724);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "Giờ:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label6.Location = new System.Drawing.Point(463, 724);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 31);
            this.label6.TabIndex = 15;
            this.label6.Text = "Tên khách:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label9.Location = new System.Drawing.Point(926, 724);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 31);
            this.label9.TabIndex = 17;
            this.label9.Text = "Trạng thái món:";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBack.AutoSize = true;
            this.btnBack.BackColor = System.Drawing.Color.Gray;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(1108, 805);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(103, 48);
            this.btnBack.TabIndex = 20;
            this.btnBack.Text = "Trở về";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label7.Location = new System.Drawing.Point(926, 535);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 31);
            this.label7.TabIndex = 5;
            this.label7.Text = "Thanh toán:";
            // 
            // btnPay
            // 
            this.btnPay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPay.AutoSize = true;
            this.btnPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(926, 805);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(139, 48);
            this.btnPay.TabIndex = 19;
            this.btnPay.Text = "Thanh toán";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(643, 805);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 48);
            this.button1.TabIndex = 19;
            this.button1.Text = "Đánh giá";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnReview);
            // 
            // lblIdOrder
            // 
            this.lblIdOrder.AutoSize = true;
            this.lblIdOrder.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdOrder.Location = new System.Drawing.Point(143, 535);
            this.lblIdOrder.Name = "lblIdOrder";
            this.lblIdOrder.Size = new System.Drawing.Size(36, 31);
            this.lblIdOrder.TabIndex = 21;
            this.lblIdOrder.Text = "ID";
            this.lblIdOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(143, 630);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(66, 31);
            this.lblDate.TabIndex = 21;
            this.lblDate.Text = "DATE";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(143, 720);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(65, 31);
            this.lblTime.TabIndex = 21;
            this.lblTime.Text = "TIME";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIdTable
            // 
            this.lblIdTable.AutoSize = true;
            this.lblIdTable.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdTable.Location = new System.Drawing.Point(643, 535);
            this.lblIdTable.Name = "lblIdTable";
            this.lblIdTable.Size = new System.Drawing.Size(96, 31);
            this.lblIdTable.TabIndex = 21;
            this.lblIdTable.Text = "IDTABLE";
            this.lblIdTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPrice.Location = new System.Drawing.Point(643, 630);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(77, 31);
            this.lblTotalPrice.TabIndex = 21;
            this.lblTotalPrice.Text = "TOTAL";
            this.lblTotalPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.Location = new System.Drawing.Point(643, 720);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(131, 31);
            this.lblCustomerName.TabIndex = 21;
            this.lblCustomerName.Text = "CUSTOMER";
            this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(1108, 535);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(89, 31);
            this.lblStatus.TabIndex = 21;
            this.lblStatus.Text = "STATUS";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPayMethod
            // 
            this.lblPayMethod.AutoSize = true;
            this.lblPayMethod.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayMethod.Location = new System.Drawing.Point(1108, 630);
            this.lblPayMethod.Name = "lblPayMethod";
            this.lblPayMethod.Size = new System.Drawing.Size(108, 31);
            this.lblPayMethod.TabIndex = 21;
            this.lblPayMethod.Text = "METHOD";
            this.lblPayMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusOrders
            // 
            this.lblStatusOrders.AutoSize = true;
            this.lblStatusOrders.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusOrders.Location = new System.Drawing.Point(1108, 720);
            this.lblStatusOrders.Name = "lblStatusOrders";
            this.lblStatusOrders.Size = new System.Drawing.Size(162, 31);
            this.lblStatusOrders.TabIndex = 21;
            this.lblStatusOrders.Text = "STATUSORDER";
            this.lblStatusOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmOrderDetails
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmOrderDetails";
            this.Load += new System.EventHandler(this.frmOrderDetails_Load);
            this.panel1.ResumeLayout(false);
            this.gbxListDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoodDetails)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private Button button1;
        private Label lblIdOrder;
        private Label lblDate;
        private Label lblTime;
        private Label lblIdTable;
        private Label lblTotalPrice;
        private Label lblCustomerName;
        private Label lblStatus;
        private Label lblPayMethod;
        private Label lblStatusOrders;
    }
}