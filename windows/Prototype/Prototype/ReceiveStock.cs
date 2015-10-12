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
    public partial class ReceiveStock : Form
    {
        OrderList ol = new OrderList();
        Order o = new Order();
        OrderLine p;
        OrderLineList pl = new OrderLineList();
        int id;
        SupplierList sl = new SupplierList();

        public ReceiveStock(int _id)
        {
            InitializeComponent();
            id = _id;
            string s = "";
            pl = new OrderLineList(id, s);
            o = ol.ViewAOrder(id);

            dataGridView1.DataSource = pl;

            foreach (Supplier u in sl)
            {
                if (o.Supplier_ID == u.Supplier_id)
                {
                    textBox1.Text = u.Name;
                }
            }
            dateTimePicker1.Value = Convert.ToDateTime(o.DatePlaced);


        }

        private void ReceiveStock_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderLineList oll = new OrderLineList();
            oll.Clear();

            DateTime todayDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            string bob = "";
            Order o = new Order
                (1, bob, dateTimePicker1.Value.ToString("yyyy-MM-dd"), 1);

            ol.UpdateOrder(o);
            MessageBox.Show("Stock Received");
            this.Close();
        }
    }
}
