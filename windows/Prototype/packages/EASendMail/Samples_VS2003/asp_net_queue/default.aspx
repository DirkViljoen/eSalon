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
    // | Ivan Lui ( ivan@EmailArchitect.net )
    // |        
    // |
    // =================================================================================

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

        txtSubject.Text = "asp.net queue sample";
        string s = "Hi {$var_rcptname}, \r\nthis sample demonstrates how to send email in asp.net with EASendMail service.\r\n\r\n";
        s += "From:[$from]\r\n";
        s += "To:{$var_rcptaddr}\r\n";
        s += "Subject:[$subject]\r\n\r\n";
        s += "recipient email address and name variable will be replaced by EASendMail service automatically\r\n\r\n";
        s += "If no server address was specified, the email will be delivered to the recipient's server by the setting in ";
        s += "EASendMail Service.\r\n\r\n";
        s += "Please separate multiple recipients with link-break (enter). No matter how many recipients there are, EASendMail ";
        s += "service will send the email in background.";



        txtBody.Text = s;
    }

    private void btnSend_Click(object sender, System.EventArgs e)
    {
        if (txtFrom.Text.Trim() == "" || txtTo.Text.Trim() == "")
        {
            lblDesc.Text = "Please input from, to";
            return;
        }

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

        // if you want to EASendMail service send the email after 10 minutes, please use the following code.
        //oMail.Date = System.DateTime.Now.AddMinutes( 10 );

        //To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
        // multiple address are separated with (,;)
        //The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

        //The example code without implicit converting
        // oMail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" );
        // oMail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");				
        oMail.To = txtTo.Text;
        //You can add more recipient by Add method
        // oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"));


        if (oMail.To.Count >= 3)
        {	//if you want to every email only display the current recipient, you can use
            //the following code
            //To avoid too many email address in the To header, using the following code can only
            //display the current recipient
            oMail.Headers.ReplaceHeader("To", "\"{$var_rcptname}\" <{$var_rcptaddr}>");
            oMail.Headers.ReplaceHeader("X-Rcpt-To", new AddressCollection(txtTo.Text).ToEncodedString(HeaderEncodingType.EncodingAuto, charset));
        }

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
        body = body.Replace("[$subject]", txtSubject.Text);

        if (chkHtml.Checked)
            oMail.HtmlBody = body;
        else
            oMail.TextBody = body;

        SmtpClient oSmtp = new SmtpClient();
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

        string err = "";
        try
        {
            if ((oServer.Server.Length == 0) && (oServer.Protocol == ServerProtocol.SMTP))
            {
                oServer = null;
            }
            
            oSmtp.SendMailToQueue(oServer, oMail);
            lblDesc.Text = "Message was sent to EASendMail service successfully!";
        }
        catch (System.Exception exp)
        {
            err = String.Format("Exception: Common: {0}", exp.Message);
            err += "<br>Please make sure you installed EASendMail Service on the server!";
        }

        if (err.Length > 0)
        {
            lblDesc.Text = err;
        }


    }

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP.NET, C# Sample For EASendMail Queue</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP.NET, C# Sample for EASendMail Queue</div>
    <form id="thisForm" enctype="multipart/form-data" method="post" runat="server">
    <div id="div_main">
        <div class="comments">
            To run this sample, you should download <a href="http://www.emailarchitect.net/webapp/download/easendmailservice.exe">
                EASendMail Service</a> and install it on the server. If you don't specify a
            smtp server, EASendMail will send email by the setting in EASendMail Service.
            <br />
            If the email was unable to the recipient, a delivery report will be sent to the
            sender email address.
            <br />
            <br />
            Please separate multiple recipients in *To with line-break( Enter );<br>
            For example:<br>
            <br>
            test@example.com<br>
            Tester &lt;test@example.com&gt;<br>
            <br>
            <br>
        </div>
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
                <td width="80%">
                    <asp:TextBox ID="txtServer" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;
                </td>
                <td width="80%">
                    <asp:CheckBox ID="chkAuth" runat="server" Text="My server requires user authentication">
                    </asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    User:
                </td>
                <td width="80%">
                    <asp:TextBox ID="txtUser" runat="server" Width="50%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    Password:
                </td>
                <td width="80%">
                    <asp:TextBox ID="txtPassword" runat="server" Width="50%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;
                </td>
                <td width="80%">
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
                <td width="20%">
                    *From:
                </td>
                <td width="80%">
                    <asp:TextBox ID="txtFrom" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    *To:
                </td>
                <td width="80%">
                    <asp:TextBox ID="txtTo" TextMode="MultiLine" Width="95%" Height="121px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    Encoding:
                </td>
                <td width="80%">
                    <asp:DropDownList ID="lstCharset" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                </td>
                <td width="80%">
                    <asp:CheckBox ID="chkHtml" runat="server" Text="HTML format"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    Subject:
                </td>
                <td width="80%">
                    <asp:TextBox ID="txtSubject" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    Attachment:
                </td>
                <td width="80%">
                    <input type="File" id="attachment" name="attachment" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="20%">
                    Email Body
                </td>
                <td width="80%">
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Width="95%" Height="121px"></asp:TextBox><br>
                </td>
            </tr>
            <tr>
                <td width="20%">
                </td>
                <td width="80%">
                    <asp:Button ID="btnSend" runat="server" Width="131px" Text="Send" OnClick="btnSend_Click">
                    </asp:Button>
                    - <a href="default.aspx">Reset</a>
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
