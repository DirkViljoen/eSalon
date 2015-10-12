//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2010  ADMINSYSTEM SOFTWARE LIMITED                        |
// |
// |    Project: It demonstrates how to use EASendMail ActiveX Object to send email without specified |
// |    smtp server in JScript
// |
// |    Author: Ivan Lui ( ivan@emailarchitect.net )
//  ===============================================================================



function Send( From, Recipient, Subject, Body )
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

	// | you needn't to specify smtp server, but you can only send email to one recipient each time.
	oSmtp.AddRecipient( Recipient, Recipient, NORMAL_RECIPIENT );

	// | -- add file attachment -- |
	// oSmtp.AddAttachment "c:\\test.doc" 

	if( oSmtp.SendMail() == 0 )
		WScript.Echo( "Sending email to " + Recipient + " succeeded!" );
	else
		WScript.Echo( oSmtp.GetLastErrDescription());
}

WScript.Echo( "Please edit this file and input sender, recipient, subject and body" );
//Send( "dennis@hotmail.com", "support@adminsystem.net", "test subject", "test body" );
