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
    public partial class DashboardForm : Form
    {
        StaffDsahboardBLL bll = new StaffDsahboardBLL();
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadDashboardCards();
            LoadRecentBills();
            LoadSalesChart();

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel5.FillColor = Color.FromArgb(221, 247, 247);
        }

        private void guna2CirclePictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void panelDashboard_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadDashboardCards()
        {
            lblAvailableMedicines.Text =
                bll.GetAvailableMedicines()
                .ToString();

            lblLowStock.Text =
                bll.GetLowStockCount()
                .ToString();

            lblExpiry.Text =
                bll.GetExpiryCount()
                .ToString();

            lblTodaySales.Text =
                "Rs " +
                bll.GetTodaySales()
                .ToString("N0");
        }
        private void LoadRecentBills()
        {
            dgvRecentBills.DataSource =
                bll.GetRecentBills();
        }
        private void LoadSalesChart()
        {
            chart1.Series.Clear();

            chart1.Series.Add("Sales");

            DataTable dt =
                bll.GetWeeklySales();

            foreach (DataRow row in dt.Rows)
            {
                chart1.Series["Sales"]
                    .Points.AddXY(
                    row["DayName"].ToString(),
                    Convert.ToDecimal(
                        row["TotalSales"]));
            }
        }
    }
}
