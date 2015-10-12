using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Prototype
{
    public partial class Form1 : Form
    {
        string constring = "server=localhost;user=root;pwd=root;database=esalon;";
        string Rstring = "server=localhost;user=root;pwd=root;";
        string file = "C:\\esalon\\backup.sql";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Login a = new Login();
            //a.ShowDialog();

        }

        private void webProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:8000");
        }

        private void viewMantainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchStock a = new SearchStock();
            a.ShowDialog();
            this.Close();
        }

        private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStock a = new AddStock();
            a.ShowDialog();
            this.Close();
        }

        private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.ShowDialog();
            this.Close();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchOrder a = new SearchOrder();
            a.ShowDialog();
            this.Close();
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show("BACKUP SUCCESS");
            }
            catch (Exception fe)
            {
                MessageBox.Show(fe.ToString());
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void feelingABitLostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "C:\\esalon\\User.docx";
           
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "WINWORD.EXE";
            startInfo.Arguments = filePath;
            Process.Start(startInfo);
            

        }

        private void dATABASEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lookupListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lookups a = new Lookups();
            a.ShowDialog();
        }

    }
}

