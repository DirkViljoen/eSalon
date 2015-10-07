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
        StockList sl = new StockList();
        SupplierList spl = new SupplierList();
        int orderId = 1;

        public SearchOrder()
        {
            InitializeComponent();
            dataGridView2.DataSource = ol;
            cmbSupp.DataSource = spl;
            cmbSupp.DisplayMember = "Name";
            cmbSupp.ValueMember = "supplierID";
        }

       
        private void button5_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateOrder a = new UpdateOrder(orderId);
            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReceiveStock a = new ReceiveStock(orderId);
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
                ol.GetOrder(cmbSupp.SelectedValue.ToString(), Convert.ToDateTime(dtpFrom.Value), Convert.ToDateTime(dtpTo.Value));

                dataGridView2.DataSource = ol;
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            } 
        }

        private void cmbSupp_SelectedValueChanged(object sender, EventArgs e)
        {
            //this.Text = cmbSupp.SelectedValue.ToString();
        }
    }
}
