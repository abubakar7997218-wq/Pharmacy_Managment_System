using Application.BL;
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
    public partial class MedicineRecord : Form
    {
        MedicineBLL bll = new MedicineBLL();

        int selectedMedicineID = 0;

        public MedicineRecord()
        {
            InitializeComponent();
        }

        private void MedicineRecord_Load(object sender, EventArgs e)
        {
            SetComboStyles();

            if (login.UserRole == "Admin")
                AddDeleteButton();

            txtStockQty.Text = "0";

            LoadAll();
        }

        private void SetComboStyles()
        {
            cmbSearchCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbSearchStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSearchStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbSupplier.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LoadAll()
        {
            LoadMedicines();
            LoadDashboardCards();
            LoadCategories();
            LoadSuppliers();
            LoadCategoryChart();
        }

        private void LoadDashboardCards()
        {
            lblTotalMedicines.Text = bll.TotalMedicines.ToString();
            lblInStock.Text = bll.InStockMedicines.ToString();
            lblLowStock.Text = bll.LowStockMedicines.ToString();
            lblOutOfStock.Text = bll.OutOfStockMedicines.ToString();
            lblExpired.Text = bll.ExpiredMedicines.ToString();
        }

        private void LoadMedicines()
        {
            dgvMedicines.DataSource = bll.GetAllMedicines();
        }

        private void LoadCategories()
        {
            cmbSearchCategory.DataSource = bll.GetCategories();
            cmbSearchCategory.DisplayMember = "CategoryName";
            cmbSearchCategory.ValueMember = "CategoryID";
            cmbSearchCategory.SelectedIndex = -1;

            cmbCategory.DataSource = bll.GetCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.SelectedIndex = -1;
        }

        private void LoadSuppliers()
        {
            cmbSupplier.DataSource = bll.GetSuppliers();
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.SelectedIndex = -1;
        }

        private void LoadCategoryChart()
        {
            try
            {
                DataTable dt = bll.GetCategoryOverview();

                chart1.Series.Clear();
                chart1.Titles.Clear();

                Series series = new Series("Categories");
                series.ChartType = SeriesChartType.Doughnut;
                series.IsValueShownAsLabel = false;
                series["PieLabelStyle"] = "Disabled";

                foreach (DataRow row in dt.Rows)
                    series.Points.AddXY(
                        row["CategoryName"].ToString(),
                        Convert.ToInt32(row["TotalMedicines"]));

                chart1.Series.Add(series);
                chart1.Legends[0].Enabled = true;
                chart1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearFields()
        {
            selectedMedicineID = 0;

            txtMedicineName.Clear();
            txtSalt.Clear();
            txtPurchasePrice.Clear();
            txtSellingPrice.Clear();
            txtUnitPrice.Clear();
            txtStockQty.Text = "0";
            txtReorderLevel.Clear();

            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;

            dtExpiry.Value = DateTime.Today;
        }

        private void AddDeleteButton()
        {
            if (!dgvMedicines.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Delete";
                btn.HeaderText = "Delete";
                btn.Text = "Delete";
                btn.UseColumnTextForButtonValue = true;
                dgvMedicines.Columns.Add(btn);
            }
        }

        private void dgvMedicines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvMedicines.Rows[e.RowIndex];

            selectedMedicineID = Convert.ToInt32(row.Cells["MedicineID"].Value);
            txtMedicineName.Text = Convert.ToString(row.Cells["MedicineName"].Value);
            txtSalt.Text = Convert.ToString(row.Cells["GenericName"].Value);
            txtPurchasePrice.Text = Convert.ToString(row.Cells["PurchasePrice"].Value);
            txtSellingPrice.Text = Convert.ToString(row.Cells["SellingPrice"].Value);
            txtUnitPrice.Text = Convert.ToString(row.Cells["UnitPrice"].Value);
            txtStockQty.Text = Convert.ToString(row.Cells["StockQty"].Value);
            txtReorderLevel.Text = Convert.ToString(row.Cells["MinStockLevel"].Value);
            dtExpiry.Value = Convert.ToDateTime(row.Cells["ExpiryDate"].Value);
            cmbCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryID"].Value);
            cmbSupplier.SelectedValue = Convert.ToInt32(row.Cells["SupplierID"].Value);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(txtStockQty.Text);

            bool result = bll.AddMedicine(
                Convert.ToInt32(cmbCategory.SelectedValue),
                Convert.ToInt32(cmbSupplier.SelectedValue),
                txtMedicineName.Text,
                txtSalt.Text,
                Convert.ToDecimal(txtPurchasePrice.Text),
                Convert.ToDecimal(txtSellingPrice.Text),
                Convert.ToDecimal(txtUnitPrice.Text),
                qty,
                Convert.ToInt32(txtReorderLevel.Text),
                dtExpiry.Value,
                qty > 0);  

            if (result)
            {
                MessageBox.Show("Medicine Saved Successfully");
                LoadMedicines();
                LoadDashboardCards();
                LoadCategoryChart();
                ClearFields();
            }
        }


        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedicineName.Text) ||
                string.IsNullOrWhiteSpace(txtSalt.Text) ||
                string.IsNullOrWhiteSpace(txtPurchasePrice.Text) ||
                string.IsNullOrWhiteSpace(txtSellingPrice.Text) ||
                string.IsNullOrWhiteSpace(txtUnitPrice.Text) ||
                string.IsNullOrWhiteSpace(txtReorderLevel.Text) ||
                cmbCategory.SelectedIndex == -1 ||
                cmbSupplier.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            int qty = Convert.ToInt32(txtStockQty.Text);

            bool result = bll.UpdateMedicine(
                selectedMedicineID,
                Convert.ToInt32(cmbCategory.SelectedValue),
                Convert.ToInt32(cmbSupplier.SelectedValue),
                txtMedicineName.Text,
                txtSalt.Text,
                Convert.ToDecimal(txtPurchasePrice.Text),
                Convert.ToDecimal(txtSellingPrice.Text),
                Convert.ToDecimal(txtUnitPrice.Text),
                qty,
                Convert.ToInt32(txtReorderLevel.Text),
                dtExpiry.Value,
                qty > 0);

            if (result)
            {
                MessageBox.Show("Medicine Updated Successfully");
                LoadMedicines();
                LoadDashboardCards();
                LoadCategoryChart();
                ClearFields();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string category = cmbSearchCategory.SelectedIndex != -1
                ? cmbSearchCategory.Text : "";

            string status = (cmbSearchStatus.SelectedIndex != -1 &&
                             cmbSearchStatus.Text != "All")
                ? cmbSearchStatus.Text : "";

            dgvMedicines.DataSource = bll.SearchMedicines(
                txtSearch.Text.Trim(), category, status);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
            => dgvMedicines.DataSource = bll.GetLowStockMedicinesList();

        private void guna2Button6_Click(object sender, EventArgs e)
            => dgvMedicines.DataSource = bll.GetExpiredMedicinesList();

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            LoadAll();
            ClearFields();
            cmbSearchCategory.SelectedIndex = -1;
            cmbSearchStatus.SelectedIndex = -1;
            txtSearch.Clear();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
            => ClearFields();

        private void dgvMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvMedicines.Columns[e.ColumnIndex].Name != "Delete") return;

            int medicineID = Convert.ToInt32(
                dgvMedicines.Rows[e.RowIndex].Cells["MedicineID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this medicine?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes && bll.DeleteMedicine(medicineID))
            {
                MessageBox.Show("Medicine Deleted Successfully");
                LoadMedicines();
                LoadDashboardCards();
                LoadCategoryChart();
            }
        }
    }
}
