using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prototype
{
    public partial class UpdateStock : Form
    {
        StockList sl = new StockList();
        Stock s;
        int id;

        public UpdateStock(int _id)
        {
            InitializeComponent();
            id = _id;

            s = sl[id];

            txtBrand.Text = s.Brand;
            txtProduct.Text = s.Product;
            txtPrice.Text = Convert.ToString(s.Price);
            txtSize.Text = Convert.ToString(s.Size);
            txtQuantity.Text = Convert.ToString(s.Quantity);
            txtBarcode.Text = s.Barcode;
            txtSupplier.Text = Convert.ToString(s.SupplierID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sl.UpdateStock(new Stock(0, txtBrand.Text, txtProduct.Text, Convert.ToDouble(txtPrice.Text),
            Convert.ToInt32(txtSize.Text), true, Convert.ToInt32(txtQuantity.Text), txtBarcode.Text, 0,
            Convert.ToInt32(txtSupplier.Text)));

                MessageBox.Show("A Product has been updated");
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void UpdateStock_Load(object sender, EventArgs e)
        {

        }
    }
}
