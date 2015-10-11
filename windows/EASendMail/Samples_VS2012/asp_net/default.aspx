<%@ Page Language="c#" AutoEventWireup="true" ValidateRequest="false" CodePage="65001" %>

<%@ Import Namespace="EASendMail" %>
<script language="c#" runat="server">
    //  ===============================================================================
    // |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
    // |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
    // |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
    // |    PARTICULAR PURPOSE.                                                    |
    // |    Copyright (c)2006  ADMINSYSTEM SOFTWARE LIMITED                         |
    // |
    // | FILE: default.aspx
    // | SYNOPSIS: SAMPLE FILE FOR EASendMail TO SEND EMAIL IN CSharp.NET/ASP.NET
    // | Ivan lui ( ivan@EmailArchitect.net )
    // |        
    // |
    // =================================================================================
    private void _DirectSend(ref SmtpMail oMail, ref SmtpClient oSmtp)
    {
        result.Text = "";
        AddressCollection recipients = oMail.Recipients.Copy();
        int count = recipients.Count;
        for (int i = 0; i < count; i++)
        {
            string err = "";
            MailAddress address = recipients[i] as MailAddress;

            try
            {
                oMail.To.Clear();
                oMail.Cc.Clear();
                oMail.Bcc.Clear();

                oMail.To.Add(address);
                SmtpServer oServer = new SmtpServer("");

                oSmtp.SendMail(oServer, oMail);
                result.Text += String.Format("The message to &lt;{0}&gt; was sent to {1} successfully!<br />",
                                address.Address,
                                oSmtp.CurrentSmtpServer.Server);

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
                result.Text += String.Format("The message was unable to delivery to &lt;{0}&gt; due to <br />{1}<br />",
                        address.Address, Server.HtmlEncode(err));

            }

            result.Text += "<pre>SMTP Log\r\n\r\n";
            result.Text +=Server.HtmlEncode(oSmtp.SmtpConversation);
            result.Text += "</pre>\r\n";
        }
    }


    private void Page_Load(Object sender, EventArgs e)
    {
        if (lstCharset.Items.Count > 0)
            return;

        const int nCount = 28;
        string[] arCharset = new string[nCount] { "Arabic(Windows):windows-1256", 
						"Baltic(ISO):iso-8859-4", 
						"Baltic(Windows):windows-1257", 
						"Central Euporean(ISO):iso-8859-2", 
						"Central Euporean(ISO):windows-1250", 
						"Chinese Simplified(GB18030):GB18030", 
						"Chinese Simplified(GB2312):gb2312", 
						"Chinese Simplified(HZ):hz-gb-2312", 
						"Chinese Traditional(Big5):big5", 
						"Cyrillic(ISO):iso-8859-5", 
						"Cyrillic(KOI8-R):koi8-r", 
						"Cyrillic(KOI8-U):koi8-u", 
						"Cyrillic(Windows):windows-1251", 
						"Greek(ISO):iso-8859-7", 
						"Greek(Windows):windows-1253", 
						"Hebrew(Windows):windows-1255", 
						"Japanese(JIS):iso-2022-jp", 
						"Korean:ks_c_5601-1987", 
						"Korean(EUC):euc-kr", 
						"Latin 9(ISO):iso-8859-15", 
						"Thai(Windows):windows-874", 
						"Turkish(ISO):iso-8859-9", 
						"Turkish(Windows):windows-1254", 
						"Unicode(UTF-7):utf-7", 
						"Unicode(UTF-8):utf-8",
						"Vietnames(Windows):windows-1258", 
						"Western European(ISO):iso-8859-1",
						"Western European(Windows):Windows-1252" 
							};


        string charset = "utf-8";

        for (int i = 0; i < nCount; i++)
        {
            string buf = arCharset[i];
            int pos = buf.IndexOf(":");
            if (pos < 0)
            {
                continue;
            }

            string s1 = buf.Substring(0, pos);
            string s2 = buf.Substring(pos + 1);
            ListItem item = new ListItem(s1, s2);

            if (s2 == charset)
            {
                item.Selected = true;
            }

            lstCharset.Items.Add(item);
        }

        lstProtocol.Items.Add("SMTP Protocol - Recommended");
        lstProtocol.Items.Add("Exchange Web Service - Exchange 2007/2010");
        lstProtocol.Items.Add("Exchange WebDav - Exchange 2000/2003");

        txtSubject.Text = "asp.net sample";
        string s = "Hi, \r\nthis sample demonstrates how to send email in asp.net\r\n\r\n";
        s += "From:[$from]\r\n";
        s += "To:[$to]\r\n";
        s += "Subject:[$subject]\r\n\r\n";
        s += "If no server address was specified, the email will be delivered to the recipient's server directly; ";
        s += "However, if you don't have a static IP address, ";
        s += "many anti-spam filters will mark it as a junk-email.\r\n\r\n";
        s += "Please separate multiple recipients with comma(,).\r\n";


        txtBody.Text = s;
    }

    private void btnSend_Click(object sender, System.EventArgs e)
    {
        result.Text = "";
        
        if (txtFrom.Text.Trim() == "" || txtTo.Text.Trim() == "")
        {
            lblDesc.Text = "Please input from, to";
            return;
        }

        lblDesc.Text = "";

        //For evaluation usage, please use "TryIt" as the license code, otherwise the 
        //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
        //"trial version expired" exception will be thrown.

        //For licensed uasage, please use your license code instead of "TryIt", then the object
        //will never expire
        SmtpMail oMail = new SmtpMail("TryIt");

        string charset = lstCharset.SelectedItem.Value;
        oMail.Charset = charset;

        //From is a MailAddress object, in c#, it supports implicit converting from string.
        //The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

        //The example code without implicit converting
        // oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
        // oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
        // oMail.From = new MailAddress( "test@adminsystem.com" )	
        oMail.From = txtFrom.Text;
        oMail.Subject = txtSubject.Text;

        //To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
        // multiple address are separated with (,;)
        //The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

        //The example code without implicit converting
        // oMail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" );
        // oMail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");		
        oMail.To = txtTo.Text;
        //You can add more recipient by Add method
        // oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"));


        string fileName = null;
        if (attachment.PostedFile != null)
        {
            fileName = attachment.PostedFile.FileName;
            if (fileName != null && fileName != "")
            {
                try
                {
                    int fileLen = attachment.PostedFile.ContentLength;
                    byte[] content = new byte[fileLen];
                    System.IO.Stream stream = attachment.PostedFile.InputStream;
                    stream.Read(content, 0, fileLen);
                    stream.Close();
                    oMail.AddAttachment(fileName, content);
                }
                catch (Exception exec)
                {
                    lblDesc.Text = String.Format("Exception with add attachment: {0}", exec.ToString());
                    return;
                }
            }
        }

        string body = txtBody.Text;
        body = body.Replace("[$from]", txtFrom.Text);
        body = body.Replace("[$to]", txtTo.Text);
        body = body.Replace("[$subject]", oMail.Subject);

        if (chkHtml.Checked)
            oMail.HtmlBody = body;
        else
            oMail.TextBody = body;

        SmtpClient oSmtp = new SmtpClient();
        //To generate a log file for SMTP transaction, please use
        //oSmtp.LogFileName = "c:\\smtp.log";

        SmtpServer oServer = new SmtpServer(txtServer.Text);
        oServer.Protocol = (ServerProtocol)lstProtocol.SelectedIndex;
        
        if (oServer.Server.Length > 0 && chkAuth.Checked)
        {
            oServer.User = txtUser.Text;
            oServer.Password = txtPassword.Text;
        }

        if (oServer.Server.Length > 0 && chkSSL.Checked)
        {
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
        }

        if (oServer.Server.Length == 0)
        {
            //To send email to the recipient directly(simulating the smtp server), 
            //please add a Received header, 
            //otherwise, many anti-spam filter will make it as junk email.	
            System.Globalization.CultureInfo cur = new System.Globalization.CultureInfo("en-US");
            string gmtdate = System.DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur);
            gmtdate.Remove(gmtdate.Length - 3, 1);
            string recvheader = String.Format("from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;\r\n\t {1}",
                                oServer.HeloDomain,
                                gmtdate);

            oMail.Headers.Insert(0, new HeaderItem("Received", recvheader));
        }

        string err = "";
        bool bSmtpConversation = false;
        try
        {
            if (oServer.Server.Length == 0 && oMail.Recipients.Count > 1)
            {
                //To send email without specified smtp server, we have to send the emails one by one 
                // to multiple recipients. That is because every recipient has different smtp server.
                _DirectSend(ref oMail, ref oSmtp);
            }
            else
            {
                bSmtpConversation = true;
                oSmtp.SendMail(oServer, oMail);
                result.Text = String.Format("The message was sent to {0} successfully!<br />", oServer.Server);
            }
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
            result.Text = Server.HtmlEncode(err);
        }

        if (bSmtpConversation)
        {
            result.Text += "<pre>SMTP Log\r\n\r\n";
            result.Text += Server.HtmlEncode(oSmtp.SmtpConversation);
            result.Text += "</pre>\r\n";
        }

    }

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP.NET, C# Sample For EASendMail</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP.NET, C# Sample for EASendMail SMTP Component</div>
    <form id="thisForm" enctype="multipart/form-data" method="post" runat="server">

    <div id="s_result">
        <asp:Literal ID="result" Text="" runat="server" />
    </div>
    <div id="div_main">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Note:
                </td>
                <td>
                <asp:Label ID="lblDesc" runat="server" ForeColor="red"></asp:Label>
                    <div class="comments">
                        If you don't have a smtp server, please ignore this field. EASendMail will email
                        via dns lookup</div>
                </td>
            </tr>
            <tr>
                <td>
                    SMTP Server:
                </td>
                <td>
                    <asp:TextBox ID="txtServer" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkAuth" runat="server" Text="My server requires user authentication">
                    </asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    User:
                </td>
                <td>
                    <asp:TextBox ID="txtUser" runat="server" Width="50%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Password:
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" Width="50%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkSSL" runat="server" Text="My server requires secure connection(SSL)">
                    </asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    Server Protocol:
                </td>
                <td>
                    <asp:DropDownList ID="lstProtocol" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    *From:
                </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    *To:
                </td>
                <td>
                    <div class="comments">
                        Please separate multiple recipients with comma(,).</div>
                    <asp:TextBox ID="txtTo" size="50" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Encoding:
                </td>
                <td>
                    <asp:DropDownList ID="lstCharset" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkHtml" runat="server" Text="HTML format"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    Subject:
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Attachment:
                </td>
                <td>
                    <input type="File" id="attachment" name="attachment" runat="server" width="95%" />
                </td>
            </tr>
            <tr>
                <td>
                    Email Body
                </td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="multiline" Width="95%" Height="120px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSend" runat="server" Width="131px" Text="Send" OnClick="btnSend_Click">
                    </asp:Button> - <a href="default.aspx">Reset</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <div id="tailer">
        Technical Support: <a href="mailto:support@emailarchitect.net">support@emailarchitect.net</a>
        <br />
        <br />
        <a href="http://www.emailarchitect.net" target="_blank">2006 - 2012 Copyright &copy;
            AdminSystem Software Limited. All rights reserved.</a>
    </div>
</body>
</html>
