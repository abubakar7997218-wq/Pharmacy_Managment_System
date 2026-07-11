using Application.BL;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Application
{
    public partial class DailyReportForm : Form
    {
        DailyReportBLL bll = new DailyReportBLL();

        public DailyReportForm()
        {
            InitializeComponent();
        }

        private void DailyReportForm_Load(object sender, EventArgs e)
        {
            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbPayment.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPayment.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCustomer.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;

            chartPayment.Series.Clear();

            LoadAll();
        }
        private void LoadAll()
        {
            LoadDashboard();
            LoadSales();
            LoadSalesTrend();
            LoadPaymentChart();
            LoadPayments();
            LoadCustomers();
            LoadStatuses();
        }

        private void LoadDashboard()
        {
           
            lblSales.Text = "Rs " + bll.TotalSales.ToString("N0");
            lblBills.Text = bll.TotalBills.ToString();
            lblItems.Text = bll.ItemsSold.ToString();
            lblCustomers.Text = bll.CustomerCount.ToString();
            lblAverage.Text = "Rs " + bll.AverageBill.ToString("N0");
        }

        private void LoadSales()
        {
            dgvSales.DataSource = bll.GetSalesDetails();
        }

        private void LoadSalesTrend()
        {
            DataTable dt = bll.GetSalesTrend();

            chartSales.Series.Clear();
            chartSales.Series.Add("Sales");

            if (cmbChart.Text == "Line")
            {
                chartSales.Series["Sales"].ChartType = SeriesChartType.Line;
            }
            else if (cmbChart.Text == "Bar")
            {
                chartSales.Series["Sales"].ChartType = SeriesChartType.Column;
            }
            else if (cmbChart.Text == "Pie")
            {
                chartSales.Series["Sales"].ChartType = SeriesChartType.Pie;
            }
            else
            {
                chartSales.Series["Sales"].ChartType = SeriesChartType.Line;
            }

            foreach (DataRow row in dt.Rows)
                chartSales.Series["Sales"].Points.AddXY(
                    row["HourNo"], row["Sales"]);
        }

        private void LoadPaymentChart()
        {
            DataTable dt = bll.GetPaymentSummary();

            chartPayment.Series.Clear();
            chartPayment.Series.Add("Payment");
            chartPayment.Series["Payment"].ChartType = SeriesChartType.Doughnut;
            chartPayment.Series["Payment"].IsValueShownAsLabel = false;
            chartPayment.Series["Payment"]["PieLabelStyle"] = "Disabled";
            chartPayment.Series["Payment"]["DoughnutRadius"] = "55";

            foreach (DataRow row in dt.Rows)
                chartPayment.Series["Payment"].Points.AddXY(
                    row["PaymentMethod"], row["Amount"]);

            chartPayment.Legends.Clear();
        }

        private void LoadPayments()
        {
            cmbPayment.DataSource = bll.GetPaymentMethods();
            cmbPayment.DisplayMember = "PaymentMethod";
            cmbPayment.ValueMember = "PaymentMethod";
            cmbPayment.SelectedIndex = -1;
        }

        private void LoadCustomers()
        {
            cmbCustomer.DataSource = bll.GetCustomers();
            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.ValueMember = "CustomerName";
            cmbCustomer.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            cmbStatus.DataSource = bll.GetStatuses();
            cmbStatus.DisplayMember = "Status";
            cmbStatus.ValueMember = "Status";
            cmbStatus.SelectedIndex = -1;
        }
        private void comboGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesTrend();
        }
        private void btnapply_Click(object sender, EventArgs e)
        {
            dgvSales.DataSource = bll.GetFilteredSales(
                dtpDate.Value,
                cmbPayment.Text,
                cmbCustomer.Text,
                cmbStatus.Text);
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadAll();
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This feature will be available in a future update.",
                "Coming Soon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e) { }
    }

}
