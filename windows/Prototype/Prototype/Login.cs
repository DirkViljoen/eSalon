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
            //if (textBox1.Text != "" && textBox2.Text != "")
            //{
            //    User i = new User();
            //    UserList l = new UserList();

            //    byte[] ba = Encoding.Default.GetBytes(textBox2.Text);
            //    var hexString = BitConverter.ToString(ba);
            //    hexString = hexString.Replace("-", "");

            //    i.User_ID = 1;
            //    i.Username = textBox1.Text;
            //    i.Password = hexString;
            //    i.Login = "";

            //    l.stuff(i);
            //    if (l.Count > 3)
            //    {
                    Form1 a = new Form1();
                    a.ShowDialog();
                    this.Close();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Invalid Login Details");
            //}
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        

    }
}
