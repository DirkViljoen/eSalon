VERSION 5.00
Begin VB.Form Dialog 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Dialog Caption"
   ClientHeight    =   1560
   ClientLeft      =   2760
   ClientTop       =   3750
   ClientWidth     =   5385
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1560
   ScaleWidth      =   5385
   ShowInTaskbar   =   0   'False
   Begin VB.TextBox txtAddress 
      Height          =   285
      Left            =   1080
      TabIndex        =   5
      Top             =   600
      Width           =   4095
   End
   Begin VB.TextBox txtName 
      Height          =   285
      Left            =   1080
      TabIndex        =   4
      Top             =   120
      Width           =   4095
   End
   Begin VB.CommandButton CancelButton 
      Caption         =   "Cancel"
      Height          =   375
      Left            =   3960
      TabIndex        =   1
      Top             =   1080
      Width           =   1215
   End
   Begin VB.CommandButton OKButton 
      Caption         =   "OK"
      Height          =   375
      Left            =   2640
      TabIndex        =   0
      Top             =   1080
      Width           =   1215
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "Address"
      Height          =   195
      Left            =   240
      TabIndex        =   3
      Top             =   645
      Width           =   570
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "Name"
      Height          =   195
      Left            =   240
      TabIndex        =   2
      Top             =   165
      Width           =   420
   End
End
Attribute VB_Name = "Dialog"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit
Public m_bOK As Boolean

Private Sub CancelButton_Click()
    m_bOK = False
    Me.Hide
End Sub

Private Sub Form_Load()
    txtAddress = ""
    txtName = ""
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
     m_bOK = False
End Sub

Private Sub OKButton_Click()
 If Trim(txtAddress) = "" Then
        MsgBox "please recipient's email address!"
        Exit Sub
    End If
    m_bOK = True
    Me.Hide
End Sub
