using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BusinessTier
{
    public class Class1
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        public string testc()
        {

            string s = "";
            string myConnectionString;
            MySqlDataReader rdr = null;

            myConnectionString = "server=localhost;uid=root;" +
                                "pwd=root;database=esalon;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string stm = "call spCategory_Read(1)";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    s += rdr.GetInt32(0) + ": "
                        + rdr.GetString(1) + "\r\n";
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                // MessageBox.Show(ex.Message);
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
    }
}
