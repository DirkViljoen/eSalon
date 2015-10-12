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
                int bob;
                double b;
                if (txtBrand.Text.Length == 0 || txtProduct.Text.Length == 0
                    || txtPrice.Text.Length == 0 || txtSize.Text.Length == 0
                    || txtQuantity.Text.Length == 0 || txtBarcode.Text.Length == 0
                    || txtSupplier.Text.Length == 0 )
                {
                    MessageBox.Show("Please Enter All Fields!");
                }
                else if (txtBrand.Text != "" || txtProduct.Text != ""
                    || double.TryParse(txtPrice.Text, out b) || int.TryParse(txtSize.Text, out bob)
                    || int.TryParse(txtQuantity.Text, out bob) || int.TryParse(txtBarcode.Text, out bob)
                    || int.TryParse(txtSupplier.Text, out bob))
                {
                    MessageBox.Show("Please Enter Correct Value Types!");
                }
                else
                {
                    sl.InsertStock(new Stock(0, txtBrand.Text, txtProduct.Text, Convert.ToDouble(txtPrice.Text),
                    Convert.ToInt32(txtSize.Text), true, Convert.ToInt32(txtQuantity.Text), txtBarcode.Text, 2,
                    Convert.ToInt32(txtSupplier.Text)));

                    MessageBox.Show("A Product has been added");
                    this.Close();
                }
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
