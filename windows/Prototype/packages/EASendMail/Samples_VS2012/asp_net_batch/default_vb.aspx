<%@ Page language="VBScript" AutoEventWireup="true" Debug="true" validateRequest=false CodePage=65001%>
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
Public Shared Sub OnBatchSendMail( _
	sender As Object, _
	server As SmtpServer, _
	mail As SmtpMail, _
	ep As Exception, _
	ByRef cancel As Boolean )
	
	' you can insert the result to database in this subroutine.
	If Not ( ep Is NOthing ) Then
		'something wrong, please refer to ep.Message
		'cancel = True ' set cancel to true can cancel the remained emails.
	Else
		'delivered
	End If
End Sub

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
	
	txtSubject.Text = "asp.net queue sample"
	Dim s As String = "Hi, " & Chr(13) & Chr(10) & "this sample demonstrates how to send email in asp.net with BatchSendMail." & Chr(13) & Chr(10) & Chr(13) & Chr(10)
	s += "From:[$from]" & Chr(13) & Chr(10)
	s += "To:[$to]" & Chr(13) & Chr(10)
	s += "Subject:[$subject]" & Chr(13) & Chr(10) & Chr(13) & Chr(10)
	s += "If no server address was specified, the email will be delivered to the recipient's server by directly"
	s += "." & Chr(13) & Chr(10) & Chr(13) & Chr(10)
	s += "Please separate multiple recipients with link-break (enter). No matter how many recipients there are, EASendMail "
	s += "will send the email in background."
	
	txtBody.Text = s
End Sub

Sub btnSend_Click( sender As Object ,  e As System.EventArgs)
	If txtFrom.Text.Trim() = "" Or txtTo.Text.Trim() = "" Then
		lblDesc.Text = "Please input sender, recipient"
		Exit Sub
	End If
	
	Dim oAddrs As New AddressCollection(txtTo.Text)
	Dim count As Integer = oAddrs.Count
	Dim maxThreads As Integer = 5 ' the maximum thread count to send email. you can increase or decrease this value.
	Dim charset As String = lstCharset.SelectedItem.Value
	
	Dim fileName As String
	Dim content() As Byte
	Dim hasAttach As Boolean = False
		
	If Not (attachment.PostedFile Is Nothing ) Then
		fileName = attachment.PostedFile.FileName
		If fileName <> vbNullString And fileName <> "" Then
			Try	
				Dim fileLen As Integer =  attachment.PostedFile.ContentLength
				ReDim content(fileLen)
				Dim stream As System.IO.Stream = attachment.PostedFile.InputStream
				stream.Read(content, 0, fileLen )
				stream.Close()
				hasAttach = True
			Catch exec As Exception 
				lblDesc.Text = String.Format( "Exception with add attachment: {0}", exec.ToString() )
				Exit Sub
			End Try
		End If
	End If
	
	Dim mails(count-1) As SmtpMail
	Dim servers(count-1) As SmtpServer
			
	For i As Integer = 0 To count - 1
		'For evaluation usage, please use "TryIt" as the license code, otherwise the 
		'"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
		'"trial version expired" exception will be thrown.

		'For licensed uasage, please use your license code instead of "TryIt", then the object
		'will never expire		
		Dim oMail As SmtpMail = New SmtpMail("TryIt")
		oMail.Charset = charset
		
		'If you want to specify a reply address
		'oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" )

		'From is a MailAddress object
		'The example code
		' oMail.From = New MailAddress( "Tester", "test@adminsystem.com" )
		' oMail.From = New MailAddress( "Tester<test@adminsystem.com>" )
		' oMail.From = New MailAddress( "test@adminsystem.com" )	
		oMail.From = New MailAddress( txtFrom.Text )
		oMail.Subject = String.Format( "{0} {1}", txtSubject.Text, i )
		
		'To, Cc and Bcc is a AddressCollection object
		'The example code
		'oMail.To = New AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" )
		'oMail.To = New AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>")		
		oMail.To.Add( oAddrs(i))

				
		Dim body As String = txtBody.Text
		body = body.Replace( "[$from]", txtFrom.Text )
		body = body.Replace( "[$to]", oAddrs(i).ToString() )
		body = body.Replace( "[$subject]", txtSubject.Text )
		
		If	chkHtml.Checked Then
			oMail.HtmlBody = body
		Else
			oMail.TextBody = body
		End If
		
		If  hasAttach Then
			oMail.AddAttachment( fileName, content )
		End If
	
		mails(i) = oMail
	Next
	
	Dim oServer As SmtpServer = New SmtpServer( txtServer.Text )
	
	If oServer.Server.Length > 0 And chkAuth.Checked Then
		oServer.User = txtUser.Text
		oServer.Password = txtPassword.Text
	End If

	If oServer.Server.Length > 0 And chkSSL.Checked Then
		oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
	End If
	
	servers(0) = oServer
    
    Dim errStr As String = ""
    Dim oSmtp As SmtpClient = New SmtpClient()
	
    Try
		
		' oSmtp.LogFileName = "c:\smtp.log"
		' if the log wasn't able to be generated, 
		' please create a smtp.log file on C: and assign everyone read-write permission to this
		' file, then try it again.
		
		' if you want to catch the result in OnBatchSendMail, please add the following code:
		'AddHandler oSmtp.OnBatchSendMail, AddressOf OnBatchSendMail
		oSmtp.BatchSendMail( maxThreads, servers, mails )
        lblDesc.Text = "EASendMail will send emails in background!"
        
    Catch exp As System.Exception
        errStr = String.Format("Exception: Common: {0}", exp.Message)
    End Try
    
    If errStr.Length > 0 Then
        lblDesc.Text = errStr
    End If 		
End Sub

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP.NET, VB BatchSend Sample For EASendMail</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP.NET, VB BatchSend Sample for EASendMail SMTP Component</div>
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
