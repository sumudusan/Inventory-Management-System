using FinalProject.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.DAL
{
    internal class DeaCustDLL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT Method for Dealer and Customer
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_dea_cust";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return dt;
        }
        #endregion
        #region INSERT Method to add details of dealer customer
        public bool Insert(DeaCustBLL dc)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            bool isSuccess = false;

            try
            {
                string sql = "INSERT INTO tbl_dea_cust (type, email, contact, address, added_date, added_by) VALUES (@type, @email, @contact, @address, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess= false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return isSuccess;
        }
        #endregion
        #region UPDATE Method for Dealer and customer Module
        public bool Update(DeaCustBLL dc)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            bool isSuccess = false;

            try
            {
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contaact=@contact, address=@address, added_date=@added_date, added_by=@added-by WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return isSuccess;
        }
        #endregion
        #region Delete method for dealer and customer Module
        public bool Delete(DeaCustBLL dc)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            bool isSuccess = false;

            try
            {
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", dc.id);

                conn.Open ();
                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    isSuccess=true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return isSuccess;
        }
        #endregion
        #region SEARCH METHOD for Dealer and Customer Module
        public DataTable Search(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%"+keyword+"%' OR type LIKE '%"+keyword+"%' OR name LIKE'%"+keyword+"%' ";

                SqlCommand cmd= new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open ();
                adapter.Fill (dt);
            }
            catch( Exception ex )
            {
                MessageBox.Show (ex.Message);
            }
            finally { conn.Close(); }

            return dt;
        }
        #endregion
        #region Method to search Dealer or Customer for Transaction Module

        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            DeaCustBLL dc = new DeaCustBLL();

            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT name, email, contact, address tbl_dea_cust WHERE id LIKE '%"+keyword+"%' name LIKE '%"+keyword+"%' ";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open ();   

                adapter.Fill (dt);

                if(dt.Rows.Count > 0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dc;
        }

        #endregion
        #region Mehod to get ID of the dealer or customer based on name

        public DeaCustBLL GetDeaCustIDFromName(string Name)
        {
            DeaCustBLL dc = new DeaCustBLL ();

            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM tbl_dea_cust WHERE name = '" + Name + "'";
                SqlDataAdapter adapter = new SqlDataAdapter( sql, conn);

                conn.Open ();
                adapter.Fill (dt);

                if(dt.Rows.Count > 0)
                {
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return dc;
        }

        #endregion
    }
}
