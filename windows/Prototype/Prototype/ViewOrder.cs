using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prototype
{
    public partial class ViewOrder : Form
    {
        public ViewOrder()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try{
                UpdateOrder a = new UpdateOrder();
            a.ShowDialog();
            this.Close();
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ReceiveStock a = new ReceiveStock();
                a.ShowDialog();
                this.Close();
            }
            catch(Exception d){
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{}
            catch(Exception d){
                MessageBox.Show("ERROR: " + d);
            }
        }
    }
}
