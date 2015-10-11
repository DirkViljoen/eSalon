CREATE PROCEDURE [dbo].[usp_SendMailToQueue]  @ServerAddr nvarchar(128),
@From nvarchar(128),
@To nvarchar(1024),
@Subject nvarchar(256),
@Bodytext nvarchar(max) = 'This is a test text email from MS SQL server, do not reply.',
@User nvarchar(128) = '',
@Password nvarchar(128) = '',
@SSLConnection int = 0,
@ServerPort int = 25

AS

DECLARE @hr int
DECLARE @oSmtp int
DECLARE @result int
DECLARE @description nvarchar(255)

EXEC @hr = sp_OACreate 'EASendMailObj.Mail',@oSmtp OUT 
If @hr <> 0 
BEGIN
	PRINT 'Please make sure you have EASendMail Component installed!'
	EXEC @hr = sp_OAGetErrorInfo @oSmtp, NULL, @description OUT
	IF @hr = 0
	BEGIN
		PRINT @description
	END
	RETURN
End

EXEC @hr = sp_OASetProperty @oSmtp, 'LicenseCode', 'TryIt'
EXEC @hr = sp_OASetProperty @oSmtp, 'ServerAddr', @ServerAddr
EXEC @hr = sp_OASetProperty @oSmtp, 'ServerPort', @ServerPort

EXEC @hr = sp_OASetProperty @oSmtp, 'UserName', @User
EXEC @hr = sp_OASetProperty @oSmtp, 'Password', @Password

EXEC @hr = sp_OASetProperty @oSmtp, 'FromAddr', @From


EXEC @hr = sp_OAMethod @oSmtp, 'AddRecipientEx', NULL,  @To, 0


EXEC @hr = sp_OASetProperty @oSmtp, 'Subject', @Subject 
EXEC @hr = sp_OASetProperty @oSmtp, 'BodyText', @BodyText 

If @SSLConnection > 0 
BEGIN
	EXEC @hr = sp_OAMethod @oSmtp, 'SSL_init', NULL
END

EXEC @hr = sp_OAMethod @oSmtp, 'SendMailToQueue', @result OUT 

If @hr <> 0 
BEGIN
	EXEC @hr = sp_OAGetErrorInfo @oSmtp, NULL, @description OUT
	IF @hr = 0
	BEGIN
		PRINT @description
	END
	RETURN
End

If @result <> 0 
BEGIN
	EXEC @hr = sp_OAMethod @oSmtp, 'GetLastErrDescription', @description OUT
	PRINT @description
	PRINT 'Please make sure you have EASendMail Service installed'
END
ELSE 
BEGIN
	PRINT 'Message submitted!'
END

EXEC @hr = sp_OADestroy @oSmtp


Go

