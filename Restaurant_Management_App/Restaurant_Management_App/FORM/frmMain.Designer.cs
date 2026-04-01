namespace Restaurant_Management_App
{
    partial class frmMain
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
            this.tlpSidebar = new System.Windows.Forms.TableLayoutPanel();
            this.btnSignout = new System.Windows.Forms.Button();
            this.btnItemManagement = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.btnOrderMNG = new System.Windows.Forms.Button();
            this.btnRevenueMNG = new System.Windows.Forms.Button();
            this.btnStaffMNG = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCustomerCaring = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tlpSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpSidebar
            // 
            this.tlpSidebar.BackColor = System.Drawing.Color.Brown;
            this.tlpSidebar.ColumnCount = 1;
            this.tlpSidebar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSidebar.Controls.Add(this.btnSignout, 0, 8);
            this.tlpSidebar.Controls.Add(this.btnItemManagement, 0, 6);
            this.tlpSidebar.Controls.Add(this.pictureBox1, 0, 0);
            this.tlpSidebar.Controls.Add(this.btnCreateOrder, 0, 2);
            this.tlpSidebar.Controls.Add(this.btnOrderMNG, 0, 3);
            this.tlpSidebar.Controls.Add(this.btnRevenueMNG, 0, 5);
            this.tlpSidebar.Controls.Add(this.btnStaffMNG, 0, 7);
            this.tlpSidebar.Controls.Add(this.label1, 0, 1);
            this.tlpSidebar.Controls.Add(this.btnCustomerCaring, 0, 4);
            this.tlpSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlpSidebar.Location = new System.Drawing.Point(0, 0);
            this.tlpSidebar.Name = "tlpSidebar";
            this.tlpSidebar.RowCount = 9;
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tlpSidebar.Size = new System.Drawing.Size(285, 664);
            this.tlpSidebar.TabIndex = 0;
            // 
            // btnSignout
            // 
            this.btnSignout.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnSignout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignout.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignout.ForeColor = System.Drawing.Color.White;
            this.btnSignout.Location = new System.Drawing.Point(3, 601);
            this.btnSignout.Name = "btnSignout";
            this.btnSignout.Size = new System.Drawing.Size(279, 60);
            this.btnSignout.TabIndex = 3;
            this.btnSignout.Text = "Sign out";
            this.btnSignout.UseVisualStyleBackColor = true;
            this.btnSignout.Click += new System.EventHandler(this.btnSignout_Click);
            // 
            // btnItemManagement
            // 
            this.btnItemManagement.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnItemManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemManagement.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemManagement.ForeColor = System.Drawing.Color.White;
            this.btnItemManagement.Location = new System.Drawing.Point(3, 483);
            this.btnItemManagement.Name = "btnItemManagement";
            this.btnItemManagement.Size = new System.Drawing.Size(279, 53);
            this.btnItemManagement.TabIndex = 2;
            this.btnItemManagement.Text = "Item Management";
            this.btnItemManagement.UseVisualStyleBackColor = true;
            this.btnItemManagement.Click += new System.EventHandler(this.btnItemMNG_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::Restaurant_Management_App.Properties.Resources.logoChillRes;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(279, 179);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.BackColor = System.Drawing.Color.Brown;
            this.btnCreateOrder.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateOrder.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrder.Location = new System.Drawing.Point(3, 247);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(279, 53);
            this.btnCreateOrder.TabIndex = 2;
            this.btnCreateOrder.Text = "Create Order";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            // 
            // btnOrderMNG
            // 
            this.btnOrderMNG.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnOrderMNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderMNG.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrderMNG.ForeColor = System.Drawing.Color.White;
            this.btnOrderMNG.Location = new System.Drawing.Point(3, 306);
            this.btnOrderMNG.Name = "btnOrderMNG";
            this.btnOrderMNG.Size = new System.Drawing.Size(279, 53);
            this.btnOrderMNG.TabIndex = 2;
            this.btnOrderMNG.Text = "Order Management";
            this.btnOrderMNG.UseVisualStyleBackColor = true;
            // 
            // btnRevenueMNG
            // 
            this.btnRevenueMNG.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnRevenueMNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevenueMNG.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevenueMNG.ForeColor = System.Drawing.Color.White;
            this.btnRevenueMNG.Location = new System.Drawing.Point(3, 424);
            this.btnRevenueMNG.Name = "btnRevenueMNG";
            this.btnRevenueMNG.Size = new System.Drawing.Size(279, 53);
            this.btnRevenueMNG.TabIndex = 2;
            this.btnRevenueMNG.Text = "Revenue Management";
            this.btnRevenueMNG.UseVisualStyleBackColor = true;
            // 
            // btnStaffMNG
            // 
            this.btnStaffMNG.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnStaffMNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaffMNG.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaffMNG.ForeColor = System.Drawing.Color.White;
            this.btnStaffMNG.Location = new System.Drawing.Point(3, 542);
            this.btnStaffMNG.Name = "btnStaffMNG";
            this.btnStaffMNG.Size = new System.Drawing.Size(279, 53);
            this.btnStaffMNG.TabIndex = 2;
            this.btnStaffMNG.Text = "Staff Management";
            this.btnStaffMNG.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "THE CHILL RES";
            // 
            // btnCustomerCaring
            // 
            this.btnCustomerCaring.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.btnCustomerCaring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerCaring.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomerCaring.ForeColor = System.Drawing.Color.White;
            this.btnCustomerCaring.Location = new System.Drawing.Point(3, 365);
            this.btnCustomerCaring.Name = "btnCustomerCaring";
            this.btnCustomerCaring.Size = new System.Drawing.Size(279, 53);
            this.btnCustomerCaring.TabIndex = 2;
            this.btnCustomerCaring.Text = "Customer Caring";
            this.btnCustomerCaring.UseVisualStyleBackColor = true;
            // 
            // panelContent
            // 
            this.panelContent.Location = new System.Drawing.Point(291, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(932, 663);
            this.panelContent.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Brown;
            this.panel1.Controls.Add(this.panelContent);
            this.panel1.Controls.Add(this.tlpSidebar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 664);
            this.panel1.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(289, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(932, 664);
            this.pnlMain.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 664);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tlpSidebar.ResumeLayout(false);
            this.tlpSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpSidebar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnOrderMNG;
        private System.Windows.Forms.Button btnItemManagement;
        private System.Windows.Forms.Button btnRevenueMNG;
        private System.Windows.Forms.Button btnCustomerCaring;
        private System.Windows.Forms.Button btnStaffMNG;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnSignout;
    }
}