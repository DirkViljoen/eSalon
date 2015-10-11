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
    public partial class ReconcileStock : Form
    {
        StockList sl;
        Stock s = new Stock();
        int id;

        public ReconcileStock(int _id)
        {
            InitializeComponent();
            id = _id;
            sl = new StockList(id);

            s = sl.ViewAStock(sl[0].StockID);

        }

        private void ReconcileStock_Load(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int sum = Convert.ToInt32(numericUpDown1.Value + 
                                            numericUpDown2.Value + 
                                            numericUpDown3.Value + 
                                            numericUpDown4.Value);
                sl.ReconcileStock(id, sum);
                MessageBox.Show("Stock Reconciliation is complete");
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
