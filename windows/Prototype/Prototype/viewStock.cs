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
    public partial class viewStock : Form
    {
        StockList sl = new StockList();
        Stock s;
        int id;

        public viewStock(int _id)
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
            UpdateStock a = new UpdateStock(id);
            a.ShowDialog();
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sl.DeleteStock(s);
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void viewStock_Load(object sender, EventArgs e)
        {

        }
    }
}
