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
    public partial class AddStock : Form
    {
        StockList sl = new StockList();

        public AddStock()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sl.InsertStock(new Stock(0, txtBrand.Text, txtProduct.Text, Convert.ToDouble(txtPrice.Text),
                Convert.ToInt32(txtSize.Text), true, Convert.ToInt32(txtQuantity.Text), txtBarcode.Text, 2,
                Convert.ToInt32(txtSupplier.Text)));

                MessageBox.Show("A Product has been added");
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void AddStock_Load(object sender, EventArgs e)
        {

        }

    }
}
