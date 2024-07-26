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
    class productsDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for product Module

        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_products";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;

        }
        #endregion

        #region Method to Insert Product in Database

        public bool Insert(productsBLL p)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "INSERRT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name",p.name);
                cmd.Parameters.AddWithValue("@category",p.category);
                cmd.Parameters.AddWithValue("@description",p.description);
                cmd.Parameters.AddWithValue("@rate",p.rate);
                cmd.Parameters.AddWithValue("@qty",p.qty);
                cmd.Parameters.AddWithValue("@added_date",p.added_date);
                cmd.Parameters.AddWithValue("@added_by",p.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;                 
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        #endregion

        #region Method to Update Database

        public bool Update(productsBLL p)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@adde_by WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id",p.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Delete product Method

        public bool Delete(categoriesBLL c)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM tbl_products WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", c.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return isSuccess;
        }

        internal bool Delete(productsBLL p)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region SEARCH Method for Product Module
        public DataTable Search (string keywords)
        {
            SqlConnection conn = new SqlConnection (myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%"+keywords+"%' OR category LIKE '%" + keywords + "%'";
                SqlCommand cmd = new SqlCommand (sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill (dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return dt;
        }
        #endregion
        #region METHOD TO SEARCH PRODUCT IN TRANSACTION MODULE

        public productsBLL GetProdcutsForTransaction(string keyword)
        {
            productsBLL p = new productsBLL();

            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%' ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();

                adapter.Fill (dt);

                if(dt.Rows.Count > 0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString()) ;

                }

            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return p;
        }

        #endregion
        #region METHOD TO GET PRODUCT ID BASED ON PRODUCT NAME

        public productsBLL GetProductIDFromName(string ProductName)
        {
            productsBLL p = new productsBLL();

            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM tbl_products WHERE name = '" + ProductName + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return p;
        }

        #endregion
        #region METHOD TO GET CURRENT Quantity from the Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            decimal qty = 0;

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT qty FROM tbl_products WHERE id = "+ProductID;
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }

            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return qty;
        }
        #endregion
        #region METHOD TO UPDATE Quantity

        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return success;
        }

        #endregion
        #region METHOD TO INCREASE PRODUCT

        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                decimal currentQty = GetProductQty(ProductID);

                decimal NewQty = currentQty + IncreaseQty;

                success = UpdateQuantity(ProductID, NewQty);

            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return success;
        }

        #endregion
        #region METHOD TO DECREASE PRODUCT

        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                decimal currentQty = GetProductQty(ProductID);

                decimal NewQty = currentQty - Qty;

                success = UpdateQuantity(ProductID, NewQty);
            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return success;
        }

        #endregion
        #region DISPLAY PRODUCT BASED ON CATEGORY

        public DataTable DisplayProductsByCategory(string category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_products WHERE category='"+category+"' ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return dt;
        }

        #endregion
    }
}
