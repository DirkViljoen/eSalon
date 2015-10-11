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
    public class OrderLineList : System.ComponentModel.BindingList<OrderLine>
    {
        string myConnectionString;
        MySqlDataReader rdr = null;
        MySql.Data.MySqlClient.MySqlConnection conn;

        public OrderLineList() { this.ViewAllOrderLine(); }

        public OrderLineList(int inID, string dummy) { this.ViewAllOrderLine(inID); }

        public OrderLineList(int OrderLineID) {
            foreach (OrderLine o in ViewOrderLine(OrderLineID))
            {
                if (OrderLineID == o.OrderLine_id)
                {
                    OrderLine OrderLine =
                        new OrderLine(o.OrderLine_id,
                                    o.Quantity, 
                                    o.Stock_ID, 
                                    o.OrderID);
                    this.Add(OrderLine);
                    break;
                }

            }
        }

        public OrderLineList ViewOrderLine(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spOrderLine_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    OrderLine s = new OrderLine(rdr.GetInt32(0), rdr.GetInt32(1),
                                        rdr.GetInt32(2), rdr.GetInt32(3));

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

        public OrderLineList ViewAllOrderLine()
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "SELECT * FROM OrderLine";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    OrderLine s = new OrderLine(rdr.GetInt32(0), rdr.GetInt32(1),
                                        rdr.GetInt32(2), rdr.GetInt32(3));

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

        public OrderLineList ViewAllOrderLine(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "SELECT * FROM esalon.order_line where Order_ID = " + inID + ";";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    OrderLine s = new OrderLine(rdr.GetInt32(0), rdr.GetInt32(1),
                                        rdr.GetInt32(2), rdr.GetInt32(3));

                    this.Add(s);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //return ex.Message;
            }

            return this;
        } 

        public void InsertOrderLine(OrderLine o) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Insert_Order_Line", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("rQuantity", o.Quantity);
                cmd.Parameters.AddWithValue("sStock_id", o.Stock_ID);
                cmd.Parameters.AddWithValue("oOrder_id", o.OrderID);

                int res = cmd.ExecuteNonQuery();

                this.Add(o);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string e = ex.Message;
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

        public void DeleteOrderLine(OrderLine s) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                MySqlCommand cmd;
                conn.Open();

                cmd = new MySqlCommand("sp_Delete_Order_Line", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("rOrderLine_id", s.OrderLine_id);
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

        public void UpdateOrderLine(OrderLine o) {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                MySqlCommand cmd;
                conn.ConnectionString = myConnectionString;
                conn.Open();

                cmd = new MySqlCommand("sp_Update_Order_Line", conn); //passing procedure name and connection object
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("rOrderLine_id", o.OrderLine_id);
                cmd.Parameters.AddWithValue("rQuantity", o.Quantity);
                cmd.Parameters.AddWithValue("sStock_id", o.Stock_ID);
                cmd.Parameters.AddWithValue("oOrder_id", o.OrderID);
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
