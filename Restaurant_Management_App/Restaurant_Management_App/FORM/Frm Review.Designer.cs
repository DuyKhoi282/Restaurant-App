namespace Restaurant_Management_App
{
    partial class Frm_Review
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Frm_Review
            // 
            this.ClientSize = new System.Drawing.Size(687, 472);
            this.Name = "Frm_Review";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.NumericUpDown numRating;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.DataGridView dgvFood;
        private System.Windows.Forms.Button btnSubmit;
    }
}
