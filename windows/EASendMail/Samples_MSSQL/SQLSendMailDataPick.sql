/*Create sample tables like this:*/

/* 

CREATE TABLE [dbo].[rcpts](
	[uid] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[email] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_rcpts] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[errorlog](
	[uid] [bigint] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](128) NULL,
	[server] [nvarchar](50) NULL,
	[errorcode] [nvarchar](50) NULL,
	[errordescription] [nvarchar](255) NULL,
 CONSTRAINT [PK_errorlog] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[sentlog](
	[uid] [bigint] IDENTITY(1,1) NOT NULL,
	[server] [nvarchar](50) NULL,
	[email] [nvarchar](128) NULL,
 CONSTRAINT [PK_sentlog] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
*/

/*change it to your server addresss */
DECLARE @ServerAddr nvarchar(128)
SET @ServerAddr = 'smtp.emailarchitect.net'

DECLARE @From nvarchar(128)
SET @From = 'Tester <test@adminsystem.com>'

/* change it to your server address, database, user and password */
DECLARE @DataConn nvarchar(255)
SET @DataConn = 'Driver={SQL Native Client};Server=serveraddress;Database=database;Uid=user;Pwd=password;'

DECLARE @SQLSelect nvarchar(255)
SET @SQLSelect = 'SELECT uid, name, email from rcpts'

DECLARE @SQLSuccess nvarchar(255)
SET @SQLSuccess = 'INSERT INTO sentlog ( server, email )' 
	+ ' VALUES( ''{$var_server}'', ''{$var_rcptaddr}'' )'

DECLARE @SQLError nvarchar(255)
SET @SQLError = 'INSERT INTO errorlog( email, server, errorcode, errordescription )' +
	' VALUES( ''{$var_rcptaddr}'', ''{$var_server}'', ''{$var_errcode}'',  ''{$var_errdesc}'' )'

DECLARE @Subject nvarchar(256)
DECLARE @Bodytext nvarchar(max)
SET @Bodytext = ''

/*User and password for ESMTP authentication, if your server doesn't require
User authentication, please set @User and @Password to '' */
DECLARE @User nvarchar(128)
Set @User = 'test@emailarchitect.net'

DECLARE @Password nvarchar(128)
Set @Password = 'testpassword'

/* If your smtp server requires SSL connection, please set @SSL = 1*/
DECLARE @SSL int
SET @SSL = 0

DECLARE @Port int 
SET @Port = 25

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

EXEC @hr = sp_OAMethod @oSmtp, 'AddHeader', NULL, 'X-Data-Connection', @DataConn
EXEC @hr = sp_OAMethod @oSmtp, 'AddHeader', NULL, 'X-Sql-Select', @SQLSelect
EXEC @hr = sp_OAMethod @oSmtp, 'AddHeader', NULL, 'X-Sql-OnSentSuccess', @SQLSuccess
EXEC @hr = sp_OAMethod @oSmtp, 'AddHeader', NULL, 'X-Sql-OnSentError', @SQLError
EXEC @hr = sp_OAMethod @oSmtp, 'AddHeader', NULL, 'X-Rcpt-To', '{$var_srecord:email}'
EXEC @hr = sp_OASetProperty @oSmtp, 'DisplayTo', '"{$var_srecord:name}" <{$var_srecord:email}>'
 
EXEC @hr = sp_OASetProperty @oSmtp, 'ServerAddr', @ServerAddr
EXEC @hr = sp_OASetProperty @oSmtp, 'ServerPort', @Port

EXEC @hr = sp_OASetProperty @oSmtp, 'UserName', @User
EXEC @hr = sp_OASetProperty @oSmtp, 'Password', @Password

EXEC @hr = sp_OASetProperty @oSmtp, 'FromAddr', @From

SET @Subject = 'test email with data pick'
EXEC @hr = sp_OASetProperty @oSmtp, 'Subject', @Subject 

SET @BodyText = 'Test email, do not reply, your id in database is {$var_srecord:uid}.'

EXEC @hr = sp_OASetProperty @oSmtp, 'BodyText', @BodyText 

If @SSL > 0 
BEGIN
	EXEC @hr = sp_OAMethod @oSmtp, 'SSL_init', NULL
	PRINT @hr
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
	PRINT 'Please make sure you have EASendMail Service installed!'
	EXEC @hr = sp_OAMethod @oSmtp, 'GetLastErrDescription', @description OUT
	PRINT @description
END
ELSE 
BEGIN
	PRINT 'Message has been submitted to EASendMail Service!'
END

EXEC @hr = sp_OADestroy @oSmtp
Go

