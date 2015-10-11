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

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Prototype
{
    public partial class AddOrder : Form
    {

        OrderList ol = new OrderList();
        OrderLineList oll = new OrderLineList();
        StockList sl = new StockList();
        SupplierList spl;
        
        public AddOrder()
        {
            InitializeComponent();
            spl = new SupplierList();

            foreach (Supplier s in spl)
            {
                cmbSupplier.Items.Add(s.Name);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime todayDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                Order o = new Order
                    (1, dtpDate.Value.ToString("yyyy-MM-dd"), 
                    todayDate.ToString("yyyy-MM-dd"), 
                    Convert.ToInt32(cmbSupplier.SelectedIndex + 1));

                ol.InsertOrder(o);
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                   oll.InsertOrderLine(new OrderLine(1, Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value),
                                                       Convert.ToInt32(cProduct.Index), 
                                                       ol.getOrderID()));
                }

                MessageBox.Show("Your order has been added");
            }
            catch(Exception d){
                MessageBox.Show("INSERT ERROR: " + d);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //supplier notification
            int bob;
            bob = cmbSupplier.SelectedIndex;

            string value = "Brand\t\tProduct\t\tQuantity";

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    value += dataGridView1.Rows[i].Cells[0].Value + "\t\t" +
                            dataGridView1.Rows[i].Cells[1].Value + "\t\t" +
                            dataGridView1.Rows[i].Cells[2].Value + "\r\n";
                }


                
            try
            {
                
                SmtpMail oMail = new SmtpMail("TryIt");
                SmtpClient oSmtp = new SmtpClient();

                oMail.From = "salonRedesigneSalon@gmail.com";
                oMail.To = spl[bob].Email; 
                oMail.Subject = "Order Placed";
                oMail.TextBody = value;

                SmtpServer oServer = new SmtpServer("smtp.gmail.com");
                oServer.User = "salonRedesigneSalon@gmail.com";
                oServer.Password = "esLuaxeiri1";
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                oServer.Port = 465;
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                try
                {
                    oSmtp.SendMail(oServer, oMail);
                    MessageBox.Show("Email was sent successfully!");

                }
                catch (Exception ep)
                {
                    MessageBox.Show("Failed to send email with the following error: ");
                    MessageBox.Show(ep.Message);
                }                
            }
            catch (Exception d)
            {
                MessageBox.Show("EMAIL ERROR: " + d);
            }
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(Convert.ToString(dataGridView1.Rows[0].Cells[1].RowIndex));
        }

    }
}
