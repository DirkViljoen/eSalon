<%@  codepage="65001" language="VBScript" %>
<%
' ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2010 ADMINSYSTEM SOFTWARE LIMITED                         |
' |
' | FILE: Default.ASP
' | SYNOPSIS: SAMPLE FILE FOR EASendMail ActiveX Object (VBScript)
' |
' =================================================================================*/
Response.CharSet = "utf-8" 
'========================================================
' fnParseAddr
'========================================================
Function fnParseAddr( src, Byref name, Byref addr )
	Dim nIndex
	nIndex = InStr(1, src, "<" )
	If nIndex > 0 Then
		name = Mid( src, 1, nIndex - 1 )
		addr = Mid( src, nIndex )
	Else
		name = ""
		addr = src
	End If
	
	Call fnTrim( name, " ,;<>""'" )
	Call fnTrim( addr, " ,;<>""'" )
End Function
'========================================================
' fnTrim
'========================================================
Function fnTrim( Byref src, trimer )
	Dim i, nCount, ch
	nCount = Len(src)
	For i = 1 To nCount
		ch = Mid( src, i, 1 )
		If InStr( 1, trimer, ch ) < 1 Then
			Exit For
		End If
	Next
	
	src = Mid( src, i )
	nCount = Len(src)
	For i = nCount To 1 Step -1
		ch = Mid( src, i, 1 )
		If InStr( 1, trimer, ch ) < 1 Then
			Exit For
		End If	
	Next
	src = Mid( src, 1, i )
End Function

'========================================================
' strpbrk
'========================================================
Function strpbrk( src, start, charset )
	strpbrk = 0
	
	Dim i, size, pos, ch
	
	size = Len(src)
	For i = start To size
		ch = Mid( src, i, 1 )
		
		If InStr( 1, charset, ch ) >= 1 Then
			strpbrk = i
			Exit Function
		End If
	Next

End Function

'========================================================
' SplitEx
'========================================================
Function SplitEx( src, charset )

	Dim find, start, pos, bquoted, ch, item, s
	Dim arItems()
	Dim nItems, nBuf, nIndex
	
	nIndex	= 0
	nBuf	= 10
	
	ReDim Preserve arItems(	nBuf )
	
	find	= charset & """"
	start	= 1
	pos		= 1
	bquoted	= False
		
	Do While True
		pos = strpbrk( src, pos, find )
		
		If pos <= 0 Then
			s = Mid( src, start ) 
			fnTrim s, charset
			If s <> "" Then
				If nIndex >= nBuf - 1 Then
					nBuf = nBuf + 10
					ReDim Preserve arItems(	nBuf )
				End If		
				arItems(nIndex) = s
			End If
			Exit Do
		End If
		
		ch = Mid( src, pos, 1 )

		If ch = """" Then
			If bquoted Then
				bquoted = False
			Else
				bquoted = True
			End If
		End If
		
		If Not bquoted And ch <> """" Then
			s = Mid( src, start, pos - start ) 
			fnTrim s, charset
			If s <> "" Then
				If nIndex >= nBuf - 1 Then
					nBuf = nBuf + 10
					ReDim Preserve arItems(	nBuf )
				End If			
				arItems(nIndex) = s
				nIndex = nIndex + 1
			End If
			pos		= pos + 1
			start	= pos
		Else
			pos		= pos + 1
		End If
	Loop
	
	ReDim Preserve arItems(nIndex)
	SplitEx = arItems
End Function

'===================================================================
' fnShowProtocol
'===================================================================
Sub fnShowProtocol( protocol )

   Dim arProtocol, i, nCount, s
   arProtocol = Array( "SMTP Protocol - Recommended:0", _
						"Exchange Web Service - Exchange 2007/2010:1", _
						"Exchange WebDav - Exchange 2000/2003:2" ) 

   nCount = UBound(arProtocol)
   
   Dim p
   p = CInt(protocol)
   Dim buf, s1, s2, pos
   
   For i = LBound(arProtocol) To nCount
		buf = arProtocol(i)
		pos = InStr( 1, buf, ":" )
		If pos > 0 Then
			s1 = Mid( buf, 1, pos-1 )
			s2 = Mid( buf, pos+1 )
		End If
		s = s & "<option value=""" & s2 & """"	
		If CInt(s2) = CInt(p) Then
			s = s & " selected"
		End If
		s = s & ">" & s1 & "</option>"
	Next
	Response.Write( s )	
End Sub

'===================================================================
' fnShowComposeCharset
'===================================================================
Sub fnShowComposeCharset()
	Dim arCharset, i, nCount, s, charset
	arCharset = Array( "Arabic(Windows):windows-1256", _
						"Baltic(ISO):iso-8859-4", _
						"Baltic(Windows):windows-1257", _
						"Central Euporean(ISO):iso-8859-2", _
						"Central Euporean(ISO):windows-1250", _
						"Chinese Simplified(GB18030):GB18030", _
						"Chinese Simplified(GB2312):gb2312", _
						"Chinese Simplified(HZ):hz-gb-2312", _
						"Chinese Traditional(Big5):big5", _
						"Cyrillic(ISO):iso-8859-5", _
						"Cyrillic(KOI8-R):koi8-r", _
						"Cyrillic(KOI8-U):koi8-u", _
						"Cyrillic(Windows):windows-1251", _
						"Greek(ISO):iso-8859-7", _
						"Greek(Windows):windows-1253", _
						"Hebrew(Windows):windows-1255", _
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
							)
						
	nCount = UBound(arCharset)
	s = ""
	charset = "utf-8"

	Dim buf, s1, s2, pos
	For i = LBound(arCharset) To nCount
		buf = arCharset(i)
		pos = InStr( 1, buf, ":" )
		If pos > 0 Then
			s1 = Mid( buf, 1, pos-1 )
			s2 = Mid( buf, pos+1 )
		End If
		s = s & "<option value=""" & s2 & """"	
		If LCase(s2) = charset Then
			s = s & " selected"
		End If
		s = s & ">" & s1 & "</option>"
	Next
	Response.Write( s )	
End Sub

'To send email without specified smtp server, we have to send the emails one by one 
' to multiple recipients. That is because every recipient has different smtp server.
Sub DirectSend( ByRef oSmtp, Recipients )
	Dim arTo
	arTo = SplitEx( Recipients, "," ) 'split the multiple address to an array 
	Dim i, count
	Dim addr
	count = UBound( arTo )
	For i = 0 To count
		addr = arTo(i)
		fnTrim addr, " ,;"
		If addr <> "" Then
			oSmtp.ClearRecipient 
			oSmtp.AddRecipientEx addr, 0 
			If oSmtp.SendMail() = 0 Then
				Response.Write "Message delivered to " & Server.HTMLEncode(addr) & " successfully!<br><br>" 
			Else 
				Response.Write "<font color=red>Failed to delivery to " & Server.HTMLEncode(addr) & " : " & Server.HTMLEncode(oSmtp.GetLastErrDescription()) & "</font><br><br>" 'Get last error description 
			End If			
		End If
	Next
End Sub

Sub SendMail()
	Dim from, recipients
	from = Request.Form("from")
	recipients = Request.Form("recipients")
	
	If from = "" Or recipients = "" Then
		Response.Write( "<font color=red>Please input from, to</font>" )
		Exit Sub
	End If
	
	Dim oSmtp
	Set oSmtp = Server.CreateObject("EASendMailObj.Mail")
    'The license code for EASendMail ActiveX Object,
    'for evaluation usage, please use "TryIt" as the license code.	
	oSmtp.LicenseCode = "TryIt"
	
	oSmtp.Charset = Request.Form("charset")
	
	Dim serveraddr
	
	serveraddr = Request.Form("serveraddr")
	oSmtp.ServerAddr = serveraddr
	oSmtp.Protocol = CInt(Request.Form("protocol"))
	'Using ESMTP authentication
	If serveraddr <> "" Then
		If Request.Form("authrequired") <> "" Then
			oSmtp.UserName = Request.Form("user")
			oSmtp.Password = Request.Form("password")
		End If
		
		If Request.Form("SSL") <> "" Then
			If oSmtp.SSL_init() <> 0 Then
				Response.Write( "<font color=red>failed to load SSL library</font>" )
				Exit Sub
			End If
			'If SSL port is 465 or other port rather than 25 port, please use
			'oSmtp.ServerPort = 465
			'oSmtp.SSL_starttls = 0
		End If		
	End If

	Dim name, addr
	fnParseAddr from, name, addr

	oSmtp.From		= name
	oSmtp.FromAddr	= addr
	
	'Using this email to be replied to another address 
	'oSmtp.ReplyTo = ReplyAddress 

	oSmtp.AddRecipientEx recipients, 0  ' Normal recipient 
	'oSmtp.AddRecipient CCName, CCEmailAddress, 1  'CC 
	'oSmtp.AddRecipient BCCName, BCCEmailAddress, 2 'BCC 


	'Attachs file to this email 
	'oSmtp.AddAttachment "c:\test.txt"
	
	Dim subject, bodytext
	subject = Request.Form("subject")
	bodytext = Request.Form("bodytext") 
	bodytext = Replace( bodytext, "[$from]", from )
	bodytext = Replace( bodytext, "[$to]", recipients )
	bodytext = Replace( bodytext, "[$subject]", subject )
	
	oSmtp.Subject	= subject
	oSmtp.BodyText	= bodytext 
	
	If Request.Form("htmlformat") <> "" Then
		oSmtp.BodyFormat = 1	' Using HTML FORMAT to send mail
	End If
	
	If InStr( 1, recipients, ",", 1 ) > 1 And serveraddr = "" Then
         'To send email without specified smtp server, we have to send the emails one by one 
         ' to multiple recipients. That is because every recipient has different smtp server.
		DirectSend oSmtp, recipients 
		Exit Sub
	End If

	If oSmtp.SendMail() = 0 Then
		Response.Write "Message delivered." 
	Else 
		Response.Write "<font color=red>" & Server.HTMLEncode(oSmtp.GetLastErrDescription()) & "</font>" 'Get last error description 
	End If
End Sub


%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP, VBScript Sample For EASendMail ActiveX Object</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP, VBScript Sample For EASendMail ActiveX Object</div>
    <form name="thisForm" method="post" action="default.asp">
    <div id="div_main">
    <%
    Dim authrequired, SSL, htmlformat
    authrequired = ""
    SSL = ""
    htmlformat = ""

    If Request.ServerVariables("REQUEST_METHOD") = "POST" Then
	    Response.Write( "<div id=""s_info"">" )
	    SendMail 
        If Request.Form("authrequired") <> "" Then
            authrequired = "checked=""checked"""
        End If
        
        If Request.Form("SSL") <> "" Then
            SSL = "checked=""checked"""
        End If
        
        If Request.Form("htmlformat") <> "" Then
            htmlformat = "checked=""checked"""
        End If

	    Response.Write( "</div>" )
    End If 
%>
        <div class="comments">
            Note: Please change Smtp Server, From, To to corresponding value.</div>
        <div class="comments">
            If you don't have a smtp server, please don't put any value to SMTP Server, EASendMail
            would send email via dns lookup</div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="150">
                    SMTP Server
                </td>
                <td>
                    <input type="text" name="serveraddr" style="width: 95%;" value="<%=Request.Form("serveraddr")%>"   />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input type="checkbox" name="authrequired" <%=authrequired%> />My server requires user authentication
                </td>
            </tr>
            <tr>
                <td>
                    User
                </td>
                <td>
                    <input type="text" name="user" style="width:50%;" value="<%=Request.Form("user")%>" />
                </td>
            </tr>
            <tr>
                <td>
                    Password
                </td>
                <td>
                    <input type="password" name="password" style="width:50%;" value="<%=Request.Form("password")%>" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input type="checkbox" name="SSL" <%=SSL%> />My server requires secure connection (SSL)
                </td>
            </tr>
            <tr>
                <td>
                    Server Protocl:
                </td>
                <td>
                    <select name="protocol">
                        <%
			fnShowProtocol Request.Form("protocol")
                        %>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    From:
                </td>
                <td>
                    <input type="text" name="from" style="width:95%;" value="<%=Server.HTMLEncode(Request.Form("from"))%>" />
                </td>
            </tr>
            <tr>
                <td>
                    To:
                </td>
                <td>
                    <div class="comments">
                    Please separate multiple recipients with comma(,)</div>
                    <input type="text" name="recipients" style="width:95%;" value="<%=Server.HTMLEncode(Request.Form("recipients"))%>" />
                </td>
            </tr>

            <tr>
                <td width="150">
                    Encoding
                </td>
                <td>
                    <select name="charset">
                        <%
			fnShowComposeCharset
                        %>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Subject
                </td>
                <td>
                    <input type="text" name="subject" value="message subject" style="width:95%;" />
                </td>
            </tr>
            <tr>
                <td>
                    Html Format
                </td>
                <td>
                    <input type="checkbox" name="htmlformat" <%=htmlformat%> />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Message Body
                </td>
                <td>
                    <textarea name="bodytext" cols="50" rows="8" style="width:95%;">
From: [$from]
To: [$to]
Subject: [$subject]

This is a test email from easendmail asp sample.
						</textarea>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="submit" name="submit1" value=" Send Mail " /> - <a href="default.asp">Reset</a>
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
