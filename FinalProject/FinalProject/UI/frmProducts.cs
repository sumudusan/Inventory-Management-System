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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        categoriesDAL cadal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable categoriesDT = cadal.Select();
            cmbCategories.DataSource = categoriesDT;
            cmbCategories.DisplayMember = "title";
            cmbCategories.ValueMember = "title";

            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            p.name = txtName.Text;
            p.category = cmbCategories.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            bool success = pdal.Insert(p);
            if (success == true)
            {
                MessageBox.Show("Product Added Successfully");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to add product");
            }
        }
        public void Clear()
        {
            txtProductId.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtProductId.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategories.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            p.id = int.Parse(txtProductId.Text);
            p.name = txtName.Text;
            p.category = cmbCategories.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;

            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            bool success = pdal.Update(p);
            if (success == true)
            {
                MessageBox.Show("Products Successfull update");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Update");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            p.id = int.Parse(txtProductId.Text) ;

            bool success = pdal.Delete(p);
            if (success == true)
            {
                MessageBox.Show("Product successfully Deleted");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource= dt;
            }
            else
            {
                MessageBox.Show("Failed to Delete Products");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if(keywords!=null)
            {
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
