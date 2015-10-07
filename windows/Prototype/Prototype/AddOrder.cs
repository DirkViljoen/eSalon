using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EASendMail;
using BusinessTier;

namespace Prototype
{
    public partial class AddOrder : Form
    {

        OrderList ol = new OrderList();
        StockList sl = new StockList();
        SupplierList spl = new SupplierList();

        public AddOrder()
        {
            InitializeComponent();
            cmbSupplier.DataSource = spl;
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "supplierID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Order Details
                int sid = Convert.ToInt32(cmbSupplier.SelectedValue);

                //Stock Details
                string stock = "[{";
                for (int i = 0; i < 2; i++)
                {
                    if (stock != "[{")
                    {
                        stock = stock + "},{";
                    }
                    string s = "stockID: 1, quantity: 3";
                    stock = stock + s;
                }
                stock = stock + "}]";


                ol.InsertOrder(new Order(1, dtpDate.Value, DateTime.Now, sid), stock);

                
                MessageBox.Show("A Product has been added");
            }
            catch(Exception d){
                MessageBox.Show("INSERT ERROR: " + d);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //supplier notification
            try
            {
                Form2 a = new Form2();
                a.ShowDialog();
                
            }
            catch (Exception d)
            {
                MessageBox.Show("EMAIL ERROR: " + d);
            }
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {

        }

        private void cmbSupplier_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
     }
}
