using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EASendMail;


namespace pocket.mobile.cs
{
    public partial class Form1 : Form
    {

        private bool m_bcancel = false;
        private ArrayList m_arAttachment = new ArrayList();


        #region	EASendMail EventHandler
        void OnIdle(object sender, ref bool cancel)
        {
            cancel = m_bcancel;
            if (!cancel)
            {
                //Current object is waiting server reponse or connecting server, 
                //that means current object is idle. Application.DoEvents
                //can processes all Windows(form) messages(events) currently in the message queue. 
                //If you don't invoke this method, the application will not respond the Cancel and other
                //events.
                Application.DoEvents();
            }
        }

        void OnConnected(object sender, ref bool cancel)
        {
            sbStatus.Text = "Connected";
            cancel = m_bcancel;
        }

        void OnSendingDataStream(object sender, int sent, int total, ref bool cancel)
        {
            sbStatus.Text = "Sending ...";
            long t = sent;
            t = t * 100;
            t = t / total;
            int x = (int)t;
            pgSending.Value = x;
            cancel = m_bcancel;
            if (sent == total)
                sbStatus.Text = "Disconnecting ...";

            Application.DoEvents();
        }

        void OnAuthorized(object sender, ref bool cancel)
        {
            sbStatus.Text = "Authorized";
            cancel = m_bcancel;
        }

        void OnSecuring(object sender, ref bool cancel)
        {
            sbStatus.Text = "Securing ...";
            cancel = m_bcancel;
        }

        #endregion



        private void _ChangeAuthStatus()
        {
            textUser.Enabled = chkAuth.Checked;
            textPassword.Enabled = chkAuth.Checked;
        }

        private void _Init()
        {


            //_InitCharset();
            _ChangeAuthStatus();
            this.attachmentDlg = new System.Windows.Forms.OpenFileDialog();

        }


        private void btnSend_Click(object sender, System.EventArgs e)
        {
            if (textFrom.Text.Length == 0)
            {
                MessageBox.Show("Please input From!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>");
                return;
            }

            if (textTo.Text.Length == 0 &&
                textCc.Text.Length == 0)
            {
                MessageBox.Show("Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients");
                return;
            }

            if (textServer.Text.Length == 0)
            {
                MessageBox.Show("Please input server address");
                return;
            }
            btnSend.Enabled = false;
            btnCancel.Enabled = true;
            m_bcancel = false;

            //For evaluation usage, please use "TryIt" as the license code, otherwise the 
            //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            //"trial version expired" exception will be thrown.

            //For licensed uasage, please use your license code instead of "TryIt", then the object
            //will never expire
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();
            //To generate a log file for SMTP transaction, please use
            //oSmtp.LogFileName = "c:\\smtp.log";
            string err = "";

            try
            {
                oMail.Reset();
                //If you want to specify a reply address
                //oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" );

                //From is a MailAddress object, in c#, it supports implicit converting from string.
                //The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

                //The example code without implicit converting
                // oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
                // oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
                // oMail.From = new MailAddress( "test@adminsystem.com" )
                oMail.From = textFrom.Text;

                //To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
                // multiple address are separated with (,;)
                //The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

                //The example code without implicit converting
                // oMail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" );
                // oMail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");

                oMail.To = textTo.Text;
                //You can add more recipient by Add method
                // oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"));

                oMail.Cc = textCc.Text;
                oMail.Subject = textSubject.Text;


                string body = textBody.Text;
                oMail.TextBody = textBody.Text;

                int count = m_arAttachment.Count;
                for (int i = 0; i < count; i++)
                {
                    //    //Add attachment
                    oMail.AddAttachment(m_arAttachment[i] as string);
                }

                SmtpServer oServer = new SmtpServer(textServer.Text);


                if (chkAuth.Checked)
                {
                    oServer.User = textUser.Text;
                    oServer.Password = textPassword.Text;
                }

                if (chkSSL.Checked)
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                //Catching the following events is not necessary, 
                //just make the application more user friendly.
                //If you use the object in asp.net/windows service or non-gui application, 
                //You need not to catch the following events.
                //To learn more detail, please refer to the code in EASendMail EventHandler region
                oSmtp.OnIdle += new SmtpClient.OnIdleEventHandler(OnIdle);
                oSmtp.OnAuthorized += new SmtpClient.OnAuthorizedEventHandler(OnAuthorized);
                oSmtp.OnConnected += new SmtpClient.OnConnectedEventHandler(OnConnected);
                oSmtp.OnSecuring += new SmtpClient.OnSecuringEventHandler(OnSecuring);
                oSmtp.OnSendingDataStream += new SmtpClient.OnSendingDataStreamEventHandler(OnSendingDataStream);


                sbStatus.Text = "Connecting ... ";
                pgSending.Value = 0;

                oSmtp.SendMail(oServer, oMail);

                MessageBox.Show(String.Format("The message was sent to {0} successfully!",
                    oSmtp.CurrentSmtpServer.Server));

                sbStatus.Text = "Completed";

                //If you want to reuse the mail object, please reset the Date and Message-ID, otherwise
                //the Date and Message-ID will not change.
                //oMail.Date = System.DateTime.Now;
                //oMail.ResetMessageID();
                //oMail.To = "another@example.com";
                //oSmtp.SendMail( oServer, oMail );
            }
            catch (SmtpTerminatedException exp)
            {
                err = exp.Message;
            }
            catch (SmtpServerException exp)
            {
                err = String.Format("Exception: Server Respond: {0}", exp.ErrorMessage);
            }
            catch (System.Net.Sockets.SocketException exp)
            {
                err = String.Format("Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message);
            }
            catch (System.ComponentModel.Win32Exception exp)
            {
                err = String.Format("Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message);
            }
            catch (System.Exception exp)
            {
                err = String.Format("Exception: Common: {0}", exp.Message);
            }

            if (err.Length > 0)
            {
                MessageBox.Show(err);
                sbStatus.Text = err;
            }
            //to get more debug information, please use
            //MessageBox.Show( oSmtp.SmtpConversation );

            btnSend.Enabled = true;
            btnCancel.Enabled = false;

        }



        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            //attachmentDlg.Reset();
            //attachmentDlg.Multiselect = true;
            //attachmentDlg.CheckFileExists = true;
            //attachmentDlg.CheckPathExists = true;
            if (attachmentDlg.ShowDialog() != DialogResult.OK)
                return;
            //Only one instance of the file can be select**Not Support multiple file**
            string fileName = attachmentDlg.FileName;
            m_arAttachment.Add(fileName);
            int pos = fileName.LastIndexOf("\\");
            if (pos != -1)
                fileName = fileName.Substring(pos + 1);

            textAttachments.Text += fileName;
            textAttachments.Text += ";";


            //string[] attachments = attachmentDlg.FileNames;
            //int nLen = attachments.Length;
            //for (int i = 0; i < nLen; i++)
            //{
            //    m_arAttachment.Add(attachments[i]);
            //    string fileName = attachments[i];
            //    int pos = fileName.LastIndexOf("\\");
            //    if (pos != -1)
            //        fileName = fileName.Substring(pos + 1);

            //    textAttachments.Text += fileName;
            //    textAttachments.Text += ";";
            //}
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            m_arAttachment.Clear();
            textAttachments.Text = "";
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            btnCancel.Enabled = false;
            m_bcancel = true;
        }


        public Form1()
        {
            InitializeComponent();
            _ChangeAuthStatus();
        }



        private void chkAuth_CheckStateChanged(object sender, EventArgs e)
        {
            _ChangeAuthStatus();
        }
    }
}