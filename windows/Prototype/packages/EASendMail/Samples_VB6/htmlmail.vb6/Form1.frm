VERSION 5.00
Object = "{EAB22AC0-30C1-11CF-A7EB-0000C05BAE0B}#1.1#0"; "shdocvw.dll"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   7095
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10080
   LinkTopic       =   "Form1"
   ScaleHeight     =   7095
   ScaleWidth      =   10080
   StartUpPosition =   3  'Windows Default
   Begin VB.CheckBox chkEncrypt 
      Caption         =   "Encryption"
      Height          =   255
      Left            =   2520
      TabIndex        =   6
      Top             =   2010
      Width           =   1815
   End
   Begin VB.CheckBox chkSign 
      Caption         =   "Digital Signature"
      Height          =   195
      Left            =   120
      TabIndex        =   5
      Top             =   2040
      Width           =   1815
   End
   Begin SHDocVwCtl.WebBrowser htmlEditor 
      Height          =   2175
      Left            =   120
      TabIndex        =   23
      Top             =   3960
      Width           =   9855
      ExtentX         =   17383
      ExtentY         =   3836
      ViewMode        =   0
      Offline         =   0
      Silent          =   0
      RegisterAsBrowser=   0
      RegisterAsDropTarget=   1
      AutoArrange     =   0   'False
      NoClientEdge    =   0   'False
      AlignLeft       =   0   'False
      NoWebView       =   0   'False
      HideFileNames   =   0   'False
      SingleClick     =   0   'False
      SingleSelection =   0   'False
      NoFolders       =   0   'False
      Transparent     =   0   'False
      ViewID          =   "{0057D0E0-3573-11CF-AE69-08002B2E1262}"
      Location        =   "http:///"
   End
   Begin VB.CommandButton btnInsert 
      Caption         =   "Insert Picture"
      Height          =   315
      Left            =   5760
      TabIndex        =   22
      Top             =   3480
      Width           =   2055
   End
   Begin VB.CommandButton btnC 
      BackColor       =   &H80000005&
      Caption         =   "C"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   5160
      TabIndex        =   21
      ToolTipText     =   "Choose font color"
      Top             =   3480
      Width           =   375
   End
   Begin VB.CommandButton btnU 
      Caption         =   "U"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   4800
      TabIndex        =   20
      ToolTipText     =   "Underline"
      Top             =   3480
      Width           =   375
   End
   Begin VB.CommandButton btnI 
      Caption         =   "I"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   4440
      TabIndex        =   19
      ToolTipText     =   "Italic"
      Top             =   3480
      Width           =   375
   End
   Begin VB.CommandButton btnB 
      Caption         =   "B"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   4080
      TabIndex        =   18
      ToolTipText     =   "Bold"
      Top             =   3480
      Width           =   375
   End
   Begin VB.ComboBox lstSize 
      Height          =   315
      Left            =   2520
      Style           =   2  'Dropdown List
      TabIndex        =   17
      Top             =   3480
      Width           =   1455
   End
   Begin VB.ComboBox lstFonts 
      Height          =   315
      Left            =   120
      Style           =   2  'Dropdown List
      TabIndex        =   16
      Top             =   3480
      Width           =   2295
   End
   Begin MSComctlLib.StatusBar textStatus 
      Align           =   2  'Align Bottom
      Height          =   390
      Left            =   0
      TabIndex        =   37
      Top             =   6705
      Width           =   10080
      _ExtentX        =   17780
      _ExtentY        =   688
      Style           =   1
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   1
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.ProgressBar pgBar 
      Height          =   135
      Left            =   120
      TabIndex        =   24
      Top             =   6360
      Width           =   6135
      _ExtentX        =   10821
      _ExtentY        =   238
      _Version        =   393216
      Appearance      =   1
   End
   Begin VB.CommandButton btnCancel 
      Caption         =   "Cancel"
      Enabled         =   0   'False
      Height          =   375
      Left            =   8400
      TabIndex        =   26
      Top             =   6240
      Width           =   1575
   End
   Begin MSComDlg.CommonDialog dlgFile 
      Left            =   4560
      Top             =   1920
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton btnSend 
      Caption         =   "Send"
      Height          =   375
      Left            =   6720
      TabIndex        =   25
      Top             =   6240
      Width           =   1575
   End
   Begin VB.CommandButton btnClear 
      Caption         =   "Clear"
      Height          =   375
      Left            =   9000
      TabIndex        =   15
      Top             =   2955
      Width           =   615
   End
   Begin VB.CommandButton btnAdd 
      Caption         =   "Add"
      Height          =   375
      Left            =   8280
      TabIndex        =   14
      Top             =   2955
      Width           =   615
   End
   Begin VB.TextBox textAttachments 
      BackColor       =   &H80000018&
      Enabled         =   0   'False
      Height          =   285
      Left            =   1080
      TabIndex        =   13
      Top             =   3000
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
      Text            =   "Test HTML subject"
      Top             =   1560
      Width           =   4335
   End
   Begin VB.TextBox textCc 
      Height          =   285
      Left            =   1080
      TabIndex        =   3
      Top             =   1200
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
      Left            =   5760
      TabIndex        =   31
      Top             =   120
      Width           =   4215
      Begin VB.ComboBox lstProtocol 
         Height          =   315
         Left            =   120
         Style           =   2  'Dropdown List
         TabIndex        =   38
         Top             =   2160
         Width           =   3975
      End
      Begin VB.CheckBox chkSSL 
         Caption         =   "My server requires secure connection (SSL)"
         Height          =   255
         Left            =   120
         TabIndex        =   11
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
         TabIndex        =   10
         Top             =   1320
         Width           =   3015
      End
      Begin VB.TextBox textUser 
         BackColor       =   &H8000000B&
         Enabled         =   0   'False
         Height          =   285
         Left            =   1080
         TabIndex        =   9
         Top             =   960
         Width           =   3015
      End
      Begin VB.CheckBox chkAuth 
         Caption         =   "My server requires user authentication"
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   600
         Width           =   3855
      End
      Begin VB.TextBox textServer 
         Height          =   285
         Left            =   1080
         TabIndex        =   7
         Top             =   240
         Width           =   3015
      End
      Begin VB.Label Label8 
         AutoSize        =   -1  'True
         Caption         =   "Password"
         Height          =   195
         Left            =   120
         TabIndex        =   34
         Top             =   1320
         Width           =   690
      End
      Begin VB.Label Label7 
         AutoSize        =   -1  'True
         Caption         =   "User"
         Height          =   195
         Left            =   120
         TabIndex        =   33
         Top             =   975
         Width           =   330
      End
      Begin VB.Label Label6 
         AutoSize        =   -1  'True
         Caption         =   "Server"
         Height          =   195
         Left            =   120
         TabIndex        =   32
         Top             =   285
         Width           =   465
      End
   End
   Begin VB.Label Label10 
      AutoSize        =   -1  'True
      Caption         =   "Please separate multiple email addresses with comma(,)"
      Height          =   195
      Left            =   1080
      TabIndex        =   36
      Top             =   600
      Width           =   3900
   End
   Begin VB.Label Label9 
      AutoSize        =   -1  'True
      Caption         =   "Attachments"
      Height          =   195
      Left            =   120
      TabIndex        =   35
      Top             =   3045
      Width           =   885
   End
   Begin VB.Label Label5 
      AutoSize        =   -1  'True
      Caption         =   "Encoding"
      Height          =   195
      Left            =   120
      TabIndex        =   30
      Top             =   2580
      Width           =   675
   End
   Begin VB.Label Label4 
      AutoSize        =   -1  'True
      Caption         =   "Subject"
      Height          =   195
      Left            =   120
      TabIndex        =   29
      Top             =   1560
      Width           =   540
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      Caption         =   "Cc"
      Height          =   285
      Left            =   120
      TabIndex        =   28
      Top             =   1200
      Width           =   195
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "To"
      Height          =   195
      Left            =   120
      TabIndex        =   27
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
' |    Copyright (c)2010 - 2012  ADMINSYSTEM SOFTWARE LIMITED                        |
' |
' |    Project: It demonstrates how to use EASendMail to send email with asynchronous mode + html + s/mime
' |
' |    Author: Ivan Lui ( ivan@emailarchitect.net )
'  ===============================================================================
Option Explicit

Const CRYPT_MACHINE_KEYSET = 32
Const CRYPT_USER_KEYSET = 4096
Const CERT_SYSTEM_STORE_CURRENT_USER = 65536
Const CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072

Private m_arCharset(27, 1) As String
Private m_arAttachment() As String

Private WithEvents m_oSmtp As EASendMailObjLib.Mail 'declare EASendMail object
Attribute m_oSmtp.VB_VarHelpID = -1
Private m_bError As Boolean 'this variable indicates if error occurs when sending email
Private m_bCancel As Boolean
Private m_bIdle As Boolean
Private m_lastErrDescription As String


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

Private Sub InitFonts()
    Dim arFonts
    'you can add more fonts in this array
    arFonts = Array("Choose Font Style ...", _
                    "Arial", _
                    "Arial Baltic", _
                    "Arial Black", _
                    "Basemic Symbol", _
                    "Bookman Old Style", _
                    "Comic Sans MS", _
                    "Courier", _
                    "Courier New", _
                    "Microsoft Sans Serif", _
                    "MS Sans Serif", _
                    "Times New Roman", _
                    "Verdana")
                                            
    Dim i, nCount As Integer
    nCount = UBound(arFonts)
    For i = LBound(arFonts) To nCount
        lstFonts.AddItem arFonts(i), i
    Next
    lstFonts.ListIndex = 0
    
    lstSize.AddItem "Font Size ...", 0
    For i = 1 To 7
        lstSize.AddItem i, i
    Next
    lstSize.ListIndex = 0

End Sub

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

Private Sub btnC_Click()
    On Error Resume Next
    dlgFile.CancelError = True
    dlgFile.ShowColor

    If Err.Number = cdlCancel Then
        Exit Sub
    End If
    
    Dim clr As OLE_COLOR
    Dim v, r, g, b
    
    clr = dlgFile.Color
    r = Hex$(clr And &HFF&)
    g = Hex$((clr And &HFF00&) \ &H100&)
    b = Hex$((clr And &HFF0000) \ &H10000)
    If Len(r) = 0 Then
        r = "00"
    ElseIf Len(r) = 1 Then
        r = "0" & r
    End If
    
    If Len(g) = 0 Then
        g = "00"
    ElseIf Len(g) = 1 Then
        g = "0" & g
    End If
    
    If Len(b) = 0 Then
        b = "00"
    ElseIf Len(b) = 1 Then
        b = "0" & b
    End If
   
    v = "#" & r & g & b
    htmlEditor.Document.execCommand "ForeColor", False, v
    htmlEditor.SetFocus
End Sub

Private Sub btnCancel_Click()
    m_oSmtp.Terminate
    m_bCancel = True
    m_bIdle = True
    btnCancel.Enabled = False
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
            
            textStatus.SimpleText = "Connecting server for " & addr & "..."
            m_bIdle = False
            m_bCancel = False
            m_bError = False
            pgBar.Value = 0
            
            oSmtp.SendMail
            
            'wait the asynchronous call finish.
            Do While Not m_bIdle
                DoEvents
            Loop
    
            If m_bCancel Then
                textStatus.SimpleText = "Operation is canceled by user."
            ElseIf m_bError Then
                textStatus.SimpleText = "Failed to delivery to " & addr & ":" & m_lastErrDescription
            Else
                textStatus.SimpleText = "Message was delivered to " & addr & " successfully"
            End If
    
            MsgBox textStatus.SimpleText
            
            If m_bCancel Then
                Exit For
            End If
        End If
    Next
End Sub

Private Sub btnInsert_Click()
    htmlEditor.Document.execCommand "InsertImage", True
End Sub

Private Sub btnSend_Click()
    If Trim(textFrom.Text) = "" Then
        MsgBox ("Please input From, the format can be test@adminsystem.com or Tester<test@adminsystem.com>")
        Exit Sub
    End If

    If Trim(textTo.Text) = "" And _
        Trim(textCc.Text) = "" Then
            MsgBox ("Please input To or Cc, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use comma(,) to separate multiple addresses")
            Exit Sub
    End If
    
    btnSend.Enabled = False
    btnCancel.Enabled = True
    'because m_oSmtp is a member variahle, so we need to clear the the property
    m_oSmtp.Reset

    m_oSmtp.Asynchronous = 1
    m_oSmtp.ServerAddr = ""
    m_oSmtp.ServerPort = 25
    m_oSmtp.SSL_uninit
    m_oSmtp.UserName = ""
    m_oSmtp.Password = ""
    
    Dim From, to_addr, cc_addr, ServerAddr As String
    From = Trim(textFrom.Text)
    to_addr = Trim(textTo.Text)
    cc_addr = Trim(textCc.Text)
    ServerAddr = Trim(textServer.Text)
    m_oSmtp.ServerAddr = ServerAddr
    m_oSmtp.Protocol = lstProtocol.ListIndex
    
    If ServerAddr <> "" Then
        If chkAuth.Value = Checked Then
            m_oSmtp.UserName = Trim(textUser.Text)
            m_oSmtp.Password = Trim(textPassword.Text)
        End If
        
        If chkSSL.Value = Checked Then
            If m_oSmtp.SSL_init() <> 0 Then
                MsgBox ("failed to load SSL library!")
                Exit Sub
            End If
            'If SSL port is 465 or other port rather than 25 port, please use
            'm_oSmtp.ServerPort = 465
            'm_oSmtp.SSL_starttls = 0
        End If
    End If
    
    m_oSmtp.Charset = m_arCharset(lstCharset.ListIndex, 1)
    Dim name, addr As String
    fnParseAddr From, name, addr
    
    'Using this email to be replied to another address
    'm_oSmtp.ReplyTo = ReplyAddress
    
    m_oSmtp.From = name
    m_oSmtp.FromAddr = addr
    
    'add digital signature
    m_oSmtp.SignerCert.Unload
    If chkSign.Value = Checked Then
        If Not m_oSmtp.SignerCert.FindSubject(addr, CERT_SYSTEM_STORE_CURRENT_USER, "my") Then
            MsgBox m_oSmtp.SignerCert.GetLastError() & ":" & addr
            btnSend.Enabled = True
            btnCancel.Enabled = False
        Exit Sub
        End If
        If Not m_oSmtp.SignerCert.HasPrivateKey Then
            MsgBox "Signer certificate has not private key, this certificate can not be used to sign email!"
            btnSend.Enabled = True
            btnCancel.Enabled = False
            Exit Sub
        End If
    End If
    
    m_oSmtp.AddRecipientEx to_addr, 0  ' 0, Normal recipient, 1, cc, 2, bcc
    m_oSmtp.AddRecipientEx cc_addr, 0
    
    Dim recipients As String
    recipients = to_addr & "," & cc_addr
    fnTrim recipients, ","
    
    Dim i, Count As Integer
    'encrypt email by recipients certificate
    m_oSmtp.RecipientsCerts.Clear
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
                        btnCancel.Enabled = False
                        Exit Sub
                    End If
                End If
                m_oSmtp.RecipientsCerts.Add oEncryptCert
            End If
        Next
    End If
    
    Count = UBound(m_arAttachment)
    For i = 0 To Count - 1
        If m_oSmtp.AddAttachment(m_arAttachment(i)) <> 0 Then
            MsgBox m_oSmtp.GetLastErrDescription() & ":" & m_arAttachment(i)
            btnSend.Enabled = True
            btnCancel.Enabled = False
            Exit Sub
        End If
    Next
    
    Dim Subject
    Subject = textSubject.Text
    
    m_oSmtp.BodyFormat = 1    ' Using HTML FORMAT to send mail
    m_oSmtp.AltBody = htmlEditor.Document.body.innerText
    Dim html As String
    html = htmlEditor.Document.body.innerHTML
    
    Dim header As String
    header = "<html><title>" & Subject & "</title><meta HTTP-EQUIV=""Content-Type"" Content=""text-html; charset=" & m_oSmtp.Charset & """><META content=""MSHTML 6.00.2900.2769"" name=GENERATOR><body>"
    'imports html with embedded pictures
    html = header & html
    html = html & "</body></html>"
    m_oSmtp.ImportHtml html, App.Path
    
    m_oSmtp.Subject = Subject
    
    If InStr(1, recipients, ",", 1) > 1 And ServerAddr = "" Then
        'To send email without specified smtp server, we have to send the emails one by one
        ' to multiple recipients. That is because every recipient has different smtp server.
        DirectSend m_oSmtp, recipients
        btnSend.Enabled = True
        btnCancel.Enabled = False
        textStatus.SimpleText = ""
        Exit Sub
    End If
    
    textStatus.SimpleText = "Connecting " & ServerAddr & " ..."
    m_bIdle = False
    m_bCancel = False
    m_bError = False
    pgBar.Value = 0
    
    m_oSmtp.SendMail
    
    'wait the asynchronous call finish.
    Do While Not m_bIdle
        DoEvents
    Loop
    
    If m_bCancel Then
        textStatus.SimpleText = "Operation is canceled by user."
    ElseIf m_bError Then
        textStatus.SimpleText = m_lastErrDescription
    Else
        textStatus.SimpleText = "Message was delivered successfully"
    End If
    
    MsgBox textStatus.SimpleText
    btnSend.Enabled = True
    btnCancel.Enabled = False
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
    InitFonts
    htmlEditor.Navigate2 "about:blank"
    htmlEditor.Document.designMode = "on"
    
    ReDim m_arAttachment(0)
     
    'Declare and create easendmail mail object instance
    Set m_oSmtp = New EASendMailObjLib.Mail
    'The license code for EASendMail ActiveX Object,
    'for evaluation usage, please use "TryIt" as the license code.
    m_oSmtp.LicenseCode = "TryIt"
    'm_oSmtp.LogFileName = App.Path & "\smtp.txt" 'enable smtp log
    
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    End
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    If (Me.Width < 10200) Then
        Me.Width = 10200
    End If

    If (Me.Height < 7500) Then
        Me.Height = 7500
    End If
    
    htmlEditor.Width = Me.Width - 450
    htmlEditor.Height = Me.Height - 5500


    btnSend.Top = htmlEditor.Height + htmlEditor.Top + 150
    btnSend.Left = Me.Width - 3600
    btnCancel.Top = btnSend.Top
    btnCancel.Left = btnSend.Left + btnSend.Width + 100
    
    pgBar.Top = btnSend.Top + 100
    pgBar.Width = Me.Width - 4000
    
    Frame1.Left = Me.Width - 4600

    textFrom.Width = Me.Width - 5800
    textTo.Width = textFrom.Width
    textCc.Width = textFrom.Width
    textSubject.Width = textFrom.Width

    textAttachments.Width = Me.Width - 2900
    btnAdd.Left = textAttachments.Width + 1200
    btnClear.Left = textAttachments.Width + 1900
End Sub

Private Sub htmlEditor_NavigateComplete2(ByVal pDisp As Object, URL As Variant)
    Dim s As String
    s = s & "<div>This sample demonstrates how to send html email.</div><div>&nbsp;</div>"
    s = s & "<div>If no sever address was specified, the email will be delivered to the recipient's server directly,"
    s = s & "However, if you don't have a static IP address, "
    s = s & "many anti-spam filters will mark it as a junk-email.</div><div>&nbsp;</div>"

    htmlEditor.Document.body.innerHTML = s
End Sub

Private Sub lstFonts_Click()
    If lstFonts.ListIndex = 0 Then
        Exit Sub
    End If
    htmlEditor.Document.execCommand "fontname", False, lstFonts.Text
    lstFonts.ListIndex = 0
    htmlEditor.SetFocus
End Sub

Private Sub lstSize_click()
    If lstSize.ListIndex = 0 Then
        Exit Sub
    End If
    htmlEditor.Document.execCommand "fontsize", False, lstSize.Text
    lstSize.ListIndex = 0
    htmlEditor.SetFocus
End Sub

Private Sub btnU_Click()
    htmlEditor.Document.execCommand "underline", False, Nothing
    htmlEditor.SetFocus
End Sub

Private Sub btnB_Click()
    htmlEditor.Document.execCommand "Bold", False, Nothing
      
    htmlEditor.SetFocus
End Sub

Private Sub btnI_Click()
    htmlEditor.Document.execCommand "Italic", False, Nothing
    htmlEditor.SetFocus
End Sub

'=====================================================
' Method Object_OnAuthenticated
' Type: Event, fired when ESMTP user authentication is successful
' Parameter: nothing
' Return value: nothing
'=====================================================
Private Sub m_oSmtp_OnAuthenticated()
    textStatus.SimpleText = "Authorized"
End Sub

'=====================================================
' Method Object_OnClosed
' Type: Event, fired when server disconncts with client
' Parameter: nothing
' Return value: nothing
'=====================================================
Private Sub m_oSmtp_OnClosed()
    m_bIdle = True
End Sub

'=====================================================
' Method Object_OnConnected
' Type: Event, fired when client successfully connects server
' Parameter: nothing
' Return value: nothing
'=====================================================
Private Sub m_oSmtp_OnConnected()
    textStatus.SimpleText = "Connected"
End Sub

'======================================================
' Method Object_OnError
' Type: Event, fired when an error occurs when sending email
' Parameter:
'     lError: error code, refer to EASendMail documentation
'     ErrDescription: error description
' Return value: nothing
'======================================================
Private Sub m_oSmtp_OnError(ByVal lError As Long, ByVal ErrDescription As String)
    m_bError = True
    m_lastErrDescription = ErrDescription
    m_bIdle = True
End Sub

'=======================================================
' Method Object_OnSending
' Type: Event, fired when EASendMail object is sending an email body
' Parameter:
'     lSent: size of sent data in bytes
'     lTotal: size of email body in bytes
' Return value: nothing
'=======================================================
Private Sub m_oSmtp_OnSending(ByVal lSent As Long, ByVal lTotal As Long)
    If lSent = 0 Then
        textStatus.SimpleText = "Sending...."
        pgBar.Min = 0
        pgBar.Max = lTotal
        pgBar.Value = 0
    ElseIf lSent = lTotal Then
        textStatus.SimpleText = "Disconnecting..."
        pgBar.Value = lTotal
    Else
        pgBar.Value = lSent
    End If
End Sub
