namespace Restaurant_Management_App
{
    partial class frmResetPassword
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurPass = new System.Windows.Forms.TextBox();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.txtReNewPass = new System.Windows.Forms.TextBox();
            this.btnSave_ResetPassword = new System.Windows.Forms.Button();
            this.btnBack_ResetPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your current Password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Your new Password :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Re-enter your new Password :";
            // 
            // txtCurPass
            // 
            this.txtCurPass.Location = new System.Drawing.Point(209, 53);
            this.txtCurPass.Name = "txtCurPass";
            this.txtCurPass.Size = new System.Drawing.Size(224, 22);
            this.txtCurPass.TabIndex = 1;
            // 
            // txtNewPass
            // 
            this.txtNewPass.Location = new System.Drawing.Point(209, 103);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(224, 22);
            this.txtNewPass.TabIndex = 1;
            // 
            // txtReNewPass
            // 
            this.txtReNewPass.Location = new System.Drawing.Point(209, 150);
            this.txtReNewPass.Name = "txtReNewPass";
            this.txtReNewPass.Size = new System.Drawing.Size(224, 22);
            this.txtReNewPass.TabIndex = 1;
            // 
            // btnSave_ResetPassword
            // 
            this.btnSave_ResetPassword.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave_ResetPassword.Location = new System.Drawing.Point(333, 195);
            this.btnSave_ResetPassword.Name = "btnSave_ResetPassword";
            this.btnSave_ResetPassword.Size = new System.Drawing.Size(100, 34);
            this.btnSave_ResetPassword.TabIndex = 2;
            this.btnSave_ResetPassword.Text = "Save";
            this.btnSave_ResetPassword.UseVisualStyleBackColor = true;
            // 
            // btnBack_ResetPassword
            // 
            this.btnBack_ResetPassword.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack_ResetPassword.Location = new System.Drawing.Point(209, 195);
            this.btnBack_ResetPassword.Name = "btnBack_ResetPassword";
            this.btnBack_ResetPassword.Size = new System.Drawing.Size(100, 34);
            this.btnBack_ResetPassword.TabIndex = 2;
            this.btnBack_ResetPassword.Text = "Back";
            this.btnBack_ResetPassword.UseVisualStyleBackColor = true;
            // 
            // frmResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 250);
            this.Controls.Add(this.btnBack_ResetPassword);
            this.Controls.Add(this.btnSave_ResetPassword);
            this.Controls.Add(this.txtReNewPass);
            this.Controls.Add(this.txtNewPass);
            this.Controls.Add(this.txtCurPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmResetPassword";
            this.Text = "frmResetPassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurPass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.TextBox txtReNewPass;
        private System.Windows.Forms.Button btnSave_ResetPassword;
        private System.Windows.Forms.Button btnBack_ResetPassword;
    }
}