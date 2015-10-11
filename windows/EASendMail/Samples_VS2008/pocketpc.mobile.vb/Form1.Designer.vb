<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Friend WithEvents components As System.ComponentModel.IContainer
    Friend WithEvents mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Protected Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.textFrom = New System.Windows.Forms.TextBox
        Me.textTo = New System.Windows.Forms.TextBox
        Me.textCc = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.textSubject = New System.Windows.Forms.TextBox
        Me.label5 = New System.Windows.Forms.Label
        Me.chkAuth = New System.Windows.Forms.CheckBox
        Me.textServer = New System.Windows.Forms.TextBox
        Me.label6 = New System.Windows.Forms.Label
        Me.textUser = New System.Windows.Forms.TextBox
        Me.label7 = New System.Windows.Forms.Label
        Me.textPassword = New System.Windows.Forms.TextBox
        Me.chkSSL = New System.Windows.Forms.CheckBox
        Me.label8 = New System.Windows.Forms.Label
        Me.textAttachments = New System.Windows.Forms.TextBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.textBody = New System.Windows.Forms.TextBox
        Me.btnSend = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.pgSending = New System.Windows.Forms.ProgressBar
        Me.sbStatus = New System.Windows.Forms.StatusBar
        Me.attachmentDlg = New System.Windows.Forms.OpenFileDialog
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(5, 7)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(69, 19)
        Me.label1.Text = "From:"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(5, 32)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(45, 23)
        Me.label2.Text = "To:"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(5, 62)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(53, 22)
        Me.label3.Text = "Cc:"
        '
        'textFrom
        '
        Me.textFrom.Location = New System.Drawing.Point(79, 3)
        Me.textFrom.Name = "textFrom"
        Me.textFrom.Size = New System.Drawing.Size(148, 21)
        Me.textFrom.TabIndex = 3
        '
        'textTo
        '
        Me.textTo.Location = New System.Drawing.Point(79, 32)
        Me.textTo.Name = "textTo"
        Me.textTo.Size = New System.Drawing.Size(148, 21)
        Me.textTo.TabIndex = 4
        '
        'textCc
        '
        Me.textCc.Location = New System.Drawing.Point(79, 61)
        Me.textCc.Name = "textCc"
        Me.textCc.Size = New System.Drawing.Size(148, 21)
        Me.textCc.TabIndex = 5
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(5, 90)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(56, 30)
        Me.label4.Text = "Subject:"
        '
        'textSubject
        '
        Me.textSubject.Location = New System.Drawing.Point(79, 90)
        Me.textSubject.Name = "textSubject"
        Me.textSubject.Size = New System.Drawing.Size(148, 21)
        Me.textSubject.TabIndex = 7
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(3, 267)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(90, 19)
        Me.label5.Text = "SMTP Server:"
        '
        'chkAuth
        '
        Me.chkAuth.Location = New System.Drawing.Point(3, 291)
        Me.chkAuth.Name = "chkAuth"
        Me.chkAuth.Size = New System.Drawing.Size(166, 23)
        Me.chkAuth.TabIndex = 9
        Me.chkAuth.Text = "Require Authentication"
        '
        'textServer
        '
        Me.textServer.Location = New System.Drawing.Point(99, 267)
        Me.textServer.Name = "textServer"
        Me.textServer.Size = New System.Drawing.Size(128, 21)
        Me.textServer.TabIndex = 10
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(7, 317)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(79, 18)
        Me.label6.Text = "Username:"
        '
        'textUser
        '
        Me.textUser.Location = New System.Drawing.Point(83, 314)
        Me.textUser.Name = "textUser"
        Me.textUser.Size = New System.Drawing.Size(133, 21)
        Me.textUser.TabIndex = 12
        Me.textUser.Enabled = False
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(7, 342)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(85, 17)
        Me.label7.Text = "Password:"
        '
        'textPassword
        '
        Me.textPassword.Location = New System.Drawing.Point(83, 340)
        Me.textPassword.Name = "textPassword"
        Me.textPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.textPassword.Size = New System.Drawing.Size(133, 21)
        Me.textPassword.TabIndex = 14
        Me.textPassword.Enabled = False
        '
        'chkSSL
        '
        Me.chkSSL.Location = New System.Drawing.Point(7, 367)
        Me.chkSSL.Name = "chkSSL"
        Me.chkSSL.Size = New System.Drawing.Size(198, 29)
        Me.chkSSL.TabIndex = 15
        Me.chkSSL.Text = "SSL Connection"
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(5, 119)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(83, 23)
        Me.label8.Text = "Attachment:"
        '
        'textAttachments
        '
        Me.textAttachments.Location = New System.Drawing.Point(79, 119)
        Me.textAttachments.Name = "textAttachments"
        Me.textAttachments.Size = New System.Drawing.Size(148, 21)
        Me.textAttachments.TabIndex = 6
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(79, 148)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(71, 24)
        Me.btnAdd.TabIndex = 18
        Me.btnAdd.Text = "Add"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(156, 148)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(71, 24)
        Me.btnClear.TabIndex = 19
        Me.btnClear.Text = "Clear"
        '
        'textBody
        '
        Me.textBody.Location = New System.Drawing.Point(5, 178)
        Me.textBody.Multiline = True
        Me.textBody.Name = "textBody"
        Me.textBody.Size = New System.Drawing.Size(222, 83)
        Me.textBody.TabIndex = 20
        Me.textBody.Text = "This is a test email"
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(12, 401)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(71, 22)
        Me.btnSend.TabIndex = 21
        Me.btnSend.Text = "Send"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(89, 401)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(71, 22)
        Me.btnCancel.TabIndex = 22
        Me.btnCancel.Text = "Cancel"
        '
        'pgSending
        '
        Me.pgSending.Location = New System.Drawing.Point(10, 430)
        Me.pgSending.Name = "pgSending"
        Me.pgSending.Size = New System.Drawing.Size(215, 10)
        '
        'sbStatus
        '
        Me.sbStatus.Location = New System.Drawing.Point(0, 443)
        Me.sbStatus.Name = "sbStatus"
        Me.sbStatus.Size = New System.Drawing.Size(234, 22)
        Me.sbStatus.Text = "status"
        '
        'attachmentDlg
        '
        Me.attachmentDlg.FileName = "attachmentDlg"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(234, 465)
        Me.Controls.Add(Me.chkSSL)
        Me.Controls.Add(Me.sbStatus)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.pgSending)
        Me.Controls.Add(Me.textServer)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.textUser)
        Me.Controls.Add(Me.chkAuth)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.textPassword)
        Me.Controls.Add(Me.textBody)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.textAttachments)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.textSubject)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.textCc)
        Me.Controls.Add(Me.textTo)
        Me.Controls.Add(Me.textFrom)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Menu = Me.mainMenu1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents textFrom As System.Windows.Forms.TextBox
    Friend WithEvents textTo As System.Windows.Forms.TextBox
    Friend WithEvents textCc As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents textSubject As System.Windows.Forms.TextBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents chkAuth As System.Windows.Forms.CheckBox
    Friend WithEvents textServer As System.Windows.Forms.TextBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents textUser As System.Windows.Forms.TextBox
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents textPassword As System.Windows.Forms.TextBox
    Friend WithEvents chkSSL As System.Windows.Forms.CheckBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents textAttachments As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents textBody As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pgSending As System.Windows.Forms.ProgressBar
    Friend attachmentDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sbStatus As System.Windows.Forms.StatusBar
End Class
