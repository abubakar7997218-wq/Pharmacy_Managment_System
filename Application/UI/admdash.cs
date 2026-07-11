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
    public partial class admdash : Form
    {
        
        public admdash()
        {
            InitializeComponent();
        }

        private void admdash_Load(object sender, EventArgs e)
        {
            LoadDashboard();
            LoadChart();
        }
        private void LoadDashboard()
        {
            DashboardBLL bll = new DashboardBLL();

            lblUsers.Text =
                bll.TotalUsers().ToString();

            lblMedicines.Text =
                bll.TotalMedicines().ToString();

            lblSuppliers.Text =
                bll.TotalSuppliers().ToString();

            lblLowStock.Text =
                bll.LowStock().ToString();

            lblTodaySales.Text =
                bll.TodaySales().ToString("N0");

            dgvTransactions.DataSource =
                bll.RecentTransactions();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ((admindashboard)this.ParentForm)
                .OpenChildForm(new UserManage());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ((admindashboard)this.ParentForm)
                .OpenChildForm(new MedicineRecord());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ((admindashboard)this.ParentForm)
                .OpenChildForm(new supplier());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ((admindashboard)this.ParentForm)
                .OpenChildForm(new delievery());
        }
        private void LoadChart()
        {
            DashboardBLL bll = new DashboardBLL();

            DataTable dt = bll.MonthlySales();

            chart1.Series.Clear();

            chart1.Series.Add("Sales");

            foreach (DataRow row in dt.Rows)
            {
                chart1.Series["Sales"].Points.AddXY(
                    row["MonthNo"],
                    row["Total"]);
            }
        }
    }
}