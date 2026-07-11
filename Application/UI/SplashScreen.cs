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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.Opacity = 0;
        }

        int dots = 1;
        int loading;
        
        int textSpeed = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            loading ++;
            guna2ProgressBar1.Value = loading;

           lblLoading.Text = "Initializing System... " + loading + "%";


            if (loading >= 100)
            {
                timer1.Stop();
                login ln = new login();
                ln.Show();


                this.Hide();
            }
        }

        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.05;
            }
            else
            {
                fadeTimer.Stop();
                timer1.Start(); 
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            
            fadeTimer.Start(); 
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}