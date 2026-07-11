using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Application
{
    public partial class staffdashboard : Form
    {
        Guna.UI2.WinForms.Guna2Button currentButton;
        public staffdashboard()
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

        private void OpenChildForm(Form childForm)
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
       

       

        

        

        

        

        private void staffdashboard_Load(object sender, EventArgs e)
        {
            ActivateButton(btnDashboard);
            OpenChildForm(new DashboardForm());

        }

        

        

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            ActivateButton(btnDashboard);

            OpenChildForm(new DashboardForm());
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            ActivateButton(btnSearch);

            OpenChildForm(new SearchMedicineForm());
        }

        private void btnBilling_Click_1(object sender, EventArgs e)
        {
            ActivateButton(btnBilling);

            OpenChildForm(new NewBillForm());
        }

        private void btnPrescription_Click_1(object sender, EventArgs e)
        {
            ActivateButton(btnPrescription);

            OpenChildForm(new PrescriptionForm());
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnStock_Click_1(object sender, EventArgs e)
        {
            ActivateButton(btnStock);

            OpenChildForm(new StockAlertForm());
        }

        private void btnExpiry_Click_1(object sender, EventArgs e)
        {
            ActivateButton(btnExpiry);

            OpenChildForm(new ExpiryAlertForm());
        }

        private void btnReport_Click_1(object sender, EventArgs e)
        {
            ActivateButton(btnReport);

            OpenChildForm(new DailyReportForm());
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLogout);
            DialogResult result = guna2MessageDialog1.Show();

            if (result == DialogResult.Yes)
            {
                login login = new login();
                login.Show();

                this.Hide();
            }
        }
    }
}
