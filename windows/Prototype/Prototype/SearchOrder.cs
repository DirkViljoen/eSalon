using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessTier;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Prototype
{
    public partial class SearchOrder : Form
    {
        OrderList ol = new OrderList();
        OrderLineList oll = new OrderLineList();
        StockList sl = new StockList();
        SupplierList spl;

        public SearchOrder()
        {
            InitializeComponent();
            //dataGridView2.DataSource = ol;
            //cmbSupp.DataSource = spl;
            //cmbSupp.DisplayMember = "Name";
            //cmbSupp.ValueMember = "supplierID";
            dataGridView2.DataSource = ol;
            spl = new SupplierList();

            foreach (Supplier s in spl)
            {
                cmbSupp.Items.Add(s.Name);
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentCell.ColumnIndex == 0)
                {
                    int orderId = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value);
                    //orderId = dataGridView2.CurrentCell.RowIndex;
                    
                    //MessageBox.Show(id);
                    ViewOrder a = new ViewOrder(orderId);
                    a.ShowDialog();

                }

                //if (dataGridView2.CurrentCell.ColumnIndex == 1)
                //{
                //    dataGridView1.DataSource = oll.ViewAllOrderLine();
                //}
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderList bob = new OrderList();

            try
            {
                bob = ol.SearchOrder(Convert.ToInt32(cmbSupp.SelectedIndex + 1),
                                        dtpFrom.Value.ToString("yyyy-MM-dd"),
                                        dtpTo.Value.ToString("yyyy-MM-dd"));

                dataGridView2.DataSource = bob;
                if (bob.Count == 0)
                {
                    MessageBox.Show("No Items Found");
                }
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

        private void SearchOrder_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                dataGridView1.DataSource = oll.ViewAllOrderLine(e.RowIndex + 1);
            }
        }

    }
}
