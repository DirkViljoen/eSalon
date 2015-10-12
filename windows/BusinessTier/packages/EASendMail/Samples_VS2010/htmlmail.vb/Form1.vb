Imports EASendMail

Public Class Form1
    Inherits System.Windows.Forms.Form

    Private m_arCharset(27, 1) As String
    Private m_arAttachment As ArrayList = New ArrayList
    Private m_bcancel As Boolean = False
    Private m_eventtick As Long = 0
    Private m_htmlDoc As mshtml.IHTMLDocument2



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
    Friend WithEvents colorDlg As System.Windows.Forms.ColorDialog
    Friend WithEvents lstFont As System.Windows.Forms.ComboBox
    Friend WithEvents lstSize As System.Windows.Forms.ComboBox
    Friend WithEvents btnB As System.Windows.Forms.Button
    Friend WithEvents btnI As System.Windows.Forms.Button
    Friend WithEvents btnU As System.Windows.Forms.Button
    Friend WithEvents btnC As System.Windows.Forms.Button
    Friend WithEvents btnP As System.Windows.Forms.Button
    Friend WithEvents attachmentDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkSignature As System.Windows.Forms.CheckBox
    Friend WithEvents htmlEditor As System.Windows.Forms.WebBrowser
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
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.textFrom = New System.Windows.Forms.TextBox()
        Me.textTo = New System.Windows.Forms.TextBox()
        Me.textCc = New System.Windows.Forms.TextBox()
        Me.textSubject = New System.Windows.Forms.TextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstProtocol = New System.Windows.Forms.ComboBox()
        Me.chkAuth = New System.Windows.Forms.CheckBox()
        Me.chkSSL = New System.Windows.Forms.CheckBox()
        Me.textPassword = New System.Windows.Forms.TextBox()
        Me.textUser = New System.Windows.Forms.TextBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.Server = New System.Windows.Forms.Label()
        Me.textServer = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.textAttachments = New System.Windows.Forms.TextBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.pgSending = New System.Windows.Forms.ProgressBar()
        Me.sbStatus = New System.Windows.Forms.StatusBar()
        Me.label9 = New System.Windows.Forms.Label()
        Me.lstCharset = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.attachmentDlg = New System.Windows.Forms.OpenFileDialog()
        Me.chkSignature = New System.Windows.Forms.CheckBox()
        Me.chkEncrypt = New System.Windows.Forms.CheckBox()
        Me.colorDlg = New System.Windows.Forms.ColorDialog()
        Me.lstFont = New System.Windows.Forms.ComboBox()
        Me.lstSize = New System.Windows.Forms.ComboBox()
        Me.btnB = New System.Windows.Forms.Button()
        Me.btnI = New System.Windows.Forms.Button()
        Me.btnU = New System.Windows.Forms.Button()
        Me.btnC = New System.Windows.Forms.Button()
        Me.btnP = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.htmlEditor = New System.Windows.Forms.WebBrowser()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(8, 19)
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
        Me.label3.Location = New System.Drawing.Point(8, 75)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(21, 15)
        Me.label3.TabIndex = 2
        Me.label3.Text = "Cc"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(8, 100)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(48, 15)
        Me.label4.TabIndex = 3
        Me.label4.Text = "Subject"
        '
        'textFrom
        '
        Me.textFrom.Location = New System.Drawing.Point(64, 19)
        Me.textFrom.Name = "textFrom"
        Me.textFrom.Size = New System.Drawing.Size(297, 21)
        Me.textFrom.TabIndex = 1
        '
        'textTo
        '
        Me.textTo.Location = New System.Drawing.Point(64, 47)
        Me.textTo.Name = "textTo"
        Me.textTo.Size = New System.Drawing.Size(297, 21)
        Me.textTo.TabIndex = 2
        '
        'textCc
        '
        Me.textCc.Location = New System.Drawing.Point(64, 73)
        Me.textCc.Name = "textCc"
        Me.textCc.Size = New System.Drawing.Size(297, 21)
        Me.textCc.TabIndex = 3
        '
        'textSubject
        '
        Me.textSubject.Location = New System.Drawing.Point(64, 99)
        Me.textSubject.Name = "textSubject"
        Me.textSubject.Size = New System.Drawing.Size(297, 21)
        Me.textSubject.TabIndex = 4
        Me.textSubject.Text = "Test HTML subject"
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
        Me.groupBox1.Location = New System.Drawing.Point(398, 7)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(274, 173)
        Me.groupBox1.TabIndex = 8
        Me.groupBox1.TabStop = False
        '
        'lstProtocol
        '
        Me.lstProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstProtocol.FormattingEnabled = True
        Me.lstProtocol.Location = New System.Drawing.Point(12, 143)
        Me.lstProtocol.Name = "lstProtocol"
        Me.lstProtocol.Size = New System.Drawing.Size(247, 23)
        Me.lstProtocol.TabIndex = 15
        '
        'chkAuth
        '
        Me.chkAuth.AutoSize = True
        Me.chkAuth.Location = New System.Drawing.Point(11, 43)
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
        Me.textPassword.Location = New System.Drawing.Point(75, 94)
        Me.textPassword.Name = "textPassword"
        Me.textPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.textPassword.Size = New System.Drawing.Size(182, 21)
        Me.textPassword.TabIndex = 13
        '
        'textUser
        '
        Me.textUser.Location = New System.Drawing.Point(75, 70)
        Me.textUser.Name = "textUser"
        Me.textUser.Size = New System.Drawing.Size(182, 21)
        Me.textUser.TabIndex = 12
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(8, 95)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(61, 15)
        Me.label7.TabIndex = 2
        Me.label7.Text = "Password"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(8, 68)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(33, 15)
        Me.label6.TabIndex = 1
        Me.label6.Text = "User"
        '
        'Server
        '
        Me.Server.AutoSize = True
        Me.Server.Location = New System.Drawing.Point(8, 17)
        Me.Server.Name = "Server"
        Me.Server.Size = New System.Drawing.Size(42, 15)
        Me.Server.TabIndex = 0
        Me.Server.Text = "Server"
        '
        'textServer
        '
        Me.textServer.Location = New System.Drawing.Point(75, 18)
        Me.textServer.Name = "textServer"
        Me.textServer.Size = New System.Drawing.Size(185, 21)
        Me.textServer.TabIndex = 10
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(6, 186)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(74, 15)
        Me.label8.TabIndex = 9
        Me.label8.Text = "Attachments"
        '
        'textAttachments
        '
        Me.textAttachments.BackColor = System.Drawing.SystemColors.Info
        Me.textAttachments.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.textAttachments.Location = New System.Drawing.Point(86, 186)
        Me.textAttachments.Name = "textAttachments"
        Me.textAttachments.ReadOnly = True
        Me.textAttachments.Size = New System.Drawing.Size(455, 21)
        Me.textAttachments.TabIndex = 6
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(509, 412)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(80, 23)
        Me.btnSend.TabIndex = 15
        Me.btnSend.TabStop = False
        Me.btnSend.Text = "Send"
        '
        'pgSending
        '
        Me.pgSending.Location = New System.Drawing.Point(8, 420)
        Me.pgSending.Name = "pgSending"
        Me.pgSending.Size = New System.Drawing.Size(480, 8)
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
        Me.label9.Location = New System.Drawing.Point(8, 133)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(59, 15)
        Me.label9.TabIndex = 15
        Me.label9.Text = "Encoding"
        '
        'lstCharset
        '
        Me.lstCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstCharset.Location = New System.Drawing.Point(79, 131)
        Me.lstCharset.Name = "lstCharset"
        Me.lstCharset.Size = New System.Drawing.Size(233, 23)
        Me.lstCharset.TabIndex = 5
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(547, 186)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(40, 23)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(595, 186)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(52, 23)
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "Clear"
        '
        'chkSignature
        '
        Me.chkSignature.AutoSize = True
        Me.chkSignature.Location = New System.Drawing.Point(8, 162)
        Me.chkSignature.Name = "chkSignature"
        Me.chkSignature.Size = New System.Drawing.Size(120, 19)
        Me.chkSignature.TabIndex = 17
        Me.chkSignature.Text = "Digitial Signature"
        '
        'chkEncrypt
        '
        Me.chkEncrypt.AutoSize = True
        Me.chkEncrypt.Location = New System.Drawing.Point(138, 164)
        Me.chkEncrypt.Name = "chkEncrypt"
        Me.chkEncrypt.Size = New System.Drawing.Size(66, 19)
        Me.chkEncrypt.TabIndex = 18
        Me.chkEncrypt.Text = "Encrypt"
        '
        'lstFont
        '
        Me.lstFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstFont.Location = New System.Drawing.Point(6, 212)
        Me.lstFont.Name = "lstFont"
        Me.lstFont.Size = New System.Drawing.Size(136, 23)
        Me.lstFont.TabIndex = 20
        '
        'lstSize
        '
        Me.lstSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstSize.Location = New System.Drawing.Point(150, 212)
        Me.lstSize.Name = "lstSize"
        Me.lstSize.Size = New System.Drawing.Size(80, 23)
        Me.lstSize.TabIndex = 21
        '
        'btnB
        '
        Me.btnB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnB.Location = New System.Drawing.Point(230, 210)
        Me.btnB.Name = "btnB"
        Me.btnB.Size = New System.Drawing.Size(24, 23)
        Me.btnB.TabIndex = 22
        Me.btnB.Text = "B"
        '
        'btnI
        '
        Me.btnI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnI.Location = New System.Drawing.Point(254, 210)
        Me.btnI.Name = "btnI"
        Me.btnI.Size = New System.Drawing.Size(24, 23)
        Me.btnI.TabIndex = 23
        Me.btnI.Text = "I"
        '
        'btnU
        '
        Me.btnU.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnU.Location = New System.Drawing.Point(278, 210)
        Me.btnU.Name = "btnU"
        Me.btnU.Size = New System.Drawing.Size(24, 23)
        Me.btnU.TabIndex = 24
        Me.btnU.Text = "U"
        '
        'btnC
        '
        Me.btnC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnC.ForeColor = System.Drawing.Color.Red
        Me.btnC.Location = New System.Drawing.Point(302, 210)
        Me.btnC.Name = "btnC"
        Me.btnC.Size = New System.Drawing.Size(24, 23)
        Me.btnC.TabIndex = 25
        Me.btnC.Text = "C"
        '
        'btnP
        '
        Me.btnP.Location = New System.Drawing.Point(350, 210)
        Me.btnP.Name = "btnP"
        Me.btnP.Size = New System.Drawing.Size(88, 23)
        Me.btnP.TabIndex = 26
        Me.btnP.Text = "Insert Picture"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(597, 412)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 23)
        Me.btnCancel.TabIndex = 28
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "Cancel"
        '
        'htmlEditor
        '
        Me.htmlEditor.Location = New System.Drawing.Point(8, 239)
        Me.htmlEditor.MinimumSize = New System.Drawing.Size(20, 20)
        Me.htmlEditor.Name = "htmlEditor"
        Me.htmlEditor.Size = New System.Drawing.Size(664, 160)
        Me.htmlEditor.TabIndex = 29
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(684, 462)
        Me.Controls.Add(Me.htmlEditor)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnP)
        Me.Controls.Add(Me.btnC)
        Me.Controls.Add(Me.btnU)
        Me.Controls.Add(Me.btnI)
        Me.Controls.Add(Me.btnB)
        Me.Controls.Add(Me.lstSize)
        Me.Controls.Add(Me.lstFont)
        Me.Controls.Add(Me.chkEncrypt)
        Me.Controls.Add(Me.chkSignature)
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

#Region "Initialize Encoding List"
    Private Sub _InitCharset()
        Dim nIndex As Integer = 0
        Dim defaultEncoding As String = "utf-8" 'System.Text.Encoding.Default.HeaderName

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
#End Region

    Private Sub _InitProtocols()
        lstProtocol.Items.Add("SMTP Protocol - Recommended")
        lstProtocol.Items.Add("Exchange Web Service - 2007/2010")
        lstProtocol.Items.Add("Exchange WebDav - 2000/2003")
        lstProtocol.SelectedIndex = 0
    End Sub

    Private Sub _Init()
        _InitCharset()
        _InitProtocols()
        _ChangeAuthStatus()
    End Sub

#Region "Sign and encrypt E-mail by digital certificate"
    Private Function _Signencrypt(ByRef oMail As SmtpMail) As Boolean
        If chkSignature.Checked Then
            Try
                oMail.From.Certificate.FindSubject(oMail.From.Address, _
                  Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, _
                  "My")
            Catch exp As Exception
                MessageBox.Show("No sign certificate found for <" + oMail.From.Address + ">:" + exp.Message)
                btnSend.Text = "Send"
                _Signencrypt = False
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
                        _Signencrypt = False
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
                        _Signencrypt = False
                        Exit Function
                    End Try
                End Try
            Next
        End If

        _Signencrypt = True
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
            'You can add more recipient by Add method
            'oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"))

            oMail.Cc = New AddressCollection(textCc.Text)
            oMail.Subject = textSubject.Text
            oMail.Charset = m_arCharset(lstCharset.SelectedIndex, 1)

            'digital signature and encryption
            If Not _Signencrypt(oMail) Then
                btnSend.Enabled = True
                btnCancel.Enabled = False
                Exit Sub
            End If

            Dim basepath As String = Application.ExecutablePath
            Dim pos As Integer = basepath.LastIndexOfAny("/\".ToCharArray())
            If pos <> -1 Then
                basepath = basepath.Substring(0, pos)
            End If

            Dim body As String = m_htmlDoc.body.innerHTML
            body = body.Replace("[$from]", _EncodeAddress(oMail.From.ToString()))
            body = body.Replace("[$to]", _EncodeAddress(oMail.To.ToString()))
            body = body.Replace("[$subject]", _EncodeAddress(oMail.Subject))
            Dim htmlheader As String = String.Format("<html><title>{0}</title><meta HTTP-EQUIV=""Content-Type"" Content=""text-html; charset={1}""><META content=""MSHTML 6.00.2900.2769"" name=GENERATOR><body>", _
     oMail.Subject, oMail.Charset)

            body = body.Insert(0, htmlheader)
            body += "</body></html>"

            oMail.ImportHtml(body, _
                Application.ExecutablePath, _
                ImportHtmlBodyOptions.ImportLocalPictures)

            ' oMail.HtmlBody = textBody.Text

            Dim count As Integer = m_arAttachment.Count
            Dim i As Integer = 0
            For i = 0 To count - 1
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
        htmlEditor.Focus()
    End Sub

    Private Sub btnB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnB.Click
        m_htmlDoc.execCommand("Bold", False, Nothing)
        htmlEditor.Focus()
    End Sub

    Private Sub btnI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnI.Click
        m_htmlDoc.execCommand("Italic", False, Nothing)
        htmlEditor.Focus()
    End Sub

    Private Sub btnU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnU.Click
        m_htmlDoc.execCommand("Underline", False, Nothing)
        htmlEditor.Focus()
    End Sub

    Private Sub btnC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnC.Click
        If colorDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim v As String = String.Format("#{0:x2}{1:x2}{2:x2}", colorDlg.Color.R, _
                 colorDlg.Color.G, _
                 colorDlg.Color.B)
            m_htmlDoc.execCommand("ForeColor", False, v)
        End If
        htmlEditor.Focus()
    End Sub

#Region "Initialize Fonts List"
    Protected Sub InitFonts()
        Dim arFonts() As String = {"Choose Font Style ...", _
                  "Allegro BT", _
                  "Arial", _
                  "Arial Baltic", _
                  "Arial Black", _
                  "Arial CE", _
                  "Arial CYR", _
                  "Arial Greek", _
                  "Arial Narrow", _
                  "Arial TUR", _
                  "AvantGarde Bk BT", _
                  "BankGothic Md BT", _
                  "Basemic", _
                  "Basemic Symbol", _
                  "Basemic Times", _
                  "Batang", _
                  "BatangChe", _
                  "Benguiat Bk BT", _
                  "BernhardFashion BT", _
                  "BernhardMod BT", _
                  "Book Antiqua", _
                  "Bookman Old Style", _
                  "Bremen Bd BT", _
                  "Century Gothic", _
                  "Charlesworth", _
                  "Comic Sans MS", _
                  "CommonBullets", _
                  "CopprplGoth Bd BT", _
                  "Courier", _
                  "Courier New", _
                  "Courier New Baltic", _
                  "Courier New CE", _
                  "Courier New CYR", _
                  "Courier New Greek", _
                  "Courier New TUR", _
                  "Dauphin", _
                  "Dotum", _
                  "DotumChe", _
                  "Dungeon", _
                  "English111 Vivace BT", _
                  "Estrangelo Edessa", _
                  "Fixedsys", _
                  "Franklin Gothic Medium", _
                  "Futura Lt BT", _
                  "Futura Md BT", _
                  "Futura XBlk BT", _
                  "FuturaBlack BT", _
                  "Garamond", _
                  "Gautami", _
                  "Georgia", _
                  "GoudyHandtooled BT", _
                  "GoudyOlSt BT", _
                  "Gulim", _
                  "GulimChe", _
                  "Gungsuh", _
                  "GungsuhChe", _
                  "Haettenschweiler", _
                  "Humanst521 BT", _
                  "Impact", _
                  "Kabel Bk BT", _
                  "Kabel Ult BT", _
                  "Kingsoft Phonetic Plain", _
                  "Latha", _
                  "Lithograph", _
                  "LithographLight", _
                  "Lucida Console", _
                  "Lucida Sans Unicode", _
                  "Mangal", _
                  "Marlett", _
                  "Microsoft Sans Serif", _
                  "MingLiU", _
                  "Modern", _
                  "Monotype Corsiva", _
                  "MS Gothic", _
                  "MS Mincho", _
                  "MS Outlook", _
                  "MS PGothic", _
                  "MS PMincho", _
                  "MS Sans Serif", _
                  "MS Serif", _
                  "MS UI Gothic", _
                  "MT Extra", _
                  "MV Boli", _
                  "Myriad Condensed Web", _
                  "Myriad Web", _
                  "OzHandicraft BT", _
                  "Palatino Linotype", _
                  "PMingLiU", _
                  "PosterBodoni BT", _
                  "Raavi", _
                  "Roman", _
                  "Script", _
                  "Serifa BT", _
                  "Serifa Th BT", _
                  "Shruti", _
                  "Small Fonts", _
                  "Souvenir Lt BT", _
                  "Staccato222 BT", _
                  "Swiss911 XCm BT", _
                  "Sylfaen", _
                  "Symbol", _
                  "System", _
                  "Tahoma", _
                  "Terminal", _
                  "Times New Roman", _
                  "Times New Roman Baltic", _
                  "Times New Roman CE", _
                  "Times New Roman CYR", _
                  "Times New Roman Greek", _
                  "Times New Roman TUR", _
                  "Trebuchet MS", _
                  "Tunga", _
                  "TypoUpright BT", _
                  "Verdana", _
                  "VisualUI", _
                  "Webdings", _
                  "Wingdings", _
                  "Wingdings 2", _
                  "Wingdings 3", _
                  "WP Arabic Sihafa", _
                  "WP ArabicScript Sihafa", _
                  "WP BoxDrawing", _
                  "WP CyrillicA", _
                  "WP CyrillicB", _
                  "WP Greek Century", _
                  "WP Greek Courier", _
                  "WP Greek Helve", _
                  "WP Hebrew David", _
                  "WP IconicSymbolsA", _
                  "WP IconicSymbolsB", _
                  "WP Japanese", _
                  "WP MathA", _
                  "WP MathB", _
                  "WP MathExtendedA", _
                  "WP MathExtendedB", _
                  "WP MultinationalA Courier", _
                  "WP MultinationalA Helve", _
                  "WP MultinationalA Roman", _
                  "WP MultinationalB Courier", _
                  "WP MultinationalB Helve", _
                  "WP MultinationalB Roman", _
                  "WP Phonetic", _
                  "WP TypographicSymbols", _
                  "WST_Czec", _
                  "WST_Engl", _
                  "WST_Fren", _
                  "WST_Germ", _
                  "WST_Ital", _
                  "WST_Span", _
                  "WST_Swed", _
                  "ZapfEllipt BT", _
                  "Zurich Ex BT"}

        Dim i As Integer
        Dim nCount As Integer = arFonts.Length
        For i = 0 To nCount - 1
            lstFont.Items.Add(arFonts(i))
        Next

        lstFont.SelectedIndex = 0
        lstSize.Items.Add("Font Size ... ")
        For i = 1 To 7
            lstSize.Items.Add(i)
        Next
        lstSize.SelectedIndex = 0
    End Sub
#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        htmlEditor.Navigate("about:blank")

        m_htmlDoc = CType(htmlEditor.Document.DomDocument, mshtml.IHTMLDocument2)
        m_htmlDoc.designMode = "on"
        InitFonts()
    End Sub

    Private Sub btnP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnP.Click
        m_htmlDoc.execCommand("InsertImage", True, Nothing)
    End Sub

    Private Sub lstSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSize.SelectedIndexChanged
        Dim size As String = lstSize.Text
        lstSize.SelectedIndex = 0
        m_htmlDoc.execCommand("fontsize", False, size)
        htmlEditor.Focus()
    End Sub

    Private Sub lstFont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFont.SelectedIndexChanged
        Dim font As String = lstFont.Text
        lstFont.SelectedIndex = 0
        m_htmlDoc.execCommand("fontname", False, font)
        htmlEditor.Focus()
    End Sub

    Private Function _EncodeAddress(ByVal v As String) As String
        v = v.Replace(">", "&gt;")
        v = v.Replace("<", "&lt;")
        _EncodeAddress = v
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        m_bcancel = True
        btnCancel.Enabled = False
    End Sub

    Private Sub htmlEditor_Navigated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles htmlEditor.Navigated
        Dim s As New System.Text.StringBuilder()
        s.Append("<div>This sample demonstrates how to send html email.</div><div>&nbsp;</div>")
        s.Append("<div>From: [$from]</div>")
        s.Append("<div>To: [$to]</div>")
        s.Append("<div>Subject: [$subject]</div><div>&nbsp;</div>")
        s.Append("<div>If no sever address was specified, the email will be delivered to the recipient's server directly,")
        s.Append("However, if you don't have a static IP address, ")
        s.Append("many anti-spam filters will mark it as a junk-email.</div><div>&nbsp;</div>")
        s.Append("<div>If ""Digitial Signature"" was checked, please make sure you have the certificate for the sender address installed on ")
        s.Append("Local User Certificate Store.</div><div>&nbsp;</div>")
        s.Append("<div>If ""Encrypt"" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store.</div>")
        m_htmlDoc.body.innerHTML = s.ToString()
    End Sub

    Private Sub Form1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If (Me.Width < 700) Then
            Me.Width = 700
        End If

        If (Me.Height < 500) Then
            Me.Height = 500
        End If


        htmlEditor.Width = Me.Width - 35
        htmlEditor.Height = Me.Height - 330

        pgSending.Top = htmlEditor.Height + htmlEditor.Top + 15
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
    'thread, to change the list item safety, we used this function to 
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
