namespace Restaurant_Management_App
{
    partial class frmOrderDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxListDetails = new System.Windows.Forms.GroupBox();
            this.dgvFoodDetails = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIdOrder = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtIdTable = new System.Windows.Forms.TextBox();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtPayMethod = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStatusOrders = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.gbxListDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoodDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbxListDetails);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1668, 396);
            this.panel1.TabIndex = 1;
            // 
            // gbxListDetails
            // 
            this.gbxListDetails.Controls.Add(this.dgvFoodDetails);
            this.gbxListDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxListDetails.Location = new System.Drawing.Point(0, 0);
            this.gbxListDetails.Name = "gbxListDetails";
            this.gbxListDetails.Size = new System.Drawing.Size(1668, 396);
            this.gbxListDetails.TabIndex = 0;
            this.gbxListDetails.TabStop = false;
            this.gbxListDetails.Text = "ID ORDER";
            this.gbxListDetails.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dgvFoodDetails
            // 
            this.dgvFoodDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFoodDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFoodDetails.Location = new System.Drawing.Point(3, 30);
            this.dgvFoodDetails.Name = "dgvFoodDetails";
            this.dgvFoodDetails.RowHeadersWidth = 51;
            this.dgvFoodDetails.RowTemplate.Height = 24;
            this.dgvFoodDetails.Size = new System.Drawing.Size(1662, 363);
            this.dgvFoodDetails.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 525);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = " Id Order:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 623);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = " Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 731);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Time:";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(508, 525);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 29);
            this.label4.TabIndex = 2;
            this.label4.Text = "Id Table:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(508, 613);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total Price:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(508, 731);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 29);
            this.label6.TabIndex = 2;
            this.label6.Text = "Customer Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(960, 525);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 29);
            this.label7.TabIndex = 2;
            this.label7.Text = "Status Payment:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(960, 613);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 29);
            this.label8.TabIndex = 2;
            this.label8.Text = "Pay method:";
            this.label8.Click += new System.EventHandler(this.label7_Click);
            // 
            // txtIdOrder
            // 
            this.txtIdOrder.Location = new System.Drawing.Point(248, 525);
            this.txtIdOrder.Name = "txtIdOrder";
            this.txtIdOrder.ReadOnly = true;
            this.txtIdOrder.Size = new System.Drawing.Size(211, 34);
            this.txtIdOrder.TabIndex = 3;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(248, 620);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(211, 34);
            this.txtDate.TabIndex = 3;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(248, 731);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(211, 34);
            this.txtTime.TabIndex = 3;
            // 
            // txtIdTable
            // 
            this.txtIdTable.Location = new System.Drawing.Point(709, 522);
            this.txtIdTable.Name = "txtIdTable";
            this.txtIdTable.ReadOnly = true;
            this.txtIdTable.Size = new System.Drawing.Size(211, 34);
            this.txtIdTable.TabIndex = 3;
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Location = new System.Drawing.Point(709, 617);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(211, 34);
            this.txtTotalPrice.TabIndex = 3;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(709, 728);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(211, 34);
            this.txtCustomerName.TabIndex = 3;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(1164, 525);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(211, 34);
            this.txtStatus.TabIndex = 3;
            // 
            // txtPayMethod
            // 
            this.txtPayMethod.Location = new System.Drawing.Point(1164, 620);
            this.txtPayMethod.Name = "txtPayMethod";
            this.txtPayMethod.ReadOnly = true;
            this.txtPayMethod.Size = new System.Drawing.Size(211, 34);
            this.txtPayMethod.TabIndex = 3;
            // 
            // btnBack
            // 
            this.btnBack.AutoSize = true;
            this.btnBack.Location = new System.Drawing.Point(1314, 809);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(102, 39);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnPay
            // 
            this.btnPay.AutoSize = true;
            this.btnPay.Location = new System.Drawing.Point(1097, 808);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(102, 39);
            this.btnPay.TabIndex = 4;
            this.btnPay.Text = "Pay";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(963, 729);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 29);
            this.label9.TabIndex = 2;
            this.label9.Text = "Status Order:";
            // 
            // txtStatusOrders
            // 
            this.txtStatusOrders.Location = new System.Drawing.Point(1164, 726);
            this.txtStatusOrders.Name = "txtStatusOrders";
            this.txtStatusOrders.ReadOnly = true;
            this.txtStatusOrders.Size = new System.Drawing.Size(211, 34);
            this.txtStatusOrders.TabIndex = 3;
            // 
            // frmOrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1668, 1073);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtStatusOrders);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtPayMethod);
            this.Controls.Add(this.txtTotalPrice);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txtIdTable);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtIdOrder);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmOrderDetails";
            this.Text = "frmOrderDetails";
            this.Load += new System.EventHandler(this.frmOrderDetails_Load);
            this.panel1.ResumeLayout(false);
            this.gbxListDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoodDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.GroupBox gbxListDetails;
        private System.Windows.Forms.DataGridView dgvFoodDetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtPayMethod;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtIdTable;
        private System.Windows.Forms.TextBox txtIdOrder;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.TextBox txtStatusOrders;
        private System.Windows.Forms.Label label9;
    }
}