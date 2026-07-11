using Application.BL;
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
    public partial class UserManage : Form
    {
        UserManagementBLL bll = new UserManagementBLL();

        public int selectedUserID = 0;

        public UserManage()
        {
            InitializeComponent();
        }

        private void UserManage_Load(object sender, EventArgs e)
        {
            cmbStatusFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbRoleFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbRole.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            if (login.UserRole == "Admin")
                AddDeleteButton();

            LoadAll();
        }
        private void LoadAll()
        {
            LoadUsers();
            LoadDashboardCards();
            LoadRoleOverview();
            LoadRoles();
            LoadStatus();
        }

        private void LoadDashboardCards()
        {
            lblTotalUsers.Text = bll.TotalUsers.ToString();
            lblActiveUsers.Text = bll.ActiveUsers.ToString();
            lblInactiveUsers.Text = bll.InactiveUsers.ToString();
            lblRoles.Text = bll.RolesCount.ToString();
            lblPermissions.Text = "N/A";
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = bll.GetAllUsers();
        }

        private void LoadRoles()
        {
            DataTable dt = bll.GetRoles();

            cmbRole.DataSource = dt.Copy();
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "RoleID";

            cmbRoleFilter.DataSource = dt;
            cmbRoleFilter.DisplayMember = "RoleName";
            cmbRoleFilter.ValueMember = "RoleID";
        }

        private void LoadStatus()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");

            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("");
            cmbStatusFilter.Items.Add("Active");
            cmbStatusFilter.Items.Add("Inactive");
        }

        private void LoadRoleOverview()
        {
            DataTable dt = bll.GetRoleOverview();

            foreach (DataRow row in dt.Rows)
            {
                string role = row["RoleName"].ToString();
                int count = Convert.ToInt32(row["TotalUsers"]);

                if (role == "Admin")
                    lblAdminCount.Text = count.ToString();
                else if (role == "Staff")
                    lblStaffCount.Text = count.ToString();
            }
        }

        private void ClearFields()
        {
            selectedUserID = 0;

            txtFullName.Clear();
            txtUserName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            txtCNIC.Clear();

            cmbRole.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
        }

        private void AddDeleteButton()
        {
            if (!dgvUsers.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Delete";
                btn.HeaderText = "Delete";
                btn.Text = "Delete";
                btn.UseColumnTextForButtonValue = true;
                dgvUsers.Columns.Add(btn);
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            bool result = bll.AddUser(
                txtFullName.Text,
                txtUserName.Text,
                txtEmail.Text,
                txtPassword.Text,
                txtCNIC.Text,
                Convert.ToInt32(cmbRole.SelectedValue),
                cmbStatus.Text == "Active");

            if (result)
            {
                MessageBox.Show("User Added Successfully");
                LoadUsers();
                LoadDashboardCards();
                LoadRoleOverview();
                ClearFields();
            }
        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (selectedUserID == 0)
            {
                MessageBox.Show("Select a user first");
                return;
            }

            bool result = bll.UpdateUser(
                selectedUserID,
                txtFullName.Text,
                txtUserName.Text,
                txtEmail.Text,
                txtCNIC.Text,
                Convert.ToInt32(cmbRole.SelectedValue),
                cmbStatus.Text == "Active");

            if (result)
            {
                MessageBox.Show("User Updated Successfully");
                LoadUsers();
                LoadDashboardCards();
                LoadRoleOverview();
                selectedUserID = 0;
            }
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            dgvUsers.DataSource = bll.SearchUsers(
                txtSearch.Text,
                cmbRoleFilter.Text,
                cmbStatusFilter.Text);
        }
        private void btnview_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbRoleFilter.SelectedIndex = -1;
            cmbStatusFilter.SelectedIndex = -1;
            LoadUsers();
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            LoadAll();
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selectedUserID = Convert.ToInt32(
                dgvUsers.Rows[e.RowIndex].Cells["UserID"].Value);

            txtFullName.Text = dgvUsers.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
            txtUserName.Text = dgvUsers.Rows[e.RowIndex].Cells["Username"].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            txtCNIC.Text = dgvUsers.Rows[e.RowIndex].Cells["CNIC"].Value.ToString();
            cmbRole.Text = dgvUsers.Rows[e.RowIndex].Cells["RoleName"].Value.ToString();
            cmbStatus.Text = dgvUsers.Rows[e.RowIndex].Cells["Status"].Value.ToString();
        }
        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvUsers.Columns[e.ColumnIndex].Name != "Delete") return;

            int userID = Convert.ToInt32(
                dgvUsers.Rows[e.RowIndex].Cells["UserID"].Value);

            if (userID == login.UserID)
            {
                MessageBox.Show("You cannot delete or deactivate your own account.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "YES = Delete User\n\nNO = Make User Inactive\n\nCANCEL = Do Nothing",
                "User Action",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (bll.DeleteUser(userID))
                {
                    MessageBox.Show("User Deleted Successfully");
                    LoadUsers();
                    LoadDashboardCards();
                    LoadRoleOverview();
                }
                else
                {
                    MessageBox.Show("Failed To Delete User");
                }
            }
            else if (result == DialogResult.No)
            {
                if (bll.MakeUserInactive(userID))
                {
                    MessageBox.Show("User Marked As Inactive");
                    LoadUsers();
                    LoadDashboardCards();
                    LoadRoleOverview();
                }
                else
                {
                    MessageBox.Show("Failed To Update User");
                }
            }
        }
    }
}
