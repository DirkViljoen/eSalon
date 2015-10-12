Imports EASendMail

Public Class Form1
    Private m_bcancel As Boolean = False
    Private m_arAttachment As New ArrayList()


#Region "EASendMail EventHandler"

    Sub OnIdle(ByVal sender As Object, ByRef cancel As Boolean)
        cancel = m_bcancel
        If Not cancel Then
            Application.DoEvents() 'waiting server reponse or connecting server.
        End If
    End Sub

    Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        sbStatus.Text = "Connected"
        cancel = m_bcancel
    End Sub


    Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        sbStatus.Text = "Sending ..."
        Dim t As Long = sent
        t = t * 100
        t = t / total
        Dim x As Integer = t
        pgSending.Value = x
        cancel = m_bcancel
        If sent = total Then
            sbStatus.Text = "Disconnecting ..."
        End If
        Application.DoEvents()
    End Sub

    Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        sbStatus.Text = "Authorized"
        cancel = m_bcancel
    End Sub

    Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)

        sbStatus.Text = "Securing ..."
        cancel = m_bcancel
    End Sub

#End Region

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If (attachmentDlg.ShowDialog() <> DialogResult.OK) Then
            Exit Sub
        End If
        'Only one instance of the file can be select**Not Support multiple file**
        Dim fileName As String = attachmentDlg.FileName
        m_arAttachment.Add(fileName)
        Dim pos As Integer = fileName.LastIndexOf("\")
        If (pos <> -1) Then
            fileName = fileName.Substring(pos + 1)
        End If

        textAttachments.Text += fileName
        textAttachments.Text += ";"
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        m_arAttachment.Clear()
        textAttachments.Text = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancel.Enabled = False
        m_bcancel = True
    End Sub

    Private Sub _ChangeAuthStatus()
        textUser.Enabled = chkAuth.Checked
        textPassword.Enabled = chkAuth.Checked
    End Sub

    Private Sub chkAuth_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuth.CheckStateChanged
        _ChangeAuthStatus()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        If textFrom.Text.Length = 0 Then
            MessageBox.Show("Please input From, the format can be test@adminsystem.com or Tester<test@adminsystem.com>")
            Exit Sub
        End If

        If textTo.Text.Length = 0 And _
        textCc.Text.Length = 0 Then
            MessageBox.Show("Please input To or Cc, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple addresses")
            Exit Sub
        End If

        If textServer.Text.Length = 0 Then
            MessageBox.Show("Please input server address!")
            Exit Sub
        End If

        btnSend.Enabled = False
        btnCancel.Enabled = True
        m_bcancel = False

        'For evaluation usage, please use "TryIt" as the license code, otherwise the 
        '"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
        '"trial version expired" exception will be thrown.

        'For licensed uasage, please use your license code instead of "TryIt", then the object
        'will never expire
        Dim oMail As SmtpMail = New SmtpMail("TryIt")
        Dim oSmtp As SmtpClient = New SmtpClient
        'To generate a log file for SMTP transaction, please use
        'oSmtp.LogFileName = "c:\smtp.log"

        Dim errStr As String = ""

        Try
            oMail.Reset()
            'If you want to specify a reply address
            'oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" )

            'From is a MailAddress object
            'The example code
            ' oMail.From = New MailAddress( "Tester", "test@adminsystem.com" )
            ' oMail.From = New MailAddress( "Tester<test@adminsystem.com>" )
            ' oMail.From = New MailAddress( "test@adminsystem.com" )

            oMail.From = New MailAddress(textFrom.Text)

            'To, Cc and Bcc is a AddressCollection object
            'The example code
            'oMail.To = New AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" )
            'oMail.To = New AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>")

            oMail.To = New AddressCollection(textTo.Text)
            oMail.Cc = New AddressCollection(textCc.Text)

            'You can add more recipient by Add method
            'oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"))

            oMail.Subject = textSubject.Text



            Dim body As String = textBody.Text
            oMail.TextBody = body


            Dim count As Integer = m_arAttachment.Count
            Dim i As Integer = 0
            For i = 0 To count - 1
                'Add attachments
                oMail.AddAttachment(CType(m_arAttachment(i), String))
            Next

            Dim oServer As SmtpServer = New SmtpServer(textServer.Text)


            If (chkAuth.Checked) Then
                oServer.User = textUser.Text
                oServer.Password = textPassword.Text
            End If

            If (chkSSL.Checked) Then
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
            End If



            'Catching the following events is not necessary, 
            'just make the application more user friendly.
            'If you use the object in asp.net/windows service or non-gui application, 
            'You need not to catch the following events.
            'To learn more detail, please refer to the code in EASendMail EventHandler region
            AddHandler oSmtp.OnIdle, AddressOf OnIdle
            AddHandler oSmtp.OnAuthorized, AddressOf OnAuthorized
            AddHandler oSmtp.OnConnected, AddressOf OnConnected
            AddHandler oSmtp.OnSecuring, AddressOf OnSecuring
            AddHandler oSmtp.OnSendingDataStream, AddressOf OnSendingDataStream

          
            sbStatus.Text = "Connecting ... "
            pgSending.Value = 0
            oSmtp.SendMail(oServer, oMail)
            MessageBox.Show(String.Format("The message was sent to {0} successfully!", _
  oSmtp.CurrentSmtpServer.Server))
            sbStatus.Text = "Completed"
        Catch exp As SmtpTerminatedException
            errStr = exp.Message
        Catch exp As SmtpServerException
            errStr = String.Format("Exception: Server Respond: {0}", exp.ErrorMessage)
        Catch exp As System.Net.Sockets.SocketException
            errStr = String.Format("Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message)
        Catch exp As System.ComponentModel.Win32Exception
            errStr = String.Format("Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message)
        Catch exp As System.Exception
            errStr = String.Format("Exception: Common: {0}", exp.Message)
        End Try

        If errStr.Length > 0 Then
            MessageBox.Show(errStr)
            sbStatus.Text = errStr
        End If

        btnSend.Enabled = True
        btnCancel.Enabled = False
    End Sub
End Class
