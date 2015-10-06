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
    public partial class UpdateOrder : Form
    {
        OrderList ol = new OrderList();
        Order o;
        int id;

        public UpdateOrder(int _id)
        {
            InitializeComponent();
            id = _id;

            o = ol[id];

            //txtBrand.Text = s.Brand;
            //txtProduct.Text = s.Product;
            //txtPrice.Text = Convert.ToString(s.Price);
            //txtSize.Text = Convert.ToString(s.Size);
            //txtQuantity.Text = Convert.ToString(s.Quantity);
            //txtBarcode.Text = s.Barcode;
            //txtSupplier.Text = Convert.ToString(s.SupplierID);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string str = "Are you sure you want to update this order?";
                string form = "UpdateOrder";

                //ol.UpdateOrder(new Stock(0, 
                //                txtBrand.Text, 
                //                txtProduct.Text, 
                //                Convert.ToDouble(txtPrice.Text),
                //                Convert.ToInt32(txtSize.Text), 
                //                true, 
                //                Convert.ToInt32(txtQuantity.Text), 
                //                txtBarcode.Text, 
                //                0,
                //                Convert.ToInt32(txtSupplier.Text)));

                ConfirmationMessage a = new ConfirmationMessage(str, form);
                a.ShowDialog();
                MessageBox.Show("Your Order has been updated");
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void UpdateOrder_Load(object sender, EventArgs e)
        {

        }

    }
}
