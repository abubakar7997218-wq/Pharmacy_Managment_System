using Application.BL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Application
{
    public partial class StockAlertForm : Form
    {
        StockAlertBLL bll = new StockAlertBLL();

        public StockAlertForm()
        {
            InitializeComponent();
        }

        private void StockAlertForm_Load(object sender, EventArgs e)
        {
            LoadLabels();
            LoadSummaryChart();
            LoadCategories();
            LoadStatus();
            LoadAllMedicines();
        }
        private void LoadLabels()
        {
            lblLowStock.Text = bll.GetLowStockCount().ToString();
            lblOutStock.Text = bll.GetOutStockCount().ToString();
            lblHighStock.Text = bll.GetHighStockCount().ToString();
            lblTotalMedicine.Text = bll.GetTotalMedicineCount().ToString();
            lblSupplier.Text = bll.GetSupplierCount().ToString();
        }

        private void LoadAllMedicines()
        {
            dgvStock.DataSource = bll.SearchMedicines("", 0, "All");
        }

        private void LoadCategories()
        {
            cmbCategory.DataSource = bll.GetCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
        }

        private void LoadStatus()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All");
            cmbStatus.Items.Add("Low Stock");
            cmbStatus.Items.Add("Out Of Stock");
            cmbStatus.Items.Add("High Stock");
            cmbStatus.Items.Add("Normal");
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadSummaryChart()
        {
            chart1.Series.Clear();
            chart1.Series.Add("Stock");
            chart1.Series["Stock"].ChartType = SeriesChartType.Doughnut;
            chart1.Series["Stock"].IsValueShownAsLabel = false;
            chart1.Series["Stock"]["PieLabelStyle"] = "Disabled";
            chart1.Series["Stock"].SmartLabelStyle.Enabled = false;

            chart1.Series["Stock"].Points.AddXY("Low Stock", bll.GetLowStockCount());
            chart1.Series["Stock"].Points.AddXY("Out Of Stock", bll.GetOutStockCount());
            chart1.Series["Stock"].Points.AddXY("High Stock", bll.GetHighStockCount());
            chart1.Series["Stock"].Points.AddXY("Normal", bll.GetNormalStockCount());

            chart1.Series["Stock"].Points[0].Color = Color.Gold;
            chart1.Series["Stock"].Points[1].Color = Color.Red;
            chart1.Series["Stock"].Points[2].Color = Color.RoyalBlue;
            chart1.Series["Stock"].Points[3].Color = Color.LimeGreen;

            chart1.Legends.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int categoryID = cmbCategory.SelectedValue != null
                ? Convert.ToInt32(cmbCategory.SelectedValue)
                : 0;
            DataTable dt = bll.SearchStock(txtSearch.Text.Trim(), categoryID);

            string status = cmbStatus.Text;

            if (status != "All")
            {
                string v = "";

                if (status == "Low Stock")
                {
                    v = "StockQty <= MinStockLevel AND StockQty > 0";
                }
                else if (status == "Out Of Stock")
                {
                    v = "StockQty = 0";
                }
                else if (status == "High Stock")
                {
                    v = "StockQty >= 100";
                }
                else if (status == "Normal")
                {
                    v = "StockQty > MinStockLevel AND StockQty < 100";
                }
            }

            dgvStock.DataSource = dt;
        }

        private void btnLowStock_Click(object sender, EventArgs e)
            => dgvStock.DataSource = bll.GetLowStockMedicines();

        private void btnOutStock_Click(object sender, EventArgs e)
            => dgvStock.DataSource = bll.GetOutOfStockMedicines();

        private void btnHighStock_Click(object sender, EventArgs e)
            => dgvStock.DataSource = bll.GetHighStockMedicines();

        private void btnNormal_Click(object sender, EventArgs e)
            => dgvStock.DataSource = bll.GetNormalStockMedicines();

        private void btnAllalerts_Click(object sender, EventArgs e)
            => dgvStock.DataSource = bll.GetAllAlerts();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllMedicines();
            LoadLabels();        
            LoadSummaryChart(); 
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This feature will be available in a future update.",
                "Coming Soon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}