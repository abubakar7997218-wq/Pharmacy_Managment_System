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
    public partial class delievery : Form
    {
        StockDeliveryBLL bll = new StockDeliveryBLL();

        private int selectedDeliveryID = 0;

        public delievery()
        {
            InitializeComponent();
        }

        private void delievery_Load(object sender, EventArgs e)
        {
            cmbMedicine.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMedicine.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbSupplier.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadDashboardCards();
            LoadDeliveries();
            LoadMedicines();
            LoadSuppliers();

            if (login.UserRole == "Admin")
                AddDeleteButton();
        }

        private void LoadDashboardCards()
        {
            lblTotalDeliveries.Text = bll.TotalDeliveries.ToString();
            lblItemsReceived.Text = bll.ItemsReceived.ToString();
            lblActiveSuppliers.Text = bll.ActiveSuppliers.ToString();
        }

        private void LoadDeliveries()
        {
            dgvDeliveries.DataSource = bll.GetAllDeliveries();
        }

        private void LoadSuppliers()
        {
            cmbSupplier.DataSource = bll.GetSuppliers();
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.SelectedIndex = -1;
        }

        private void LoadMedicines()
        {
            cmbMedicine.DataSource = bll.GetMedicines();
            cmbMedicine.DisplayMember = "MedicineName";
            cmbMedicine.ValueMember = "MedicineID";
            cmbMedicine.SelectedIndex = -1;
        }

        private void CalculateTotal()
        {
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;
                grandTotal += Convert.ToDecimal(row.Cells["Total"].Value);
            }

            lblTotalAmount.Text = grandTotal.ToString("N2");
        }

        private void AddDeleteButton()
        {
            if (!dgvDeliveries.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Delete";
                btn.HeaderText = "Delete";
                btn.Text = "Delete";
                btn.UseColumnTextForButtonValue = true;
                dgvDeliveries.Columns.Add(btn);
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            decimal total =
                Convert.ToDecimal(txtQty.Text) *
                Convert.ToDecimal(txtUnitPrice.Text);

            dgvItems.Rows.Add(
                cmbMedicine.SelectedValue,
                cmbMedicine.Text,
                txtBatchNo.Text,
                dtExpiryDate.Value.ToShortDateString(),
                txtQty.Text,
                txtUnitPrice.Text,
                total);

            CalculateTotal();
        }
        private void btnSaveDelivery_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedIndex == -1)
            {
                MessageBox.Show("Select Supplier");
                return;
            }

            if (dgvItems.Rows.Count <= 1)
            {
                MessageBox.Show("Add at least one item");
                return;
            }
            int deliveryID = bll.SaveDeliveryMaster(
                Convert.ToInt32(cmbSupplier.SelectedValue),
                txtInvoiceNo.Text,
                dtDeliveryDate.Value,
                1);  

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                int medicineID = Convert.ToInt32(row.Cells["MedicineID"].Value);
                int qty = Convert.ToInt32(row.Cells["Quantity"].Value);

                bll.SaveDeliveryDetail(
                    deliveryID,
                    medicineID,
                    row.Cells["BatchNo"].Value.ToString(),
                    Convert.ToDateTime(row.Cells["ExpiryDate"].Value),
                    qty,
                    Convert.ToDecimal(row.Cells["UnitPrice"].Value));

                bll.UpdateMedicineStock(medicineID, qty);
            }

            MessageBox.Show("Delivery Saved Successfully");
            LoadDeliveries();
            LoadDashboardCards();
            btnNewDelivery.PerformClick();
        }
        private void btnNewDelivery_Click(object sender, EventArgs e)
        {
            cmbSupplier.SelectedIndex = -1;
            cmbMedicine.SelectedIndex = -1;
            txtInvoiceNo.Clear();
            txtBatchNo.Clear();
            txtQty.Clear();
            txtUnitPrice.Clear();
            dgvItems.Rows.Clear();
            lblTotalAmount.Text = "0.00";
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            dgvDeliveries.DataSource =
                bll.SearchDeliveries(txtSearchDelivery.Text.Trim());
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LoadDeliveries();
            LoadDashboardCards();
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LoadDeliveries();
        }
        private void dgvDeliveries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selectedDeliveryID = Convert.ToInt32(
                dgvDeliveries.Rows[e.RowIndex].Cells["DeliveryID"].Value);
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (selectedDeliveryID == 0)
            {
                MessageBox.Show("Select Delivery First");
                return;
            }

            DataTable dt = bll.GetDeliveryByID(selectedDeliveryID);

            if (dt.Rows.Count == 0) return;

            cmbSupplier.SelectedValue = dt.Rows[0]["SupplierID"];
            txtInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
            dtDeliveryDate.Value = Convert.ToDateTime(dt.Rows[0]["DeliveryDate"]);

            dgvItems.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                dgvItems.Rows.Add(
                    row["MedicineID"],
                    "",
                    row["BatchNumber"],
                    row["ExpiryDate"],
                    row["QtyReceived"],
                    row["UnitCost"],
                    Convert.ToDecimal(row["QtyReceived"]) *
                    Convert.ToDecimal(row["UnitCost"]));
            }

            CalculateTotal();
        }
        private void dgvDeliveries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDeliveries.Columns[e.ColumnIndex].Name != "Delete") return;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this delivery?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            int deliveryID = Convert.ToInt32(
                dgvDeliveries.Rows[e.RowIndex].Cells["DeliveryID"].Value);

            if (bll.DeleteDelivery(deliveryID))
            {
                MessageBox.Show("Delivery Deleted Successfully");
                LoadDeliveries();
                LoadDashboardCards();
            }
        }

        private void guna2HtmlLabel17_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel18_Click(object sender, EventArgs e) { }
    }
}
