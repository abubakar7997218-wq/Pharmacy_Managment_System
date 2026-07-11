using Application.BL;
using Application.DL;
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
    public partial class forgetpass : Form
    {
        public forgetpass()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            panelReset.Visible = false;
            

            UserBLL bll = new UserBLL();

            bool verified = bll.VerifyIdentity(
                txtUsername.Text.Trim(),
                txtCNIC.Text.Trim());

            

            if (verified)
            {   panelVerify.Visible = false;
                panelReset.Visible = true;
            }
            else
            {
                MessageBox.Show("Username/Email or CNIC is incorrect.");
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            UserDAL dal = new UserDAL();

            if (dal.UpdatePassword(txtUsername.Text.Trim(), txtNewPassword.Text))
            {
                MessageBox.Show("Password updated successfully.");

                login frm = new login();
                frm.Show();

                this.Close();
            }
        }
    }
    
}
