using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EASendMail;
using BusinessTier;

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
                sl.InsertOrder(new Order(1, System.DateTime.Parse("30 - 12 - 2011"), System.DateTime.Parse("30 - 12 - 2011"), 1));

                
                MessageBox.Show("A Product has been added");
            }
            catch(Exception d){
                MessageBox.Show("INSERT ERROR: " + d);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //supplier notification
            try
            {
                Form2 a = new Form2();
                a.ShowDialog();
                
            }
            catch (Exception d)
            {
                MessageBox.Show("EMAIL ERROR: " + d);
            }
        }
     }
}
