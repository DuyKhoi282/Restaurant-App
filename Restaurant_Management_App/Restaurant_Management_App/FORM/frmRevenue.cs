using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Restaurant_Management_App.FORM.frmRevenueDetail;

namespace Restaurant_Management_App.FORM
{
    public partial class frmRevenue : Form
    {
        static frmRevenueDetail currentForm;
        public frmRevenue()
        {
            InitializeComponent();
        }

        private void btnRevenueByDate_Click(object sender, EventArgs e)
        {
            if (currentForm != null && !currentForm.IsDisposed)
                currentForm.Close();

            currentForm = new frmRevenueDetail(frmRevenueDetail.ReportType.Date);
            currentForm.Show();
        }

        private void btnRevenueByMonth_Click(object sender, EventArgs e)
        {
            if (currentForm != null && !currentForm.IsDisposed)
                currentForm.Close();

            currentForm = new frmRevenueDetail(frmRevenueDetail.ReportType.Month);
            currentForm.Show();
        }
        private void btnTopFood_Click(object sender, EventArgs e)
        {
            if (currentForm != null && !currentForm.IsDisposed)
                currentForm.Close();

            currentForm = new frmRevenueDetail(frmRevenueDetail.ReportType.TopFood);
            currentForm.Show();
        }
    }
}
