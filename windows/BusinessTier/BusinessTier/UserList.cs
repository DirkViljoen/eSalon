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
    public class UserList : System.ComponentModel.BindingList<User>
    {
        string myConnectionString;
        MySqlDataReader rdr = null;
        MySql.Data.MySqlClient.MySqlConnection conn;


        public UserList() { this.ViewAllUser(); }

        public UserList(int UserID)
        {
            foreach (User o in ViewUser(UserID))
            {
                if (UserID == o.User_ID)
                {
                    User User = 
                        new User(o.User_ID, o.Username, o.Password, o.Login);
                    this.Add(User);
                    break;
                }

            }
        }

        public UserList ViewUser(int inID)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "SELECT * from User WHERE User_ID = " + inID + ";";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User s = new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3));

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

        public UserList ViewAllUser()
        {
            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "SELECT * from User";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User s = new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3));

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

        public User stuff(User u)
        {
            myConnectionString = "server=localhost;uid=root;" +
                                    "pwd=root;database=esalon;";

            try
            {

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call sp_login_compare(" + u.Username + "," + u.Password + ")";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    u = new User(rdr.GetInt32(0), rdr.GetString(1),
                                        rdr.GetString(2), rdr.GetString(3));

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string e = ex.Message;

            }
            return u;
        }

    }
}
