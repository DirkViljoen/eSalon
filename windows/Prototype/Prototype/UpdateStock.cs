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

        public UpdateStock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sl.InsertStock(new Stock(0, txtBrand.Text, txtProduct.Text, Convert.ToDouble(txtPrice.Text),
            Convert.ToInt32(txtSize.Text), true, Convert.ToInt32(txtQuantity.Text), 0,
            Convert.ToInt32(txtSupplier.Text))))
            {
                MessageBox.Show("A Product has been updated");
                this.Close();
            }
        }
    }
}
