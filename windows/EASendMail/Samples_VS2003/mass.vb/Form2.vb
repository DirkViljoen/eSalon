Public Class Form2
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Public textName As System.Windows.Forms.TextBox
    Public textAddress As System.Windows.Forms.TextBox

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.textName = New System.Windows.Forms.TextBox
        Me.textAddress = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 24)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(48, 16)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Name"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(8, 48)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(80, 16)
        Me.label2.TabIndex = 1
        Me.label2.Text = "Email Address"
        '
        'textName
        '
        Me.textName.Location = New System.Drawing.Point(96, 16)
        Me.textName.Name = "textName"
        Me.textName.Size = New System.Drawing.Size(184, 20)
        Me.textName.TabIndex = 2
        Me.textName.Text = ""
        '
        'textAddress
        '
        Me.textAddress.Location = New System.Drawing.Point(96, 48)
        Me.textAddress.Name = "textAddress"
        Me.textAddress.Size = New System.Drawing.Size(184, 20)
        Me.textAddress.TabIndex = 3
        Me.textAddress.Text = ""
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(128, 80)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(208, 80)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'Form2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 109)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.textAddress)
        Me.Controls.Add(Me.textName)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        textName.Text = textName.Text.Trim()
        textAddress.Text = textAddress.Text.Trim()
        If textAddress.Text.Length = 0 Then
            MessageBox.Show(Me, "Please input a valid email address!")
            textAddress.Focus()
            Exit Sub
        End If

        btnOK.DialogResult = Windows.Forms.DialogResult.OK
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Exit Sub
    End Sub
End Class
