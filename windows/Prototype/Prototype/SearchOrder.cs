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
    public partial class SearchOrder : Form
    {
        OrderList ol = new OrderList();

        public SearchOrder()
        {
            InitializeComponent();
            dataGridView2.DataSource = ol;
        }

       
        private void button5_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateOrder a = new UpdateOrder();
            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReceiveStock a = new ReceiveStock();
            a.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentCell.ColumnIndex == 0)
                {
                    int id = dataGridView2.CurrentCell.RowIndex;
                    //MessageBox.Show(id);
                    ViewOrder a = new ViewOrder(id);
                    a.ShowDialog();

                }
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void SearchOrder_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ol.GetOrder(cmbSupp.Text, dtpFrom.Text, dtpTo.Text);

                dataGridView2.DataSource = ol;
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }
    }
}
