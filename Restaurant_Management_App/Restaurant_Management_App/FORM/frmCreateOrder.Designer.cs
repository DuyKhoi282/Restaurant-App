using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    partial class frmCreateOrder
    {
        private IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.dgvMenu = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFoodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFoodPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMenuSearch = new System.Windows.Forms.Panel();
            this.txtSearchFood = new System.Windows.Forms.TextBox();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblMenu = new System.Windows.Forms.Label();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoodID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.pnlOrderTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.cbTable = new System.Windows.Forms.ComboBox();
            this.lblOrderType = new System.Windows.Forms.Label();
            this.cbOrderType = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblCase = new System.Windows.Forms.Label();
            this.cbCase = new System.Windows.Forms.ComboBox();
            this.lblPayMethod = new System.Windows.Forms.Label();
            this.cbPayMethod = new System.Windows.Forms.ComboBox();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.lblSubtotalCaption = new System.Windows.Forms.Label();
            this.lblSubtotalValue = new System.Windows.Forms.Label();
            this.lblDiscountCaption = new System.Windows.Forms.Label();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();
            this.lblTaxCaption = new System.Windows.Forms.Label();
            this.lblTaxValue = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).BeginInit();
            this.pnlMenuSearch.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlNote.SuspendLayout();
            this.pnlOrderTop.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.pnlMenu);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.pnlOrder);
            this.splitContainerMain.Size = new System.Drawing.Size(1200, 720);
            this.splitContainerMain.SplitterDistance = 431;
            this.splitContainerMain.TabIndex = 0;
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.dgvMenu);
            this.pnlMenu.Controls.Add(this.pnlMenuSearch);
            this.pnlMenu.Controls.Add(this.lblMenu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMenu.Size = new System.Drawing.Size(431, 720);
            this.pnlMenu.TabIndex = 0;
            // 
            // dgvMenu
            // 
            this.dgvMenu.AllowUserToAddRows = false;
            this.dgvMenu.AllowUserToDeleteRows = false;
            this.dgvMenu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenu.BackgroundColor = System.Drawing.Color.White;
            this.dgvMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.colFoodName,
            this.colCategory,
            this.colFoodPrice});
            this.dgvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMenu.Location = new System.Drawing.Point(10, 60);
            this.dgvMenu.MultiSelect = false;
            this.dgvMenu.Name = "dgvMenu";
            this.dgvMenu.ReadOnly = true;
            this.dgvMenu.RowHeadersVisible = false;
            this.dgvMenu.RowHeadersWidth = 51;
            this.dgvMenu.RowTemplate.Height = 28;
            this.dgvMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenu.Size = new System.Drawing.Size(411, 650);
            this.dgvMenu.TabIndex = 2;
            this.dgvMenu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AddFootToBill);
            // 
            // id
            // 
            this.id.HeaderText = "MenuId";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // colFoodName
            // 
            this.colFoodName.HeaderText = "Food";
            this.colFoodName.MinimumWidth = 6;
            this.colFoodName.Name = "colFoodName";
            this.colFoodName.ReadOnly = true;
            // 
            // colCategory
            // 
            this.colCategory.HeaderText = "Category";
            this.colCategory.MinimumWidth = 6;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colFoodPrice
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            this.colFoodPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.colFoodPrice.HeaderText = "Price";
            this.colFoodPrice.MinimumWidth = 6;
            this.colFoodPrice.Name = "colFoodPrice";
            this.colFoodPrice.ReadOnly = true;
            // 
            // pnlMenuSearch
            // 
            this.pnlMenuSearch.Controls.Add(this.txtSearchFood);
            this.pnlMenuSearch.Controls.Add(this.cbCategory);
            this.pnlMenuSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenuSearch.Location = new System.Drawing.Point(10, 10);
            this.pnlMenuSearch.Name = "pnlMenuSearch";
            this.pnlMenuSearch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlMenuSearch.Size = new System.Drawing.Size(411, 50);
            this.pnlMenuSearch.TabIndex = 1;
            // 
            // txtSearchFood
            // 
            this.txtSearchFood.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFood.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtSearchFood.Location = new System.Drawing.Point(0, 12);
            this.txtSearchFood.Name = "txtSearchFood";
            this.txtSearchFood.Size = new System.Drawing.Size(211, 28);
            this.txtSearchFood.TabIndex = 0;
            this.txtSearchFood.TextChanged += new System.EventHandler(this.txtSearchFood_TextChanged);
            // 
            // cbCategory
            // 
            this.cbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(228, 17);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(180, 24);
            this.cbCategory.TabIndex = 1;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.Location = new System.Drawing.Point(13, 12);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(66, 25);
            this.lblMenu.TabIndex = 0;
            this.lblMenu.Text = "Menu";
            // 
            // pnlOrder
            // 
            this.pnlOrder.Controls.Add(this.dgvCart);
            this.pnlOrder.Controls.Add(this.pnlNote);
            this.pnlOrder.Controls.Add(this.pnlOrderTop);
            this.pnlOrder.Controls.Add(this.pnlSummary);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Padding = new System.Windows.Forms.Padding(10);
            this.pnlOrder.Size = new System.Drawing.Size(765, 720);
            this.pnlOrder.TabIndex = 0;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDelete,
            this.colPrice,
            this.FoodID,
            this.colName,
            this.colQty});
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(10, 200);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.RowTemplate.Height = 28;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(745, 360);
            this.dgvCart.TabIndex = 2;
            this.dgvCart.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellContentClick);
            // 
            // colDelete
            // 
            this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDelete.Frozen = true;
            this.colDelete.HeaderText = "Action";
            this.colDelete.MinimumWidth = 6;
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "X";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 148;
            // 
            // colPrice
            // 
            this.colPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPrice.DataPropertyName = "price";
            this.colPrice.Frozen = true;
            this.colPrice.HeaderText = "Unit price";
            this.colPrice.MinimumWidth = 6;
            this.colPrice.Name = "colPrice";
            this.colPrice.Width = 149;
            // 
            // FoodID
            // 
            this.FoodID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FoodID.DataPropertyName = "foodId";
            this.FoodID.HeaderText = "ID";
            this.FoodID.MinimumWidth = 6;
            this.FoodID.Name = "FoodID";
            this.FoodID.Visible = false;
            this.FoodID.Width = 148;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.HeaderText = "Item";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            // 
            // colQty
            // 
            this.colQty.DataPropertyName = "quantity";
            this.colQty.HeaderText = "Qty";
            this.colQty.MinimumWidth = 6;
            this.colQty.Name = "colQty";
            // 
            // pnlNote
            // 
            this.pnlNote.Controls.Add(this.txtNote);
            this.pnlNote.Controls.Add(this.lblNote);
            this.pnlNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNote.Location = new System.Drawing.Point(10, 130);
            this.pnlNote.Name = "pnlNote";
            this.pnlNote.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlNote.Size = new System.Drawing.Size(745, 70);
            this.pnlNote.TabIndex = 1;
            // 
            // txtNote
            // 
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.Location = new System.Drawing.Point(0, 5);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(745, 60);
            this.txtNote.TabIndex = 1;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(0, 5);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(36, 16);
            this.lblNote.TabIndex = 0;
            this.lblNote.Text = "Note";
            // 
            // pnlOrderTop
            // 
            this.pnlOrderTop.Controls.Add(this.lblTitle);
            this.pnlOrderTop.Controls.Add(this.lblOrderNo);
            this.pnlOrderTop.Controls.Add(this.txtOrderNo);
            this.pnlOrderTop.Controls.Add(this.lblTable);
            this.pnlOrderTop.Controls.Add(this.cbTable);
            this.pnlOrderTop.Controls.Add(this.lblOrderType);
            this.pnlOrderTop.Controls.Add(this.cbOrderType);
            this.pnlOrderTop.Controls.Add(this.lblCustomer);
            this.pnlOrderTop.Controls.Add(this.txtCustomerName);
            this.pnlOrderTop.Controls.Add(this.lblCase);
            this.pnlOrderTop.Controls.Add(this.cbCase);
            this.pnlOrderTop.Controls.Add(this.lblPayMethod);
            this.pnlOrderTop.Controls.Add(this.cbPayMethod);
            this.pnlOrderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderTop.Location = new System.Drawing.Point(10, 10);
            this.pnlOrderTop.Name = "pnlOrderTop";
            this.pnlOrderTop.Size = new System.Drawing.Size(745, 120);
            this.pnlOrderTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "New order";
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Location = new System.Drawing.Point(12, 40);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(62, 16);
            this.lblOrderNo.TabIndex = 1;
            this.lblOrderNo.Text = "Order no.";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(90, 36);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.ReadOnly = true;
            this.txtOrderNo.Size = new System.Drawing.Size(130, 22);
            this.txtOrderNo.TabIndex = 2;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(240, 40);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(43, 16);
            this.lblTable.TabIndex = 3;
            this.lblTable.Text = "Table";
            // 
            // cbTable
            // 
            this.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTable.FormattingEnabled = true;
            this.cbTable.Location = new System.Drawing.Point(290, 36);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(120, 24);
            this.cbTable.TabIndex = 4;
            // 
            // lblOrderType
            // 
            this.lblOrderType.AutoSize = true;
            this.lblOrderType.Location = new System.Drawing.Point(428, 42);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(70, 16);
            this.lblOrderType.TabIndex = 5;
            this.lblOrderType.Text = "Order type";
            // 
            // cbOrderType
            // 
            this.cbOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderType.FormattingEnabled = true;
            this.cbOrderType.Items.AddRange(new object[] {
            "Dine-in",
            "Take away",
            "Delivery"});
            this.cbOrderType.Location = new System.Drawing.Point(515, 36);
            this.cbOrderType.Name = "cbOrderType";
            this.cbOrderType.Size = new System.Drawing.Size(130, 24);
            this.cbOrderType.TabIndex = 6;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(12, 74);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(101, 16);
            this.lblCustomer.TabIndex = 7;
            this.lblCustomer.Text = "Customer name";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(119, 71);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(145, 22);
            this.txtCustomerName.TabIndex = 8;
            // 
            // lblCase
            // 
            this.lblCase.AutoSize = true;
            this.lblCase.Location = new System.Drawing.Point(281, 74);
            this.lblCase.Name = "lblCase";
            this.lblCase.Size = new System.Drawing.Size(39, 16);
            this.lblCase.TabIndex = 9;
            this.lblCase.Text = "Case";
            this.lblCase.Visible = false;
            // 
            // cbCase
            // 
            this.cbCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCase.FormattingEnabled = true;
            this.cbCase.Location = new System.Drawing.Point(326, 71);
            this.cbCase.Name = "cbCase";
            this.cbCase.Size = new System.Drawing.Size(84, 24);
            this.cbCase.TabIndex = 10;
            this.cbCase.Visible = false;
            // 
            // lblPayMethod
            // 
            this.lblPayMethod.AutoSize = true;
            this.lblPayMethod.Location = new System.Drawing.Point(430, 74);
            this.lblPayMethod.Name = "lblPayMethod";
            this.lblPayMethod.Size = new System.Drawing.Size(79, 16);
            this.lblPayMethod.TabIndex = 11;
            this.lblPayMethod.Text = "Pay method";
            // 
            // cbPayMethod
            // 
            this.cbPayMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayMethod.FormattingEnabled = true;
            this.cbPayMethod.Location = new System.Drawing.Point(515, 69);
            this.cbPayMethod.Name = "cbPayMethod";
            this.cbPayMethod.Size = new System.Drawing.Size(130, 24);
            this.cbPayMethod.TabIndex = 12;
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.btnDelete);
            this.pnlSummary.Controls.Add(this.btnClear);
            this.pnlSummary.Controls.Add(this.btnCheckout);
            this.pnlSummary.Controls.Add(this.lblSubtotalCaption);
            this.pnlSummary.Controls.Add(this.lblSubtotalValue);
            this.pnlSummary.Controls.Add(this.lblDiscountCaption);
            this.pnlSummary.Controls.Add(this.numDiscount);
            this.pnlSummary.Controls.Add(this.lblTaxCaption);
            this.pnlSummary.Controls.Add(this.lblTaxValue);
            this.pnlSummary.Controls.Add(this.lblTotalCaption);
            this.pnlSummary.Controls.Add(this.lblTotalValue);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Location = new System.Drawing.Point(10, 560);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlSummary.Size = new System.Drawing.Size(745, 150);
            this.pnlSummary.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(565, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 32);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(463, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 32);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnCheckout
            // 
            this.btnCheckout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckout.BackColor = System.Drawing.Color.IndianRed;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(663, 12);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(67, 32);
            this.btnCheckout.TabIndex = 11;
            this.btnCheckout.Text = "Submit";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click_1);
            // 
            // lblSubtotalCaption
            // 
            this.lblSubtotalCaption.AutoSize = true;
            this.lblSubtotalCaption.Location = new System.Drawing.Point(14, 16);
            this.lblSubtotalCaption.Name = "lblSubtotalCaption";
            this.lblSubtotalCaption.Size = new System.Drawing.Size(56, 16);
            this.lblSubtotalCaption.TabIndex = 0;
            this.lblSubtotalCaption.Text = "Subtotal";
            // 
            // lblSubtotalValue
            // 
            this.lblSubtotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotalValue.Location = new System.Drawing.Point(287, 13);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(160, 23);
            this.lblSubtotalValue.TabIndex = 1;
            this.lblSubtotalValue.Text = "$0.00";
            this.lblSubtotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountCaption
            // 
            this.lblDiscountCaption.AutoSize = true;
            this.lblDiscountCaption.Location = new System.Drawing.Point(14, 48);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new System.Drawing.Size(82, 16);
            this.lblDiscountCaption.TabIndex = 2;
            this.lblDiscountCaption.Text = "Discount (%)";
            // 
            // numDiscount
            // 
            this.numDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numDiscount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numDiscount.Location = new System.Drawing.Point(515, 50);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(80, 22);
            this.numDiscount.TabIndex = 3;
            this.numDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDiscount.ValueChanged += new System.EventHandler(this.numDiscount_ValueChanged_1);
            // 
            // lblTaxCaption
            // 
            this.lblTaxCaption.AutoSize = true;
            this.lblTaxCaption.Location = new System.Drawing.Point(14, 80);
            this.lblTaxCaption.Name = "lblTaxCaption";
            this.lblTaxCaption.Size = new System.Drawing.Size(64, 16);
            this.lblTaxCaption.TabIndex = 5;
            this.lblTaxCaption.Text = "VAT (7%)";
            // 
            // lblTaxValue
            // 
            this.lblTaxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaxValue.Location = new System.Drawing.Point(419, 79);
            this.lblTaxValue.Name = "lblTaxValue";
            this.lblTaxValue.Size = new System.Drawing.Size(160, 23);
            this.lblTaxValue.TabIndex = 6;
            this.lblTaxValue.Text = "$0.00";
            this.lblTaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.Location = new System.Drawing.Point(14, 112);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(61, 25);
            this.lblTotalCaption.TabIndex = 7;
            this.lblTotalCaption.Text = "Total";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.IndianRed;
            this.lblTotalValue.Location = new System.Drawing.Point(419, 106);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(160, 29);
            this.lblTotalValue.TabIndex = 8;
            this.lblTotalValue.Text = "$0.00";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCreateOrder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.splitContainerMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCreateOrder";
            this.Text = "frmOrder";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).EndInit();
            this.pnlMenuSearch.ResumeLayout(false);
            this.pnlMenuSearch.PerformLayout();
            this.pnlOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlNote.ResumeLayout(false);
            this.pnlNote.PerformLayout();
            this.pnlOrderTop.ResumeLayout(false);
            this.pnlOrderTop.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainerMain;

        // Left menu controls
        private Panel pnlMenu;
        private Panel pnlMenuSearch;
        private TextBox txtSearchFood;
        private ComboBox cbCategory;
        private Label lblMenu;
        private DataGridView dgvMenu;

        // Right order controls
        private Panel pnlOrder;
        private Panel pnlOrderTop;
        private Label lblTitle;
        private Label lblOrderNo;
        private TextBox txtOrderNo;
        private Label lblTable;
        private ComboBox cbTable;
        private Label lblOrderType;
        private ComboBox cbOrderType;
        private Label lblCustomer;
        private TextBox txtCustomerName;
        private Label lblCase;
        private ComboBox cbCase;
        private Label lblPayMethod;
        private ComboBox cbPayMethod;

        private Panel pnlNote;
        private Label lblNote;

        private DataGridView dgvCart;

        private Panel pnlSummary;
        private Label lblSubtotalCaption;
        private Label lblSubtotalValue;
        private Label lblDiscountCaption;
        private NumericUpDown numDiscount;
        private Label lblTaxCaption;
        private Label lblTaxValue;
        private Label lblTotalCaption;
        private Label lblTotalValue;
        private Button btnClear;
        private Button btnCheckout;
        private TextBox txtNote;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn colFoodName;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colFoodPrice;
        private DataGridViewButtonColumn colDelete;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn FoodID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colQty;
        private Button btnDelete;
    }
}
