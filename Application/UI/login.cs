using Application .BL;
using Application.DL;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
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
    public partial class login : Form
    {
        
        public static int UserID;
        public static string UserRole;
        public login()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {
            cmbRole.DrawMode = DrawMode.OwnerDrawFixed;

            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }

        private void guna2TextBox2_IconRightClick(object sender, EventArgs e)
        {
         
            txtPass.UseSystemPasswordChar =
                !txtPass.UseSystemPasswordChar;

       
            if (txtPass.UseSystemPasswordChar)
            {
              
                txtPass.IconRight = Properties.Resources.eye_Photoroom;
            }
            else
            {
                
                txtPass.IconRight = Properties.Resources.eye_open_Photoroom;
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            forgetpass fp = new forgetpass();
            fp.Show();
            this.Hide();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            string role = cmbRole.Text;

            UserBLL bll = new UserBLL();


            DataTable dt = bll.Login(
                txtName.Text.Trim(),
                txtPass.Text.Trim(),
                role);

            if (dt.Rows.Count > 0)
            {
                UserID =
                    Convert.ToInt32(
                        dt.Rows[0]["UserID"]);

                UserRole = role;

                if (role == "Admin")
                {
                    admindashboard frm =
                        new admindashboard();

                    frm.Show();
                    this.Hide();
                }
                else if (role == "Staff")
                {
                    staffdashboard frm =
                        new staffdashboard();

                    frm.Show();
                    this.Hide();
                }
                else if (role == "Supplier Manager")
                {
                    supplierdashboard s=new supplierdashboard();
                    s.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid Username, Password, or Role.");
            }
        }
        
    }
    }
    
    

