<%@ Page language="JScript" AutoEventWireup="true" validateRequest=false CodePage=65001%>
<%@ Import Namespace="EASendMail"%>
<script language="JScript" runat="server">
//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2006  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// | FILE: default.aspx
// | SYNOPSIS: SAMPLE FILE FOR EASendMail TO SEND EMAIL IN JSCRIPT.NET/ASP.NET
// | Ivan Lui ( ivan@EmailArchitect.net )
// |        
// |
// =================================================================================

function Page_Load(sender:Object, e:EventArgs)
{
	if( lstCharset.Items.Count > 0 )
		return;
		
	var nCount:int = 28;
	var arCharset:String[]  = new Array( "Arabic(Windows):windows-1256", 
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
							);
						

	var charset:String = "utf-8";
	
	for( var i:int = 0; i < nCount; i++ )
	{
		var buf:String = arCharset[i];
		var pos:int = buf.IndexOf( ":" );
		if( pos < 0 )
		{
			continue;
		}
		
		var s1:String = buf.Substring( 0, pos );
		var s2:String = buf.Substring( pos+1 );
		var item:ListItem = new ListItem( s1, s2 );

		if( s2 == charset )
		{
			item.Selected = true;
		}
		
		lstCharset.Items.Add( item );	
	}
	
    lstProtocol.Items.Add("SMTP Protocol - Recommended");
    lstProtocol.Items.Add("Exchange Web Service - Exchange 2007/2010");
    lstProtocol.Items.Add("Exchange WebDav - Exchange 2000/2003"); 
    	
	txtSubject.Text = "asp.net queue sample";
	//{$var_srecord:name} will be replaced by EASendMail automatically.
	var s:String = "Hi {$var_srecord:name}, \r\nthis sample demonstrates how to send email in asp.net with EASendMail service.\r\n\r\n";
	s += "From:[$from]\r\n";
	s += "To:{$var_srecord:address}\r\n"; //{$var_srecord:address} will be replaced by EASendMail automatically.
	s += "Subject:[$subject]\r\n\r\n";
	s += "recipient email address and name variable will be replaced by EASendMail service automatically\r\n\r\n";
	s += "If no server address was specified, the email will be delivered to the recipient's server by the setting in ";
	s += "EASendMail Service.\r\n\r\n";
	s += "Your id in database is {$var_srecord:id}.\r\n\r\n"; //{$var_srecord:id} will be replaced by EASendMail automatically.
	s += "No matter how many recipients there are, EASendMail ";
	s += "service will send the email in background.\r\n\r\n";
	
	s += "Database connection string reference:\r\n";
	s += "MS SQL server\r\n";
	s += "\"Driver={SQL Server};Server=localhost;Database=pubs;Uid=sa;Pwd=asdasd;\"\r\n"; 
	s += "MS Access\r\n";
	s += "\"Driver={Microsoft Access Driver (*.mdb)};Dbq=C:\\mydatabase.mdb;Uid=Admin;Pwd=;\"\r\n";
	s += "ORACLE\r\n";
	s += "\"Driver={Microsoft ODBC for Oracle};Server=OracleServer.world;Uid=Username;Pwd=asdasd;\"\r\n"; 
	s += "MySQL\r\n";
	s += "\"DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=myDatabase;USER=myUsername;PASSWORD=myPassword;OPTION=3;\"";
	
	txtBody.Text = s;

	txtDataConnection.Text = "Driver={Microsoft Access Driver (*.mdb)};Dbq={$var_easendmailpath}\\easendmail_demo.mdb;Uid=;Pwd=;";
	txtTo.Text = "SELECT id, name, address FROM Recipients";
	txtOnSuccess.Text = "INSERT INTO sentlog ( server, email ) VALUES( '{$var_server}', '{$var_rcptaddr}' )";
	txtOnError.Text = "INSERT INTO errorlog( email, server, errorcode, errordescription ) VALUES( '{$var_rcptaddr}', '{$var_server}', '{$var_errcode}', '{$var_errdesc}' )";

}

function btnSend_Click( sender:Object ,  e:System.EventArgs)
{
	if( txtFrom.Text.Trim() == "" || txtTo.Text.Trim() == "" )
	{
		lblDesc.Text = "Please input from, to";
		return;
	}
	
	//For evaluation usage, please use "TryIt" as the license code, otherwise the 
	//"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
	//"trial version expired" exception will be thrown.

	//For licensed uasage, please use your license code instead of "TryIt", then the object
	//will never expire		
	var oMail:SmtpMail =  new SmtpMail("TryIt");
	
	var charset:String = lstCharset.SelectedItem.Value;
	oMail.Charset = charset;
	
	//From is a MailAddress object, in c#, it supports implicit converting from string.
	//The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"
				
	//The example code without implicit converting
	// oMail.From = new MailAddress( "Tester", "test@adminsystem.com" );
	// oMail.From = new MailAddress( "Tester<test@adminsystem.com>" );
	// oMail.From = new MailAddress( "test@adminsystem.com" );		
	oMail.From	= new MailAddress(txtFrom.Text);
	oMail.Subject = txtSubject.Text;
	// if you want to EASendMail service send the email after 10 minutes, please use the following code.
	//oMail.Date = System.DateTime.Now.AddMinutes( 10 );
	
	//pick "name" field as the recipient name and "address" field as the recipient address.
	//you can also use {$var_srecord:fieldname} to pick any field in X-Sql-Select statement
	// and put it to subject, bodytext, then EASendMail will replace it automatially.
	
	oMail.Headers.ReplaceHeader( "To", "\"{$var_srecord:name}\" <{$var_srecord:address}>" );
	oMail.Headers.ReplaceHeader( "X-Rcpt-To", "{$var_srecord:address}");
	
	// For more connection string
	// MS SQL server
	//"Driver={SQL Server};Server=localhost;Database=pubs;Uid=sa;Pwd=asdasd;" 
	// MS Access
	//"Driver={Microsoft Access Driver (*.mdb)};Dbq=C:\mydatabase.mdb;Uid=Admin;Pwd=;"
	// ORACLE
	//"Driver={Microsoft ODBC for Oracle};Server=OracleServer.world;Uid=Username;Pwd=asdasd;" 
	// MySQL
	//"DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=myDatabase;USER=myUsername;PASSWORD=myPassword;OPTION=3;" 
	// other connection string
	// please refer to: http://www.connectionstrings.com/
	
	// To check the database error, please use EASendMail Service Manager->Journal->System Error
	
	oMail.Headers.ReplaceHeader( "X-Data-Connection", txtDataConnection.Text );
	oMail.Headers.ReplaceHeader( "X-Sql-Select", txtTo.Text );
	oMail.Headers.ReplaceHeader( "X-Sql-OnSentSuccess", txtOnSuccess.Text );
	oMail.Headers.ReplaceHeader( "X-Sql-OnSentError", txtOnError.Text );
	
	var fileName:String = null;
	if( attachment.PostedFile != null )
	{
		fileName = attachment.PostedFile.FileName;
		if( fileName != null && fileName != "" )
		{
			try
			{				
				var fileLen:int =  attachment.PostedFile.ContentLength;
				var content:Array = new byte[fileLen];
				var stream:System.IO.Stream = attachment.PostedFile.InputStream;
				stream.Read(content, 0, fileLen );
				stream.Close();
				oMail.AddAttachment( fileName, content );
			}
			catch( exec:Exception )
			{
				lblDesc.Text = String.Format( "Exception with add attachment: {0}", exec.ToString() );
				return;
			}
		}
	}	

	var body:String = txtBody.Text;
	body = body.Replace( "[$from]", txtFrom.Text );
	body = body.Replace( "[$subject]", txtSubject.Text );
	
	if(	chkHtml.Checked )
		oMail.HtmlBody = body;
	else
		oMail.TextBody = body;

	var oSmtp:SmtpClient = new SmtpClient();
	var oServer:SmtpServer = new SmtpServer(txtServer.Text);
	oServer.Protocol = lstProtocol.SelectedIndex;
	
	if( oServer.Server.Length > 0 && chkAuth.Checked )
	{
		oServer.User = txtUser.Text;
		oServer.Password = txtPassword.Text;
	}

	if( oServer.Server.Length > 0 && chkSSL.Checked )
	{
		oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
	}
	
	
	var err:String = "";
	try
	{
		if( oServer.Server.Length == 0 && oServer.Protocol == ServerProtocol.SMTP )
			oServer = null;	
		oSmtp.SendMailToQueue( oServer, oMail );
		lblDesc.Text = "Message was sent to EASendMail service successfully!";
	}
	catch( exp:System.Exception )
	{
		err = String.Format( "Exception: Common: {0}", exp.Message );
		err += "<br>Please make sure you installed EASendMail Service on the server!";					
	}	
	
	if( err.Length > 0 )
	{
		lblDesc.Text = err;
	}
}

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP.NET, JScript Sample For EASendMail Queue Database</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP.NET, JScript Sample for EASendMail Queue Database</div>
    <form id="thisForm" enctype="multipart/form-data" method="post" runat="server">
    <div id="div_main">
        <div class="comments">
            To run this sample, you should download <a href="http://www.emailarchitect.net/webapp/download/easendmailservice.exe">
                EASendMail Service</a> and install it on the server. If you don't specify a
            smtp server, EASendMail will send email by the setting in EASendMail Service.
            <br />
            <br />
            easendmail_demo.mdb is a demo database in EASendMail service(c:\program files\EASendMailService),
            this sample will send email to all the recipients in recipients table, before run
            the sample, we suggest that you edit the recipients table at first.
            <br />
            <br />
            If the email could not be delivered to the recipient, a non-delivery report will be sent to the
            sender email address.
            <br />
            <br>
        </div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="150">
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
                    *To(X-Sql-Select):
                </td>
                <td>
                    <div class="comments">
                        X-Sql-Select: EASendMail will select the fields by the following sql statement before
                        sending email, then pick the recipient address from specified field(please refer
                        to the source code).
                    </div>
                    <asp:TextBox ID="txtTo" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    X-Data-Connection:
                </td>
                <td>
                    <div class="comments">
                        X-Data-Connection:<br>
                        EASendMail will use the following connection to connect to the database, the syntax
                        is same as ADO connection object.
                    </div>
                    <div class="comments">
                        {$var_easendmailpath}\easendmail_demo.mdb is a demo database in EASendMail service,
                        {$var_easendmailpath} will be replace by EASendMail with current path automatically.
                    </div>
                                        <div>
                        <b>x64: If your windows is x64 bit.
                        Please change the connection string to the following value.
                            You also need to download x64 MS Access driver and install it on your machine.
</b>
                    </div>
                                        <p><b>MS Access Database Driver Download</b></p>
                <p>
                <a href="http://www.microsoft.com/en-us/download/details.aspx?id=13255" target="_blank">Microsoft Access Database Engine 2010 Redistributable</a>
                    </p>
                    <p>
<b>
 Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq={$var_easendmailpath}\easendmail_demo.mdb;Uid=;Pwd=;</b>
                    </p>
                    <asp:TextBox ID="txtDataConnection"  runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    X-Sql-OnSentSuccess:
                </td>
                <td>
                    <div class="comments">
                        X-Sql-OnSentSuccess:EASendMail service will execute the following sql statement
                        on every email was sent successfully.
                    </div>
                    <asp:TextBox ID="txtOnSuccess" Width="95%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    X-Sql-OnSentError:
                </td>
                <td>
                    <div class="comments">
                        X-Sql-OnSentError:EASendMail service will execute the following sql statement on
                        every email could not be delivered to recipient.
                    </div>
                    <asp:TextBox ID="txtOnError" Width="95%" runat="server"></asp:TextBox>
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
                    <input type="File" id="attachment" name="attachment" runat="server">
                </td>
            </tr>
            <tr>
                <td>
                    Email Body
                </td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Width="95%" Height="121px"></asp:TextBox><br>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSend" runat="server" Width="131px" Text="Send" OnClick="btnSend_Click">
                    </asp:Button> - <a href="default_jscript.aspx">Reset</a>
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