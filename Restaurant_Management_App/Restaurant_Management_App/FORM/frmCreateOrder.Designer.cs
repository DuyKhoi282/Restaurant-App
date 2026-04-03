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
            this.splitContainerMain = new SplitContainer();
            // Left (Menu)
            this.pnlMenu = new Panel();
            this.pnlMenuSearch = new Panel();
            this.txtSearchFood = new TextBox();
            this.cboCategory = new ComboBox();
            this.lblMenu = new Label();
            this.dgvMenu = new DataGridView();
            this.colMenuId = new DataGridViewTextBoxColumn();
            this.colFoodName = new DataGridViewTextBoxColumn();
            this.colCategory = new DataGridViewTextBoxColumn();
            this.colFoodPrice = new DataGridViewTextBoxColumn();

            // Right (Order)
            this.pnlOrder = new Panel();
            this.pnlOrderTop = new Panel();
            this.lblTitle = new Label();
            this.lblOrderNo = new Label();
            this.txtOrderNo = new TextBox();
            this.lblTable = new Label();
            this.cboTable = new ComboBox();
            this.lblOrderType = new Label();
            this.cboOrderType = new ComboBox();
            this.lblCustomer = new Label();
            this.txtCustomerName = new TextBox();
            this.lblCase = new Label();
            this.cboCase = new ComboBox();
            this.lblPayMethod = new Label();
            this.cboPayMethod = new ComboBox();

            this.pnlNote = new Panel();
            this.lblNote = new Label();
            this.txtNote = new TextBox();

            this.dgvCart = new DataGridView();
            this.colCartId = new DataGridViewTextBoxColumn();
            this.colItem = new DataGridViewTextBoxColumn();
            this.colQty = new DataGridViewTextBoxColumn();
            this.colUnitPrice = new DataGridViewTextBoxColumn();
            this.colLineTotal = new DataGridViewTextBoxColumn();

            this.pnlSummary = new Panel();
            this.lblSubtotalCaption = new Label();
            this.lblSubtotalValue = new Label();
            this.lblDiscountCaption = new Label();
            this.numDiscount = new NumericUpDown();
            this.lblTaxCaption = new Label();
            this.lblTaxValue = new Label();
            this.lblTotalCaption = new Label();
            this.lblTotalValue = new Label();
            this.btnClear = new Button();
            this.btnHold = new Button();
            this.btnCheckout = new Button();

            ((ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();

            this.pnlMenu.SuspendLayout();
            this.pnlMenuSearch.SuspendLayout();
            ((ISupportInitialize)(this.dgvMenu)).BeginInit();

            this.pnlOrder.SuspendLayout();
            this.pnlOrderTop.SuspendLayout();
            this.pnlNote.SuspendLayout();
            ((ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlSummary.SuspendLayout();
            ((ISupportInitialize)(this.numDiscount)).BeginInit();

            this.SuspendLayout();

            // splitContainerMain
            this.splitContainerMain.Dock = DockStyle.Fill;
            this.splitContainerMain.Location = new Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Size = new Size(1200, 720);
            this.splitContainerMain.SplitterDistance = 520;
            this.splitContainerMain.TabIndex = 0;

            // splitContainerMain.Panel1 -> pnlMenu
            this.splitContainerMain.Panel1.Controls.Add(this.pnlMenu);

            // splitContainerMain.Panel2 -> pnlOrder
            this.splitContainerMain.Panel2.Controls.Add(this.pnlOrder);

            // pnlMenu
            this.pnlMenu.Controls.Add(this.dgvMenu);
            this.pnlMenu.Controls.Add(this.pnlMenuSearch);
            this.pnlMenu.Controls.Add(this.lblMenu);
            this.pnlMenu.Dock = DockStyle.Fill;
            this.pnlMenu.Location = new Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new Padding(10);
            this.pnlMenu.Size = new Size(520, 720);
            this.pnlMenu.TabIndex = 0;

            // pnlMenuSearch
            this.pnlMenuSearch.Controls.Add(this.txtSearchFood);
            this.pnlMenuSearch.Controls.Add(this.cboCategory);
            this.pnlMenuSearch.Dock = DockStyle.Top;
            this.pnlMenuSearch.Location = new Point(10, 10);
            this.pnlMenuSearch.Name = "pnlMenuSearch";
            this.pnlMenuSearch.Padding = new Padding(0, 0, 0, 8);
            this.pnlMenuSearch.Size = new Size(500, 50);
            this.pnlMenuSearch.TabIndex = 1;

            // txtSearchFood
            this.txtSearchFood.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtSearchFood.Font = new Font("Microsoft Sans Serif", 11F);
            this.txtSearchFood.Location = new Point(0, 12);
            this.txtSearchFood.Name = "txtSearchFood";
            this.txtSearchFood.Size = new Size(300, 24);
            this.txtSearchFood.TabIndex = 0;

            // cboCategory
            this.cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new Point(320, 12);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new Size(180, 24);
            this.cboCategory.TabIndex = 1;

            // lblMenu
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.lblMenu.Location = new Point(13, 12);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new Size(62, 20);
            this.lblMenu.TabIndex = 0;
            this.lblMenu.Text = "Menu";

            // dgvMenu
            this.dgvMenu.AllowUserToAddRows = false;
            this.dgvMenu.AllowUserToDeleteRows = false;
            this.dgvMenu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenu.BackgroundColor = Color.White;
            this.dgvMenu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenu.Columns.AddRange(new DataGridViewColumn[] {
                this.colMenuId,
                this.colFoodName,
                this.colCategory,
                this.colFoodPrice
            });
            this.dgvMenu.Dock = DockStyle.Fill;
            this.dgvMenu.Location = new Point(10, 60);
            this.dgvMenu.MultiSelect = false;
            this.dgvMenu.Name = "dgvMenu";
            this.dgvMenu.ReadOnly = true;
            this.dgvMenu.RowHeadersVisible = false;
            this.dgvMenu.RowTemplate.Height = 28;
            this.dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenu.Size = new Size(500, 650);
            this.dgvMenu.TabIndex = 2;

            // colMenuId (hidden)
            this.colMenuId.HeaderText = "MenuId";
            this.colMenuId.Name = "colMenuId";
            this.colMenuId.Visible = false;

            // colFoodName
            this.colFoodName.HeaderText = "Food";
            this.colFoodName.Name = "colFoodName";
            this.colFoodName.ReadOnly = true;

            // colCategory
            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;

            // colFoodPrice
            this.colFoodPrice.HeaderText = "Price";
            this.colFoodPrice.Name = "colFoodPrice";
            this.colFoodPrice.ReadOnly = true;
            this.colFoodPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colFoodPrice.DefaultCellStyle.Format = "C2";

            // pnlOrder
            this.pnlOrder.Controls.Add(this.dgvCart);
            this.pnlOrder.Controls.Add(this.pnlNote);
            this.pnlOrder.Controls.Add(this.pnlOrderTop);
            this.pnlOrder.Controls.Add(this.pnlSummary);
            this.pnlOrder.Dock = DockStyle.Fill;
            this.pnlOrder.Location = new Point(0, 0);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Padding = new Padding(10);
            this.pnlOrder.Size = new Size(676, 720);
            this.pnlOrder.TabIndex = 0;

            // pnlOrderTop
            this.pnlOrderTop.Controls.Add(this.lblTitle);
            this.pnlOrderTop.Controls.Add(this.lblOrderNo);
            this.pnlOrderTop.Controls.Add(this.txtOrderNo);
            this.pnlOrderTop.Controls.Add(this.lblTable);
            this.pnlOrderTop.Controls.Add(this.cboTable);
            this.pnlOrderTop.Controls.Add(this.lblOrderType);
            this.pnlOrderTop.Controls.Add(this.cboOrderType);
            this.pnlOrderTop.Controls.Add(this.lblCustomer);
            this.pnlOrderTop.Controls.Add(this.txtCustomerName);
            this.pnlOrderTop.Controls.Add(this.lblCase);
            this.pnlOrderTop.Controls.Add(this.cboCase);
            this.pnlOrderTop.Controls.Add(this.lblPayMethod);
            this.pnlOrderTop.Controls.Add(this.cboPayMethod);
            this.pnlOrderTop.Dock = DockStyle.Top;
            this.pnlOrderTop.Location = new Point(10, 10);
            this.pnlOrderTop.Name = "pnlOrderTop";
            this.pnlOrderTop.Size = new Size(656, 120);
            this.pnlOrderTop.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.lblTitle.Location = new Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(90, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "New order";

            // lblOrderNo
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Location = new Point(12, 40);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new Size(60, 16);
            this.lblOrderNo.TabIndex = 1;
            this.lblOrderNo.Text = "Order no.";

            // txtOrderNo
            this.txtOrderNo.Location = new Point(90, 36);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.ReadOnly = true;
            this.txtOrderNo.Size = new Size(130, 22);
            this.txtOrderNo.TabIndex = 2;

            // lblTable
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new Point(240, 40);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new Size(40, 16);
            this.lblTable.TabIndex = 3;
            this.lblTable.Text = "Table";

            // cboTable
            this.cboTable.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new Point(290, 36);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new Size(120, 24);
            this.cboTable.TabIndex = 4;

            // lblOrderType
            this.lblOrderType.AutoSize = true;
            this.lblOrderType.Location = new Point(430, 40);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new Size(68, 16);
            this.lblOrderType.TabIndex = 5;
            this.lblOrderType.Text = "Order type";

            // cboOrderType
            this.cboOrderType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboOrderType.FormattingEnabled = true;
            this.cboOrderType.Items.AddRange(new object[] { "Dine-in", "Take away", "Delivery" });
            this.cboOrderType.Location = new Point(504, 36);
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.Size = new Size(130, 24);
            this.cboOrderType.TabIndex = 6;

            // lblCustomer
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new Point(12, 74);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new Size(86, 16);
            this.lblCustomer.TabIndex = 7;
            this.lblCustomer.Text = "Customer name";

            // txtCustomerName
            this.txtCustomerName.Location = new Point(110, 70);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new Size(200, 22);
            this.txtCustomerName.TabIndex = 8;

            // lblCase
            this.lblCase.AutoSize = true;
            this.lblCase.Location = new Point(330, 74);
            this.lblCase.Name = "lblCase";
            this.lblCase.Size = new Size(35, 16);
            this.lblCase.TabIndex = 9;
            this.lblCase.Text = "Case";

            // cboCase
            this.cboCase.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCase.FormattingEnabled = true;
            this.cboCase.Location = new Point(370, 70);
            this.cboCase.Name = "cboCase";
            this.cboCase.Size = new Size(120, 24);
            this.cboCase.TabIndex = 10;

            // lblPayMethod
            this.lblPayMethod.AutoSize = true;
            this.lblPayMethod.Location = new Point(500, 74);
            this.lblPayMethod.Name = "lblPayMethod";
            this.lblPayMethod.Size = new Size(70, 16);
            this.lblPayMethod.TabIndex = 11;
            this.lblPayMethod.Text = "Pay method";

            // cboPayMethod
            this.cboPayMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPayMethod.FormattingEnabled = true;
            this.cboPayMethod.Location = new Point(574, 70);
            this.cboPayMethod.Name = "cboPayMethod";
            this.cboPayMethod.Size = new Size(130, 24);
            this.cboPayMethod.TabIndex = 12;

            // pnlNote
            this.pnlNote.Controls.Add(this.txtNote);
            this.pnlNote.Controls.Add(this.lblNote);
            this.pnlNote.Dock = DockStyle.Top;
            this.pnlNote.Location = new Point(10, 130);
            this.pnlNote.Name = "pnlNote";
            this.pnlNote.Padding = new Padding(0, 5, 0, 5);
            this.pnlNote.Size = new Size(656, 70);
            this.pnlNote.TabIndex = 1;

            // lblNote
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new Point(0, 5);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(36, 16);
            this.lblNote.TabIndex = 0;
            this.lblNote.Text = "Note";

            // txtNote
            this.txtNote.Dock = DockStyle.Fill;
            this.txtNote.Location = new Point(0, 24);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new Size(656, 41);
            this.txtNote.TabIndex = 1;

            // dgvCart
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = Color.White;
            this.dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new DataGridViewColumn[] {
                this.colCartId,
                this.colItem,
                this.colQty,
                this.colUnitPrice,
                this.colLineTotal
            });
            this.dgvCart.Dock = DockStyle.Fill;
            this.dgvCart.Location = new Point(10, 200);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowTemplate.Height = 28;
            this.dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new Size(656, 360);
            this.dgvCart.TabIndex = 2;

            // colCartId (hidden)
            this.colCartId.HeaderText = "FoodId";
            this.colCartId.Name = "colCartId";
            this.colCartId.Visible = false;

            // colItem
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;

            // colQty
            this.colQty.HeaderText = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            this.colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // colUnitPrice
            this.colUnitPrice.HeaderText = "Unit price";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            this.colUnitPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colUnitPrice.DefaultCellStyle.Format = "C2";

            // colLineTotal
            this.colLineTotal.HeaderText = "Total";
            this.colLineTotal.Name = "colLineTotal";
            this.colLineTotal.ReadOnly = true;
            this.colLineTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colLineTotal.DefaultCellStyle.Format = "C2";

            // pnlSummary
            this.pnlSummary.Controls.Add(this.btnClear);
            this.pnlSummary.Controls.Add(this.btnHold);
            this.pnlSummary.Controls.Add(this.btnCheckout);
            this.pnlSummary.Controls.Add(this.lblSubtotalCaption);
            this.pnlSummary.Controls.Add(this.lblSubtotalValue);
            this.pnlSummary.Controls.Add(this.lblDiscountCaption);
            this.pnlSummary.Controls.Add(this.numDiscount);
            this.pnlSummary.Controls.Add(this.lblTaxCaption);
            this.pnlSummary.Controls.Add(this.lblTaxValue);
            this.pnlSummary.Controls.Add(this.lblTotalCaption);
            this.pnlSummary.Controls.Add(this.lblTotalValue);
            this.pnlSummary.Dock = DockStyle.Bottom;
            this.pnlSummary.Location = new Point(10, 560);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new Padding(0, 10, 0, 0);
            this.pnlSummary.Size = new Size(656, 150);
            this.pnlSummary.TabIndex = 3;

            // lblSubtotalCaption
            this.lblSubtotalCaption.AutoSize = true;
            this.lblSubtotalCaption.Location = new Point(14, 16);
            this.lblSubtotalCaption.Name = "lblSubtotalCaption";
            this.lblSubtotalCaption.Size = new Size(56, 16);
            this.lblSubtotalCaption.TabIndex = 0;
            this.lblSubtotalCaption.Text = "Subtotal";

            // lblSubtotalValue
            this.lblSubtotalValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblSubtotalValue.Location = new Point(330, 16);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new Size(160, 23);
            this.lblSubtotalValue.TabIndex = 1;
            this.lblSubtotalValue.Text = "$0.00";
            this.lblSubtotalValue.TextAlign = ContentAlignment.MiddleRight;

            // lblDiscountCaption
            this.lblDiscountCaption.AutoSize = true;
            this.lblDiscountCaption.Location = new Point(14, 48);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new Size(82, 16);
            this.lblDiscountCaption.TabIndex = 2;
            this.lblDiscountCaption.Text = "Discount (%)";

            // numDiscount
            ((ISupportInitialize)(this.numDiscount)).BeginInit();
            this.numDiscount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.numDiscount.Increment = 5;
            this.numDiscount.Location = new Point(438, 45);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new Size(80, 22);
            this.numDiscount.TabIndex = 3;
            this.numDiscount.TextAlign = HorizontalAlignment.Right;
            ((ISupportInitialize)(this.numDiscount)).EndInit();

            // lblTaxCaption
            this.lblTaxCaption.AutoSize = true;
            this.lblTaxCaption.Location = new Point(14, 80);
            this.lblTaxCaption.Name = "lblTaxCaption";
            this.lblTaxCaption.Size = new Size(64, 16);
            this.lblTaxCaption.TabIndex = 5;
            this.lblTaxCaption.Text = "VAT (7%)";

            // lblTaxValue
            this.lblTaxValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblTaxValue.Location = new Point(330, 79);
            this.lblTaxValue.Name = "lblTaxValue";
            this.lblTaxValue.Size = new Size(160, 23);
            this.lblTaxValue.TabIndex = 6;
            this.lblTaxValue.Text = "$0.00";
            this.lblTaxValue.TextAlign = ContentAlignment.MiddleRight;

            // lblTotalCaption
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.lblTotalCaption.Location = new Point(14, 112);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new Size(61, 20);
            this.lblTotalCaption.TabIndex = 7;
            this.lblTotalCaption.Text = "Total";

            // lblTotalValue
            this.lblTotalValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblTotalValue.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.lblTotalValue.ForeColor = Color.IndianRed;
            this.lblTotalValue.Location = new Point(330, 106);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new Size(160, 29);
            this.lblTotalValue.TabIndex = 8;
            this.lblTotalValue.Text = "$0.00";
            this.lblTotalValue.TextAlign = ContentAlignment.MiddleRight;

            // btnClear
            this.btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClear.Location = new Point(442, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(80, 32);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;

            // btnHold
            this.btnHold.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnHold.Location = new Point(528, 12);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new Size(55, 32);
            this.btnHold.TabIndex = 10;
            this.btnHold.Text = "Hold";
            this.btnHold.UseVisualStyleBackColor = true;

            // btnCheckout
            this.btnCheckout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnCheckout.BackColor = Color.IndianRed;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = FlatStyle.Flat;
            this.btnCheckout.ForeColor = Color.White;
            this.btnCheckout.Location = new Point(589, 12);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new Size(67, 32);
            this.btnCheckout.TabIndex = 11;
            this.btnCheckout.Text = "Pay";
            this.btnCheckout.UseVisualStyleBackColor = false;

            // frmOrder (form)
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1200, 720);
            this.Controls.Add(this.splitContainerMain);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "frmOrder";
            this.Text = "frmOrder";

            // Resume layout sequence
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);

            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.pnlMenuSearch.ResumeLayout(false);
            this.pnlMenuSearch.PerformLayout();
            ((ISupportInitialize)(this.dgvMenu)).EndInit();

            this.pnlOrder.ResumeLayout(false);
            this.pnlOrderTop.ResumeLayout(false);
            this.pnlOrderTop.PerformLayout();

            this.pnlNote.ResumeLayout(false);
            this.pnlNote.PerformLayout();
            ((ISupportInitialize)(this.dgvCart)).EndInit();

            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            ((ISupportInitialize)(this.numDiscount)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerMain;

        // Left menu controls
        private Panel pnlMenu;
        private Panel pnlMenuSearch;
        private TextBox txtSearchFood;
        private ComboBox cboCategory;
        private Label lblMenu;
        private DataGridView dgvMenu;
        private DataGridViewTextBoxColumn colMenuId;
        private DataGridViewTextBoxColumn colFoodName;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colFoodPrice;

        // Right order controls
        private Panel pnlOrder;
        private Panel pnlOrderTop;
        private Label lblTitle;
        private Label lblOrderNo;
        private TextBox txtOrderNo;
        private Label lblTable;
        private ComboBox cboTable;
        private Label lblOrderType;
        private ComboBox cboOrderType;
        private Label lblCustomer;
        private TextBox txtCustomerName;
        private Label lblCase;
        private ComboBox cboCase;
        private Label lblPayMethod;
        private ComboBox cboPayMethod;

        private Panel pnlNote;
        private Label lblNote;
        private TextBox txtNote;

        private DataGridView dgvCart;
        private DataGridViewTextBoxColumn colCartId;
        private DataGridViewTextBoxColumn colItem;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colUnitPrice;
        private DataGridViewTextBoxColumn colLineTotal;

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
        private Button btnHold;
        private Button btnCheckout;
    }
}
