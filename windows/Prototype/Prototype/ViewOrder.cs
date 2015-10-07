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
    public partial class ViewOrder : Form
    {
        OrderList ol = new OrderList();
        Order o;
        int id;

        public ViewOrder(int _id)
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

        private void button2_Click(object sender, EventArgs e)
        {
            try{
                UpdateOrder a = new UpdateOrder(id);
                a.ShowDialog();
                this.Close();
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ReceiveStock a = new ReceiveStock(id);
                a.ShowDialog();
                this.Close();
            }
            catch(Exception d){
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{}
            catch(Exception d){
                MessageBox.Show("ERROR: " + d);
            }
        }
    }
}
