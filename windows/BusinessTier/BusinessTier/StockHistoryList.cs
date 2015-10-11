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
    public class StockHistoryList : System.ComponentModel.BindingList<StockHistory>
    {
        string myConnectionString;
        MySqlDataReader rdr = null;
        MySqlDataAdapter mysqla = null;
        MySql.Data.MySqlClient.MySqlConnection conn;

        public StockHistoryList()
        {
            
        }

        public StockHistoryList(int StockHistoryID)
        {
            foreach (StockHistory StockHistoryRow in GetStockHistory(StockHistoryID))
            {
                if (StockHistoryID == StockHistoryRow.StockHistory_id)
                {
                    StockHistory StockHistory =
                        new StockHistory(StockHistoryRow.StockHistory_id,
                                    StockHistoryRow.Price,
                                    StockHistoryRow.pFrom,
                                    StockHistoryRow.pTo,
                                    StockHistoryRow.StockID);
                    this.Add(StockHistory);
                    break;
                }

            }
        }

        public void InsertStockHistory(StockHistory o)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Insert_Stock_History", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("hPrice", o.Price);
                cmd.Parameters.AddWithValue("hPriceDateFrom", o.pFrom);
                cmd.Parameters.AddWithValue("hPriceDateTo", o.pTo);
                cmd.Parameters.AddWithValue("sStock_id", o.StockID);

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

        public StockHistoryList GetStockHistory(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spStockHistory_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StockHistory s = new StockHistory(rdr.GetInt32(0), rdr.GetDouble(1),
                                                        rdr.GetDateTime(2), rdr.GetDateTime(3), rdr.GetInt32(4));

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
        }

        public void UpdateStockHistory(StockHistory o)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Update_Stock_History", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("hStockHistory_id", o.StockHistory_id);
                cmd.Parameters.AddWithValue("hPrice", o.Price);
                cmd.Parameters.AddWithValue("hPriceDateFrom", o.pFrom);
                cmd.Parameters.AddWithValue("hPriceDateTo", o.pTo);
                cmd.Parameters.AddWithValue("sStock_id", o.StockID);
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
