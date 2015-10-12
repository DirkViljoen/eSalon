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
    public partial class UpdateOrder : Form
    {
        OrderList ol = new OrderList();
        Order o = new Order();
        OrderLine p;
        OrderLineList pl = new OrderLineList();
        int id;
        SupplierList sl = new SupplierList();

        public UpdateOrder(int _id)
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
            dateTimePicker1.Value = Convert.ToDateTime( o.DatePlaced);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string str = "Are you sure you want\nto update this order? \nYes to confirm,\nNo to edit details,\nCancel to exit";
                string form = "UpdateOrder";

                OrderLineList oll = new OrderLineList();
                oll.Clear();

                DateTime todayDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                //string bob = "";
                //Order o = new Order
                //    (1, bob, dateTimePicker1.Value.ToString("yyyy-MM-dd"), 1);

                //ol.UpdateOrder(o);

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    oll.UpdateOrderLine(new OrderLine(
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value),
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value),
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[2].Value),
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[3].Value)
                        ));
                }

                ConfirmationMessage a = new ConfirmationMessage(str, form);
                a.ShowDialog();
                MessageBox.Show("Your Order has been updated");
                this.Close();
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void UpdateOrder_Load(object sender, EventArgs e)
        {

        }

    }
}
