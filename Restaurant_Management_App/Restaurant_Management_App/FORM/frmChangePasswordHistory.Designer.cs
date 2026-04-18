namespace Restaurant_Management_App
{
    partial class frmChangePasswordHistory
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvHistoryChangePass = new System.Windows.Forms.DataGridView();
            this.cbxIdUser_ChangePass = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryChangePass)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dgvHistoryChangePass, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbxIdUser_ChangePass, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(716, 638);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // dgvHistoryChangePass
            // 
            this.dgvHistoryChangePass.AllowUserToAddRows = false;
            this.dgvHistoryChangePass.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistoryChangePass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistoryChangePass.Location = new System.Drawing.Point(3, 41);
            this.dgvHistoryChangePass.Name = "dgvHistoryChangePass";
            this.dgvHistoryChangePass.RowHeadersVisible = false;
            this.dgvHistoryChangePass.RowHeadersWidth = 51;
            this.dgvHistoryChangePass.RowTemplate.Height = 24;
            this.dgvHistoryChangePass.Size = new System.Drawing.Size(710, 594);
            this.dgvHistoryChangePass.TabIndex = 1;
            // 
            // cbxIdUser_ChangePass
            // 
            this.cbxIdUser_ChangePass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxIdUser_ChangePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxIdUser_ChangePass.FormattingEnabled = true;
            this.cbxIdUser_ChangePass.Location = new System.Drawing.Point(3, 3);
            this.cbxIdUser_ChangePass.Name = "cbxIdUser_ChangePass";
            this.cbxIdUser_ChangePass.Size = new System.Drawing.Size(237, 33);
            this.cbxIdUser_ChangePass.TabIndex = 0;
            // 
            // frmChangePasswordHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 638);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "frmChangePasswordHistory";
            this.Text = "frmHistoryChangePass";
            this.Load += new System.EventHandler(this.frmChangePasswordHistory_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryChangePass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cbxIdUser_ChangePass;
        private System.Windows.Forms.DataGridView dgvHistoryChangePass;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}