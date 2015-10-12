<%@ Page language="VBScript" AutoEventWireup="true" validateRequest=false CodePage=65001%>
<%@ Import Namespace="EASendMail"%>
<script language="VBScript" runat="server">
'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2001-2003  ADMINSYSTEM SOFTWARE LIMITED                         |
' |
' | FILE: default_vb.aspx
' | SYNOPSIS: SAMPLE FILE FOR EASendMail TO SEND EMAIL IN VB.NET/ASP.NET
' | Ivan Lui ( ivan@EmailArchitect.net )
' |        
' |
' =================================================================================
Sub Page_Load( sender As Object, e As EventArgs )
	If lstCharset.Items.Count > 0 Then
		Exit Sub
	End If
		
	Dim nCount As Integer = 28
	Dim arCharset() As String = { "Arabic(Windows):windows-1256", _
						"Baltic(ISO):iso-8859-4", _
						"Baltic(Windows):windows-1257", _ 
						"Central Euporean(ISO):iso-8859-2", _
						"Central Euporean(Windows):windows-1250", _ 
						"Chinese Simplified(GB18030):GB18030",  _
						"Chinese Simplified(GB2312):gb2312",  _
						"Chinese Simplified(HZ):hz-gb-2312", _
						"Chinese Traditional(Big5):big5", _
						"Cyrillic(ISO):iso-8859-5", _
						"Cyrillic(KOI8-R):koi8-r", _
						"Cyrillic(KOI8-U):koi8-u", _
						"Cyrillic(Windows):windows-1251", _ 
						"Greek(ISO):iso-8859-7", _
						"Greek(Windows):windows-1253", _ 
						"Hebrew(Windows):windows-1255",  _
						"Japanese(JIS):iso-2022-jp", _
						"Korean:ks_c_5601-1987", _
						"Korean(EUC):euc-kr", _
						"Latin 9(ISO):iso-8859-15", _ 
						"Thai(Windows):windows-874", _
						"Turkish(ISO):iso-8859-9", _
						"Turkish(Windows):windows-1254", _ 
						"Unicode(UTF-7):utf-7", _
						"Unicode(UTF-8):utf-8", _
						"Vietnames(Windows):windows-1258", _ 
						"Western European(ISO):iso-8859-1", _
						"Western European(Windows):Windows-1252" _ 
							}
						

	Dim charset As String = "utf-8"
	Dim i As Integer = 0
	
	For i = 0 To  nCount - 1
		Dim buf As String = arCharset(i)
		Dim pos As Integer = buf.IndexOf( ":" )
		If pos > 0 Then	
			Dim s1 As String = buf.Substring( 0, pos )
			Dim s2 As String = buf.Substring( pos+1 )
			Dim item As ListItem = New ListItem( s1, s2 )
			If s2 = charset Then
				item.Selected = True
			End If
			lstCharset.Items.Add( item )
		End If	
	Next
	
    lstProtocol.Items.Add("SMTP Protocol - Recommended")
    lstProtocol.Items.Add("Exchange Web Service - Exchange 2007/2010")
    lstProtocol.Items.Add("Exchange WebDav - Exchange 2000/2003")

	txtSubject.Text = "asp.net queue sample"
	Dim s As String = "Hi {$var_rcptname}, " & Chr(13) & Chr(10) & "this sample demonstrates how to send email in asp.net with EASendMail service." & Chr(13) & Chr(10) & Chr(13) & Chr(10)
	s += "From:[$from]" & Chr(13) & Chr(10)
	s += "To:{$var_rcptaddr}" & Chr(13) & Chr(10)
	s += "Subject:[$subject]" & Chr(13) & Chr(10) & Chr(13) & Chr(10)
	s += "recipient email address and name variable will be replaced by EASendMail service automatically" & Chr(13) & Chr(10) & Chr(13) & Chr(10)
	s += "If no server address was specified, the email will be delivered to the recipient's server by the setting in "
	s += "EASendMail Service." & Chr(13) & Chr(10) & Chr(13) & Chr(10)
	s += "Please separate multiple recipients with link-break (enter). No matter how many recipients there are, EASendMail "
	s += "service will send the email in background."
	
	txtBody.Text = s
End Sub

Sub btnSend_Click( sender As Object ,  e As System.EventArgs)
	If txtFrom.Text.Trim() = "" Or txtTo.Text.Trim() = "" Then
		lblDesc.Text = "Please input sender, recipient"
		Exit Sub
	End If
	
    'For evaluation usage, please use "TryIt" as the license code, otherwise the 
    '"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
    '"trial version expired" exception will be thrown.

   'For licensed uasage, please use your license code instead of "TryIt", then the object
   'will never expire		
	Dim oMail As SmtpMail = New SmtpMail("TryIt")
	
	Dim charset As String = lstCharset.SelectedItem.Value
	oMail.Charset = charset
	
	'If you want to specify a reply address
    'oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" )

    'From is a MailAddress object
    'The example code
    ' oMail.From = New MailAddress( "Tester", "test@adminsystem.com" )
    ' oMail.From = New MailAddress( "Tester<test@adminsystem.com>" )
    ' oMail.From = New MailAddress( "test@adminsystem.com" )	
	oMail.From = New MailAddress( txtFrom.Text )
	oMail.Subject = txtSubject.Text
	
	' if you want to EASendMail service send the email after 10 minutes, please use the following code.
	'oMail.Date = System.DateTime.Now.AddMinutes( 10 )
		
    'To, Cc and Bcc is a AddressCollection object
    'The example code
    'oMail.To = New AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" )
    'oMail.To = New AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>")		
	oMail.To = New AddressCollection( txtTo.Text )
    'You can add more recipient by Add method
    'oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"))		
	
	'To avoid too many email address in the To header, using the following code can only
	'display the current recipient
	If oMail.To.Count >= 3 Then
		'if you want to every email only display the current recipient, you can use
		'the following code
		oMail.Headers.ReplaceHeader( "To", """{$var_rcptname}"" <{$var_rcptaddr}>" )
		oMail.Headers.ReplaceHeader( "X-Rcpt-To", new AddressCollection( txtTo.Text ).ToEncodedString( HeaderEncodingType.EncodingAuto, charset ))
	End If
		
	Dim fileName As String = vbNullString
	If Not (attachment.PostedFile Is Nothing ) Then
		fileName = attachment.PostedFile.FileName
		If fileName <> vbNullString And fileName <> "" Then
			Try	
				Dim fileLen As Integer =  attachment.PostedFile.ContentLength
				Dim content(fileLen) As Byte
				Dim stream As System.IO.Stream = attachment.PostedFile.InputStream
				stream.Read(content, 0, fileLen )
				stream.Close()
				oMail.AddAttachment( fileName, content )
			Catch exec As Exception 
				lblDesc.Text = String.Format( "Exception with add attachment: {0}", exec.ToString() )
				Exit Sub
			End Try
		End If
	End If
			
	Dim body As String = txtBody.Text
	body = body.Replace( "[$from]", txtFrom.Text )
	body = body.Replace( "[$subject]", txtSubject.Text )
	
	If	chkHtml.Checked Then
		oMail.HtmlBody = body
	Else
		oMail.TextBody = body
	End If
	
	Dim oSmtp As SmtpClient = New SmtpClient()
	Dim oServer As SmtpServer = New SmtpServer( txtServer.Text )
    oServer.Protocol = lstProtocol.SelectedIndex
	
	If oServer.Server.Length > 0 And chkAuth.Checked Then
		oServer.User = txtUser.Text
		oServer.Password = txtPassword.Text
	End If

	If oServer.Server.Length > 0 And chkSSL.Checked Then
		oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
	End If
		    
    Dim errStr As String = ""
    
    Try
		If  oServer.Server.Length = 0 And oServer.Protocol = ServerProtocol.SMTP  Then
			oServer = Nothing
		End If
		oSmtp.SendMailToQueue( oServer, oMail )
        lblDesc.Text = "Message was sent to EASendMail service successfully!"
    Catch exp As System.Exception
        errStr = String.Format("Exception: Common: {0}", exp.Message)
  		errStr += "<br>Please make sure you installed EASendMail Service on the server!"				
    End Try
    
    If errStr.Length > 0 Then
        lblDesc.Text = errStr
    End If 		
End Sub

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP.NET, VB Sample For EASendMail Queue</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP.NET, VB Sample for EASendMail Queue</div>
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
                    - <a href="default_vb.aspx">Reset</a>
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