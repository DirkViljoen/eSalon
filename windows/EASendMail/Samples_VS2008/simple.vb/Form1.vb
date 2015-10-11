'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2006-2012  ADMINSYSTEM SOFTWARE LIMITED                         |
' |
' |    Project: It demonstrates how to use EASendMail to send text/plain email with 
' |             vb.net.
' |
' |    File: Form1 : implementation file
' |
' |    Author: Ivan Lui ( ivan@emailarchitect.net )
'  ===============================================================================
Imports EASendMail

Public Class Form1
    Inherits System.Windows.Forms.Form

    Private m_arCharset(27, 1) As String
    Private m_arAttachment As ArrayList = New ArrayList
    Private m_bcancel As Boolean = False
    Private m_eventtick As Long = 0

#Region " Windows Form Designer generated code "
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents chkSSL As System.Windows.Forms.CheckBox
    Friend WithEvents textFrom As System.Windows.Forms.TextBox
    Friend WithEvents textTo As System.Windows.Forms.TextBox
    Friend WithEvents textCc As System.Windows.Forms.TextBox
    Friend WithEvents textSubject As System.Windows.Forms.TextBox
    Friend WithEvents textPassword As System.Windows.Forms.TextBox
    Friend WithEvents textUser As System.Windows.Forms.TextBox
    Friend WithEvents Server As System.Windows.Forms.Label
    Friend WithEvents textServer As System.Windows.Forms.TextBox
    Friend WithEvents textAttachments As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents pgSending As System.Windows.Forms.ProgressBar
    Friend WithEvents sbStatus As System.Windows.Forms.StatusBar
    Friend WithEvents chkAuth As System.Windows.Forms.CheckBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lstCharset As System.Windows.Forms.ComboBox

    Friend WithEvents textBody As System.Windows.Forms.RichTextBox
    Private attachmentDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkHtml As System.Windows.Forms.CheckBox
    Friend WithEvents chkSignature As System.Windows.Forms.CheckBox
    Friend WithEvents lstProtocol As System.Windows.Forms.ComboBox
    Friend WithEvents chkEncrypt As System.Windows.Forms.CheckBox

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _Init()
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.textFrom = New System.Windows.Forms.TextBox
        Me.textTo = New System.Windows.Forms.TextBox
        Me.textCc = New System.Windows.Forms.TextBox
        Me.textSubject = New System.Windows.Forms.TextBox
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.chkAuth = New System.Windows.Forms.CheckBox
        Me.chkSSL = New System.Windows.Forms.CheckBox
        Me.textPassword = New System.Windows.Forms.TextBox
        Me.textUser = New System.Windows.Forms.TextBox
        Me.label7 = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.Server = New System.Windows.Forms.Label
        Me.textServer = New System.Windows.Forms.TextBox
        Me.label8 = New System.Windows.Forms.Label
        Me.textAttachments = New System.Windows.Forms.TextBox
        Me.btnSend = New System.Windows.Forms.Button
        Me.pgSending = New System.Windows.Forms.ProgressBar
        Me.sbStatus = New System.Windows.Forms.StatusBar
        Me.label9 = New System.Windows.Forms.Label
        Me.lstCharset = New System.Windows.Forms.ComboBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.textBody = New System.Windows.Forms.RichTextBox
        Me.attachmentDlg = New System.Windows.Forms.OpenFileDialog
        Me.chkHtml = New System.Windows.Forms.CheckBox
        Me.chkSignature = New System.Windows.Forms.CheckBox
        Me.chkEncrypt = New System.Windows.Forms.CheckBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lstProtocol = New System.Windows.Forms.ComboBox
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(8, 18)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(36, 15)
        Me.label1.TabIndex = 0
        Me.label1.Text = "From"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(8, 49)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(21, 15)
        Me.label2.TabIndex = 1
        Me.label2.Text = "To"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(8, 77)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(21, 15)
        Me.label3.TabIndex = 2
        Me.label3.Text = "Cc"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(8, 104)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(48, 15)
        Me.label4.TabIndex = 3
        Me.label4.Text = "Subject"
        '
        'textFrom
        '
        Me.textFrom.Location = New System.Drawing.Point(64, 18)
        Me.textFrom.Name = "textFrom"
        Me.textFrom.Size = New System.Drawing.Size(309, 21)
        Me.textFrom.TabIndex = 1
        '
        'textTo
        '
        Me.textTo.Location = New System.Drawing.Point(64, 47)
        Me.textTo.Name = "textTo"
        Me.textTo.Size = New System.Drawing.Size(309, 21)
        Me.textTo.TabIndex = 2
        '
        'textCc
        '
        Me.textCc.Location = New System.Drawing.Point(64, 75)
        Me.textCc.Name = "textCc"
        Me.textCc.Size = New System.Drawing.Size(309, 21)
        Me.textCc.TabIndex = 3
        '
        'textSubject
        '
        Me.textSubject.Location = New System.Drawing.Point(64, 103)
        Me.textSubject.Name = "textSubject"
        Me.textSubject.Size = New System.Drawing.Size(309, 21)
        Me.textSubject.TabIndex = 4
        Me.textSubject.Text = "Test subject"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.lstProtocol)
        Me.groupBox1.Controls.Add(Me.chkAuth)
        Me.groupBox1.Controls.Add(Me.chkSSL)
        Me.groupBox1.Controls.Add(Me.textPassword)
        Me.groupBox1.Controls.Add(Me.textUser)
        Me.groupBox1.Controls.Add(Me.label7)
        Me.groupBox1.Controls.Add(Me.label6)
        Me.groupBox1.Controls.Add(Me.Server)
        Me.groupBox1.Controls.Add(Me.textServer)
        Me.groupBox1.Location = New System.Drawing.Point(379, 8)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(295, 179)
        Me.groupBox1.TabIndex = 8
        Me.groupBox1.TabStop = False
        '
        'chkAuth
        '
        Me.chkAuth.AutoSize = True
        Me.chkAuth.Location = New System.Drawing.Point(8, 42)
        Me.chkAuth.Name = "chkAuth"
        Me.chkAuth.Size = New System.Drawing.Size(206, 19)
        Me.chkAuth.TabIndex = 11
        Me.chkAuth.Text = "My server requires authentication"
        '
        'chkSSL
        '
        Me.chkSSL.AutoSize = True
        Me.chkSSL.Location = New System.Drawing.Point(8, 122)
        Me.chkSSL.Name = "chkSSL"
        Me.chkSSL.Size = New System.Drawing.Size(114, 19)
        Me.chkSSL.TabIndex = 14
        Me.chkSSL.Text = "SSL Connection"
        '
        'textPassword
        '
        Me.textPassword.Location = New System.Drawing.Point(67, 94)
        Me.textPassword.Name = "textPassword"
        Me.textPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.textPassword.Size = New System.Drawing.Size(218, 21)
        Me.textPassword.TabIndex = 13
        '
        'textUser
        '
        Me.textUser.Location = New System.Drawing.Point(67, 67)
        Me.textUser.Name = "textUser"
        Me.textUser.Size = New System.Drawing.Size(218, 21)
        Me.textUser.TabIndex = 12
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(4, 97)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(61, 15)
        Me.label7.TabIndex = 2
        Me.label7.Text = "Password"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(5, 69)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(33, 15)
        Me.label6.TabIndex = 1
        Me.label6.Text = "User"
        '
        'Server
        '
        Me.Server.AutoSize = True
        Me.Server.Location = New System.Drawing.Point(6, 17)
        Me.Server.Name = "Server"
        Me.Server.Size = New System.Drawing.Size(42, 15)
        Me.Server.TabIndex = 0
        Me.Server.Text = "Server"
        '
        'textServer
        '
        Me.textServer.Location = New System.Drawing.Point(67, 17)
        Me.textServer.Name = "textServer"
        Me.textServer.Size = New System.Drawing.Size(218, 21)
        Me.textServer.TabIndex = 10
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(9, 198)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(74, 15)
        Me.label8.TabIndex = 9
        Me.label8.Text = "Attachments"
        '
        'textAttachments
        '
        Me.textAttachments.BackColor = System.Drawing.SystemColors.Info
        Me.textAttachments.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.textAttachments.Location = New System.Drawing.Point(103, 195)
        Me.textAttachments.Name = "textAttachments"
        Me.textAttachments.ReadOnly = True
        Me.textAttachments.Size = New System.Drawing.Size(464, 21)
        Me.textAttachments.TabIndex = 6
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(522, 406)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(72, 23)
        Me.btnSend.TabIndex = 15
        Me.btnSend.TabStop = False
        Me.btnSend.Text = "Send"
        '
        'pgSending
        '
        Me.pgSending.Location = New System.Drawing.Point(8, 414)
        Me.pgSending.Name = "pgSending"
        Me.pgSending.Size = New System.Drawing.Size(504, 8)
        Me.pgSending.TabIndex = 13
        '
        'sbStatus
        '
        Me.sbStatus.Location = New System.Drawing.Point(0, 440)
        Me.sbStatus.Name = "sbStatus"
        Me.sbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.sbStatus.Size = New System.Drawing.Size(684, 22)
        Me.sbStatus.TabIndex = 14
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(8, 138)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(59, 15)
        Me.label9.TabIndex = 15
        Me.label9.Text = "Encoding"
        '
        'lstCharset
        '
        Me.lstCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstCharset.Location = New System.Drawing.Point(86, 134)
        Me.lstCharset.Name = "lstCharset"
        Me.lstCharset.Size = New System.Drawing.Size(168, 23)
        Me.lstCharset.TabIndex = 5
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(573, 194)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(40, 23)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(619, 194)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(53, 23)
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "Clear"
        '
        'textBody
        '
        Me.textBody.Location = New System.Drawing.Point(8, 226)
        Me.textBody.Name = "textBody"
        Me.textBody.Size = New System.Drawing.Size(664, 174)
        Me.textBody.TabIndex = 14
        Me.textBody.Text = ""
        '
        'chkHtml
        '
        Me.chkHtml.AutoSize = True
        Me.chkHtml.Location = New System.Drawing.Point(12, 166)
        Me.chkHtml.Name = "chkHtml"
        Me.chkHtml.Size = New System.Drawing.Size(90, 19)
        Me.chkHtml.TabIndex = 16
        Me.chkHtml.Text = "HTML Body"
        '
        'chkSignature
        '
        Me.chkSignature.AutoSize = True
        Me.chkSignature.Location = New System.Drawing.Point(113, 166)
        Me.chkSignature.Name = "chkSignature"
        Me.chkSignature.Size = New System.Drawing.Size(120, 19)
        Me.chkSignature.TabIndex = 17
        Me.chkSignature.Text = "Digitial Signature"
        '
        'chkEncrypt
        '
        Me.chkEncrypt.Location = New System.Drawing.Point(256, 164)
        Me.chkEncrypt.Name = "chkEncrypt"
        Me.chkEncrypt.Size = New System.Drawing.Size(104, 23)
        Me.chkEncrypt.TabIndex = 18
        Me.chkEncrypt.Text = "Encrypt"
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(602, 406)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 23)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "Cancel"
        '
        'lstProtocol
        '
        Me.lstProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstProtocol.FormattingEnabled = True
        Me.lstProtocol.Location = New System.Drawing.Point(9, 145)
        Me.lstProtocol.Name = "lstProtocol"
        Me.lstProtocol.Size = New System.Drawing.Size(275, 23)
        Me.lstProtocol.TabIndex = 15
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(684, 462)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.chkEncrypt)
        Me.Controls.Add(Me.chkSignature)
        Me.Controls.Add(Me.chkHtml)
        Me.Controls.Add(Me.textBody)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lstCharset)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.textAttachments)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.textSubject)
        Me.Controls.Add(Me.textCc)
        Me.Controls.Add(Me.textTo)
        Me.Controls.Add(Me.textFrom)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.sbStatus)
        Me.Controls.Add(Me.pgSending)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.groupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "EASendMail EventHandler"

    Sub OnIdle(ByVal sender As Object, ByRef cancel As Boolean)
        cancel = m_bcancel
        If Not cancel Then
            Application.DoEvents() 'waiting server reponse or connecting server.
        End If
    End Sub

    Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        _SetStatus("Connected")
        cancel = m_bcancel
    End Sub


    Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        If pgSending.Value = 0 Then
            _SetStatus("Sending ...")
        End If

        _SetProgress(sent, total)
        cancel = m_bcancel
        If sent = total Then
            _SetStatus("Disconnecting ...")
        End If
    End Sub

    Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        _SetStatus("Authorized")
        cancel = m_bcancel
    End Sub

    Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        _SetStatus("Securing ...")
        cancel = m_bcancel
    End Sub

#End Region

    Private Sub _ChangeAuthStatus()
        textUser.Enabled = chkAuth.Checked
        textPassword.Enabled = chkAuth.Checked
    End Sub

#Region "Initilaize the Encoding List"
    Private Sub _InitCharset()
        Dim nIndex As Integer = 0
        Dim defaultEncoding As String = "utf-8" ' System.Text.Encoding.Default.HeaderName

        m_arCharset(nIndex, 0) = "Arabic(Windows)"
        m_arCharset(nIndex, 1) = "windows-1256"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Baltic(ISO)"
        m_arCharset(nIndex, 1) = "iso-8859-4"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Baltic(Windows)"
        m_arCharset(nIndex, 1) = "windows-1257"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Central Euporean(ISO)"
        m_arCharset(nIndex, 1) = "iso-8859-2"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Central Euporean(Windows)"
        m_arCharset(nIndex, 1) = "windows-1250"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Chinese Simplified(GB18030)"
        m_arCharset(nIndex, 1) = "GB18030"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Chinese Simplified(GB2312)"
        m_arCharset(nIndex, 1) = "gb2312"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Chinese Simplified(HZ)"
        m_arCharset(nIndex, 1) = "hz-gb-2312"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Chinese Traditional(Big5)"
        m_arCharset(nIndex, 1) = "big5"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Cyrillic(ISO)"
        m_arCharset(nIndex, 1) = "iso-8859-5"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Cyrillic(KOI8-R)"
        m_arCharset(nIndex, 1) = "koi8-r"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Cyrillic(KOI8-U)"
        m_arCharset(nIndex, 1) = "koi8-u"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Cyrillic(Windows)"
        m_arCharset(nIndex, 1) = "windows-1251"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Greek(ISO)"
        m_arCharset(nIndex, 1) = "iso-8859-7"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Greek(Windows)"
        m_arCharset(nIndex, 1) = "windows-1253"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Hebrew(Windows)"
        m_arCharset(nIndex, 1) = "windows-1255"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Japanese(JIS)"
        m_arCharset(nIndex, 1) = "iso-2022-jp"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Korean"
        m_arCharset(nIndex, 1) = "ks_c_5601-1987"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Korean(EUC)"
        m_arCharset(nIndex, 1) = "euc-kr"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Latin 9(ISO)"
        m_arCharset(nIndex, 1) = "iso-8859-15"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Thai(Windows)"
        m_arCharset(nIndex, 1) = "windows-874"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Turkish(ISO)"
        m_arCharset(nIndex, 1) = "iso-8859-9"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Turkish(Windows)"
        m_arCharset(nIndex, 1) = "windows-1254"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Unicode(UTF-7)"
        m_arCharset(nIndex, 1) = "utf-7"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Unicode(UTF-8)"
        m_arCharset(nIndex, 1) = "utf-8"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Vietnames(Windows)"
        m_arCharset(nIndex, 1) = "windows-1258"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Western European(ISO)"
        m_arCharset(nIndex, 1) = "iso-8859-1"
        nIndex = nIndex + 1

        m_arCharset(nIndex, 0) = "Western European(Windows)"
        m_arCharset(nIndex, 1) = "Windows-1252"
        nIndex = nIndex + 1

        Dim selectIndex As Integer = 25 'utf-8
        Dim i As Integer = 0
        For i = 0 To nIndex - 1
            lstCharset.Items.Add(m_arCharset(i, 0))
            If String.Compare(m_arCharset(i, 1), defaultEncoding, True) = 0 Then
                selectIndex = i
            End If
        Next
        lstCharset.SelectedIndex = selectIndex
    End Sub

    Private Sub _InitProtocols()
        lstProtocol.Items.Add("SMTP Protocol - Recommended")
        lstProtocol.Items.Add("Exchange Web Service - 2007/2010")
        lstProtocol.Items.Add("Exchange WebDav - 2000/2003")
        lstProtocol.SelectedIndex = 0
    End Sub

#End Region

    Private Sub _Init()

        Dim s As System.Text.StringBuilder = New System.Text.StringBuilder
        s.Append("This sample demonstrates how to send simple email." & vbCrLf & vbCrLf)
        s.Append("From: [$from]" & vbCrLf)
        s.Append("To: [$to]" & vbCrLf)
        s.Append("Subject: [$subject]" & vbCrLf & vbCrLf)
        s.Append("If no sever address was specified, the email will be delivered to the recipient's server directly,")
        s.Append("However, if you don't have a static IP address, ")
        s.Append("many anti-spam filters will mark it as a junk-email." & vbCrLf & vbCrLf)
        s.Append("If ""Digitial Signature"" was checked, please make sure you have the certificate for the sender address installed on ")
        s.Append("Local User Certificate Store." & vbCrLf & vbCrLf)
        s.Append("If ""Encrypt"" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store." & vbCrLf & vbCrLf)

        textBody.Text = s.ToString()

        _InitCharset()
        _InitProtocols()
        _ChangeAuthStatus()
    End Sub

#Region "Sign and Encrypt E-mail by Digital Certificate"
    Private Function _SignEncrypt(ByRef oMail As SmtpMail) As Boolean
        If chkSignature.Checked Then
            Try
                oMail.From.Certificate.FindSubject(oMail.From.Address, _
                  Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, _
                  "My")
            Catch exp As Exception
                MessageBox.Show("No sign certificate found for <" + oMail.From.Address + ">:" + exp.Message)
                btnSend.Text = "Send"
                _SignEncrypt = False
                Exit Function
            End Try
        End If

        Dim count As Integer = 0
        If chkEncrypt.Checked Then
            count = oMail.To.Count
            Dim i As Integer = 0
            For i = 0 To count - 1

                Dim oAddress As MailAddress = oMail.To(i)
                Try

                    oAddress.Certificate.FindSubject(oAddress.Address, _
                     Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, _
                     "AddressBook")
                Catch ep As Exception
                    Try

                        oAddress.Certificate.FindSubject(oAddress.Address, _
                         Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, _
                         "My")
                    Catch exp As Exception
                        MessageBox.Show("No encryption certificate found for <" + oAddress.Address + ">:" + exp.Message)
                        _SignEncrypt = False
                        Exit Function
                    End Try
                End Try
            Next

            count = oMail.Cc.Count
            For i = 0 To count - 1
                Dim oAddress As MailAddress = oMail.Cc(i)
                Try
                    oAddress.Certificate.FindSubject(oAddress.Address, _
                      Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, _
                      "AddressBook")
                Catch ep As Exception
                    Try
                        oAddress.Certificate.FindSubject(oAddress.Address, _
                         Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, _
                         "My")
                    Catch exp As Exception
                        MessageBox.Show("No encryption certificate found for <" + oAddress.Address + ">:" + exp.Message)
                        _SignEncrypt = False
                        Exit Function
                    End Try
                End Try
            Next
        End If

        _SignEncrypt = True
    End Function
#End Region

#Region "Send E-mail without SMTP server to multiple recipients"
    Private Sub _DirectSend(ByRef oMail As SmtpMail, ByRef oSmtp As SmtpClient)
        Dim recipients As AddressCollection = oMail.Recipients.Copy()
        Dim count As Integer = recipients.Count
        Dim i As Integer

        For i = 0 To count - 1
            Dim errStr As String = ""
            Dim address As MailAddress = recipients(i)

            Dim terminated As Boolean = False
            Try
                oMail.To.Clear()
                oMail.Cc.Clear()
                oMail.Bcc.Clear()

                oMail.To.Add(address)
                Dim oServer As SmtpServer = New SmtpServer("")

                sbStatus.Text = String.Format("Connecting server for {0} ... ", address.Address)
                pgSending.Value = 0
                oSmtp.SendMail(oServer, oMail)
                MessageBox.Show(String.Format("The message to <{0}> was sent to {1} successfully!", _
                    address.Address, _
                    oSmtp.CurrentSmtpServer.Server))

                sbStatus.Text = "Completed"

            Catch exp As SmtpTerminatedException
                errStr = exp.Message
                terminated = True
            Catch exp As SmtpServerException
                errStr = String.Format("Exception: Server Respond: {0}", exp.ErrorMessage)
            Catch exp As System.Net.Sockets.SocketException
                errStr = String.Format("Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message)
            Catch exp As System.ComponentModel.Win32Exception
                errStr = String.Format("Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message)
            Catch exp As System.Exception
                errStr = String.Format("Exception: Common: {0}", exp.Message)
            End Try


            If (terminated) Then
                Exit For
            End If

            If errStr.Length > 0 Then
                MessageBox.Show(String.Format("The message was unable to delivery to <{0}> due to " & vbCrLf & " {1}", _
       address.Address, errStr))
                sbStatus.Text = errStr
            End If
        Next
    End Sub
#End Region

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
            oMail.Charset = m_arCharset(lstCharset.SelectedIndex, 1)

            'Digital signature and encryption
            If Not _SignEncrypt(oMail) Then
                btnSend.Enabled = True
                btnCancel.Enabled = False
                Exit Sub
            End If

            Dim body As String = textBody.Text
            body = body.Replace("[$from]", oMail.From.ToString())
            body = body.Replace("[$to]", oMail.To.ToString())
            body = body.Replace("[$subject]", oMail.Subject)

            If chkHtml.Checked Then
                oMail.HtmlBody = body
            Else
                oMail.TextBody = body
            End If

            Dim count As Integer = m_arAttachment.Count
            Dim i As Integer = 0
            For i = 0 To count - 1
                'Add attachments
                oMail.AddAttachment(CType(m_arAttachment(i), String))
            Next

            Dim oServer As SmtpServer = New SmtpServer(textServer.Text)
            oServer.Protocol = lstProtocol.SelectedIndex

            If oServer.Server.Length <> 0 Then
                If (chkAuth.Checked) Then
                    oServer.User = textUser.Text
                    oServer.Password = textPassword.Text
                End If

                If (chkSSL.Checked) Then
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
                End If
            Else
                'To send email to the recipient directly(simulating the smtp server), 
                'please add a Received header, 
                'otherwise, many anti-spam filter will make it as junk email.
                Dim cur As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
                Dim gmtdate As String = System.DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur)
                gmtdate.Remove(gmtdate.Length - 3, 1)
                Dim recvheader As String = String.Format("from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;" & vbCrLf & Chr(9) & " {1}", _
                    oServer.HeloDomain, _
                    gmtdate)
                oMail.Headers.Insert(0, New HeaderItem("Received", recvheader))
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

            If oServer.Server.Length = 0 And oMail.Recipients.Count > 1 Then
                'To send email without specified smtp server, we have to send the emails one by one 
                ' to multiple recipients. That is because every recipient has different smtp server.
                _DirectSend(oMail, oSmtp)
            Else
                sbStatus.Text = "Connecting ... "
                pgSending.Value = 0
                oSmtp.SendMail(oServer, oMail)
                MessageBox.Show(String.Format("The message was sent to {0} successfully!", _
      oSmtp.CurrentSmtpServer.Server))
                sbStatus.Text = "Completed"
            End If
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

    Private Sub chkAuth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuth.CheckedChanged
        _ChangeAuthStatus()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        attachmentDlg.Reset()
        attachmentDlg.Multiselect = True
        attachmentDlg.CheckFileExists = True
        attachmentDlg.CheckPathExists = True
        If attachmentDlg.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Dim attachments() As String = attachmentDlg.FileNames
        Dim nLen As Integer = attachments.Length
        Dim i As Integer = 0
        For i = 0 To nLen - 1
            m_arAttachment.Add(attachments(i))
            Dim fileName As String = attachments(i)
            Dim pos As Integer = fileName.LastIndexOf("\")
            If pos <> -1 Then
                fileName = fileName.Substring(pos + 1)
            End If
            textAttachments.Text += fileName
            textAttachments.Text += ";"
        Next
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        m_arAttachment.Clear()
        textAttachments.Text = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        m_bcancel = True
        btnCancel.Enabled = False
    End Sub


    Private Sub AdjustControlSize()
        If (Me.Width < 700) Then
            Me.Width = 700
        End If

        If (Me.Height < 500) Then
            Me.Height = 500
        End If


        textBody.Width = Me.Width - 35
        textBody.Height = Me.Height - 330

        pgSending.Top = textBody.Height + textBody.Top + 15
        pgSending.Width = Me.Width - 240
        btnSend.Top = pgSending.Top - 8
        btnCancel.Top = btnSend.Top
        btnSend.Left = pgSending.Width + 40
        btnCancel.Left = pgSending.Width + 125

        groupBox1.Left = Me.Width - 320

        textFrom.Width = Me.Width - 400
        textTo.Width = textFrom.Width
        textCc.Width = textFrom.Width
        textSubject.Width = textFrom.Width

        textAttachments.Width = Me.Width - 260
        btnAdd.Left = textAttachments.Width + 110
        btnClear.Left = textAttachments.Width + 160
    End Sub

    Private Sub Form1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        AdjustControlSize()
    End Sub

#Region "Cross Thread Access Control"
    Private Delegate Sub SetStatusDelegate(ByVal v As String)
    Private Delegate Sub SetProgressDelegate(ByVal sent As Integer, ByVal total As Integer)

    Private Sub _SetStatusCallBack(ByVal v As String)
        sbStatus.Text = v
    End Sub

    Private Sub _SetProgressCallBack(ByVal sent As Integer, ByVal total As Integer)
        Dim t As Long = sent
        t = t * 100
        t = t / total
        Dim x As Integer = t
        pgSending.Value = x

        Dim tick As Long = System.DateTime.Now.Ticks
        ' call DoEvents every 0.2 second 

        If (tick - m_eventtick > 2000000) Then
            ' Do not call DoEvents too frequently in a very fast lan + larg email.
            m_eventtick = tick
            Application.DoEvents()
        End If
    End Sub

    'Why we need to change the list item text by this function.
    'Because with BeginSendMail method, all the events are fired on another
    'thread, to change the control safety, we used this function to 
    'update list item. more detail, please refer to Control.BeginInvoke method
    'in MSDN
    Private Sub _SetStatus(ByVal v As String)
        If InvokeRequired Then
            Dim args(0) As Object
            args(0) = v

            Dim d As SetStatusDelegate = New SetStatusDelegate(AddressOf _SetStatusCallBack)
            BeginInvoke(d, args)
        Else
            _SetStatusCallBack(v)
        End If
    End Sub
    Private Sub _SetProgress(ByVal sent As Integer, ByVal total As Integer)
        If InvokeRequired Then
            Dim args(1) As Object
            args(0) = sent
            args(1) = total
            Dim d As SetProgressDelegate = New SetProgressDelegate(AddressOf _SetProgressCallBack)
            BeginInvoke(d, args)
        Else
            _SetProgressCallBack(sent, total)
        End If
    End Sub
#End Region

End Class
