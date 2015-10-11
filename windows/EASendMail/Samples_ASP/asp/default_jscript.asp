<%@  codepage="65001" language="JScript" %>
<%
// ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2010  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// | FILE: SAMPLE.ASP
// | SYNOPSIS: SAMPLE FILE FOR EASendMail ActiveX Object (JScript)
// | IVON LUI ( ivan@emailarchitect.net )
// |        CREATED MAY 2001
// |
// =================================================================================*/
Response.CharSet = "utf-8";
function getFromName( src )
{
	var pos = src.indexOf("<");
	if( pos != -1 )
	{
		return fnTrim( src.substr(0, pos), ",<\"> \t" )
	}
	else
	{
		return "";
	}
}

function getFromAddr( src )
{
	var pos = src.indexOf("<");
	if( pos !=-1 )
	{
		return fnTrim( src.substr(pos), ",<\"> \t" )
	}
	else
	{
		return fnTrim( src, ",<\"> \t" )
	}
}

function fnTrim( src, trimer )
{
	var len = src.length;
	var i = 0;
	if( len > 0 )
	{
		for( i = 0; i < len; i++ )
		{
			var ch = src.charAt(i);
			if( trimer.indexOf( ch ) == -1 )
			{
				break;
			}
		}
		src = src.substr(i);
	}
	
	len = src.length
	if( len > 0 )
	{
		for( i = len-1; i >=0; i-- )
		{
			var ch = src.charAt(i);
			if( trimer.indexOf( ch ) == -1 )
			{
				break;
			}		
		}
		
		src = src.substr(0, i+1);
	}
	return src;
}

function fnShowProtocol(protocol) {

    if (isNaN(protocol)) {
        protocol = 0;
    }
    
    Response.write('/' + protocol + '/')
 
    var arProtocol = new Array("SMTP Protocol - Recommended:0", 
						"Exchange Web Service - Exchange 2007/2010:1", 
						"Exchange WebDav - Exchange 2000/2003:2"
							);

    var nCount = arProtocol.length;
    var s = "";
  
    for (var i = 0; i < nCount; i++) {
        var buf = arProtocol[i];
        var pos = buf.indexOf(":");
        if (pos < 0) {
            continue;
        }

        var s1 = buf.substr(0, pos);
        var s2 = buf.substr(pos + 1);
        s = s + "<option value=\"" + s2 + "\"";
        if (s2*1 == protocol*1 ){
              s += " selected";
        }
        s = s + ">" + s1 + "</option>\r\n";
    }
    Response.Write(s); 
}

function fnShowComposeCharset()
{
	var arCharset = new Array( "Arabic(Windows):windows-1256", 
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
						
	var nCount = arCharset.length;
	var s = "";
	var charset = "utf-8";
	
	for( var i = 0; i < nCount; i++ )
	{
		var buf = arCharset[i];
		var pos = buf.indexOf( ":" );
		if( pos < 0 )
		{
			continue;
		}
		
		var s1 = buf.substr( 0, pos );
		var	s2 = buf.substr( pos+1 );
		s = s + "<option value=\"" + s2 + "\"";
		if( s2 == charset )
		{
			s += " selected";
		}
		s = s + ">" + s1 + "</option>\r\n";
	}
	Response.Write( s )	;
}


//To send email without specified smtp server, we have to send the emails one by one 
//to multiple recipients. That is because every recipient has different smtp server.
function DirectSend( oSmtp, recipients )
{
	var arTo = recipients.split( "," );
	for( var i = 0; i < arTo.length; i++ )
	{
		var s = fnTrim( arTo[i], " ,;" );
		if( s.length == 0 )
			continue;
			
		oSmtp.ClearRecipient();
		oSmtp.AddRecipientEx( s, 0 );
		if( oSmtp.SendMail() == 0 )
			Response.Write( "Message delivered to " + Server.HTMLEncode(s) + " successfully!<br><br>" );
		else
			Response.Write("<font color=red>Failed to delivery to " + Server.HTMLEncode(s) + " : " + Server.HTMLEncode(oSmtp.GetLastErrDescription()) + "</font><br><br>" );
					
	}
}

function SendMail()
{
    var from, recipients;
    from = new String(Request.Form("from"));
    recipients = new String(Request.Form("recipients"));
	
	if( from.length == 0 || recipients.length == 0 )
	{
		Response.Write( "<font color=red>Please input From, To</font>" );
		return;
	}
	
	var oSmtp = Server.CreateObject("EASendMailObj.Mail");
    //The license code for EASendMail ActiveX Object,
    //for evaluation usage, please use "TryIt" as the license code.		
	oSmtp.LicenseCode = "TryIt";
	var serveraddr = new String(Request.Form("serveraddr"));
	oSmtp.ServerAddr = serveraddr;
	oSmtp.Protocol = Request.Form("protocol") * 1;

	oSmtp.Charset = Request.Form("charset");	
	//Using ESMTP authentication
	if( serveraddr.length > 0 )
	{
		if( Request.Form("authrequired") == "on" )
		{
			oSmtp.UserName = Request.Form("user");
			oSmtp.Password = Request.Form("password");
		}
		
		if( Request.Form("SSL") == "on" )
		{
			if( oSmtp.SSL_init() != 0 )
			{
				Response.Write( "<font color=red>failed to load SSL library</font>" );
				return;
			}
			//If SSL port is 465 or other port rather than 25 port, please use
			//oSmtp.ServerPort = 465;
			//oSmtp.SSL_starttls = 0;
		}	
	}

	oSmtp.From		= getFromName( from );
	oSmtp.FromAddr	= getFromAddr( from );
	
	//Using this email to be replied to another address 
	//oSmtp.ReplyTo = "ReplyAddress: 

	oSmtp.AddRecipientEx( recipients, 0 );  // Normal recipient 
	//oSmtp.AddRecipient( CCName, CCEmailAddress, 1 ); 'CC 
	//oSmtp.AddRecipient( BCCName, BCCEmailAddress, 2 ); 'BCC 

	//Attachs file to this email 
	//oSmtp.AddAttachment( "c:\\test.txt" );

	oSmtp.Subject	= Request.Form("subject");
	oSmtp.BodyText	= Request.Form("bodytext"); 
	
	if( Request.Form("htmlformat") == "on" )
		oSmtp.BodyFormat = 1; // Using HTML FORMAT to send mail

	if( recipients.indexOf( "," ) != -1 && serveraddr.length == 0 )
	{ 
        // To send email without specified smtp server, we have to send the emails one by one 
        // to multiple recipients. That is because every recipient has different smtp server.
		DirectSend( oSmtp, recipients );
		return;
	}
	
	//Sending email 
	if( oSmtp.SendMail() == 0 ) 
		Response.Write( "Message delivered." );
	else 
		Response.Write( "<font color=red>" + Server.HTMLEncode(oSmtp.GetLastErrDescription()) + "</font>");  //Get last error description 
}


%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP, JScript Sample For EASendMail ActiveX Object</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP, JScript Sample For EASendMail ActiveX Object</div>
    <form name="thisForm" method="post" action="default_jscript.asp">
    <div id="div_main">
    <%
    var authrequired, SSL, htmlformat;
    authrequired = "";
    SSL = "";
    htmlformat = "";

    if (Request.ServerVariables("REQUEST_METHOD") == "POST") 
    {
	    Response.Write( "<div id=\"s_info\">" );
	    SendMail();
	    if (Request.Form("authrequired") == "on") {
	        authrequired = "checked=\"checked\"";
	    }

        if (Request.Form("SSL") == "on") {
            SSL = "checked=\"checked\"";
        }
        
        if( Request.Form("htmlformat") == "on" )
            htmlformat = "checked=\"checked\"";
        
	    Response.Write( "</div>" );
    }

%>
        <div class="comments">
            Note: Please change Smtp Server, From, To to corresponding value.</div>
        <div class="comments">
            If you don't have a smtp server, please don't put any value to SMTP Server, EASendMail
            would send email via dns lookup</div>
        <table width="100%">
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
                    <input type="checkbox" name="SSL" <%=SSL%>  />My server requires secure connection (SSL)
                </td>
            </tr>
              <tr>
                <td>
                    Server Protocl:
                </td>
                <td>
                    <select name="protocol">
                        <%
                            fnShowProtocol(Request.Form("protocol"));
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
                            fnShowComposeCharset();
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
                    <input type="submit" name="submit1" value=" Send Mail " /> - <a href="default_jscript.asp">Reset</a>
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

