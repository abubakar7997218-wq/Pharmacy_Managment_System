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
    public partial class PrescriptionForm : Form
    {
        PrescriptionBLL bll = new PrescriptionBLL();

        public PrescriptionForm()
        {
            InitializeComponent();
        }

        private void PrescriptionForm_Load(object sender, EventArgs e)
        {
            LoadData();

            if (login.UserRole == "Admin")
                AddDeleteButton();
        }

        private void LoadData()
        {
            dgvPrescription.DataSource = bll.GetAllPrescriptions();

            lblTotal.Text = bll.TotalPrescriptions().ToString();
            lblPatients.Text = bll.TotalPatients().ToString();
            lblScannedToday.Text = bll.ScannedToday().ToString();
        }

        private void AddDeleteButton()
        {
            if (!dgvPrescription.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Delete";
                btn.HeaderText = "Delete";
                btn.Text = "Delete";
                btn.UseColumnTextForButtonValue = true;

                dgvPrescription.Columns.Add(btn);
            }
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            dgvPrescription.DataSource =
                bll.SearchPrescription(TxtS.Text.Trim());
        }

        private void dgvPrescription_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPrescription.Columns[e.ColumnIndex].Name == "Delete")
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this prescription?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int prescriptionID = Convert.ToInt32(
                        dgvPrescription.Rows[e.RowIndex]
                        .Cells["PrescriptionID"].Value);

                    bool deleted = bll.DeletePrescription(prescriptionID, true);

                    if (deleted)
                        LoadData(); 
                }
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Prescription Upload feature coming soon.",
                "Coming Soon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Prescription Scanner coming soon.",
                "Coming Soon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel7_Paint(object sender, PaintEventArgs e) { }
        private void guna2Button4_Click(object sender, EventArgs e) { }
    }
}
    
    

