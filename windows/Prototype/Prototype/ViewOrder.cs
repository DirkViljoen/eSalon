using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessTier;

namespace Prototype
{
    public partial class ViewOrder : Form
    {
        OrderList ol = new OrderList();
        Order o;
        int id;


        public ViewOrder(int _id)
        {
            InitializeComponent();
            id = _id;

            o = ol[id];

            //MessageBox.Show(Convert.ToString(o));
            List<Order> nol = new List<Order>();
            nol.Add(o);
           // MessageBox.Show(Convert.ToString(o.OrderID));
            dataGridView1.DataSource = nol;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try{
                UpdateOrder a = new UpdateOrder(id);
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
                ReceiveStock a = new ReceiveStock(id);
                a.ShowDialog();
                this.Close();
            }
            catch(Exception d){
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                ol.DeleteOrder(o);
            }
            catch(Exception d){
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void ViewOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
