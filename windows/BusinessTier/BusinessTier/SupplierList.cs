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
    public class SupplierList : List<Supplier>
    {
        string myConnectionString;
        MySqlDataReader rdr = null;
        MySqlDataAdapter mysqla = null;
        MySql.Data.MySqlClient.MySqlConnection conn;

        public SupplierList() {
            this.ViewAllSupplier();
        }

        public SupplierList(int SupplierID) {
            foreach (Supplier o in ViewSupplier(SupplierID))
            {
                if (SupplierID == o.Supplier_id)
                {
                    Supplier Supplier =
                        new Supplier   (o.Supplier_id,
                                        o.Name, 
                                        o.Email);
                    this.Add(Supplier);
                    break;
                }

            }
        }

        public SupplierList ViewSupplier(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spSupplier_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Supplier s = new Supplier(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(3));

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

        public Supplier ViewASupplier(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";
            Supplier s = new Supplier();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spSupplier_Read(" + inID + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    s = new Supplier(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(3));

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

        public SupplierList ViewAllSupplier()
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "SELECT * FROM Supplier";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Supplier s = new Supplier(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(3));

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


    }
}
