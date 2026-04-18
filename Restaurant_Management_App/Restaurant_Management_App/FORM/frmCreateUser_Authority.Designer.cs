namespace Restaurant_Management_App
{
    partial class frmCreateUser_Authority
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFullname_CUA = new System.Windows.Forms.TextBox();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.txtEmail_CUA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhone_CUA = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUploadImage_CUA = new System.Windows.Forms.Button();
            this.dtpBirthday_CUA = new System.Windows.Forms.DateTimePicker();
            this.txtAddress_CUA = new System.Windows.Forms.TextBox();
            this.cbxWard_CUA = new System.Windows.Forms.ComboBox();
            this.cbxDistrict_CUA = new System.Windows.Forms.ComboBox();
            this.cbxCity_CUA = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxRole_CUA = new System.Windows.Forms.ComboBox();
            this.btnCreate_CUA = new System.Windows.Forms.Button();
            this.btnUpdate_CUA = new System.Windows.Forms.Button();
            this.txtUserId_CUA = new System.Windows.Forms.TextBox();
            this.btnBack_CUA = new System.Windows.Forms.Button();
            this.txtPassword_CUA = new System.Windows.Forms.TextBox();
            this.btnClear_CUA = new System.Windows.Forms.Button();
            this.txtSalary_CUA = new System.Windows.Forms.TextBox();
            this.tlpCUA = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tlpInformations_CUA = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tlpAccounts_CUA = new System.Windows.Forms.TableLayoutPanel();
            this.picAvaUser = new System.Windows.Forms.PictureBox();
            this.btnHistoryChangePass = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.tlpCUA.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tlpInformations_CUA.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tlpAccounts_CUA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvaUser)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(527, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 52);
            this.label6.TabIndex = 1;
            this.label6.Text = "Email :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(527, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 52);
            this.label7.TabIndex = 1;
            this.label7.Text = "Phone :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFullname_CUA
            // 
            this.txtFullname_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtFullname_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullname_CUA.Location = new System.Drawing.Point(241, 11);
            this.txtFullname_CUA.Name = "txtFullname_CUA";
            this.txtFullname_CUA.Size = new System.Drawing.Size(280, 30);
            this.txtFullname_CUA.TabIndex = 3;
            // 
            // dgvAccount
            // 
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpCUA.SetColumnSpan(this.dgvAccount, 2);
            this.dgvAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccount.Location = new System.Drawing.Point(3, 278);
            this.dgvAccount.Name = "dgvAccount";
            this.dgvAccount.RowHeadersWidth = 51;
            this.dgvAccount.RowTemplate.Height = 24;
            this.dgvAccount.Size = new System.Drawing.Size(1308, 508);
            this.dgvAccount.TabIndex = 8;
            this.dgvAccount.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccount_CellClick);
            this.dgvAccount.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvAccount_RowPostPaint_1);
            // 
            // txtEmail_CUA
            // 
            this.txtEmail_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmail_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail_CUA.Location = new System.Drawing.Point(622, 11);
            this.txtEmail_CUA.Name = "txtEmail_CUA";
            this.txtEmail_CUA.Size = new System.Drawing.Size(282, 30);
            this.txtEmail_CUA.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(146, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 52);
            this.label3.TabIndex = 1;
            this.label3.Text = "Address :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(146, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 53);
            this.label2.TabIndex = 1;
            this.label2.Text = "District :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(527, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 52);
            this.label8.TabIndex = 1;
            this.label8.Text = "City :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(527, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 53);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ward :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(146, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 52);
            this.label4.TabIndex = 1;
            this.label4.Text = "Fullname :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPhone_CUA
            // 
            this.txtPhone_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPhone_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone_CUA.Location = new System.Drawing.Point(622, 63);
            this.txtPhone_CUA.Name = "txtPhone_CUA";
            this.txtPhone_CUA.Size = new System.Drawing.Size(282, 30);
            this.txtPhone_CUA.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(146, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 52);
            this.label5.TabIndex = 1;
            this.label5.Text = "Birthday :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUploadImage_CUA
            // 
            this.btnUploadImage_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadImage_CUA.ForeColor = System.Drawing.Color.Firebrick;
            this.btnUploadImage_CUA.Location = new System.Drawing.Point(3, 107);
            this.btnUploadImage_CUA.Name = "btnUploadImage_CUA";
            this.btnUploadImage_CUA.Size = new System.Drawing.Size(136, 39);
            this.btnUploadImage_CUA.TabIndex = 2;
            this.btnUploadImage_CUA.Text = "Upload Image";
            this.btnUploadImage_CUA.UseVisualStyleBackColor = true;
            // 
            // dtpBirthday_CUA
            // 
            this.dtpBirthday_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpBirthday_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBirthday_CUA.Location = new System.Drawing.Point(241, 63);
            this.dtpBirthday_CUA.Name = "dtpBirthday_CUA";
            this.dtpBirthday_CUA.Size = new System.Drawing.Size(280, 30);
            this.dtpBirthday_CUA.TabIndex = 4;
            // 
            // txtAddress_CUA
            // 
            this.txtAddress_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAddress_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress_CUA.Location = new System.Drawing.Point(241, 115);
            this.txtAddress_CUA.Name = "txtAddress_CUA";
            this.txtAddress_CUA.Size = new System.Drawing.Size(280, 30);
            this.txtAddress_CUA.TabIndex = 3;
            // 
            // cbxWard_CUA
            // 
            this.cbxWard_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxWard_CUA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWard_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxWard_CUA.FormattingEnabled = true;
            this.cbxWard_CUA.Items.AddRange(new object[] {
            "Phường 1",
            "Phường 2",
            "Phường 3"});
            this.cbxWard_CUA.Location = new System.Drawing.Point(622, 167);
            this.cbxWard_CUA.Name = "cbxWard_CUA";
            this.cbxWard_CUA.Size = new System.Drawing.Size(282, 30);
            this.cbxWard_CUA.TabIndex = 5;
            // 
            // cbxDistrict_CUA
            // 
            this.cbxDistrict_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxDistrict_CUA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDistrict_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDistrict_CUA.FormattingEnabled = true;
            this.cbxDistrict_CUA.Items.AddRange(new object[] {
            "Bình Thạnh ",
            "Phú Nhuận",
            "Thủ Đức",
            "Quận 7"});
            this.cbxDistrict_CUA.Location = new System.Drawing.Point(241, 167);
            this.cbxDistrict_CUA.Name = "cbxDistrict_CUA";
            this.cbxDistrict_CUA.Size = new System.Drawing.Size(280, 30);
            this.cbxDistrict_CUA.TabIndex = 5;
            this.cbxDistrict_CUA.SelectedIndexChanged += new System.EventHandler(this.cbxDistrict_CUA_SelectedIndexChanged);
            // 
            // cbxCity_CUA
            // 
            this.cbxCity_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxCity_CUA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCity_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCity_CUA.FormattingEnabled = true;
            this.cbxCity_CUA.Items.AddRange(new object[] {
            "Ho Chi Minh",
            "Ha Noi",
            "Nha Trang",
            "Da Nang",
            "Can Tho"});
            this.cbxCity_CUA.Location = new System.Drawing.Point(622, 115);
            this.cbxCity_CUA.Name = "cbxCity_CUA";
            this.cbxCity_CUA.Size = new System.Drawing.Size(282, 30);
            this.cbxCity_CUA.TabIndex = 5;
            this.cbxCity_CUA.SelectedIndexChanged += new System.EventHandler(this.cbxCity_CUA_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 34);
            this.label9.TabIndex = 0;
            this.label9.Text = "UserID  :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(3, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 34);
            this.label10.TabIndex = 0;
            this.label10.Text = "Password :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(3, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 34);
            this.label11.TabIndex = 1;
            this.label11.Text = "Role :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(3, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 34);
            this.label12.TabIndex = 1;
            this.label12.Text = "Salary ( /h ) :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxRole_CUA
            // 
            this.cbxRole_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpAccounts_CUA.SetColumnSpan(this.cbxRole_CUA, 3);
            this.cbxRole_CUA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRole_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRole_CUA.FormattingEnabled = true;
            this.cbxRole_CUA.Location = new System.Drawing.Point(117, 71);
            this.cbxRole_CUA.Name = "cbxRole_CUA";
            this.cbxRole_CUA.Size = new System.Drawing.Size(263, 30);
            this.cbxRole_CUA.TabIndex = 5;
            // 
            // btnCreate_CUA
            // 
            this.tlpAccounts_CUA.SetColumnSpan(this.btnCreate_CUA, 2);
            this.btnCreate_CUA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreate_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate_CUA.Location = new System.Drawing.Point(3, 139);
            this.btnCreate_CUA.Name = "btnCreate_CUA";
            this.btnCreate_CUA.Size = new System.Drawing.Size(184, 28);
            this.btnCreate_CUA.TabIndex = 6;
            this.btnCreate_CUA.Text = "Create";
            this.btnCreate_CUA.UseVisualStyleBackColor = true;
            this.btnCreate_CUA.Click += new System.EventHandler(this.btnCreate_CUA_Click);
            // 
            // btnUpdate_CUA
            // 
            this.tlpAccounts_CUA.SetColumnSpan(this.btnUpdate_CUA, 2);
            this.btnUpdate_CUA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate_CUA.Location = new System.Drawing.Point(193, 139);
            this.btnUpdate_CUA.Name = "btnUpdate_CUA";
            this.btnUpdate_CUA.Size = new System.Drawing.Size(187, 28);
            this.btnUpdate_CUA.TabIndex = 6;
            this.btnUpdate_CUA.Text = "Update";
            this.btnUpdate_CUA.UseVisualStyleBackColor = true;
            this.btnUpdate_CUA.Click += new System.EventHandler(this.btnUpdate_CUA_Click);
            // 
            // txtUserId_CUA
            // 
            this.txtUserId_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpAccounts_CUA.SetColumnSpan(this.txtUserId_CUA, 3);
            this.txtUserId_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId_CUA.Location = new System.Drawing.Point(117, 3);
            this.txtUserId_CUA.Name = "txtUserId_CUA";
            this.txtUserId_CUA.Size = new System.Drawing.Size(263, 30);
            this.txtUserId_CUA.TabIndex = 2;
            // 
            // btnBack_CUA
            // 
            this.tlpAccounts_CUA.SetColumnSpan(this.btnBack_CUA, 2);
            this.btnBack_CUA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBack_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack_CUA.Location = new System.Drawing.Point(193, 173);
            this.btnBack_CUA.Name = "btnBack_CUA";
            this.btnBack_CUA.Size = new System.Drawing.Size(187, 33);
            this.btnBack_CUA.TabIndex = 7;
            this.btnBack_CUA.Text = "Back";
            this.btnBack_CUA.UseVisualStyleBackColor = true;
            this.btnBack_CUA.Click += new System.EventHandler(this.btnBack_CUA_Click);
            // 
            // txtPassword_CUA
            // 
            this.txtPassword_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpAccounts_CUA.SetColumnSpan(this.txtPassword_CUA, 3);
            this.txtPassword_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword_CUA.Location = new System.Drawing.Point(117, 37);
            this.txtPassword_CUA.Name = "txtPassword_CUA";
            this.txtPassword_CUA.Size = new System.Drawing.Size(263, 30);
            this.txtPassword_CUA.TabIndex = 2;
            // 
            // btnClear_CUA
            // 
            this.tlpAccounts_CUA.SetColumnSpan(this.btnClear_CUA, 2);
            this.btnClear_CUA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear_CUA.Location = new System.Drawing.Point(3, 173);
            this.btnClear_CUA.Name = "btnClear_CUA";
            this.btnClear_CUA.Size = new System.Drawing.Size(184, 33);
            this.btnClear_CUA.TabIndex = 7;
            this.btnClear_CUA.Text = "Clear";
            this.btnClear_CUA.UseVisualStyleBackColor = true;
            this.btnClear_CUA.Click += new System.EventHandler(this.btnClear_CUA_Click);
            // 
            // txtSalary_CUA
            // 
            this.txtSalary_CUA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpAccounts_CUA.SetColumnSpan(this.txtSalary_CUA, 3);
            this.txtSalary_CUA.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalary_CUA.Location = new System.Drawing.Point(117, 105);
            this.txtSalary_CUA.Name = "txtSalary_CUA";
            this.txtSalary_CUA.Size = new System.Drawing.Size(263, 30);
            this.txtSalary_CUA.TabIndex = 2;
            // 
            // tlpCUA
            // 
            this.tlpCUA.ColumnCount = 2;
            this.tlpCUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpCUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpCUA.Controls.Add(this.groupBox1, 0, 0);
            this.tlpCUA.Controls.Add(this.groupBox2, 1, 0);
            this.tlpCUA.Controls.Add(this.dgvAccount, 0, 2);
            this.tlpCUA.Controls.Add(this.textBox1, 0, 1);
            this.tlpCUA.Controls.Add(this.button1, 1, 1);
            this.tlpCUA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCUA.Location = new System.Drawing.Point(0, 0);
            this.tlpCUA.Name = "tlpCUA";
            this.tlpCUA.RowCount = 3;
            this.tlpCUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpCUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpCUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpCUA.Size = new System.Drawing.Size(1314, 789);
            this.tlpCUA.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tlpInformations_CUA);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(913, 230);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // tlpInformations_CUA
            // 
            this.tlpInformations_CUA.ColumnCount = 5;
            this.tlpInformations_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.tlpInformations_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tlpInformations_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895F));
            this.tlpInformations_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tlpInformations_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895F));
            this.tlpInformations_CUA.Controls.Add(this.label4, 1, 0);
            this.tlpInformations_CUA.Controls.Add(this.label7, 3, 1);
            this.tlpInformations_CUA.Controls.Add(this.txtAddress_CUA, 2, 2);
            this.tlpInformations_CUA.Controls.Add(this.txtFullname_CUA, 2, 0);
            this.tlpInformations_CUA.Controls.Add(this.dtpBirthday_CUA, 2, 1);
            this.tlpInformations_CUA.Controls.Add(this.label6, 3, 0);
            this.tlpInformations_CUA.Controls.Add(this.txtEmail_CUA, 4, 0);
            this.tlpInformations_CUA.Controls.Add(this.label5, 1, 1);
            this.tlpInformations_CUA.Controls.Add(this.label3, 1, 2);
            this.tlpInformations_CUA.Controls.Add(this.txtPhone_CUA, 4, 1);
            this.tlpInformations_CUA.Controls.Add(this.picAvaUser, 0, 0);
            this.tlpInformations_CUA.Controls.Add(this.btnUploadImage_CUA, 0, 2);
            this.tlpInformations_CUA.Controls.Add(this.cbxCity_CUA, 4, 2);
            this.tlpInformations_CUA.Controls.Add(this.label8, 3, 2);
            this.tlpInformations_CUA.Controls.Add(this.label1, 3, 3);
            this.tlpInformations_CUA.Controls.Add(this.label2, 1, 3);
            this.tlpInformations_CUA.Controls.Add(this.cbxDistrict_CUA, 2, 3);
            this.tlpInformations_CUA.Controls.Add(this.cbxWard_CUA, 4, 3);
            this.tlpInformations_CUA.Controls.Add(this.btnHistoryChangePass, 0, 3);
            this.tlpInformations_CUA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInformations_CUA.Location = new System.Drawing.Point(3, 18);
            this.tlpInformations_CUA.Name = "tlpInformations_CUA";
            this.tlpInformations_CUA.RowCount = 4;
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInformations_CUA.Size = new System.Drawing.Size(907, 209);
            this.tlpInformations_CUA.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tlpAccounts_CUA);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(922, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 230);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Accounts";
            // 
            // tlpAccounts_CUA
            // 
            this.tlpAccounts_CUA.ColumnCount = 4;
            this.tlpAccounts_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpAccounts_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAccounts_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpAccounts_CUA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpAccounts_CUA.Controls.Add(this.btnClear_CUA, 0, 5);
            this.tlpAccounts_CUA.Controls.Add(this.label9, 0, 0);
            this.tlpAccounts_CUA.Controls.Add(this.txtUserId_CUA, 1, 0);
            this.tlpAccounts_CUA.Controls.Add(this.btnCreate_CUA, 0, 4);
            this.tlpAccounts_CUA.Controls.Add(this.txtPassword_CUA, 1, 1);
            this.tlpAccounts_CUA.Controls.Add(this.label10, 0, 1);
            this.tlpAccounts_CUA.Controls.Add(this.label11, 0, 2);
            this.tlpAccounts_CUA.Controls.Add(this.cbxRole_CUA, 1, 2);
            this.tlpAccounts_CUA.Controls.Add(this.label12, 0, 3);
            this.tlpAccounts_CUA.Controls.Add(this.txtSalary_CUA, 1, 3);
            this.tlpAccounts_CUA.Controls.Add(this.btnUpdate_CUA, 2, 4);
            this.tlpAccounts_CUA.Controls.Add(this.btnBack_CUA, 2, 5);
            this.tlpAccounts_CUA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAccounts_CUA.Location = new System.Drawing.Point(3, 18);
            this.tlpAccounts_CUA.Name = "tlpAccounts_CUA";
            this.tlpAccounts_CUA.RowCount = 6;
            this.tlpAccounts_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpAccounts_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpAccounts_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpAccounts_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpAccounts_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpAccounts_CUA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpAccounts_CUA.Size = new System.Drawing.Size(383, 209);
            this.tlpAccounts_CUA.TabIndex = 0;
            // 
            // picAvaUser
            // 
            this.picAvaUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.picAvaUser.Image = global::Restaurant_Management_App.Properties.Resources.ava;
            this.picAvaUser.Location = new System.Drawing.Point(3, 3);
            this.picAvaUser.Name = "picAvaUser";
            this.tlpInformations_CUA.SetRowSpan(this.picAvaUser, 2);
            this.picAvaUser.Size = new System.Drawing.Size(136, 98);
            this.picAvaUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAvaUser.TabIndex = 0;
            this.picAvaUser.TabStop = false;
            // 
            // btnHistoryChangePass
            // 
            this.btnHistoryChangePass.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistoryChangePass.ForeColor = System.Drawing.Color.Firebrick;
            this.btnHistoryChangePass.Location = new System.Drawing.Point(3, 159);
            this.btnHistoryChangePass.Name = "btnHistoryChangePass";
            this.btnHistoryChangePass.Size = new System.Drawing.Size(136, 47);
            this.btnHistoryChangePass.TabIndex = 6;
            this.btnHistoryChangePass.Text = "Change Password History";
            this.btnHistoryChangePass.UseVisualStyleBackColor = true;
            this.btnHistoryChangePass.Click += new System.EventHandler(this.btnHistoryChangePass_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 239);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(913, 22);
            this.textBox1.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(922, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmCreateUser_Authority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 789);
            this.Controls.Add(this.tlpCUA);
            this.Name = "frmCreateUser_Authority";
            this.Text = "frmCreateUser_Authority";
            this.Load += new System.EventHandler(this.frmCreateUser_Authority_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.tlpCUA.ResumeLayout(false);
            this.tlpCUA.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tlpInformations_CUA.ResumeLayout(false);
            this.tlpInformations_CUA.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tlpAccounts_CUA.ResumeLayout(false);
            this.tlpAccounts_CUA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvaUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFullname_CUA;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.ComboBox cbxCity_CUA;
        private System.Windows.Forms.ComboBox cbxDistrict_CUA;
        private System.Windows.Forms.ComboBox cbxWard_CUA;
        private System.Windows.Forms.TextBox txtAddress_CUA;
        private System.Windows.Forms.DateTimePicker dtpBirthday_CUA;
        private System.Windows.Forms.PictureBox picAvaUser;
        private System.Windows.Forms.Button btnUploadImage_CUA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPhone_CUA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail_CUA;
        private System.Windows.Forms.TextBox txtSalary_CUA;
        private System.Windows.Forms.Button btnClear_CUA;
        private System.Windows.Forms.TextBox txtPassword_CUA;
        private System.Windows.Forms.Button btnBack_CUA;
        private System.Windows.Forms.TextBox txtUserId_CUA;
        private System.Windows.Forms.Button btnUpdate_CUA;
        private System.Windows.Forms.Button btnCreate_CUA;
        private System.Windows.Forms.ComboBox cbxRole_CUA;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tlpCUA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tlpInformations_CUA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tlpAccounts_CUA;
        private System.Windows.Forms.Button btnHistoryChangePass;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}