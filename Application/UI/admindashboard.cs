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
    public partial class admindashboard : Form
    {
        Guna.UI2.WinForms.Guna2Button currentButton;
        public admindashboard()
        {
            InitializeComponent();
        }

        private void ActivateButton(Guna.UI2.WinForms.Guna2Button btn)
        {

            if (currentButton != null)
            {
                currentButton.FillColor = Color.White;
                currentButton.ForeColor = Color.Black;
            }


            currentButton = btn;

            currentButton.FillColor = Color.FromArgb(220, 245, 245);
            currentButton.ForeColor = Color.Teal;
        }
        private Form activeForm = null;

        public void OpenChildForm(Form childForm)
        {
            // agar koi form already open he
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(childForm);

            childForm.BringToFront();
            childForm.Show();
        }

        private void admindashboard_Load(object sender, EventArgs e)
        {
            ActivateButton(btnDashboard);
            OpenChildForm(new admdash());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(btnDashboard);
            OpenChildForm(new admdash());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ActivateButton(btnSearch);
            OpenChildForm(new SearchMedicineForm());
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            ActivateButton(btnBilling);
            OpenChildForm(new NewBillForm());
        }

        private void btnPrescription_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPrescription);
            OpenChildForm(new PrescriptionForm());
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            ActivateButton(btnStock);
            OpenChildForm(new StockAlertForm());
        }

        private void btnExpiry_Click(object sender, EventArgs e)
        {
            ActivateButton(btnExpiry);
            OpenChildForm(new ExpiryAlertForm());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(btnReport);
            OpenChildForm(new DailyReportForm());
        }

        private void btnsupplier_Click(object sender, EventArgs e)
        {
            ActivateButton(btnsupplier);
            OpenChildForm(new supplier());
        }

        private void btndelievery_Click(object sender, EventArgs e)
        {
            ActivateButton(btndelievery);
            OpenChildForm(new delievery());
        }

        private void btnuser_Click(object sender, EventArgs e)
        {
            ActivateButton(btnuser);
            OpenChildForm(new UserManage());
        }

        private void btnmed_Click(object sender, EventArgs e)
        {
            ActivateButton(btnmed);
            OpenChildForm(new MedicineRecord());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLogout);
            DialogResult result = guna2MessageDialog1.Show();

            if (result == DialogResult.Yes)
            {
                login lg = new login();
                lg.Show();
                this.Hide();
            }
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
