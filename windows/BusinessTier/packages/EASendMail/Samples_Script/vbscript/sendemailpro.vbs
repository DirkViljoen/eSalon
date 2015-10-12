'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2010  ADMINSYSTEM SOFTWARE LIMITED                        |
' |
' |    Project: It demonstrates how to use EASendMail to send email without specified |
' |    smtp server in VBScript
' |
' |    Author: Ivan Lui ( ivan@emailarchitect.net )
'  ===============================================================================
Sub Send( From, Recipient, Subject, Body )
	
	NORMAL_RECIPIENT	= 0
	COPY_RECIPIENT		= 1
	BLIND_COPY_RECIPIENT	= 2

	TEXT_PLAIN	= 0
	TEXT_HTML	= 1	

	Dim oSmtp
	Set oSmtp = CreateObject("EASendMailObj.Mail")
	'The license code for EASendMail ActiveX Object,
	'for evaluation usage, please use "TryIt" as the license code.
	oSmtp.LicenseCode = "TryIt"
	oSmtp.Reset
	oSmtp.FromAddr 	= From
	oSmtp.Subject	= Subject
	oSmtp.BodyFormat	= TEXT_PLAIN
	oSmtp.BodyText	= Body

	' | you needn't to specify smtp server, but you can only send email to one recipient each time.
	oSmtp.AddRecipient Recipient, Recipient, NORMAL_RECIPIENT

	' | -- add file attachment -- |
	' oSmtp.AddAttachment "c:\\test.doc" 

	If oSmtp.SendMail() = 0 Then
		WScript.Echo "Sending email to " & Recipient & " succeeded!"
	Else
		WScript.Echo oSmtp.GetLastErrDescription()
	End If

End Sub

WScript.Echo "Please edit this file and input sender, recipient, subject and body" 
'Send "dennis@hotmail.com", "support@adminsystem.net", "test subject", "test body"
