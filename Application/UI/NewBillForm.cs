using Application.BL;
using Application.DL;
using Mysqlx.Expr;
using Org.BouncyCastle.Pqc.Crypto.Lms;
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
    public partial class NewBillForm : Form
    {
        public NewBillForm()
        {
            InitializeComponent();
        }
        private void CalculateBill()
        {
            decimal subTotal = 0;

            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    subTotal += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }

            CalculateBill(subTotal);  
        }
        private void CalculateBill(decimal subTotal, decimal extraDiscount = 0)
        {
            decimal discounted = subTotal - extraDiscount;
            decimal tax = discounted * 0.17m;
            decimal grandTotal = discounted + tax;

            lblSubTotal.Text = subTotal.ToString("N2");
            lblTax.Text = tax.ToString("N2");
            lblGrandTotal.Text = grandTotal.ToString("N2");
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            billBLL bll = new billBLL();

            DataTable dt = bll.SearchMedicine(txtMedicine.Text.Trim());

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                dgvBill.Rows.Add(
                    row["MedicineID"],
                    row["MedicineName"],
                    row["Brand"],
                    row["BatchNumber"],
                    Convert.ToDateTime(row["ExpiryDate"]).ToString("dd/MM/yyyy"),
                    row["UnitPrice"],   
                    row["UnitPrice"],   
                    1,                  
                    0,                  
                    row["UnitPrice"],  
                    "Delete"           
                );

                CalculateBill();
            }
            else
            {
                MessageBox.Show("Medicine not found.");
            }
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            billBLL bll = new billBLL();

            int billID = bll.SaveBill(
                txtCustomer.Text,
                txtPhone.Text,
                Convert.ToDecimal(lblSubTotal.Text),
                0,
                Convert.ToDecimal(lblTax.Text),
                Convert.ToDecimal(lblGrandTotal.Text));

            if (billID == -1)
                return; 

            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["MedicineID"].Value == null) continue;

                bll.SaveBillItem(
                    billID,
                    Convert.ToInt32(row.Cells["MedicineID"].Value),
                    Convert.ToInt32(row.Cells["Qty"].Value),
                    Convert.ToDecimal(row.Cells["Price"].Value),
                    Convert.ToDecimal(row.Cells["Discount"].Value),
                    Convert.ToDecimal(row.Cells["Total"].Value)
                );
            }

            MessageBox.Show("Bill Generated Successfully");
        }

        private void NewBillForm_Load(object sender, EventArgs e)
        {
            if (dgvBill.Columns.Count == 0)
            {
                dgvBill.Columns.Add("MedicineID", "MedicineID");
                dgvBill.Columns.Add("Medicine", "Medicine");
                dgvBill.Columns.Add("Company", "Company");
                dgvBill.Columns.Add("BatchNo", "Batch No");
                dgvBill.Columns.Add("Expiry", "Expiry");
                dgvBill.Columns.Add("MRP", "MRP");
                dgvBill.Columns.Add("Price", "Price");
                dgvBill.Columns.Add("Qty", "Qty");
                dgvBill.Columns.Add("Discount", "Discount");
                dgvBill.Columns.Add("Total", "Total");
                dgvBill.Columns.Add("Action", "Action");
            }

            CalculateBill();
        }

        private void dgvBill_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                CalculateBill();
            }));
        }
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            txtMedicine.Clear();
            txtCustomer.Clear();
            txtPhone.Clear();

            dgvBill.Rows.Clear();

            lblSubTotal.Text = "0";
            lblTax.Text = "0";
            lblGrandTotal.Text = "0";

            MessageBox.Show("Bill Cancelled");
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This feature will be available in the next update.",
                "Coming Soon");
        }
    }
}
