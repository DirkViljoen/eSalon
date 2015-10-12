'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2006 - 2012  ADMINSYSTEM SOFTWARE LIMITED                         |
' |
' |    Project: It demonstrates how to use EASendMail to send  email to muliple recipients with 
' |             vb.net.
' |
' |    File: Form1 : implementation file
' |
' |    Author: Ivan Lui ( ivan@emailarchitect.net )
'  ===============================================================================
Imports EASendMail
Public Class Form1
    Inherits System.Windows.Forms.Form
    Private m_ntotal As Integer = 0
    Private m_nsent As Integer = 0
    Private m_nsuccess As Integer = 0
    Private m_nfailure As Integer = 0
    Private m_arCharset(27, 1) As String
    Private m_arAttachment As ArrayList = New ArrayList
    Private m_bcancel As Boolean = False

#Region "EASendMail EventHandler"

    Sub OnIdle(ByVal sender As Object, ByRef cancel As Boolean)
        cancel = m_bcancel
        If Not cancel Then
            Application.DoEvents() 'waiting server reponse or connecting server.
        End If
    End Sub

    Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        Dim oSmtp As SmtpClient = sender
        Dim index As Integer = oSmtp.Tag
        _CrossThreadSetItemText("Connected", index)
        cancel = m_bcancel
    End Sub


    Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        Dim oSmtp As SmtpClient = sender
        Dim index As Integer = oSmtp.Tag

        If sent <> total Then
            Dim v As String = String.Format("Sending {0}/{1} ... ", sent, total)
            _CrossThreadSetItemText(v, index)
        Else
            _CrossThreadSetItemText("Disconnecting ...", index)
        End If

        cancel = m_bcancel
        Application.DoEvents()
    End Sub

    Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        Dim oSmtp As SmtpClient = sender
        Dim index As Integer = oSmtp.Tag
        _CrossThreadSetItemText("Authorized", index)
        cancel = m_bcancel
    End Sub

    Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        Dim oSmtp As SmtpClient = sender
        Dim index As Integer = oSmtp.Tag
        _CrossThreadSetItemText("Securing ...", index)
        cancel = m_bcancel
    End Sub

#End Region

#Region "Cross Thread Access List Item"
    Private Delegate Sub SetItemTextDelegate(ByVal v As String, ByVal index As Integer)
    Private Sub _SetItemText(ByVal v As String, ByVal index As Integer)
        lstTo.Items(index).SubItems(2).Text = v
    End Sub

    'Why we need to change the list item text by this function.
    'Because with BeginSendMail method, all the events are fired on another
    'thread, to change the list item safety, we used this function to 
    'update list item. more detail, please refer to Control.BeginInvoke method
    'in MSDN
    Private Sub _CrossThreadSetItemText(ByVal v As String, ByVal index As Integer)

        Dim args(1) As Object
        args(0) = v
        args(1) = index
        Dim d As SetItemTextDelegate = New SetItemTextDelegate(AddressOf _SetItemText)
        BeginInvoke(d, args)
    End Sub
#End Region

#Region " Windows Form Designer generated code "
    Friend WithEvents status As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents chkSSL As System.Windows.Forms.CheckBox
    Friend WithEvents textFrom As System.Windows.Forms.TextBox
    Friend WithEvents textSubject As System.Windows.Forms.TextBox
    Friend WithEvents textPassword As System.Windows.Forms.TextBox
    Friend WithEvents textUser As System.Windows.Forms.TextBox
    Friend WithEvents Server As System.Windows.Forms.Label
    Friend WithEvents textServer As System.Windows.Forms.TextBox
    Friend WithEvents textAttachments As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents chkAuth As System.Windows.Forms.CheckBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lstCharset As System.Windows.Forms.ComboBox
    Friend WithEvents textBody As System.Windows.Forms.RichTextBox
    Friend WithEvents attachmentDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkTestRecipients As System.Windows.Forms.CheckBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents lstTo As System.Windows.Forms.ListView
    Friend WithEvents sbStatus As System.Windows.Forms.Label
    Friend WithEvents btnAddTo As System.Windows.Forms.Button
    Friend WithEvents btnClearTo As System.Windows.Forms.Button
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAddress As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents textThreads As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSimple As System.Windows.Forms.Button



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

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.textFrom = New System.Windows.Forms.TextBox()
        Me.textSubject = New System.Windows.Forms.TextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
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
        Me.label9 = New System.Windows.Forms.Label()
        Me.lstCharset = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.textBody = New System.Windows.Forms.RichTextBox()
        Me.attachmentDlg = New System.Windows.Forms.OpenFileDialog()
        Me.chkTestRecipients = New System.Windows.Forms.CheckBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.textThreads = New System.Windows.Forms.TextBox()
        Me.lstTo = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAddress = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.sbStatus = New System.Windows.Forms.Label()
        Me.btnAddTo = New System.Windows.Forms.Button()
        Me.btnClearTo = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSimple = New System.Windows.Forms.Button()
        Me.status = New System.Windows.Forms.Label()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(8, 8)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(36, 15)
        Me.label1.TabIndex = 0
        Me.label1.Text = "From"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(8, 34)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(21, 15)
        Me.label2.TabIndex = 1
        Me.label2.Text = "To"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(8, 254)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(48, 15)
        Me.label4.TabIndex = 3
        Me.label4.Text = "Subject"
        '
        'textFrom
        '
        Me.textFrom.Location = New System.Drawing.Point(64, 8)
        Me.textFrom.Name = "textFrom"
        Me.textFrom.Size = New System.Drawing.Size(328, 21)
        Me.textFrom.TabIndex = 1
        '
        'textSubject
        '
        Me.textSubject.Location = New System.Drawing.Point(64, 251)
        Me.textSubject.Name = "textSubject"
        Me.textSubject.Size = New System.Drawing.Size(328, 21)
        Me.textSubject.TabIndex = 4
        Me.textSubject.Text = "Test sample"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.chkAuth)
        Me.groupBox1.Controls.Add(Me.chkSSL)
        Me.groupBox1.Controls.Add(Me.textPassword)
        Me.groupBox1.Controls.Add(Me.textUser)
        Me.groupBox1.Controls.Add(Me.label7)
        Me.groupBox1.Controls.Add(Me.label6)
        Me.groupBox1.Controls.Add(Me.Server)
        Me.groupBox1.Controls.Add(Me.textServer)
        Me.groupBox1.Location = New System.Drawing.Point(409, 8)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(263, 181)
        Me.groupBox1.TabIndex = 8
        Me.groupBox1.TabStop = False
        '
        'chkAuth
        '
        Me.chkAuth.AutoSize = True
        Me.chkAuth.Location = New System.Drawing.Point(8, 44)
        Me.chkAuth.Name = "chkAuth"
        Me.chkAuth.Size = New System.Drawing.Size(206, 19)
        Me.chkAuth.TabIndex = 11
        Me.chkAuth.Text = "My server requires authentication"
        '
        'chkSSL
        '
        Me.chkSSL.AutoSize = True
        Me.chkSSL.Location = New System.Drawing.Point(8, 142)
        Me.chkSSL.Name = "chkSSL"
        Me.chkSSL.Size = New System.Drawing.Size(114, 19)
        Me.chkSSL.TabIndex = 14
        Me.chkSSL.Text = "SSL Connection"
        '
        'textPassword
        '
        Me.textPassword.Location = New System.Drawing.Point(70, 100)
        Me.textPassword.Name = "textPassword"
        Me.textPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.textPassword.Size = New System.Drawing.Size(184, 21)
        Me.textPassword.TabIndex = 13
        '
        'textUser
        '
        Me.textUser.Location = New System.Drawing.Point(70, 72)
        Me.textUser.Name = "textUser"
        Me.textUser.Size = New System.Drawing.Size(184, 21)
        Me.textUser.TabIndex = 12
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(8, 100)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(61, 15)
        Me.label7.TabIndex = 2
        Me.label7.Text = "Password"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(8, 74)
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
        Me.textServer.Location = New System.Drawing.Point(70, 16)
        Me.textServer.Name = "textServer"
        Me.textServer.Size = New System.Drawing.Size(184, 21)
        Me.textServer.TabIndex = 10
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(10, 312)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(74, 15)
        Me.label8.TabIndex = 9
        Me.label8.Text = "Attachments"
        '
        'textAttachments
        '
        Me.textAttachments.BackColor = System.Drawing.SystemColors.Info
        Me.textAttachments.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.textAttachments.Location = New System.Drawing.Point(88, 312)
        Me.textAttachments.Name = "textAttachments"
        Me.textAttachments.ReadOnly = True
        Me.textAttachments.Size = New System.Drawing.Size(440, 21)
        Me.textAttachments.TabIndex = 6
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(361, 434)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(104, 23)
        Me.btnSend.TabIndex = 15
        Me.btnSend.TabStop = False
        Me.btnSend.Text = "Send"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(10, 284)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(59, 15)
        Me.label9.TabIndex = 15
        Me.label9.Text = "Encoding"
        '
        'lstCharset
        '
        Me.lstCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstCharset.Location = New System.Drawing.Point(88, 280)
        Me.lstCharset.Name = "lstCharset"
        Me.lstCharset.Size = New System.Drawing.Size(183, 23)
        Me.lstCharset.TabIndex = 5
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(564, 312)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(40, 23)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(612, 312)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(55, 23)
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "Clear"
        '
        'textBody
        '
        Me.textBody.Location = New System.Drawing.Point(11, 341)
        Me.textBody.Name = "textBody"
        Me.textBody.Size = New System.Drawing.Size(661, 87)
        Me.textBody.TabIndex = 14
        Me.textBody.Text = ""
        '
        'chkTestRecipients
        '
        Me.chkTestRecipients.AutoSize = True
        Me.chkTestRecipients.Location = New System.Drawing.Point(315, 282)
        Me.chkTestRecipients.Name = "chkTestRecipients"
        Me.chkTestRecipients.Size = New System.Drawing.Size(131, 19)
        Me.chkTestRecipients.TabIndex = 16
        Me.chkTestRecipients.Text = "Test Email Address"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(461, 283)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(111, 15)
        Me.label3.TabIndex = 17
        Me.label3.Text = "Maximum Threads"
        '
        'textThreads
        '
        Me.textThreads.Location = New System.Drawing.Point(583, 281)
        Me.textThreads.Name = "textThreads"
        Me.textThreads.Size = New System.Drawing.Size(48, 21)
        Me.textThreads.TabIndex = 18
        Me.textThreads.Text = "10"
        '
        'lstTo
        '
        Me.lstTo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colAddress, Me.colStatus})
        Me.lstTo.FullRowSelect = True
        Me.lstTo.GridLines = True
        Me.lstTo.Location = New System.Drawing.Point(64, 32)
        Me.lstTo.Name = "lstTo"
        Me.lstTo.Size = New System.Drawing.Size(328, 213)
        Me.lstTo.TabIndex = 19
        Me.lstTo.UseCompatibleStateImageBehavior = False
        Me.lstTo.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        '
        'colAddress
        '
        Me.colAddress.Text = "Address"
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        Me.colStatus.Width = 300
        '
        'sbStatus
        '
        Me.sbStatus.AutoSize = True
        Me.sbStatus.Location = New System.Drawing.Point(8, 438)
        Me.sbStatus.Name = "sbStatus"
        Me.sbStatus.Size = New System.Drawing.Size(42, 15)
        Me.sbStatus.TabIndex = 20
        Me.sbStatus.Text = "Ready"
        '
        'btnAddTo
        '
        Me.btnAddTo.Location = New System.Drawing.Point(8, 56)
        Me.btnAddTo.Name = "btnAddTo"
        Me.btnAddTo.Size = New System.Drawing.Size(48, 23)
        Me.btnAddTo.TabIndex = 21
        Me.btnAddTo.Text = "Add"
        '
        'btnClearTo
        '
        Me.btnClearTo.Location = New System.Drawing.Point(8, 88)
        Me.btnClearTo.Name = "btnClearTo"
        Me.btnClearTo.Size = New System.Drawing.Size(48, 23)
        Me.btnClearTo.TabIndex = 22
        Me.btnClearTo.Text = "Clear"
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(581, 434)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 23)
        Me.btnCancel.TabIndex = 23
        Me.btnCancel.Text = "Cancel"
        '
        'btnSimple
        '
        Me.btnSimple.Location = New System.Drawing.Point(471, 434)
        Me.btnSimple.Name = "btnSimple"
        Me.btnSimple.Size = New System.Drawing.Size(104, 23)
        Me.btnSimple.TabIndex = 24
        Me.btnSimple.TabStop = False
        Me.btnSimple.Text = "Simple Send"
        '
        'status
        '
        Me.status.AutoSize = True
        Me.status.Location = New System.Drawing.Point(8, 336)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(0, 15)
        Me.status.TabIndex = 25
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(684, 462)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.btnSimple)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnClearTo)
        Me.Controls.Add(Me.btnAddTo)
        Me.Controls.Add(Me.sbStatus)
        Me.Controls.Add(Me.lstTo)
        Me.Controls.Add(Me.textThreads)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.chkTestRecipients)
        Me.Controls.Add(Me.textBody)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lstCharset)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.textAttachments)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.textSubject)
        Me.Controls.Add(Me.textFrom)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
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

#Region "Initilaize the Encoding List"
    Private Sub _InitCharset()
        Dim nIndex As Integer = 0
        Dim defaultEncoding As String = "utf-8"

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

    Private Sub _WaitAllInstances(ByRef arSmtp() As SmtpClient, _
       ByRef arResult() As SmtpClientAsyncResult)
        Dim bcontinue As Boolean = False
        Do
            bcontinue = False
            Dim count As Integer = arSmtp.Length
            Dim i As Integer = 0
            For i = 0 To count - 1
                Application.DoEvents()
                Dim oSmtp As SmtpClient = arSmtp(i)
                If Not (oSmtp Is Nothing) Then
                    ' not all object finished.
                    bcontinue = True
                    Dim oResult As SmtpClientAsyncResult = arResult(i)
                    If oResult.AsyncWaitHandle.WaitOne(0, False) Then
                        'this message was finished, get the result by 
                        '_UpdateResult
                        _UpdateResult(oSmtp, oResult)
                        'Set the object instanct to null.
                        arSmtp(i) = Nothing
                        arResult(i) = Nothing
                    End If
                End If
            Next
        Loop While (bcontinue)
    End Sub

    Private Function _WaitInstances(ByRef arSmtp() As SmtpClient, _
       ByRef arResult() As SmtpClientAsyncResult) As Boolean
        Dim b As Boolean = False
        Dim count As Integer = arSmtp.Length
        Dim i As Integer = 0
        For i = 0 To count - 1
            Application.DoEvents()
            Dim oSmtp As SmtpClient = arSmtp(i)
            If Not (oSmtp Is Nothing) Then
                Dim oResult As SmtpClientAsyncResult = arResult(i)
                If oResult.AsyncWaitHandle.WaitOne(0, False) Then
                    'this message was finished, get the result by 
                    '_UpdateResult
                    b = True
                    'Set the object instanct to null.
                    _UpdateResult(oSmtp, oResult)
                    arSmtp(i) = Nothing
                    arResult(i) = Nothing
                    Exit For
                End If
            End If
        Next
        _WaitInstances = b
    End Function

    Private Sub _UpdateResult(ByRef oSmtp As SmtpClient, _
       ByRef oResult As SmtpClientAsyncResult)
        'Get the item index from Tag property
        Dim index As Integer = oSmtp.Tag
        Try
            If (Not chkTestRecipients.Checked) Then
                oSmtp.EndSendMail(oResult)
                _CrossThreadSetItemText("Completed", index)
            Else
                oSmtp.EndTestRecipients(oResult)
                _CrossThreadSetItemText("PASS", index)
            End If
            m_nsuccess = m_nsuccess + 1
        Catch exp As SmtpTerminatedException
            Dim errStr As String = exp.Message
            _CrossThreadSetItemText(errStr, index)
            m_nfailure = m_nfailure + 1
        Catch exp As SmtpServerException
            Dim errStr As String = String.Format("Exception: Server Respond: {0}", exp.ErrorMessage)
            _CrossThreadSetItemText(Errstr, index)
            m_nfailure = m_nfailure + 1
        Catch exp As System.Net.Sockets.SocketException
            Dim errStr As String = String.Format("Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message)
            _CrossThreadSetItemText(errStr, index)
            m_nfailure = m_nfailure + 1
        Catch exp As System.ComponentModel.Win32Exception
            Dim errStr As String = String.Format("Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message)
            _CrossThreadSetItemText(errStr, index)
            m_nfailure = m_nfailure + 1
        Catch exp As System.Exception
            Dim errStr As String = String.Format("Exception: Common: {0}", exp.Message)
            _CrossThreadSetItemText(errStr, index)
            m_nfailure = m_nfailure + 1
        End Try
        m_nsent = m_nsent + 1
        status.Text = String.Format("Total {0}, Finished {1}, Succeeded {2}, Failed {3}", _
          m_ntotal, _
          m_nsent, _
          m_nsuccess, _
          m_nfailure)
    End Sub

    Private Sub _AddInstances(ByRef arSmtp() As SmtpClient, _
       ByRef arResult() As SmtpClientAsyncResult, _
    ByVal index As Integer)
        Dim i As Integer = 0
        Dim count As Integer = arSmtp.Length
        For i = 0 To count - 1
            Dim oSmtp As SmtpClient = arSmtp(i)
            If oSmtp Is Nothing Then
                'idle instance found.
                oSmtp = New SmtpClient
                'store current list item index to object instance
                'and we can retrieve it in EASendMail events.
                oSmtp.Tag = index

                'For evaluation usage, please use "TryIt" as the license code, otherwise the 
                '"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
                '"trial version expired" exception will be thrown.

                'For licensed uasage, please use your license code instead of "TryIt", then the object
                'will never expire
                Dim oMail As SmtpMail = New SmtpMail("TryIt")

                'If you want to specify a reply address
                'oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" )

                'From is a MailAddress object, in c#, it supports implicit converting from string.
                'The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

                'The example code without implicit converting
                ' oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
                ' oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
                ' oMail.From = new MailAddress( "test@adminsystem.com" )
                oMail.From = New MailAddress(textFrom.Text)

                Dim name As String = ""
                Dim address As String = ""
                Dim item As ListViewItem = lstTo.Items(index)
                name = item.Text
                address = item.SubItems(1).Text

                oMail.To.Add(New MailAddress(name, address))

                oMail.Subject = textSubject.Text
                oMail.Charset = m_arCharset(lstCharset.SelectedIndex, 1)

                'replace keywords in body text.
                Dim body As String = textBody.Text
                body = body.Replace("[$subject]", oMail.Subject)
                body = body.Replace("[$from]", oMail.From.ToString())
                body = body.Replace("[$name]", name)
                body = body.Replace("[$address]", address)

                oMail.TextBody = body

                Dim y As Integer = m_arAttachment.Count
                Dim x As Integer = 0
                For x = 0 To y - 1
                    'add attachment
                    oMail.AddAttachment(m_arAttachment(x))
                Next

                Dim oServer As SmtpServer = New SmtpServer(textServer.Text)
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

                _CrossThreadSetItemText("Connecting ...", index)

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

                Dim oResult As SmtpClientAsyncResult = Nothing
                If (Not chkTestRecipients.Checked) Then

                    oResult = oSmtp.BeginSendMail( _
                     oServer, oMail, Nothing, Nothing)
                Else

                    'Just test the email address without sending email data.
                    oResult = oSmtp.BeginTestRecipients( _
                     Nothing, oMail, Nothing, Nothing)
                End If
                'Add the object instance to the array.
                arSmtp(i) = oSmtp
                arResult(i) = oResult
                Exit For
            End If
        Next
    End Sub

    Private Sub _Init()
        Dim s As System.Text.StringBuilder = New System.Text.StringBuilder
        s.Append("Hi [$name], " & vbCrLf & "This sample demonstrates how to send email to mutilple recipients." & vbCrLf & vbCrLf)
        s.Append("From: [$from]" & vbCrLf)
        s.Append("To: <[$address]>" & vbCrLf)
        s.Append("Subject: [$subject]" & vbCrLf & vbCrLf)

        s.Append("If no sever address was specified, the email will be delivered to the recipient's server directly, ")
        s.Append("However, if you don't have a static IP address, ")
        s.Append("many anti-spam filters will mark it as a junk-email." & vbCrLf & vbCrLf)

        s.Append("If ""Test Email Address"" was checked, then only the recipient address will be tested and no message will be sent." & vbCrLf)

        textBody.Text = s.ToString()
        _InitCharset()
        _ChangeAuthStatus()
    End Sub

    Private Sub _ChangeAuthStatus()
        textUser.Enabled = chkAuth.Checked
        textPassword.Enabled = chkAuth.Checked
    End Sub

    Private Sub btnAddTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTo.Click
        Dim dlg As Form2 = New Form2
        If dlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            Dim name As String = dlg.textName.Text
            If name.Length = 0 Then
                name = ""
            End If

            'For i As Integer = 0 To 50
            Dim item As ListViewItem = lstTo.Items.Add(name)
            item.SubItems.Add(dlg.textAddress.Text)
            item.SubItems.Add("Ready")
            'Next
        End If
        dlg.Dispose()
    End Sub

    Private Sub btnClearTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTo.Click
        lstTo.Items.Clear()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        m_bcancel = True
        btnCancel.Enabled = False
    End Sub

    Private Sub chkTestRecipients_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTestRecipients.CheckedChanged
        textServer.Enabled = (Not chkTestRecipients.Checked)
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
            textAttachments.Text += ""
        Next
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        m_arAttachment.Clear()
        textAttachments.Text = ""
    End Sub

    Private Sub chkAuth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuth.CheckedChanged
        _ChangeAuthStatus()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        If textFrom.Text.Length = 0 Then
            MessageBox.Show("Please input From, the format can be test@domain.com or Tester<test@domain.com>")
            Exit Sub
        End If

        Dim to_count As Integer = lstTo.Items.Count
        If to_count = 0 Then
            MessageBox.Show("please add a recipient at least!")
            Exit Sub
        End If

        btnSend.Enabled = False
        btnSimple.Enabled = False
        btnAdd.Enabled = False
        btnClear.Enabled = False
        btnAddTo.Enabled = False
        btnClearTo.Enabled = False
        chkTestRecipients.Enabled = False

        btnCancel.Enabled = True

        m_bcancel = False
        Dim maxInstances As Integer = 10
        Try
            maxInstances = Int32.Parse(textThreads.Text)
        Catch exp As Exception
        End Try

        If maxInstances < 1 Then
            maxInstances = 1
        End If

        Dim curInstances As Integer = 0
        Dim arSmtp(maxInstances - 1) As SmtpClient
        Dim arResult(maxInstances - 1) As SmtpClientAsyncResult

        Dim i As Integer = 0
        For i = 0 To maxInstances - 1
            arSmtp(i) = Nothing
        Next

        Dim sent As Integer = 0
        For sent = 0 To to_count - 1
            lstTo.Items(sent).SubItems(2).Text = "Ready"
        Next

        m_ntotal = to_count
        m_nsent = 0
        m_nsuccess = 0
        m_nfailure = 0
        status.Text = String.Format("Total {0}, Finished {1}, Succeeded {2}, Failed {3}", _
    m_ntotal, m_nsent, m_nsuccess, m_nfailure)

        sent = 0
        Do While (sent < to_count And (Not m_bcancel))
            If (curInstances >= maxInstances) Then
                If _WaitInstances(arSmtp, arResult) Then
                    curInstances = curInstances - 1
                Else
                    Application.DoEvents()
                    'System.Threading.Thread.Sleep( 10 )
                End If
            Else
                curInstances = curInstances + 1
                _AddInstances(arSmtp, arResult, sent)
                sent = sent + 1
                Application.DoEvents()
            End If
        Loop

        'Wait all message sent.
        _WaitAllInstances(arSmtp, arResult)

        If (m_bcancel) Then
            Dim t As Integer = sent
            For sent = t To to_count - 1
                lstTo.Items(sent).SubItems(2).Text = "Operation was cancelled"
            Next
        End If

        btnSend.Enabled = True
        btnSimple.Enabled = True
        btnAdd.Enabled = True
        btnClear.Enabled = True
        btnAddTo.Enabled = True
        btnClearTo.Enabled = True
        chkTestRecipients.Enabled = True

        btnCancel.Enabled = False
    End Sub

#Region "Send Mass E-mail with Single Thread"
    Private Sub btnSimple_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimple.Click
        If textFrom.Text.Length = 0 Then
            MessageBox.Show("Please input From, the format can be test@domain.com or Tester<test@domain.com>")
            Exit Sub
        End If

        Dim to_count As Integer = lstTo.Items.Count
        If to_count = 0 Then
            MessageBox.Show("please add a recipient at least!")
            Exit Sub
        End If

        MessageBox.Show( _
         "Simple Send will send email with single thread, the code is vey simple." & vbCrLf & "If you don't want the extreme performance, the code is recommended to beginer!")

        btnSend.Enabled = False
        btnSimple.Enabled = False
        btnAdd.Enabled = False
        btnClear.Enabled = False
        btnAddTo.Enabled = False
        btnClearTo.Enabled = False
        chkTestRecipients.Enabled = False

        btnCancel.Enabled = True
        m_bcancel = False


        Dim sent As Integer = 0
        For sent = 0 To to_count - 1
            lstTo.Items(sent).SubItems(2).Text = "Ready"
        Next

        m_ntotal = to_count
        m_nsent = 0
        m_nsuccess = 0
        m_nfailure = 0
        status.Text = String.Format("Total {0}, Finished {1}, Succeeded {2}, Failed {3}", _
    m_ntotal, m_nsent, m_nsuccess, m_nfailure)

        sent = 0
        Do While (sent < to_count And (Not m_bcancel))
            Application.DoEvents()
            Dim index As Integer = sent
            sent = sent + 1
            'For evaluation usage, please use "TryIt" as the license code, otherwise the 
            '"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            '"trial version expired" exception will be thrown.

            'For licensed uasage, please use your license code instead of "TryIt", then the object
            'will never expire
            Dim oMail As SmtpMail = New SmtpMail("TryIt")
            Dim oSmtp As SmtpClient = New SmtpClient
            oSmtp.Tag = index
            'To generate a log file for SMTP transaction, please use
            'oSmtp.LogFileName = "c:\\smtp.log"
            Try
                oMail.Reset()
                'If you want to specify a reply address
                'oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" )

                'From is a MailAddress object, in c#, it supports implicit converting from string.
                'The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

                'The example code without implicit converting
                ' oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
                ' oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
                ' oMail.From = new MailAddress( "test@adminsystem.com" )
                oMail.From = New MailAddress(textFrom.Text)

                'To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
                ' multiple address are separated with (,)
                'The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

                'The example code without implicit converting
                ' oMail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" )
                ' oMail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>")

                Dim name As String = ""
                Dim address As String = ""
                Dim item As ListViewItem = lstTo.Items(index)
                name = item.Text
                address = item.SubItems(1).Text

                oMail.To.Add(New MailAddress(name, address))

                oMail.Subject = textSubject.Text
                oMail.Charset = m_arCharset(lstCharset.SelectedIndex, 1)

                'replace keywords in body text.
                Dim body As String = textBody.Text
                body = body.Replace("[$subject]", oMail.Subject)
                body = body.Replace("[$from]", oMail.From.ToString())
                body = body.Replace("[$name]", name)
                body = body.Replace("[$address]", address)

                oMail.TextBody = body

                Dim y As Integer = m_arAttachment.Count
                For x As Integer = 0 To y - 1
                    'add attachment
                    oMail.AddAttachment(m_arAttachment(x))
                Next

                Dim oServer As SmtpServer = New SmtpServer(textServer.Text)
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

                _CrossThreadSetItemText("Connecting ...", index)

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

                If Not chkTestRecipients.Checked Then
                    oSmtp.SendMail(oServer, oMail)
                    _CrossThreadSetItemText("Completed", index)
                Else
                    oSmtp.TestRecipients(Nothing, oMail)
                    _CrossThreadSetItemText("PASS", index)
                End If

                m_nsuccess = m_nsuccess + 1
                'If you want to reuse the mail object, please reset the Date and Message-ID, otherwise
                'the Date and Message-ID will not change.
                'oMail.Date = System.DateTime.Now
                'oMail.ResetMessageID()
                'oMail.To = "another@example.com"
                'oSmtp.SendMail( oServer, oMail )

            Catch exp As SmtpTerminatedException
                Dim errStr As String = exp.Message
                _CrossThreadSetItemText(errStr, index)
                m_nfailure = m_nfailure + 1
            Catch exp As SmtpServerException
                Dim errStr As String = String.Format("Exception: Server Respond: {0}", exp.ErrorMessage)
                _CrossThreadSetItemText(Errstr, index)
                m_nfailure = m_nfailure + 1
            Catch exp As System.Net.Sockets.SocketException
                Dim errStr As String = String.Format("Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message)
                _CrossThreadSetItemText(errStr, index)
                m_nfailure = m_nfailure + 1
            Catch exp As System.ComponentModel.Win32Exception
                Dim errStr As String = String.Format("Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message)
                _CrossThreadSetItemText(errStr, index)
                m_nfailure = m_nfailure + 1
            Catch exp As System.Exception
                Dim errStr As String = String.Format("Exception: Common: {0}", exp.Message)
                _CrossThreadSetItemText(errStr, index)
                m_nfailure = m_nfailure + 1
            End Try

            m_nsent = m_nsent + 1
            status.Text = String.Format("Total {0}, Finished {1}, Succeeded {2}, Failed {3}", _
     m_ntotal, _
     m_nsent, _
     m_nsuccess, _
     m_nfailure)
        Loop

        If (m_bcancel) Then
            Dim t As Integer = sent
            For sent = t To to_count - 1
                lstTo.Items(sent).SubItems(2).Text = "Operation was cancelled"
            Next
        End If

        btnSend.Enabled = True
        btnSimple.Enabled = True
        btnAdd.Enabled = True
        btnClear.Enabled = True
        btnAddTo.Enabled = True
        btnClearTo.Enabled = True
        chkTestRecipients.Enabled = True

        btnCancel.Enabled = False
    End Sub
#End Region

    Private Sub Form1_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize
        If (Me.Width < 700) Then
            Me.Width = 700
        End If

        If (Me.Height < 500) Then
            Me.Height = 500
        End If


        textBody.Width = Me.Width - 35
        textBody.Height = Me.Height - 430

        btnSend.Top = textBody.Height + textBody.Top + 5
        sbStatus.Top = btnSend.Top
        btnCancel.Top = btnSend.Top
        btnSimple.Top = btnSend.Top
        btnSend.Left = Me.Width - 350
        btnSimple.Left = btnSend.Left + 110
        btnCancel.Left = btnSimple.Left + 110

        groupBox1.Left = Me.Width - 290

        textFrom.Width = Me.Width - 380
        textSubject.Width = textFrom.Width
        lstTo.Width = textFrom.Width

        textAttachments.Width = Me.Width - 260
        btnAdd.Left = textAttachments.Width + 110
        btnClear.Left = textAttachments.Width + 160
    End Sub
End Class
