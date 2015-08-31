using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EASendMail;

namespace Prototype
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            byte[] ascii = Encoding.ASCII.GetBytes(textBox1.Text);

            foreach (Byte b in ascii)
            {
                if ((int)(b - 32) == 65 || (int)(b - 32) == 69 || (int)(b - 32) == 73 ||
                    (int)(b - 32) == 79 || (int)(b - 32) == 85)
                {

                    SmtpMail oMail = new SmtpMail("TryIt");
                    SmtpClient oSmtp = new SmtpClient();

                    oMail.From = "u13077130@tuks.co.za";
                    oMail.To = "u13077130@tuks.co.za";
                    oMail.Subject = "Order Placed";
                    oMail.TextBody = "Good day \r\n \r\n" +
                                     "We would like to place an order for the following items. \r\n \r\n" +
                                     "Shampoo: 5 \r\n \r\n" +
                                     "Hair Dye: 7 \r\n \r\n" +
                                     "Regards \r\n" +
                                     "eSalon \r\n";

                    SmtpServer oServer = new SmtpServer("smtp.gmail.com");
                    oServer.User = "u13077130@tuks.co.za";
                    oServer.Password = "Ellemd33n";
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                    oServer.Port = 465;
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    try
                    {
                        progressBar1.Visible = true;
                        textBox1.Visible = false;

                        progressBar1.Value = 0;
                        progressBar1.Step = 1;

                        for (int k = 0; k <= 100; k++)
                        {
                            progressBar1.PerformStep();
                        }

                        oSmtp.SendMail(oServer, oMail);
                        MessageBox.Show("Email was sent successfully!");

                        progressBar1.Visible = false;
                        textBox1.Visible = true;
                    }
                    catch (Exception ep)
                    {
                        MessageBox.Show("Failed to send email with the following error: ");
                        MessageBox.Show(ep.Message);
                    }
                }
            }

            textBox1.Clear();
            textBox1.Focus();
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            byte[] ascii = Encoding.ASCII.GetBytes(textBox1.Text);

            foreach (Byte b in ascii)
            {
                if ((int)(b - 32) == 65 || (int)(b - 32) == 69 || (int)(b - 32) == 73 ||
                    (int)(b - 32) == 79 || (int)(b - 32) == 85)
                {

                    SmtpMail oMail = new SmtpMail("TryIt");
                    SmtpClient oSmtp = new SmtpClient();

                    oMail.From = "u13077130@tuks.co.za";
                    oMail.To = "u13077130@tuks.co.za";
                    oMail.Subject = "Order Placed";
                    oMail.TextBody = "Good day \r\n \r\n" +
                                     "We would like to place an order for the following items. \r\n \r\n" +
                                     "Shampoo: 5 \r\n \r\n" +
                                     "Hair Dye: 7 \r\n \r\n" +
                                     "Regards \r\n" +
                                     "eSalon \r\n";

                    SmtpServer oServer = new SmtpServer("smtp.gmail.com");
                    oServer.User = "u13077130@tuks.co.za";
                    oServer.Password = "Ellemd33n";
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                    oServer.Port = 465;
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    try
                    {
                        progressBar1.Visible = true;
                        textBox1.Visible = false;

                        progressBar1.Value = 0;
                        progressBar1.Step = 1;

                        for (int k = 0; k <= 100; k++)
                        {
                            progressBar1.PerformStep();
                        }

                        oSmtp.SendMail(oServer, oMail);
                        MessageBox.Show("Email was sent successfully!");

                        progressBar1.Visible = false;
                        textBox1.Visible = true;
                    }
                    catch (Exception ep)
                    {
                        MessageBox.Show("Failed to send email with the following error: ");
                        MessageBox.Show(ep.Message);
                    }
                }
            }

            textBox1.Clear();
            textBox1.Focus();
        }
    }
}
