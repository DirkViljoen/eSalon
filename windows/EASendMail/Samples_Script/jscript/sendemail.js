//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2010 ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// |    Project: It demonstrates how to use EASendMail ActiveX Object to send email in JScript
// |
// |    Author: Ivan Lui ( ivan@emailarchitect.net )
//  ===============================================================================


function Send( From, Recipient, Subject, Body, Server )
{	
	var NORMAL_RECIPIENT		= 0;
	var COPY_RECIPIENT		= 1;
	var BLIND_COPY_RECIPIENT	= 2;

	var TEXT_PLAIN	= 0;
	var TEXT_HTML	= 1;	

	var oSmtp = new ActiveXObject("EASendMailObj.Mail");
    	//The license code for EASendMail ActiveX Object,
    	//for evaluation usage, please use "TryIt" as the license code.	
	oSmtp.LicenseCode = "TryIt";
	
	oSmtp.Reset();
	oSmtp.FromAddr 	= From;
	oSmtp.Subject	= Subject;
	oSmtp.BodyFormat	= TEXT_PLAIN;
	oSmtp.BodyText	= Body;
	oSmtp.ServerAddr	= Server;

	oSmtp.AddRecipient( Recipient, Recipient, NORMAL_RECIPIENT );

	// Add cc recipient
	// oSmtp.AddRecipient( "test", "test@hotmail.com", COPY_RECIPIENT );

	// Add bcc recipient
	// oSmtp.AddRecipient( "test", "test@hotmail.com", BLIND_COPY_RECIPIENT );

	// ESMTP authentication 
	// oSmtp.UserName = "your user name";
	// oSmtp.Password = "your password";	

	// | -- add file attachment -- |
	// oSmtp.AddAttachment "c:\\test.doc" 

	if( oSmtp.SendMail() == 0 )
		WScript.Echo( "Sending email to " + Recipient + " succeeded!" );
	else
		WScript.Echo( oSmtp.GetLastErrDescription());
}

WScript.Echo( "Please edit this file and input sender, recipient, subject, body and smtp server" );
//Send( "dennis@hotmail.com", "support@adminsystem.net", "test subject", "test body", "127.0.0.1" );
