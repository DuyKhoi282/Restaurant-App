using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Restaurant_Management_App.FORM
{
    public partial class frmCreateOrder : Form
    {
        private readonly OrderRepository _repo = new OrderRepository();

        public frmCreateOrder()
        {
            InitializeComponent();

            // Wire minimal events used here
            this.Load += FrmOrder_Load;
            btnCheckout.Click += BtnCheckout_Click;
            btnHold.Click += BtnHold_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            LoadTables(); // populate cboTable (id,name)
        }

        private void LoadTables()
        {
            try
            {
                var tables = _repo.GetTableList();
                cboTable.DisplayMember = "Item2";
                cboTable.ValueMember = "Item1";
                cboTable.DataSource = tables;
                // If you want to show "Select table" add a dummy entry and adjust binding.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tables: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0) return;
            if (MessageBox.Show("Clear all items?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dgvCart.Rows.Clear();
            }
        }

        private void BtnHold_Click(object sender, EventArgs e)
        {
            // Save as held (status = 0)
            if (!ValidateCartAndTable(out int tableId)) return;
            var items = CollectCartItems();
            try
            {
                int billId = _repo.InsertBillWithItems(tableId, items, txtCustomerName.Text.Trim(), cboCase.Text, cboPayMethod.Text, txtNote.Text, status: 0);
                MessageBox.Show($"Order held (Bill id = {billId}).", "Held", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCart.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hold failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            // Save as paid (status = 1) — repository will set dateCheckOut
            if (!ValidateCartAndTable(out int tableId)) return;
            var items = CollectCartItems();
            try
            {
                int billId = _repo.InsertBillWithItems(tableId, items, txtCustomerName.Text.Trim(), cboCase.Text, cboPayMethod.Text, txtNote.Text, status: 1);
                MessageBox.Show($"Order saved and marked paid (Bill id = {billId}).", "Paid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCart.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Checkout failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateCartAndTable(out int tableId)
        {
            tableId = -1;
            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboTable.SelectedItem == null)
            {
                MessageBox.Show("Please select a table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // cboTable is bound to Tuple<int,string>
            var selected = cboTable.SelectedItem as Tuple<int, string>;
            if (selected == null)
            {
                MessageBox.Show("Invalid table selection.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            tableId = selected.Item1;
            return true;
        }

        /// <summary>
        /// Collects cart rows into List of (foodId, qty).
        /// Expects hidden colCartId with food id, colQty numeric, colUnitPrice currency-string.
        /// </summary>
        private List<Tuple<int, int>> CollectCartItems()
        {
            var items = new List<Tuple<int, int>>();
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;
                if (!(row.Cells["colCartId"].Value is int) && !(row.Tag is int))
                {
                    // try to parse from cell
                    int.TryParse(Convert.ToString(row.Cells["colCartId"].Value), out int maybeId);
                    if (maybeId == 0) continue;
                    items.Add(Tuple.Create(maybeId, ParseInt(row.Cells["colQty"].Value)));
                    continue;
                }

                int foodId = row.Cells["colCartId"].Value is int ? (int)row.Cells["colCartId"].Value : (int)row.Tag;
                int qty = ParseInt(row.Cells["colQty"].Value);
                if (qty <= 0) qty = 1;
                items.Add(Tuple.Create(foodId, qty));
            }
            return items;
        }

        private int ParseInt(object value)
        {
            if (value == null) return 0;
            int i;
            if (int.TryParse(Convert.ToString(value), out i)) return i;
            return 0;
        }

        // Optional: currency parsing helper if you need
        private decimal ParseCurrencyString(object value)
        {
            if (value == null) return 0m;
            if (value is decimal) return (decimal)value;
            var s = Convert.ToString(value).Trim();
            if (string.IsNullOrEmpty(s)) return 0m;
            decimal d;
            if (decimal.TryParse(s, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out d))
                return d;
            var cleaned = s.Replace(",", "").Replace("$", "").Replace("₫", "").Trim();
            if (decimal.TryParse(cleaned, System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out d))
                return d;
            return 0m;
        }
    }
}
