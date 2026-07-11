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
using System.Windows.Forms.DataVisualization.Charting;

namespace Application
{
    public partial class ExpiryAlertForm : Form
    {
        ExpiryAlertBLL bll = new ExpiryAlertBLL();

        public ExpiryAlertForm()
        {
            InitializeComponent();
        }

        private void ExpiryAlertForm_Load(object sender, EventArgs e)
        {
            comboGraph.DrawMode = DrawMode.OwnerDrawFixed;
            comboGraph.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbSupplier.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadCategories();
            LoadSuppliers();
            LoadData();
            LoadSummary();
            LoadChart();
        }

        private void LoadData()
        {
            dgvExpiry.DataSource = bll.GetExpiryMedicines();
        }

        private void LoadSummary()
        {
            int d7 = bll.Days7Count;
            int d30 = bll.Days30Count;
            int d90 = bll.Days90Count;
            int a90 = bll.Above90Count;
            int total = bll.TotalExpiryCount;

            lbl7Days.Text = d7.ToString();
            lbl30Days.Text = d30.ToString();
            lbl90Days.Text = d90.ToString();
            lblAbove90.Text = a90.ToString();
            lblTotal.Text = total.ToString();

            lbl7.Text = d7.ToString();
            lbl30.Text = d30.ToString();
            lbl90.Text = d90.ToString();
            lblA90.Text = a90.ToString();
            lblt.Text = total.ToString();
        }

        private void LoadChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Series.Add("Expiry");

            if (comboGraph.Text == "Pie")
            {
                chart1.Series["Expiry"].ChartType = SeriesChartType.Pie;
            }
            else if (comboGraph.Text == "Bar")
            {
                chart1.Series["Expiry"].ChartType = SeriesChartType.Bar;
            }
            else if (comboGraph.Text == "Line")
            {
                chart1.Series["Expiry"].ChartType = SeriesChartType.Line;
            }
            else
            {
                chart1.Series["Expiry"].ChartType = SeriesChartType.Doughnut;
            }
            chart1.Series["Expiry"].IsValueShownAsLabel = false;
            chart1.Series["Expiry"]["PieLabelStyle"] = "Disabled";

            chart1.Series["Expiry"].Points.AddXY("0-7 Days", bll.Days7Count);
            chart1.Series["Expiry"].Points.AddXY("8-30 Days", bll.Days30Count);
            chart1.Series["Expiry"].Points.AddXY("31-90 Days", bll.Days90Count);
            chart1.Series["Expiry"].Points.AddXY("90+ Days", bll.Above90Count);

            chart1.Series["Expiry"].Points[0].Color = Color.Orange;
            chart1.Series["Expiry"].Points[1].Color = Color.Red;
            chart1.Series["Expiry"].Points[2].Color = Color.Blue;
            chart1.Series["Expiry"].Points[3].Color = Color.Green;

            foreach (DataPoint p in chart1.Series["Expiry"].Points)
                p.Label = "";

            if (chart1.Series["Expiry"].ChartType == SeriesChartType.Doughnut)
                chart1.Series["Expiry"]["DoughnutRadius"] = "50";

            chart1.Legends.Clear();
        }

        private void LoadCategories()
        {
            cmbCategory.DataSource = bll.GetCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
        }

        private void LoadSuppliers()
        {
            cmbSupplier.DataSource = bll.GetSuppliers();
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSuppliers();
            LoadData();
            LoadSummary();
            LoadChart();
        }

        private void btnapply_Click(object sender, EventArgs e)
        {
            int categoryID = cmbCategory.SelectedValue != null
                ? Convert.ToInt32(cmbCategory.SelectedValue) : 0;

            int supplierID = cmbSupplier.SelectedValue != null
                ? Convert.ToInt32(cmbSupplier.SelectedValue) : 0;

            dgvExpiry.DataSource = bll.FilterExpiryMedicines(
                categoryID,
                supplierID,
                dtFrom.Value.Date,
                dtTo.Value.Date);
        }

        private void comboGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export feature coming soon!", "Coming Soon");
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e) { }
    }
}
