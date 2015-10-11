using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace BusinessTier
{
    public class StockList : System.ComponentModel.BindingList<Stock>
    {
        string myConnectionString;
        MySqlDataReader rdr = null;
        MySql.Data.MySqlClient.MySqlConnection conn;
        
        
        public StockList() { }

        public StockList(int StockID) {
            foreach (Stock o in ViewStock(StockID))
            {
                if (StockID == o.StockID)
                {
                    Stock Stock =
                        new Stock   (o.StockID,
                                    o.Brand,
                                    o.Product,
                                    o.Price,
                                    o.Size,
                                    o.Active,
                                    o.Quantity,
                                    o.Barcode,
                                    o.CategoryID,
                                    o.SupplierID);
                    this.Add(Stock);
                    break;
                }

            }
        }

        public StockList SearchStock(string sname, string bname, string pname)
        {

            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";
            this.ClearItems();
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spStock_Search";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("sname", sname);
                cmd.Parameters.AddWithValue("bname", bname);
                cmd.Parameters.AddWithValue("pname", pname);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Stock s = new Stock(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetDouble(3),
                                        rdr.GetInt32(4), rdr.GetBoolean(5),
                                        rdr.GetInt32(6), rdr.GetString(7),
                                        rdr.GetInt32(8), rdr.GetInt32(9));

                    this.Add(s);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }

            return this;
        } //DONE

        public StockList ViewStock(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spStock_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Stock s = new Stock(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetDouble(3),
                                        rdr.GetInt32(4), rdr.GetBoolean(5),
                                        rdr.GetInt32(6), rdr.GetString(7),
                                        rdr.GetInt32(8), rdr.GetInt32(9));

                    this.Add(s);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }

            return this;
        } //DONE

        public Stock ViewAStock(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";
            Stock s = new Stock();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spStock_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    s = new Stock(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetDouble(3),
                                        rdr.GetInt32(4), rdr.GetBoolean(5),
                                        rdr.GetInt32(6), rdr.GetString(7),
                                        rdr.GetInt32(8), rdr.GetInt32(9));

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }

            return s;
        } //DONE

        public StockList ViewAllStock()
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "SELECT * FROM stock WHERE Active = True";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Stock s = new Stock(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetDouble(3),
                                        rdr.GetInt32(4), rdr.GetBoolean(5),
                                        rdr.GetInt32(6), rdr.GetString(7),
                                        rdr.GetInt32(8), rdr.GetInt32(9));

                    this.Add(s);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }

            return this;
        } //DONE

        public void InsertStock(Stock o) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Insert_Stock", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sStock_id", o.StockID);
                cmd.Parameters.AddWithValue("sBrandName", o.Brand);
                cmd.Parameters.AddWithValue("sProductName", o.Product);
                cmd.Parameters.AddWithValue("sPrice", o.Price);
                cmd.Parameters.AddWithValue("sSize", o.Size);
                cmd.Parameters.AddWithValue("sActive", o.Active);
                cmd.Parameters.AddWithValue("sQuantity", o.Quantity);
                cmd.Parameters.AddWithValue("sBarcode", o.Barcode);
                cmd.Parameters.AddWithValue("cCategory_ID", o.CategoryID);
                cmd.Parameters.AddWithValue("sSupplier_ID", o.SupplierID);

                int res = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
        } //DONE

        public void DeleteStock(Stock s) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                MySqlCommand cmd;
                conn.Open();

                cmd = new MySqlCommand("sp_Delete_Stock", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sStock_id", s.StockID);
                int res = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        public void UpdateStock(Stock o) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Update_Stock", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sStock_id", o.StockID);
                cmd.Parameters.AddWithValue("sBrandName", o.Brand);
                cmd.Parameters.AddWithValue("sProductName", o.Product);
                cmd.Parameters.AddWithValue("sPrice", o.Price);
                cmd.Parameters.AddWithValue("sSize", o.Size);
                cmd.Parameters.AddWithValue("sQuantity", o.Quantity);
                cmd.Parameters.AddWithValue("sBarcode", o.Barcode);
                cmd.Parameters.AddWithValue("sSupplier_ID", o.SupplierID);
                int res = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        public void ReconcileStock(int id, int sum)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Reconcile_Stock", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sStock_id", id);
                cmd.Parameters.AddWithValue("sSum", sum);

                int res = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

    }
}
