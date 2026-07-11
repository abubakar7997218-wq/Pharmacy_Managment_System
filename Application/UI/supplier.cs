using Application.BL;
using Application.DL;
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

namespace Application
{
    public partial class supplier : Form
    {
        suppliersBLL bll = new suppliersBLL();

        private int supplierID = 0;

        public supplier()
        {
            InitializeComponent();
        }

        private void supplier_Load(object sender, EventArgs e)
        {
            cmbS.DrawMode = DrawMode.OwnerDrawFixed;
            cmbS.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbcat.DrawMode = DrawMode.OwnerDrawFixed;
            cmbcat.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbSupplier.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            if (login.UserRole == "Admin")
                AddDeleteButton();

            LoadSuppliers();
            LoadDashboardCards();
            LoadStatus();
            LoadSupplierNames();
        }

        private void LoadSuppliers()
        {
            dgvSuppliers.DataSource = bll.GetAllSuppliers();
        }

        private void LoadDashboardCards()
        {
            lblTotalSuppliers.Text = bll.TotalSuppliers.ToString();
            lblActiveSuppliers.Text = bll.ActiveSuppliers.ToString();
            lblNewThisMonth.Text = bll.NewThisMonth.ToString();
        }

        private void LoadStatus()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.SelectedIndex = -1;
        }

        private void LoadSupplierNames()
        {
            cmbSupplier.DataSource = bll.GetSupplierNames();
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
        }

        private void ClearFields()
        {
            txtSupplierName.Clear();
            txtContactName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();

            cmbStatus.SelectedIndex = -1;
            supplierID = 0;
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (dgvSuppliers.Rows[e.RowIndex].Cells["SupplierID"].Value == DBNull.Value)
                    return;

                supplierID = Convert.ToInt32(
                    dgvSuppliers.Rows[e.RowIndex].Cells["SupplierID"].Value);

                txtSupplierName.Text = dgvSuppliers.Rows[e.RowIndex].Cells["SupplierName"].Value?.ToString() ?? "";
                txtContactName.Text = dgvSuppliers.Rows[e.RowIndex].Cells["ContactName"].Value?.ToString() ?? "";
                txtPhone.Text = dgvSuppliers.Rows[e.RowIndex].Cells["Phone"].Value?.ToString() ?? "";
                txtEmail.Text = dgvSuppliers.Rows[e.RowIndex].Cells["Email"].Value?.ToString() ?? "";
                txtAddress.Text = dgvSuppliers.Rows[e.RowIndex].Cells["Address"].Value?.ToString() ?? "";
                cmbStatus.Text = dgvSuppliers.Rows[e.RowIndex].Cells["Status"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null)
            {
                MessageBox.Show("Please Select Supplier");
                return;
            }

            dgvSuppliers_CellClick(dgvSuppliers,
                new DataGridViewCellEventArgs(0, dgvSuppliers.CurrentRow.Index));
        }
        private void btnadds_Click(object sender, EventArgs e)
        {
            try
            {
                bool active = cmbStatus.Text == "Active";

                bool result = bll.AddSupplier(
                    txtSupplierName.Text,
                    txtContactName.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtAddress.Text,
                    active);

                if (result)
                {
                    MessageBox.Show("Supplier Added Successfully");
                    LoadSuppliers();
                    LoadDashboardCards();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed To Add Supplier");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            bool active = cmbStatus.Text == "Active";

            bool result = bll.UpdateSupplier(
                supplierID,
                txtSupplierName.Text,
                txtContactName.Text,
                txtPhone.Text,
                txtEmail.Text,
                txtAddress.Text,
                active);

            if (result)
            {
                MessageBox.Show("Supplier Updated Successfully");
                LoadSuppliers();
                LoadDashboardCards();
            }
            else
            {
                MessageBox.Show("Update Failed");
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvSuppliers.DataSource = bll.SearchSuppliers(
                txtSearch.Text.Trim(),
                cmbS.Text,
                cmbSupplier.Text);
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
            LoadDashboardCards();
            ClearFields();
            txtSearch.Clear();
            cmbS.SelectedIndex = -1;
        }

        private void AddDeleteButton()
        {
            if (!dgvSuppliers.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Delete";
                btn.HeaderText = "Delete";
                btn.Text = "Delete";
                btn.UseColumnTextForButtonValue = true;

                dgvSuppliers.Columns.Add(btn);
            }
        }

        private void dgvSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSuppliers.Columns[e.ColumnIndex].Name != "Delete")
                return;

            int id = Convert.ToInt32(
                dgvSuppliers.Rows[e.RowIndex].Cells["SupplierID"].Value);

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this supplier?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            if (bll.IsSupplierUsed(id))
            {
                DialogResult deactivate = MessageBox.Show(
                    "Supplier is linked with medicines.\nDo you want to make it inactive?",
                    "Supplier Linked",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (deactivate == DialogResult.Yes)
                {
                    if (bll.DeactivateSupplier(id))
                    {
                        MessageBox.Show("Supplier marked as Inactive");
                        LoadSuppliers();
                        LoadDashboardCards();
                    }
                }

                return;
            }

            if (bll.DeleteSupplier(id))
            {
                MessageBox.Show("Supplier Deleted Successfully");
                LoadSuppliers();
                LoadDashboardCards();
            }
        }
    }



}
