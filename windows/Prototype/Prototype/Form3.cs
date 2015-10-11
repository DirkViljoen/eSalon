using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessTier;

using MySql.Data.MySqlClient;



namespace Prototype
{
    public partial class Form3 : Form
    {
        Stock sl = new Stock();
        string constring = "server=localhost;user=root;pwd=root;database=esalon;";
        string Rstring = "server=localhost;user=root;pwd=root;";
        string file = "C:\\Users\\Fatima\\Desktop\\THE FINAL PROJECT THINGS\\backup.sql";

        public Form3()
        {
            InitializeComponent();
 
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            //textBox1.Text = Convert.t sl.readStock(1);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(Rstring);
            MySqlCommand cmd;
            string s0;

            try
            {
                conn.Open();
                s0 = "CREATE DATABASE IF NOT EXISTS `esalon`;";
                cmd = new MySqlCommand(s0, conn); 
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("CREATE SUCCESS");
            }
            catch (Exception fe)
            {
                MessageBox.Show(fe.ToString());
            }
            try
            {
                string constring = "server=localhost;user=root;pwd=root;database=esalon;";
                using (MySqlConnection conn1 = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd1 = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd1))
                        {
                            cmd1.Connection = conn1;
                            conn1.Open();
                            mb.ImportFromFile(file);
                            conn1.Close();
                        }
                    }
                }
                MessageBox.Show("RESTORE SUCCESS");
            }
            catch (Exception fe)
            {
                MessageBox.Show(fe.ToString());
            }


        }
            
    }
}
