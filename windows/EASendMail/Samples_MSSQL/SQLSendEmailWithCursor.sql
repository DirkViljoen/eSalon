
/*
Create usp_SendMailToQueue by SQLSendMailToQueue.sql at first

Create a sample table like this:

CREATE TABLE [dbo].[rcpts](
	[uid] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[email] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_rcpts] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO*/

DECLARE @email nvarchar(128)

DECLARE rcpt_cursor CURSOR FOR 
SELECT email
FROM rcpts

OPEN rcpt_cursor

FETCH NEXT FROM rcpt_cursor 
INTO @email


DECLARE @ServerAddr nvarchar(128)
Set @ServerAddr = 'smtp.emailarchitect.net'

DECLARE @From nvarchar(128)
Set @From = 'test@emailarchitect.net'

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

DECLARE @Port int
Set @Port = 25

WHILE @@FETCH_STATUS = 0
BEGIN
	PRINT @email
	DECLARE @subject nvarchar(255)
	SELECT @Subject = 'test email for ' + @email
	EXEC usp_SendMailToQueue @ServerAddr, 
		@from, @email, @subject, @BodyText, @User, @Password, @SSL, @Port
	FETCH NEXT FROM rcpt_cursor
	INTO @email
END 
CLOSE rcpt_cursor
DEALLOCATE rcpt_cursor