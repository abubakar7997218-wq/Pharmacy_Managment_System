using Application.BL;
using Guna.UI2.WinForms;
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
    public partial class SearchMedicineForm : Form
    {
        searchmedicineBLL bll = new searchmedicineBLL();

        public SearchMedicineForm()
        {
            InitializeComponent();
        }

        private void SearchMedicineForm_Load(object sender, EventArgs e)
        {
            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Text = "Select Status";

            cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Text = "Select Category";

            cmbCompany.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCompany.Text = "Select Company";

            dgvMedicines.DataSource = bll.GetAllMedicines();

            if (login.UserRole == "Admin")
                AddDeleteButton();
        }

        private void LoadMedicines()
        {
            dgvMedicines.DataSource = bll.SearchMedicine(
                txtSearch.Text.Trim(),
                cmbCategory.Text,
                cmbCompany.Text,
                cmbStatus.Text);
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

        private void dgvMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvMedicines.Columns[e.ColumnIndex].Name == "Delete")
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this medicine?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int medicineID = Convert.ToInt32(
                        dgvMedicines.Rows[e.RowIndex].Cells["MedicineID"].Value);

                    bool deleted = bll.DeleteMedicine(medicineID, false);

                    if (deleted)
                    {
                        MessageBox.Show("Medicine Deleted Successfully");
                        dgvMedicines.DataSource = bll.GetAllMedicines();
                        AddDeleteButton();
                    }
                }
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadMedicines();
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e) => LoadMedicines();
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e) => LoadMedicines();
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) => LoadMedicines();
    }
}
