using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    partial class frmKitchen
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvKitchen;
        private DataGridView dgvDetail;
        private Button btnCooking;
        private Button btnReady;
        private Label lblTitle;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvKitchen = new System.Windows.Forms.DataGridView();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.btnCooking = new System.Windows.Forms.Button();
            this.btnReady = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Text = "Kitchen Orders";
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.AutoSize = true;

            // panelLeft
            this.panelLeft.Location = new System.Drawing.Point(20, 50);
            this.panelLeft.Size = new System.Drawing.Size(500, 300);
            this.panelLeft.BorderStyle = BorderStyle.FixedSingle;

            // panelRight
            this.panelRight.Location = new System.Drawing.Point(550, 50);
            this.panelRight.Size = new System.Drawing.Size(400, 300);
            this.panelRight.BorderStyle = BorderStyle.FixedSingle;

            // dgvKitchen
            this.dgvKitchen.Dock = DockStyle.Fill;
            this.dgvKitchen.ReadOnly = true;
            this.dgvKitchen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // dgvDetail
            this.dgvDetail.Dock = DockStyle.Fill;
            this.dgvDetail.ReadOnly = true;

            // add dgv vào panel
            this.panelLeft.Controls.Add(this.dgvKitchen);
            this.panelRight.Controls.Add(this.dgvDetail);

            // btnCooking
            this.btnCooking.Text = "Cooking";
            this.btnCooking.Location = new System.Drawing.Point(20, 370);
            this.btnCooking.Size = new System.Drawing.Size(120, 40);

            // btnReady
            this.btnReady.Text = "Ready";
            this.btnReady.Location = new System.Drawing.Point(160, 370);
            this.btnReady.Size = new System.Drawing.Size(120, 40);

            // frmKitchen
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.btnCooking);
            this.Controls.Add(this.btnReady);

            this.Text = "Kitchen";
            this.Size = new System.Drawing.Size(1000, 500);

            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}