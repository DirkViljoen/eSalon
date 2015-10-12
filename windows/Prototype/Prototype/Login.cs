using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using BusinessTier;

namespace Prototype
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (textBox1.Text == "Admin" && textBox2.Text == "Admin")
                {
                    Form1 a = new Form1();
                    a.ShowDialog();
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Please Enter Login Details (Admin: Admin)");
                
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        

    }
}
