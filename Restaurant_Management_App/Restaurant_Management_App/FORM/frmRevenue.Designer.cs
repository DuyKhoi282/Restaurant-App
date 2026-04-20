namespace Restaurant_Management_App.FORM
{
    partial class frmRevenue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRevenue));
            this.panelTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.tblMenu = new System.Windows.Forms.TableLayoutPanel();
            this.btnRevenueByDate = new System.Windows.Forms.Button();
            this.btnRevenueByMonth = new System.Windows.Forms.Button();
            this.btnTopFood = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.tblMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.IndianRed;
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 86);
            this.panelTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(783, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "REVENUE MANAGEMENT";
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.tblMenu);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Location = new System.Drawing.Point(0, 86);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(800, 364);
            this.panelMenu.TabIndex = 1;
            // 
            // tblMenu
            // 
            this.tblMenu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tblMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblMenu.BackgroundImage")));
            this.tblMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tblMenu.ColumnCount = 1;
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMenu.Controls.Add(this.btnRevenueByDate, 0, 0);
            this.tblMenu.Controls.Add(this.btnRevenueByMonth, 0, 1);
            this.tblMenu.Controls.Add(this.btnTopFood, 0, 2);
            this.tblMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMenu.Location = new System.Drawing.Point(0, 0);
            this.tblMenu.Name = "tblMenu";
            this.tblMenu.RowCount = 3;
            this.tblMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMenu.Size = new System.Drawing.Size(800, 364);
            this.tblMenu.TabIndex = 1;
            // 
            // btnRevenueByDate
            // 
            this.btnRevenueByDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRevenueByDate.AutoSize = true;
            this.btnRevenueByDate.BackColor = System.Drawing.Color.Firebrick;
            this.btnRevenueByDate.FlatAppearance.BorderSize = 0;
            this.btnRevenueByDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevenueByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevenueByDate.ForeColor = System.Drawing.SystemColors.Window;
            this.btnRevenueByDate.Location = new System.Drawing.Point(123, 19);
            this.btnRevenueByDate.Margin = new System.Windows.Forms.Padding(10);
            this.btnRevenueByDate.Name = "btnRevenueByDate";
            this.btnRevenueByDate.Size = new System.Drawing.Size(554, 82);
            this.btnRevenueByDate.TabIndex = 0;
            this.btnRevenueByDate.Text = "📅 Doanh thu theo ngày";
            this.btnRevenueByDate.UseVisualStyleBackColor = false;
            this.btnRevenueByDate.Click += new System.EventHandler(this.btnRevenueByDate_Click);
            // 
            // btnRevenueByMonth
            // 
            this.btnRevenueByMonth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRevenueByMonth.AutoSize = true;
            this.btnRevenueByMonth.BackColor = System.Drawing.Color.Firebrick;
            this.btnRevenueByMonth.FlatAppearance.BorderSize = 0;
            this.btnRevenueByMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevenueByMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevenueByMonth.ForeColor = System.Drawing.SystemColors.Window;
            this.btnRevenueByMonth.Location = new System.Drawing.Point(115, 137);
            this.btnRevenueByMonth.Margin = new System.Windows.Forms.Padding(10);
            this.btnRevenueByMonth.Name = "btnRevenueByMonth";
            this.btnRevenueByMonth.Size = new System.Drawing.Size(570, 89);
            this.btnRevenueByMonth.TabIndex = 0;
            this.btnRevenueByMonth.Text = "📆 Doanh thu theo tháng";
            this.btnRevenueByMonth.UseVisualStyleBackColor = false;
            this.btnRevenueByMonth.Click += new System.EventHandler(this.btnRevenueByMonth_Click);
            // 
            // btnTopFood
            // 
            this.btnTopFood.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTopFood.AutoSize = true;
            this.btnTopFood.BackColor = System.Drawing.Color.Firebrick;
            this.btnTopFood.FlatAppearance.BorderSize = 0;
            this.btnTopFood.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopFood.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTopFood.ForeColor = System.Drawing.SystemColors.Window;
            this.btnTopFood.Location = new System.Drawing.Point(196, 253);
            this.btnTopFood.Margin = new System.Windows.Forms.Padding(10);
            this.btnTopFood.Name = "btnTopFood";
            this.btnTopFood.Size = new System.Drawing.Size(408, 100);
            this.btnTopFood.TabIndex = 0;
            this.btnTopFood.Text = "🍔 Món bán chạy";
            this.btnTopFood.UseVisualStyleBackColor = false;
            this.btnTopFood.Click += new System.EventHandler(this.btnTopFood_Click);
            // 
            // frmRevenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRevenue";
            this.Text = "frmmRevenue";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.tblMenu.ResumeLayout(false);
            this.tblMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblMenu;
        private System.Windows.Forms.Button btnRevenueByDate;
        private System.Windows.Forms.Button btnRevenueByMonth;
        private System.Windows.Forms.Button btnTopFood;
    }
}