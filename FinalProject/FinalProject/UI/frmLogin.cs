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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dAL = new loginDAL();
        public static string loggedIn;

        private void pboxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            l.username=txtUsername.Text.Trim();
            l.password=txtPassword.Text.Trim();
            l.user_type=txtUsername.Text.Trim();

            //checkong the login credentials
            bool sucess = dAL.loginCheck(l);
            if(sucess == true)
            {
                MessageBox.Show("Login Successful");
                loggedIn = l.username;
                switch(l.user_type)
                {
                    case "Admin":
                        {
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;

                    case "User":
                        {
                            frmuserDashboard user = new frmuserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;

                    default:
                        {
                           
                            //Display Error Message
                            MessageBox.Show("Invalid User Type!!!");
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Login Falied...Try Again..");
            }



        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
