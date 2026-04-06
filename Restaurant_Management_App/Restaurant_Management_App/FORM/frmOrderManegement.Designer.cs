namespace Restaurant_Management_App
{
    partial class frmOrderManegement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBestSeller = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dtgvOrderMagagement = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvOrderMagagement)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(34, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(598, 54);
            this.label1.TabIndex = 3;
            this.label1.Text = "Order Management System";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panel2.Controls.Add(this.btnBestSeller);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1808, 159);
            this.panel2.TabIndex = 4;
            // 
            // btnBestSeller
            // 
            this.btnBestSeller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBestSeller.AutoSize = true;
            this.btnBestSeller.BackColor = System.Drawing.Color.Gold;
            this.btnBestSeller.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBestSeller.ForeColor = System.Drawing.Color.Black;
            this.btnBestSeller.Location = new System.Drawing.Point(1555, 60);
            this.btnBestSeller.Name = "btnBestSeller";
            this.btnBestSeller.Size = new System.Drawing.Size(215, 64);
            this.btnBestSeller.TabIndex = 5;
            this.btnBestSeller.Text = "Best Seller";
            this.btnBestSeller.UseVisualStyleBackColor = false;
            this.btnBestSeller.Click += new System.EventHandler(this.btnBestSeller_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(1374, 60);
            this.btnUpdate.MaximumSize = new System.Drawing.Size(163, 64);
            this.btnUpdate.MinimumSize = new System.Drawing.Size(163, 64);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(163, 64);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dtgvOrderMagagement
            // 
            this.dtgvOrderMagagement.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue;
            this.dtgvOrderMagagement.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvOrderMagagement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvOrderMagagement.BackgroundColor = System.Drawing.Color.White;
            this.dtgvOrderMagagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvOrderMagagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvOrderMagagement.EnableHeadersVisualStyles = false;
            this.dtgvOrderMagagement.Location = new System.Drawing.Point(0, 159);
            this.dtgvOrderMagagement.Margin = new System.Windows.Forms.Padding(9);
            this.dtgvOrderMagagement.Name = "dtgvOrderMagagement";
            this.dtgvOrderMagagement.RowHeadersVisible = false;
            this.dtgvOrderMagagement.RowHeadersWidth = 51;
            this.dtgvOrderMagagement.RowTemplate.Height = 24;
            this.dtgvOrderMagagement.Size = new System.Drawing.Size(1808, 512);
            this.dtgvOrderMagagement.TabIndex = 0;
            this.dtgvOrderMagagement.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvOrderMagagement_CellClick);
            // 
            // frmOrderManegement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1808, 671);
            this.Controls.Add(this.dtgvOrderMagagement);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmOrderManegement";
            this.Text = "OrderManegement";
            this.Load += new System.EventHandler(this.frmOrderManegement_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvOrderMagagement)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnBestSeller;
        private System.Windows.Forms.DataGridView dtgvOrderMagagement;
    }
}