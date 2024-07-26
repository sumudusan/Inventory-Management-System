using FinalProject.BLL;
using FinalProject.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = dal.GetIDFromUsername(loggedUser);

            u.added_by = usr.id;

            bool success = dal.Insert(u);
            if (success == true)
            {
                MessageBox.Show("User Successfully Created.");
                clear();
            }
            else
            {
                MessageBox.Show("Faild to add new user");
            }
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }
        private void clear()
        {
            txtUserId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";

        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtUserId.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text=dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text= dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text= dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUserName.Text= dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text= dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text= dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text= dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text= dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text= dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            u.id = Convert.ToInt32(txtUserId.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;


            bool success = dal.Update(u);
            if (success == true)
            {
                MessageBox.Show("User successfully updates");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to update user");
            }

            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            u.id = Convert.ToInt32(txtUserId.Text);

            bool success = dal.Delete(u);

            if (success == true)
            {
                MessageBox.Show("User Deleted Successfully");
            }
            else
            {
                MessageBox.Show("Faild to delete user");
            }
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if (keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;

            }
            else
            {
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }

        private void pictureBoxClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
