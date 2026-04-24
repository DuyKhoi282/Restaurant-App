namespace Restaurant_Management_App
{
    partial class frmListRatingService
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
            this.dgvRating = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRating)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRating
            // 
            this.dgvRating.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRating.Location = new System.Drawing.Point(0, 0);
            this.dgvRating.Name = "dgvRating";
            this.dgvRating.RowHeadersWidth = 51;
            this.dgvRating.RowTemplate.Height = 24;
            this.dgvRating.Size = new System.Drawing.Size(800, 450);
            this.dgvRating.TabIndex = 0;
            // 
            // frmListRatingService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvRating);
            this.Name = "frmListRatingService";
            this.Text = "frmViewListRate";
            this.Load += new System.EventHandler(this.frmListRatingService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRating)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRating;
    }
}