CREATE PROCEDURE [dbo].[usp_SendTextEmail]  @ServerAddr nvarchar(128),
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

EXEC @hr = sp_OAMethod @oSmtp, 'AddRecipientEx', NULL, @To, 0

EXEC @hr = sp_OASetProperty @oSmtp, 'Subject', @Subject 
EXEC @hr = sp_OASetProperty @oSmtp, 'BodyText', @BodyText 

If @SSLConnection > 0 
BEGIN
	EXEC @hr = sp_OAMethod @oSmtp, 'SSL_init', NULL
END

PRINT 'Start to send email ...' 

EXEC @hr = sp_OAMethod @oSmtp, 'SendMail', @result OUT 

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
	PRINT 'failed to send email with the following error:'
	PRINT @description
END
ELSE 
BEGIN
	PRINT 'email was sent successfully!'
END

EXEC @hr = sp_OADestroy @oSmtp

Go


/* Usage Example
DECLARE @ServerAddr nvarchar(128)
Set @ServerAddr = 'smtp.emailarchitect.net'

DECLARE @From nvarchar(128)
Set @From = 'test@emailarchitect.net'

DECLARE @To nvarchar(1024)
/*You can input multiple recipients and use comma (,) to separate multiple addresses */
Set @To = 'support@emailarchitect.net'

DECLARE @Subject nvarchar(256)
Set @Subject = 'simple email from SQL server'

DECLARE @Bodytext nvarchar(512)
Set @BodyText = 'This is a test text email from MS SQL server, do not reply.'

/*User and password for ESMTP authentication, if your server doesn't require
User authentication, please set @User and @Password to '' */
DECLARE @User nvarchar(128)
Set @User = 'test@emailarchitect.net'

DECLARE @Password nvarchar(128)
Set @Password = 'testpassword'

/* If your smtp server requires SSL connection, please set @SSL = 1*/
DECLARE @SSL int
Set @SSL = 0

exec usp_SendTextEmail @ServerAddr, @From, @To, @Subject, @BodyText, @User, @Password, @SSL*/