VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   6690
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11085
   LinkTopic       =   "Form1"
   ScaleHeight     =   6690
   ScaleWidth      =   11085
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox textSubject 
      Height          =   285
      Left            =   1080
      TabIndex        =   5
      Text            =   "Test Sample"
      Top             =   3000
      Width           =   5295
   End
   Begin VB.CommandButton btnClearTo 
      Caption         =   "Clear"
      Height          =   375
      Left            =   120
      TabIndex        =   4
      Top             =   1680
      Width           =   735
   End
   Begin VB.CommandButton btnAddTo 
      Caption         =   "Add"
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   1200
      Width           =   735
   End
   Begin MSComctlLib.StatusBar sBar 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   29
      Top             =   6315
      Width           =   11085
      _ExtentX        =   19553
      _ExtentY        =   661
      Style           =   1
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   1
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.ListView lstTo 
      Height          =   2055
      Left            =   1080
      TabIndex        =   2
      Top             =   840
      Width           =   5295
      _ExtentX        =   9340
      _ExtentY        =   3625
      View            =   3
      LabelWrap       =   -1  'True
      HideSelection   =   -1  'True
      FullRowSelect   =   -1  'True
      GridLines       =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   1
      NumItems        =   4
      BeginProperty ColumnHeader(1) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Text            =   "Name"
         Object.Width           =   1834
      EndProperty
      BeginProperty ColumnHeader(2) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   1
         Text            =   "Address"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(3) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   2
         Text            =   "Status"
         Object.Width           =   6597
      EndProperty
      BeginProperty ColumnHeader(4) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   3
         Object.Width           =   2540
      EndProperty
   End
   Begin VB.TextBox txtThreads 
      Height          =   285
      Left            =   8040
      TabIndex        =   13
      Text            =   "10"
      Top             =   3465
      Width           =   615
   End
   Begin VB.CheckBox chkTest 
      Caption         =   "Test Email Address"
      Height          =   255
      Left            =   4440
      TabIndex        =   12
      Top             =   3480
      Width           =   1935
   End
   Begin MSComDlg.CommonDialog dlgFile 
      Left            =   2400
      Top             =   5280
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton btnSend 
      Caption         =   "Send"
      Height          =   375
      Left            =   9360
      TabIndex        =   18
      Top             =   5880
      Width           =   1575
   End
   Begin VB.TextBox textBody 
      Height          =   1455
      Left            =   120
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   17
      Top             =   4320
      Width           =   10815
   End
   Begin VB.CommandButton btnClear 
      Caption         =   "Clear"
      Height          =   375
      Left            =   9000
      TabIndex        =   16
      Top             =   3870
      Width           =   615
   End
   Begin VB.CommandButton btnAdd 
      Caption         =   "Add"
      Height          =   375
      Left            =   8280
      TabIndex        =   15
      Top             =   3870
      Width           =   615
   End
   Begin VB.TextBox textAttachments 
      BackColor       =   &H80000018&
      Enabled         =   0   'False
      Height          =   285
      Left            =   1080
      TabIndex        =   14
      Top             =   3915
      Width           =   6975
   End
   Begin VB.ComboBox lstCharset 
      Height          =   315
      Left            =   1080
      Style           =   2  'Dropdown List
      TabIndex        =   11
      Top             =   3450
      Width           =   3135
   End
   Begin VB.TextBox textFrom 
      Height          =   285
      Left            =   1080
      TabIndex        =   1
      Top             =   240
      Width           =   5295
   End
   Begin VB.Frame Frame1 
      Height          =   2775
      Left            =   6600
      TabIndex        =   20
      Top             =   120
      Width           =   4335
      Begin VB.ComboBox lstProtocol 
         Height          =   315
         Left            =   120
         Style           =   2  'Dropdown List
         TabIndex        =   31
         Top             =   2160
         Width           =   3975
      End
      Begin VB.CheckBox chkSSL 
         Caption         =   "My server requires secure connection (SSL)"
         Height          =   255
         Left            =   120
         TabIndex        =   10
         Top             =   1680
         Width           =   3855
      End
      Begin VB.TextBox textPassword 
         BackColor       =   &H8000000B&
         Enabled         =   0   'False
         Height          =   285
         IMEMode         =   3  'DISABLE
         Left            =   1080
         PasswordChar    =   "*"
         TabIndex        =   9
         Top             =   1320
         Width           =   3015
      End
      Begin VB.TextBox textUser 
         BackColor       =   &H8000000B&
         Enabled         =   0   'False
         Height          =   285
         Left            =   1080
         TabIndex        =   8
         Top             =   960
         Width           =   3015
      End
      Begin VB.CheckBox chkAuth 
         Caption         =   "My server requires user authentication"
         Height          =   255
         Left            =   120
         TabIndex        =   7
         Top             =   600
         Width           =   3855
      End
      Begin VB.TextBox textServer 
         Height          =   285
         Left            =   1080
         TabIndex        =   6
         Top             =   240
         Width           =   3015
      End
      Begin VB.Label Label8 
         AutoSize        =   -1  'True
         Caption         =   "Password"
         Height          =   195
         Left            =   120
         TabIndex        =   23
         Top             =   1320
         Width           =   690
      End
      Begin VB.Label Label7 
         AutoSize        =   -1  'True
         Caption         =   "User"
         Height          =   195
         Left            =   120
         TabIndex        =   22
         Top             =   975
         Width           =   330
      End
      Begin VB.Label Label6 
         AutoSize        =   -1  'True
         Caption         =   "Server"
         Height          =   195
         Left            =   120
         TabIndex        =   21
         Top             =   285
         Width           =   465
      End
   End
   Begin VB.Label Label4 
      AutoSize        =   -1  'True
      Caption         =   "Subject"
      Height          =   195
      Left            =   120
      TabIndex        =   30
      Top             =   3045
      Width           =   540
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      Caption         =   "To"
      Height          =   195
      Left            =   120
      TabIndex        =   28
      Top             =   840
      Width           =   195
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "Maximum Threads"
      Height          =   195
      Left            =   6600
      TabIndex        =   27
      Top             =   3510
      Width           =   1290
   End
   Begin VB.Label textStatus 
      AutoSize        =   -1  'True
      Height          =   195
      Left            =   120
      TabIndex        =   26
      Top             =   4800
      Width           =   45
   End
   Begin VB.Label Label10 
      AutoSize        =   -1  'True
      Caption         =   "Please separate multiple email addresses with comma(,)"
      Height          =   195
      Left            =   1080
      TabIndex        =   25
      Top             =   600
      Width           =   3900
   End
   Begin VB.Label Label9 
      AutoSize        =   -1  'True
      Caption         =   "Attachments"
      Height          =   195
      Left            =   120
      TabIndex        =   24
      Top             =   3960
      Width           =   885
   End
   Begin VB.Label Label5 
      AutoSize        =   -1  'True
      Caption         =   "Encoding"
      Height          =   195
      Left            =   120
      TabIndex        =   19
      Top             =   3510
      Width           =   675
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "From"
      Height          =   195
      Left            =   120
      TabIndex        =   0
      Top             =   285
      Width           =   345
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2010 - 2012 ADMINSYSTEM SOFTWARE LIMITED                        |
' |
' |    Project: It demonstrates how to use EASendMailObj to mass email with multiple thread
' |
' |    Author: Ivan Lui ( ivan@emailarchitect.net )
'  ===============================================================================
Option Explicit
Private m_arCharset(27, 1) As String
Private m_arAttachment() As String

Private WithEvents m_oFastSender As EASendMailObjLib.FastSender
Attribute m_oFastSender.VB_VarHelpID = -1

Const CRYPT_MACHINE_KEYSET = 32
Const CRYPT_USER_KEYSET = 4096
Const CERT_SYSTEM_STORE_CURRENT_USER = 65536
Const CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072

Const Max_QueuedCount = 100
Const BodyFormat = 0 'text/plain
Private g_lSent As Integer
Private g_lSend As Integer
Private g_failure As Integer
Private g_bSending As Boolean
Private g_bStopped As Boolean
Private g_lTotal As Integer

'========================================================
' fnParseAddr
'========================================================
Function fnParseAddr(src, ByRef name, ByRef addr)
    Dim nIndex
    nIndex = InStr(1, src, "<")
    If nIndex > 0 Then
        name = Mid(src, 1, nIndex - 1)
        addr = Mid(src, nIndex)
    Else
        name = ""
        addr = src
    End If
    
    Call fnTrim(name, " ,;<>""'")
    Call fnTrim(addr, " ,;<>""'")
End Function
'========================================================
' fnTrim
'========================================================
Function fnTrim(ByRef src, trimer)
    Dim i, nCount, ch
    nCount = Len(src)
    For i = 1 To nCount
        ch = Mid(src, i, 1)
        If InStr(1, trimer, ch) < 1 Then
            Exit For
        End If
    Next
    
    src = Mid(src, i)
    nCount = Len(src)
    For i = nCount To 1 Step -1
        ch = Mid(src, i, 1)
        If InStr(1, trimer, ch) < 1 Then
            Exit For
        End If
    Next
    src = Mid(src, 1, i)
End Function

'========================================================
' strpbrk
'========================================================
Function strpbrk(src, start, sCharset)
    strpbrk = 0
    
    Dim i, size, pos, ch
    
    size = Len(src)
    For i = start To size
        ch = Mid(src, i, 1)
        
        If InStr(1, sCharset, ch) >= 1 Then
            strpbrk = i
            Exit Function
        End If
    Next

End Function

'========================================================
' SplitEx
'========================================================
Function SplitEx(src, sCharset)

    Dim find, start, pos, bquoted, ch, item, s
    Dim arItems()
    Dim nItems, nBuf, nIndex
    
    nIndex = 0
    nBuf = 10
    
    ReDim Preserve arItems(nBuf)
    
    find = sCharset & """"
    start = 1
    pos = 1
    bquoted = False
        
    Do While True
        pos = strpbrk(src, pos, find)
        
        If pos <= 0 Then
            s = Mid(src, start)
            fnTrim s, sCharset
            If s <> "" Then
                If nIndex >= nBuf - 1 Then
                    nBuf = nBuf + 10
                    ReDim Preserve arItems(nBuf)
                End If
                arItems(nIndex) = s
            End If
            Exit Do
        End If
        
        ch = Mid(src, pos, 1)

        If ch = """" Then
            If bquoted Then
                bquoted = False
            Else
                bquoted = True
            End If
        End If
        
        If Not bquoted And ch <> """" Then
            s = Mid(src, start, pos - start)
            fnTrim s, sCharset
            If s <> "" Then
                If nIndex >= nBuf - 1 Then
                    nBuf = nBuf + 10
                    ReDim Preserve arItems(nBuf)
                End If
                arItems(nIndex) = s
                nIndex = nIndex + 1
            End If
            pos = pos + 1
            start = pos
        Else
            pos = pos + 1
        End If
    Loop
    
    ReDim Preserve arItems(nIndex)
    SplitEx = arItems
End Function

Private Sub InitProtocol()
    lstProtocol.AddItem "SMTP Protocol - Recommended"
    lstProtocol.AddItem "Exchange Web Service - 2007/2010"
    lstProtocol.AddItem "Exchange Web Dav - 2000/2003"
    
    lstProtocol.ListIndex = 0
End Sub

Private Sub InitCharset()
    Dim nIndex As Integer
    nIndex = 0
    
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
    
    Dim oSmtp As New EASendMailObjLib.Mail
    'The license code for EASendMail ActiveX Object,
    'for evaluation usage, please use "TryIt" as the license code.
    oSmtp.LicenseCode = "TryIt"
    
    Dim DefaultCharset As String
    DefaultCharset = oSmtp.Charset 'query system default charset
    
    Dim i As Integer
    Dim ListIndex As Integer
    ListIndex = 26
    For i = 0 To nIndex - 1
        lstCharset.AddItem m_arCharset(i, 0), i
        If StrComp(m_arCharset(i, 1), DefaultCharset, vbTextCompare) = 0 Then
            ListIndex = i
        End If
    Next
    lstCharset.ListIndex = ListIndex
End Sub

Private Sub btnAdd_Click()
    dlgFile.Filter = "All files(*.*) | *.*"
    dlgFile.ShowOpen
    If dlgFile.fileName <> vbNullString And dlgFile.fileName <> "" Then
        Dim Count As Integer
        Count = UBound(m_arAttachment)
        m_arAttachment(Count) = dlgFile.fileName
        ReDim Preserve m_arAttachment(Count + 1)
        
        Dim pos As Integer
        Dim fileName As String
        fileName = dlgFile.fileName
        pos = InStrRev(fileName, "\")
        If pos > 0 Then
            fileName = Mid(fileName, pos + 1)
        End If
        
        textAttachments.Text = textAttachments.Text & fileName & "; "
    End If
End Sub

Private Sub btnAddTo_Click()
    Dialog.Show 1, Me
    If Dialog.m_bOK Then
    Dim i As Integer
    'For i = 1 To 100
        Dim item As ListItem
        Set item = lstTo.ListItems.Add()
        item.Text = Dialog.txtName
        item.ListSubItems.Add , , Dialog.txtAddress
        item.ListSubItems.Add , , "Ready"
    'Next
    End If
End Sub

Private Sub btnClear_Click()
    ReDim m_arAttachment(0)
    textAttachments.Text = ""
End Sub


Private Sub btnClearTo_Click()
    lstTo.ListItems.Clear
End Sub

'=============================================================================
' btnSend_Click
'=============================================================================
Private Sub btnSend_Click()
    If btnSend.Caption = "Cancel" Then
        m_oFastSender.ClearQueuedMails
        btnSend.Enabled = False
        g_bSending = False
        Exit Sub
    End If

    btnSend.Caption = "Cancel"
    btnAddTo.Enabled = False
    btnClearTo.Enabled = False
    btnAdd.Enabled = False
    btnClear.Enabled = False
    Call Sending
End Sub
'=============================================================================
' SubmitTask
'=============================================================================
Private Sub SubmitTask(ByVal index As Integer)

    Dim From, To_Name, To_Addr, ServerAddr As String
    From = Trim(textFrom.Text)
    ServerAddr = Trim(textServer.Text)
    
    Dim item As ListItem
    Set item = lstTo.ListItems(index)
    To_Name = item.Text
    To_Addr = item.SubItems(1)
    
    'Declare and create easendmail mail object instance
    Dim oSmtp As EASendMailObjLib.Mail
    Set oSmtp = New EASendMailObjLib.Mail
    'The license code for EASendMail ActiveX Object,
    'for evaluation usage, please use "TryIt" as the license code.
    oSmtp.LicenseCode = "TryIt"
    'oSmtp.LogFileName = App.Path & "\smtp.txt" 'enable smtp log
    oSmtp.ServerAddr = ServerAddr
    oSmtp.Protocol = lstProtocol.ListIndex
    If ServerAddr <> "" Then
        If chkAuth.Value = Checked Then
            oSmtp.UserName = Trim(textUser.Text)
            oSmtp.Password = Trim(textPassword.Text)
        End If
        
        If chkSSL.Value = Checked Then
            oSmtp.SSL_init
            'If SSL port is 465 or other port rather than 25 or 587 port, please use
            'oSmtp.ServerPort = 465
            'oSmtp.SSL_starttls = 0
        End If
    End If
    
    oSmtp.Charset = m_arCharset(lstCharset.ListIndex, 1)
    Dim name, addr As String
    fnParseAddr From, name, addr
    
    'Using this email to be replied to another address
    'oSmtp.ReplyTo = ReplyAddress
    oSmtp.From = name
    oSmtp.FromAddr = addr
    
    oSmtp.AddRecipient To_Name, To_Addr, 0 ' 0, Normal recipient, 1, cc, 2, bcc
    Dim i, Count As Integer
    Count = UBound(m_arAttachment)
    For i = 0 To Count - 1
        oSmtp.AddAttachment m_arAttachment(i)
    Next
    
    Dim Subject, Bodytext
    Subject = textSubject.Text
    Bodytext = textBody.Text
    
    Bodytext = Replace(Bodytext, "[$from]", From)
    Bodytext = Replace(Bodytext, "[$to]", To_Addr)
    Bodytext = Replace(Bodytext, "[$subject]", Subject)
    
    oSmtp.Subject = Subject
    oSmtp.Bodytext = Bodytext
    
    'oSmtp.BodyFormat = 1    ' Using HTML FORMAT to send mail
    If chkTest.Value = Checked Then
    'only test if the server accept the recipient address, but do not send the email to the mailbox.
        Call m_oFastSender.Test(oSmtp, _
                            index, _
                            "any value")
                            
    Else
        Call m_oFastSender.Send(oSmtp, _
                            index, _
                            "any value")
    End If
End Sub
'=============================================================================
' Sending
'=============================================================================
Private Sub Sending()
    Dim i, nCount As Integer
    
    nCount = lstTo.ListItems.Count
    m_oFastSender.MaxThreads = CLng(txtThreads.Text)
    
    g_lSend = 0
    g_lSent = 0
    g_failure = 0
    g_bSending = True
    g_bStopped = False
    
    Do While (g_bSending) And (g_lSend < nCount)
        If m_oFastSender.GetQueuedCount() < Max_QueuedCount Then
            g_lSend = g_lSend + 1
            Call ChangeStatus(g_lSend, "Queued ...")
            Call SubmitTask(g_lSend)
        End If
        DoEvents
    Loop
    
    Call WaitAllTaskFinished
    btnAdd.Enabled = True
    btnClear.Enabled = True
    btnAddTo.Enabled = True
    btnClearTo.Enabled = True
    
    btnSend.Enabled = True
    btnSend.Caption = "Send"
    g_bStopped = True
End Sub
'=============================================================================
' resetstatus
'=============================================================================
Private Sub ResetStatus()
    Dim i As Integer
    Dim nCount As Integer
    
    nCount = lstTo.ListItems.Count
    For i = 1 To nCount
        ChangeStatus i, "ready"
    Next
    DoEvents
End Sub

Private Sub chkAuth_Click()
    If chkAuth.Value = Checked Then
        textUser.Enabled = True
        textPassword.Enabled = True
        textUser.BackColor = &H80000005
        textPassword.BackColor = &H80000005
    Else
        textUser.Enabled = False
        textPassword.Enabled = False
        textUser.BackColor = &H8000000B
        textPassword.BackColor = &H8000000B
    End If
End Sub

Private Sub Form_Load()
    InitProtocol
    InitCharset
    ReDim m_arAttachment(0)
    Dim s As String
    s = s & "This sample demonstrates how to send email with fastsender + multiple thread." & vbCrLf & vbCrLf
    s = s & "From: [$from]" & vbCrLf
    s = s & "To: [$to]" & vbCrLf
    s = s & "Subject: [$subject]" & vbCrLf & vbCrLf
    s = s & "If no sever address was specified, the email will be delivered to the recipient's server directly,"
    s = s & "However, if you don't have a static IP address, "
    s = s & "many anti-spam filters will mark it as a junk-email." & vbCrLf & vbCrLf
     
    textBody.Text = s
    
    Set m_oFastSender = New EASendMailObjLib.FastSender
    m_oFastSender.MaxThreads = CLng(txtThreads.Text)
    g_lSend = 0
    g_lSent = 0
End Sub

'=============================================================================
' Form_QueryUnload
'=============================================================================
Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    Dim i As Integer
    g_bSending = False
    m_oFastSender.ClearQueuedMails
    Call WaitAllTaskFinished
    m_oFastSender.StopAllThreads
    End
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    If (Me.Width < 10200) Then
        Me.Width = 10200
    End If

    If (Me.Height < 6400) Then
        Me.Height = 6400
    End If
    
    textBody.Width = Me.Width - 450
    textBody.Height = Me.Height - 5900


    btnSend.Top = textBody.Height + textBody.Top + 150
    btnSend.Left = Me.Width - 1900
    
    Frame1.Left = Me.Width - 4600

    textFrom.Width = Me.Width - 5800
    lstTo.Width = textFrom.Width
    textSubject.Width = textFrom.Width

    textAttachments.Width = Me.Width - 2900
    btnAdd.Left = textAttachments.Width + 1200
    btnClear.Left = textAttachments.Width + 1900
End Sub

'=============================================================================
' OnAuthenticated Event
'=============================================================================
Private Sub m_oFastSender_OnAuthenticated(ByVal nKey As Long, ByVal tParam As String)
    Dim desc As String
    desc = "Authorized"
    Dim index As Integer
    index = nKey
    ChangeStatus index, desc
End Sub
'=============================================================================
' OnConnected Event
'=============================================================================
Private Sub m_oFastSender_OnConnected(ByVal nKey As Long, ByVal tParam As String)
    
    Dim desc As String
    desc = "Connected"
    Dim index As Integer
    index = nKey
    ChangeStatus index, desc
  
End Sub
'=============================================================================
' OnSending Event
'=============================================================================
Private Sub m_oFastSender_OnSending(ByVal lSent As Long, ByVal lTotal As Long, ByVal nKey As Long, ByVal tParam As String)
    Dim desc As String
    desc = "sending data " & lSent & "/" & lTotal & "..."
    Dim index As Integer
    index = nKey
    ChangeStatus index, desc
End Sub
'=============================================================================
' OnSent Event
'=============================================================================
Private Sub m_oFastSender_OnSent(ByVal lRet As Long, _
                                ByVal ErrDesc As String, _
                                ByVal nKey As Long, _
                                ByVal tParam As String, _
                                ByVal SenderAddr As String, _
                                ByVal recipients As String)
    Dim statusDesc As String
    Dim nIndex As Integer
    
    If lRet = 0 Then
        If chkTest.Value = Checked Then
            statusDesc = "test is ok"
        Else
            statusDesc = "sent successfully"
        End If
    Else
        statusDesc = ErrDesc
        g_failure = g_failure + 1
    End If
    
    nIndex = nKey
    g_lSent = g_lSent + 1
    Call ChangeStatus(nIndex, statusDesc)
End Sub

'=============================================================================
' WaitAllTaskFinished
'=============================================================================
Private Sub WaitAllTaskFinished()
    Do While m_oFastSender.GetQueuedCount() > 0
        DoEvents
    Loop
    
    Do While Not (m_oFastSender.GetIdleThreads() = m_oFastSender.GetCurrentThreads())
        DoEvents
    Loop
End Sub

'=============================================================================
' ChangeStatus
'=============================================================================
Private Sub ChangeStatus(nIndex As Integer, status As String)
    lstTo.ListItems(nIndex).ListSubItems(2).Text = status
    Dim nsent, nerror As Integer
    nsent = g_lSent - g_failure
    nerror = g_failure
    sBar.SimpleText = "Total " & lstTo.ListItems.Count & " emails, " & nsent & " success, " & nerror & " failed."
End Sub
