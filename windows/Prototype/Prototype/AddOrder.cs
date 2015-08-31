using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EASendMail;

namespace Prototype
{
    public partial class AddOrder : Form
    {

        OrderList sl = new OrderList();
        public AddOrder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sl.InsertOrder(new Order(1, Convert.ToDateTime("30 - 12 - 2011"), Convert.ToDateTime("30 - 12 - 2011"), 1));

                //supplier notification
                try
                {
                    if (checkBox1.Checked)
                    {
                        MessageBox.Show("Email Sent");
                    }
                }
                catch (Exception d)
                {
                    MessageBox.Show("ERROR: " + d);
                }
                MessageBox.Show("A Product has been added");
            }
            catch(Exception d){
                MessageBox.Show("ERROR: " + d);
            }

        }
    }
}
