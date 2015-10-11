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
    public partial class UpdateStock : Form
    {
        StockList sl;
        Stock s = new Stock();
        int id;

        public UpdateStock(int _id)
        {
            InitializeComponent();
            id = _id;
            sl = new StockList(id);

            s = sl.ViewAStock(sl[0].StockID);

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
                string str = "Are you sure you want\nto update this product? \nYes to confirm,\nNo to edit details,\nCancel to exit";
                string form = "UpdateStock";
                //StockList sls = new StockList();

                sl.UpdateStock(new Stock(id, txtBrand.Text, txtProduct.Text, Convert.ToDouble(txtPrice.Text),
                Convert.ToInt32(txtSize.Text), true, Convert.ToInt32(txtQuantity.Text), txtBarcode.Text, 0,
                Convert.ToInt32(txtSupplier.Text)));
                ConfirmationMessage a = new ConfirmationMessage(str, form);
                a.ShowDialog();

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
