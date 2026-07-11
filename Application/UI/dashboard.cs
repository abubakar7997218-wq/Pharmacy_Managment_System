using Application.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application
{
    
    public partial class dashboard : Form
    {
        stockdashboardBLL bll = new stockdashboardBLL();
        public dashboard()
        {
            InitializeComponent();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            
            LoadCards();
            LoadLowStock();
            LoadRecentDeliveries();
            LoadDeliveryChart();
        }
        private void LoadCards()
        {
            lblTotalDeliveries.Text =
                bll.GetTotalDeliveries()
                .ToString();

            lblItemsDelivered.Text =
                bll.GetTotalItemsDelivered()
                .ToString();

            lblSuppliers.Text =
                bll.GetTotalSuppliers()
                .ToString();

            lblTotalValue.Text =
                "Rs " +
                bll.GetTotalDeliveryValue()
                .ToString("N0");
        }
        private void LoadLowStock()
        {
            dgvLowStock.DataSource =
                bll.GetLowStockMedicines();
        }
        private void LoadRecentDeliveries()
        {
            dgvRecentDeliveries.DataSource =
                bll.GetRecentDeliveries();
        }
        private void LoadDeliveryChart()
        {
            chart1.Series.Clear();

            chart1.Series.Add(
                "Deliveries");

            DataTable dt =
                bll.GetMonthlyDeliveries();

            foreach (DataRow row in dt.Rows)
            {
                chart1.Series["Deliveries"]
                    .Points.AddXY(
                    row["MonthName"],
                    row["TotalDeliveries"]);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            LoadCards();
            LoadLowStock();
            LoadRecentDeliveries();
            LoadDeliveryChart();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            ((supplierdashboard)this.ParentForm)
         .OpenChildForm(new supplier());
        }

        private void btnsupplier_Click(object sender, EventArgs e)
        {
            ((supplierdashboard)this.ParentForm)
        .OpenChildForm(new supplier());
        }
    }
}
