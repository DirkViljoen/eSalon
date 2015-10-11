VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   6000
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10215
   LinkTopic       =   "Form1"
   ScaleHeight     =   6000
   ScaleWidth      =   10215
   StartUpPosition =   3  'Windows Default
   Begin VB.CheckBox chkEncrypt 
      Caption         =   "Encryption"
      Height          =   255
      Left            =   2280
      TabIndex        =   11
      Top             =   2100
      Width           =   1575
   End
   Begin VB.CheckBox chkSign 
      Caption         =   "Digital Signature"
      Height          =   375
      Left            =   120
      TabIndex        =   10
      Top             =   2040
      Width           =   1695
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
      Left            =   8400
      TabIndex        =   17
      Top             =   5400
      Width           =   1575
   End
   Begin VB.TextBox textBody 
      Height          =   1695
      Left            =   120
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   16
      Top             =   3480
      Width           =   9855
   End
   Begin VB.CommandButton btnClear 
      Caption         =   "Clear"
      Height          =   375
      Left            =   9000
      TabIndex        =   15
      Top             =   2910
      Width           =   615
   End
   Begin VB.CommandButton btnAdd 
      Caption         =   "Add"
      Height          =   375
      Left            =   8280
      TabIndex        =   14
      Top             =   2910
      Width           =   615
   End
   Begin VB.TextBox textAttachments 
      BackColor       =   &H80000018&
      Enabled         =   0   'False
      Height          =   285
      Left            =   1080
      TabIndex        =   13
      Top             =   2955
      Width           =   6975
   End
   Begin VB.ComboBox lstCharset 
      Height          =   315
      Left            =   1080
      Style           =   2  'Dropdown List
      TabIndex        =   12
      Top             =   2520
      Width           =   3615
   End
   Begin VB.TextBox textSubject 
      Height          =   285
      Left            =   1080
      TabIndex        =   4
      Text            =   "Test sample"
      Top             =   1680
      Width           =   4335
   End
   Begin VB.TextBox textCc 
      Height          =   285
      Left            =   1080
      TabIndex        =   3
      Top             =   1275
      Width           =   4335
   End
   Begin VB.TextBox textTo 
      Height          =   285
      Left            =   1080
      TabIndex        =   2
      Top             =   840
      Width           =   4335
   End
   Begin VB.TextBox textFrom 
      Height          =   285
      Left            =   1080
      TabIndex        =   1
      Top             =   240
      Width           =   4335
   End
   Begin VB.Frame Frame1 
      Height          =   2655
      Left            =   5640
      TabIndex        =   22
      Top             =   120
      Width           =   4335
      Begin VB.ComboBox lstProtocol 
         Height          =   315
         Left            =   120
         Style           =   2  'Dropdown List
         TabIndex        =   29
         Top             =   2160
         Width           =   4095
      End
      Begin VB.CheckBox chkSSL 
         Caption         =   "My server requires secure connection (SSL)"
         Height          =   255
         Left            =   120
         TabIndex        =   9
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
         TabIndex        =   8
         Top             =   1320
         Width           =   3015
      End
      Begin VB.TextBox textUser 
         BackColor       =   &H8000000B&
         Enabled         =   0   'False
         Height          =   285
         Left            =   1080
         TabIndex        =   7
         Top             =   960
         Width           =   3015
      End
      Begin VB.CheckBox chkAuth 
         Caption         =   "My server requires user authentication"
         Height          =   255
         Left            =   120
         TabIndex        =   6
         Top             =   600
         Width           =   3855
      End
      Begin VB.TextBox textServer 
         Height          =   285
         Left            =   1080
         TabIndex        =   5
         Top             =   240
         Width           =   3015
      End
      Begin VB.Label Label8 
         AutoSize        =   -1  'True
         Caption         =   "Password"
         Height          =   195
         Left            =   120
         TabIndex        =   25
         Top             =   1320
         Width           =   690
      End
      Begin VB.Label Label7 
         AutoSize        =   -1  'True
         Caption         =   "User"
         Height          =   195
         Left            =   120
         TabIndex        =   24
         Top             =   975
         Width           =   330
      End
      Begin VB.Label Label6 
         AutoSize        =   -1  'True
         Caption         =   "Server"
         Height          =   195
         Left            =   120
         TabIndex        =   23
         Top             =   285
         Width           =   465
      End
   End
   Begin VB.Label textStatus 
      AutoSize        =   -1  'True
      Height          =   195
      Left            =   120
      TabIndex        =   28
      Top             =   4800
      Width           =   45
   End
   Begin VB.Label Label10 
      AutoSize        =   -1  'True
      Caption         =   "Please separate multiple email addresses with comma(,)"
      Height          =   195
      Left            =   1080
      TabIndex        =   27
      Top             =   600
      Width           =   3900
   End
   Begin VB.Label Label9 
      AutoSize        =   -1  'True
      Caption         =   "Attachments"
      Height          =   195
      Left            =   120
      TabIndex        =   26
      Top             =   3000
      Width           =   885
   End
   Begin VB.Label Label5 
      AutoSize        =   -1  'True
      Caption         =   "Encoding"
      Height          =   195
      Left            =   120
      TabIndex        =   21
      Top             =   2580
      Width           =   675
   End
   Begin VB.Label Label4 
      AutoSize        =   -1  'True
      Caption         =   "Subject"
      Height          =   195
      Left            =   120
      TabIndex        =   20
      Top             =   1725
      Width           =   540
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      Caption         =   "Cc"
      Height          =   195
      Left            =   120
      TabIndex        =   19
      Top             =   1320
      Width           =   195
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "To"
      Height          =   195
      Left            =   120
      TabIndex        =   18
      Top             =   885
      Width           =   195
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
' |    Copyright (c)2010 ADMINSYSTEM SOFTWARE LIMITED                        |
' |
' |    Project: It demonstrates how to use EASendMailObj to send email with synchronous mode
' |
' |    Author: Ivan Lui ( ivan@emailarchitect.net )
'  ===============================================================================
Option Explicit
Private m_arCharset(27, 1) As String
Private m_arAttachment() As String

Const CRYPT_MACHINE_KEYSET = 32
Const CRYPT_USER_KEYSET = 4096
Const CERT_SYSTEM_STORE_CURRENT_USER = 65536
Const CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072


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

Private Sub btnClear_Click()
    ReDim m_arAttachment(0)
    textAttachments.Text = ""
End Sub

'==================================================================================
'To send email without specified smtp server, we have to send the emails one by one
' to multiple recipients. That is because every recipient has different smtp server.
'===================================================================================
Private Sub DirectSend(ByRef oSmtp As EASendMailObjLib.Mail, recipients As String)
    Dim arTo
    arTo = SplitEx(recipients, ",")   'split the multiple address to an array
    Dim i, Count
    Dim addr
    Count = UBound(arTo)
    For i = 0 To Count
        addr = arTo(i)
        fnTrim addr, " ,;"
        If addr <> "" Then
            oSmtp.ClearRecipient
            oSmtp.AddRecipientEx addr, 0
            textStatus.Caption = "Sending email to " & addr & ", please wait ... "
            If oSmtp.SendMail() = 0 Then
                MsgBox "Message delivered to " & addr & " successfully!"
            Else
                MsgBox "Failed to delivery to " & addr & " : " & oSmtp.GetLastErrDescription() 'Get last error description
            End If
        End If
    Next
End Sub

Private Sub btnSend_Click()
    If Trim(textFrom.Text) = "" Then
        MsgBox ("Please input From email address!")
        Exit Sub
    End If

    If Trim(textTo.Text) = "" And _
        Trim(textCc.Text) = "" Then
            MsgBox ("Please input To or Cc email addresses, please use comma(,) to separate multiple addresses")
            Exit Sub
    End If
    
    btnSend.Enabled = False
    
    Dim From, to_addr, cc_addr, ServerAddr As String
    From = Trim(textFrom.Text)
    to_addr = Trim(textTo.Text)
    cc_addr = Trim(textCc.Text)
    ServerAddr = Trim(textServer.Text)
    textStatus.Caption = "Please wait ... "
    
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
    
    'add digital signature
    oSmtp.SignerCert.Unload
    If chkSign.Value = Checked Then
        If Not oSmtp.SignerCert.FindSubject(addr, CERT_SYSTEM_STORE_CURRENT_USER, "my") Then
            MsgBox oSmtp.SignerCert.GetLastError() & ":" & addr
            btnSend.Enabled = True
        Exit Sub
        End If
        If Not oSmtp.SignerCert.HasPrivateKey Then
            MsgBox "Signer certificate has not private key, this certificate can not be used to sign email!"
            btnSend.Enabled = True
            Exit Sub
        End If
    End If
    
    oSmtp.AddRecipientEx to_addr, 0  ' 0, Normal recipient, 1, cc, 2, bcc
    oSmtp.AddRecipientEx cc_addr, 0
    
    Dim recipients As String
    recipients = to_addr & "," & cc_addr
    fnTrim recipients, ","
    
    Dim i, Count As Integer
    'encrypt email by recipients certificate
    oSmtp.RecipientsCerts.Clear
    If chkEncrypt.Value = Checked Then
        Dim arAddr
        arAddr = SplitEx(recipients, ",")   'split the multiple address to an array
        Count = UBound(arAddr)
        For i = LBound(arAddr) To Count
            addr = arAddr(i)
            fnTrim addr, " ,;"
            If addr <> "" Then
                'find the encrypting certificate for every recipients
                Dim oEncryptCert As New EASendMailObjLib.Certificate
                If Not oEncryptCert.FindSubject(addr, CERT_SYSTEM_STORE_CURRENT_USER, "AddressBook") Then
                    If Not oEncryptCert.FindSubject(addr, CERT_SYSTEM_STORE_CURRENT_USER, "my") Then
                        MsgBox oEncryptCert.GetLastError() & ":" & addr
                        btnSend.Enabled = True
                        Exit Sub
                    End If
                End If
                oSmtp.RecipientsCerts.Add oEncryptCert
            End If
        Next
    End If
    
    
    Count = UBound(m_arAttachment)
    For i = 0 To Count - 1
        If oSmtp.AddAttachment(m_arAttachment(i)) <> 0 Then
            MsgBox oSmtp.GetLastErrDescription() & ":" & m_arAttachment(i)
            btnSend.Enabled = True
            Exit Sub
        End If
    Next
    
    Dim Subject, Bodytext
    Subject = textSubject.Text
    Bodytext = textBody.Text
    
    
    Bodytext = Replace(Bodytext, "[$from]", From)
    Bodytext = Replace(Bodytext, "[$to]", recipients)
    Bodytext = Replace(Bodytext, "[$subject]", Subject)
    
    oSmtp.Subject = Subject
    oSmtp.Bodytext = Bodytext
    
    'oSmtp.BodyFormat = 1    ' Using HTML FORMAT to send mail
    
    If InStr(1, recipients, ",", 1) > 1 And ServerAddr = "" Then
        'To send email without specified smtp server, we have to send the emails one by one
        ' to multiple recipients. That is because every recipient has different smtp server.
        DirectSend oSmtp, recipients
        btnSend.Enabled = True
        textStatus.Caption = ""
        Exit Sub
    End If
    
    If oSmtp.SendMail() = 0 Then
        MsgBox "Message delivered."
    Else
        MsgBox oSmtp.GetLastErrDescription() 'Get last error description
    End If
    
    textStatus.Caption = ""
    btnSend.Enabled = True
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
    InitCharset
    InitProtocol
    ReDim m_arAttachment(0)
    Dim s As String
    s = s & "This sample demonstrates how to send simple email." & vbCrLf & vbCrLf
    s = s & "From: [$from]" & vbCrLf
    s = s & "To: [$to]" & vbCrLf
    s = s & "Subject: [$subject]" & vbCrLf & vbCrLf
    s = s & "If no sever address was specified, the email will be delivered to the recipient's server directly,"
    s = s & "However, if you don't have a static IP address, "
    s = s & "many anti-spam filters will mark it as a junk-email." & vbCrLf & vbCrLf
     
    textBody.Text = s

End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
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
        textBody.Height = Me.Height - 4700


        btnSend.Top = textBody.Height + textBody.Top + 150
        btnSend.Left = Me.Width - 1900
        
        Frame1.Left = Me.Width - 4700

        textFrom.Width = Me.Width - 5900
        textTo.Width = textFrom.Width
        textCc.Width = textFrom.Width
        textSubject.Width = textFrom.Width

        textAttachments.Width = Me.Width - 2800
        btnAdd.Left = textAttachments.Width + 1200
        btnClear.Left = textAttachments.Width + 1900
End Sub
