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
    public partial class frmTransaction : Form
    {
        public frmTransaction()
        {
            InitializeComponent();
        }

        transactionDAL tDAL = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTransaction_Load(object sender, EventArgs e)
        {
            DataTable dt = tDAL.DisplayAllTransactions();
            dgvTransactions.DataSource = dt;
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = cmbTransactionType.Text;

            DataTable dt = tDAL.DisplayTransactionByType(type);
            dgvTransactions.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            DataTable dt = tDAL.DisplayAllTransactions();
            dgvTransactions.DataSource= dt;
        }
    }
}
