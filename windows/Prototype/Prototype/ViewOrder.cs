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
        Order o = new Order();
        OrderLine p;
        OrderLineList pl = new OrderLineList();
        int id;
        SupplierList sl = new SupplierList();
        


        public ViewOrder(int _id)
        {
            InitializeComponent();
            id = _id;
            string s = "";
            pl = new OrderLineList(id, s);
            o = ol.ViewAOrder(id);

            dataGridView1.DataSource = pl;

            foreach (Supplier u in sl)
            {
                if(o.Supplier_ID == u.Supplier_id){   
                    textBox2.Text = u.Name;
                }
            }
           textBox1.Text = o.DatePlaced;
            
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
                MessageBox.Show("Order Deleted");
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
