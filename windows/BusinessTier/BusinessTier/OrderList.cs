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
    public class OrderList : System.ComponentModel.BindingList<Order>
    {
        string myConnectionString;
        MySqlDataReader rdr = null;
        MySqlDataAdapter mysqla = null;
        MySql.Data.MySqlClient.MySqlConnection conn;

        public OrderList() {
            this.ViewAllOrder();
        }

        public OrderList(int OrderID) {
            foreach (Order o in ViewOrder(OrderID))
            {
                if (OrderID == o.OrderID)
                {
                    Order Order = new Order(o.OrderID,
                                    o.DatePlaced, 
                                    o.DateReceived, 
                                    o.Supplier_ID);
                    this.Add(Order);
                    break;
                }

            }
        }

        public OrderList SearchOrder(int sid, string from, string to)
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
                cmd.CommandText = "spOrder_Search";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("sid", sid);
                cmd.Parameters.AddWithValue("dateFrom", from);
                cmd.Parameters.AddWithValue("dateTo", to);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order s = new Order(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetInt32(3));

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

        public OrderList ViewOrder(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spOrder_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order s = new Order(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetInt32(3));

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

        public Order ViewAOrder(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";
            Order s = new Order();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spOrder_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    s = new Order(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetInt32(3));

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
        } 

        public OrderList ViewAllOrder()
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "SELECT * FROM esalon.order;";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order s = new Order(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetInt32(3));

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

        public int getOrderID()
        {
            int count = 0;
            OrderList ool = new OrderList();
            foreach (Order o in ool)
            {
                count++;
            }
            return count;
        } 

        public void InsertOrder(Order o) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Insert_Order", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("oDatePlaced", o.DatePlaced);
                cmd.Parameters.AddWithValue("oDateReceived", o.DateReceived);
                cmd.Parameters.AddWithValue("sSupplier_id", o.Supplier_ID);

                int res = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string e = ex.Message;
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

        public void DeleteOrder(Order s) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                MySqlCommand cmd;
                conn.Open();

                cmd = new MySqlCommand("sp_Delete_Order", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("oOrder_id", s.OrderID);
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
        } //BROKEN LOGIC

        public void UpdateOrder(Order o) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Update_Order", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("oOrder_id", o.OrderID);
                cmd.Parameters.AddWithValue("oDateReceived", o.DateReceived);

                int res = cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string s = ex.Message;
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
