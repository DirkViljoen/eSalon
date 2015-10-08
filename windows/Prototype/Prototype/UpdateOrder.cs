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
        Order o;
        int id;

        public UpdateOrder(int _id)
        {
            InitializeComponent();
            id = _id;

            o = ol[id];

            List<Order> nol = new List<Order>();
            nol.Add(o);
            dataGridView1.DataSource = nol;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string str = "Are you sure you want to update this order?";
                string form = "UpdateOrder";

                OrderLineList oll = new OrderLineList();
                oll.Clear();

                MessageBox.Show(Convert.ToString(dataGridView1.Rows[0].Cells[5].Value));
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    oll.Add(new OrderLine(
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value),
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[5].Value),
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[6].Value),
                        Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value)
                        ));
                }

                    ol.UpdateOrder(new Order(Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value),
                                            Convert.ToDateTime(dataGridView1.Rows[0].Cells[1].Value),
                                            dateTimePicker1.Value,
                                            Convert.ToInt32(dataGridView1.Rows[0].Cells[3].Value)
                                            ), oll);

                ConfirmationMessage a = new ConfirmationMessage(str, form);
                a.ShowDialog();
                MessageBox.Show("Your Order has been updated");
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
