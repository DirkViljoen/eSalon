<%@ Page Language="c#" AutoEventWireup="true" Debug="true" ValidateRequest="false"
    CodePage="65001" %>

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

    public static void OnBatchSendMail(
        object sender, SmtpServer server, SmtpMail mail, Exception ep, ref bool cancel)
    {
        // you can insert the result to database in this subroutine.
        if (ep != null)
        {
            //something wrong, please refer to ep.Message
            //cancel = true; // set cancel to true can cancel the remained emails.
        }
        else
        {
            //delivered
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

        txtSubject.Text = "asp.net queue sample";
        string s = "Hi, \r\nthis sample demonstrates how to send email in asp.net with BatchSendMail.\r\n\r\n";
        s += "From:[$from]\r\n";
        s += "To:[$to]\r\n";
        s += "Subject:[$subject]\r\n\r\n";
        s += "If no server address was specified, the email will be delivered to the recipient's server directly";
        s += ".\r\n\r\n";
        s += "Please separate multiple recipients with link-break (enter). No matter how many recipients there are, EASendMail ";
        s += "will send the email in background.";

        txtBody.Text = s;
    }

    private void btnSend_Click(object sender, System.EventArgs e)
    {
        if (txtFrom.Text.Trim() == "" || txtTo.Text.Trim() == "")
        {
            lblDesc.Text = "Please input from, to";
            return;
        }

        AddressCollection oAddrs = new AddressCollection(txtTo.Text);
        int count = oAddrs.Count;
        int maxThreads = 5; // the maximum thread count to send email. you can increase or decrease this value.
        string charset = lstCharset.SelectedItem.Value;

        string fileName = null;
        byte[] content = null;

        bool hasAttach = false;

        if (attachment.PostedFile != null)
        {
            fileName = attachment.PostedFile.FileName;
            if (fileName != null && fileName != "")
            {
                try
                {
                    int fileLen = attachment.PostedFile.ContentLength;
                    content = new byte[fileLen];
                    System.IO.Stream stream = attachment.PostedFile.InputStream;
                    stream.Read(content, 0, fileLen);
                    stream.Close();
                    hasAttach = true;
                }
                catch (Exception exec)
                {
                    lblDesc.Text = String.Format("Exception with add attachment: {0}", exec.ToString());
                    return;
                }
            }
        }

        SmtpMail[] mails = new SmtpMail[count];
        SmtpServer[] servers = new SmtpServer[1];
        for (int i = 0; i < count; i++)
        {
            // For evaluation usage, please use "TryIt" as the license code, otherwise the 
            // "invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            // "trial version expired" exception will be thrown.

            // For licensed uasage, please use your license code instead of "TryIt", then the object
            // will never expire	

            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.Charset = charset;
            //From is a MailAddress object, in c#, it supports implicit converting from string.
            //The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

            //The example code without implicit converting
            // oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
            // oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
            // oMail.From = new MailAddress( "test@adminsystem.com" )	

            oMail.From = txtFrom.Text;
            oMail.Subject = String.Format("{0} {1}", txtSubject.Text, i);
            oMail.To.Add(oAddrs[i]);

            string body = txtBody.Text;
            body = body.Replace("[$from]", txtFrom.Text);
            body = body.Replace("[$to]", oAddrs[i].ToString());
            body = body.Replace("[$subject]", txtSubject.Text);

            if (chkHtml.Checked)
                oMail.HtmlBody = body;
            else
                oMail.TextBody = body;

            if (hasAttach)
            {
                oMail.AddAttachment(fileName, content);
            }

            mails[i] = oMail;
        }

        SmtpServer oServer = new SmtpServer(txtServer.Text);
        if (oServer.Server.Length > 0 && chkAuth.Checked)
        {
            oServer.User = txtUser.Text;
            oServer.Password = txtPassword.Text;
        }

        if (oServer.Server.Length > 0 && chkSSL.Checked)
        {
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
        }

        servers[0] = oServer;

        SmtpClient oSmtp = new SmtpClient();

        string err = "";
        try
        {
            //oSmtp.LogFileName = "c:\\smtp.log";
            // if the log wasn't able to be generated, 
            // please create a smtp.log file on C: and assign everyone read-write permission to this
            // file, then try it again.

            // if you want to catch the result in OnBatchSendMail event, please add the following code
            //oSmtp.OnBatchSendMail += new EASendMail.SmtpClient.OnBatchSendMailEventHandler( OnBatchSendMail );
            oSmtp.BatchSendMail(maxThreads, servers, mails);
            lblDesc.Text = "EASendMail will send message in background!";
        }
        catch (System.Exception exp)
        {
            err = String.Format("Exception: Common: {0}", exp.Message);
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
    <title>ASP.NET, C# BatchSend Sample For EASendMail</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP.NET, C# BatchSend Sample for EASendMail SMTP Component</div>
    <form id="thisForm" enctype="multipart/form-data" method="post" runat="server">
    <div id="s_result">
        <asp:Literal ID="result" Text="" runat="server" />
    </div>
    <div id="div_main">
        <asp:Label ID="lblDesc" runat="server" ForeColor="red"></asp:Label>
        <div class="comments">
            Basically, we don't recommend to use this method. To send email with multiple threads
            in desktop application, we suggest that you use the BeginSendMail method, for more
            detail, please refer to mass.vb and mass.csharp samples in EASendMail installation
            package; To send bulk emails in web application, we suggest that you use the EASendMail
            Service, please refer to <b>asp_net_queue sample</b>.
            <br />
            Only in the following case, we recommend that you use this method.<br />
            <b>You need to send bulk emails in web application and your website is hosted by ISP,
                you don't have the permission to install the EASendMail Service.</b>
            <br />
            <br />
            If you use BatchSendMail method to send bulk emails in ASP.NET page, the advantage
            is: no matter how many emails you need to send, this method returns immediately,
            EASendMail will send the emails in background; The disadvantage is: you can't get
            the result immediately, you have to trace the email by log file or get the result
            by catching the OnBatchSendMail event (please refer to source code).
            <br />
            <br />
            Please separate multiple recipients in *To with line-break( Enter );<br />
            For example:<br />
            <br />
            test@example.com<br />
            Tester &lt;test@example.com&gt;<br />
            <br />
            <br />
        </div>
        <table width="100%" cellpadding="0" cellspacing="0">
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
                    <asp:CheckBox ID="chkSSL" runat="server" Text="My server requires secure connection(SSL)">
                    </asp:CheckBox>
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
                    <asp:TextBox ID="txtTo" TextMode="MultiLine" Width="95%" Height="121px" runat="server"></asp:TextBox>
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
                    <input type="File" id="attachment" name="attachment" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Email Body
                </td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Width="95%" Height="121px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
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
