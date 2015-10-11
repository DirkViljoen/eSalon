
'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2013  ADMINSYSTEM SOFTWARE LIMITED                         |
' |
' |    Project: It demonstrates how to use EASendMail to send text/html email with 
' |             VB.NET in Windows 8 Application.
' |
' |    File: Form1 : implementation file
' |
' |    Author: Ivan Lui ( ivan@emailarchitect.net )
'  ===============================================================================
' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
Imports Windows.UI.Xaml.Navigation
Imports Windows.UI.Popups
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports EASendMailRT
''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private linkPopup As Popup
    Private m_cts As CancellationTokenSource = Nothing
    Private m_atts As New List(Of String)()
    Private htmlInited As Boolean = False
    Private asyncCancel As IAsyncAction = Nothing

    ''' <summary>
    ''' Invoked when this page is about to be displayed in a Frame.
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.  The Parameter
    ''' property is typically used to configure the page.</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)
    
    End Sub

#Region "EASendMail Event Handler"
    Private Sub OnSecuring(sender As Object, e As SmtpStatusEventArgs)
        textStatus.Text = "Securing ... "
    End Sub

    Private Sub OnAuthorized(sender As Object, e As SmtpStatusEventArgs)
        textStatus.Text = "Authorized"
    End Sub


    Public Sub OnConnected(sender As Object, e As SmtpStatusEventArgs)
        textStatus.Text = "Connected"
    End Sub

    Public Sub OnSendingDataStream(sender As Object, e As SmtpDataStreamEventArgs)
        textStatus.Text = [String].Format("{0}/{1} sent", e.Sent, e.Total)
        pgBar.Maximum = e.Sent
        pgBar.Value = e.Total
    End Sub

#End Region

    Private Sub chkAuth_Click(sender As Object, e As RoutedEventArgs)
        Dim b As System.Nullable(Of Boolean) = chkAuth.IsChecked
        If Not b.HasValue Then
            Return
        End If

        textUser.IsEnabled = CBool(chkAuth.IsChecked)
        textPassword.IsEnabled = CBool(chkAuth.IsChecked)

    End Sub


    Private Sub Page_Loaded_1(sender As Object, e As RoutedEventArgs)
        lstProtocols.Items.Add("SMTP Protocol - Recommended")
        lstProtocols.Items.Add("Exchange Web Service - 2007/2010/2013")
        lstProtocols.Items.Add("Exchange WebDAV - 2000/2003")

        lstProtocols.SelectedIndex = 0

        lstFormat.Items.Add("Body Format: Text/Plain")
        lstFormat.Items.Add("Body Format: Text/Html")
        lstFormat.SelectedIndex = 0

        textBody.Text = "Hi," & vbCr & vbLf & vbCr & vbLf & "This is a simple test email sent from VB.NET Windows 8 Store Applicatoin (Metro) project." & vbCr & vbLf & "Please do not reply."

        imgSize.Items.Add("Size")
        For i As Integer = 0 To 6
            imgSize.Items.Add([String].Format("{0}", i + 1))
        Next

        imgSize.SelectedIndex = 0

        imgFont.Items.Add("Arial")
        imgFont.Items.Add("Calibri")
        imgFont.Items.Add("Comic Sans MS")
        imgFont.Items.Add("Courier New")
        imgFont.Items.Add("Times New Roman")
        imgFont.Items.Add("Tahoma")
        imgFont.Items.Add("Verdana")
        imgFont.Items.Add("Segoe UI")

        imgFont.SelectedIndex = 0

        Dim colors = GetType(Windows.UI.Colors).GetTypeInfo().DeclaredProperties
        For Each item In colors
            lstColor.Items.Add(item)
        Next

        lstColor.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        textFrom.Focus(Windows.UI.Xaml.FocusState.Programmatic)
    End Sub

    Private Async Sub btnAtts_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Dim openPicker As New FileOpenPicker()
        openPicker.ViewMode = PickerViewMode.List
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary
        openPicker.FileTypeFilter.Add("*")

        Dim files As IReadOnlyList(Of StorageFile) = Await openPicker.PickMultipleFilesAsync()
        If files.Count = 0 Then
            Return
        End If

        Dim s As String = ""
        For Each file As StorageFile In files
            s += file.Name
            s += ";"
            m_atts.Add(file.Path)
        Next

        textAtts.Text += s
    End Sub

    Private Sub btnClear_Tapped(sender As Object, e As TappedRoutedEventArgs)
        m_atts.Clear()
        textAtts.Text = ""
    End Sub

    Private Async Sub btnSend_Tapped(sender As Object, e As TappedRoutedEventArgs)
        If textFrom.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input from address!")
            Await dlg.ShowAsync()
            textFrom.Text = ""
            textFrom.Focus(Windows.UI.Xaml.FocusState.Programmatic)
            Return
        End If

        If textTo.Text.Trim().Length = 0 AndAlso textCc.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input a recipient at least!")
            Await dlg.ShowAsync()
            textTo.Text = ""
            textCc.Text = ""
            textTo.Focus(Windows.UI.Xaml.FocusState.Programmatic)
            Return
        End If

        If textServer.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input server address!")
            Await dlg.ShowAsync()
            textServer.Text = ""
            textServer.Focus(Windows.UI.Xaml.FocusState.Programmatic)
            Return
        End If


        Dim bAuth As Boolean = If((chkAuth.IsChecked.HasValue), CBool(chkAuth.IsChecked), False)
        If bAuth Then
            If textUser.Text.Trim().Length = 0 Then
                Dim dlg As New MessageDialog("Please input user name!")
                Await dlg.ShowAsync()
                textUser.Text = ""
                textUser.Focus(Windows.UI.Xaml.FocusState.Programmatic)
                Return
            End If

            If textPassword.Password.Trim().Length = 0 Then
                Dim dlg As New MessageDialog("Please input password!")
                Await dlg.ShowAsync()
                textPassword.Password = ""
                textPassword.Focus(Windows.UI.Xaml.FocusState.Programmatic)
                Return
            End If
        End If
        asyncCancel = Nothing
        btnSend.IsEnabled = False
        pgBar.Value = 0

        Try
            Dim oSmtp As New SmtpClient()

            AddHandler oSmtp.Authorized, AddressOf OnAuthorized
            AddHandler oSmtp.Connected, AddressOf OnConnected
            AddHandler oSmtp.Securing, AddressOf OnSecuring
            AddHandler oSmtp.SendingDataStream, AddressOf OnSendingDataStream

            Dim oServer As New SmtpServer(textServer.Text)
            Dim bSSL As Boolean = If((chkSSL.IsChecked.HasValue), CBool(chkSSL.IsChecked), False)
            If bSSL Then
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
            End If

            oServer.Protocol = DirectCast(lstProtocols.SelectedIndex, ServerProtocol)

            If bAuth Then
                oServer.User = textUser.Text
                oServer.Password = textPassword.Password
            End If

            ' For evaluation usage, please use "TryIt" as the license code, otherwise the 
            ' "Invalid License Code" exception will be thrown. However, the trial object only can be used 
            ' with developer license

            ' For licensed usage, please use your license code instead of "TryIt", then the object
            ' can used with published windows store application.
            Dim oMail As New SmtpMail("TryIt")

            oMail.From = New MailAddress(textFrom.Text)

            ' If your Exchange Server is 2007 and used Exchange Web Service protocol, please add the following line;
            ' oMail.Headers.RemoveKey("From")
            oMail.[To] = New AddressCollection(textTo.Text)
            oMail.Cc = New AddressCollection(textCc.Text)
            oMail.Subject = textSubject.Text

            If lstFormat.SelectedIndex = 0 Then
                oMail.TextBody = textBody.Text
            Else
                If chkHtml.IsChecked.HasValue Then
                    If CBool(chkHtml.IsChecked) Then
                        editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible
                        htmlEditor.InvokeScript("OnViewHtmlSource", New String() {"false"})
                        chkHtml.IsChecked = True
                    End If
                End If
                Dim html As String = htmlEditor.InvokeScript("getHtml", Nothing)
                html = "<html><head><meta charset=""utf-8"" /></head><body style=""font-family:Calibri;font-size: 15px;"">" & html & "<body></html>"
                Await oMail.ImportHtmlAsync(html, Windows.ApplicationModel.Package.Current.InstalledLocation.Path, ImportHtmlBodyOptions.ErrorThrowException Or ImportHtmlBodyOptions.ImportLocalPictures Or ImportHtmlBodyOptions.ImportHttpPictures Or ImportHtmlBodyOptions.ImportCss)
            End If

            Dim count As Integer = m_atts.Count
            For i As Integer = 0 To count - 1
                Await oMail.AddAttachmentAsync(m_atts(i))
            Next
            btnCancel.IsEnabled = True
            textStatus.Text = [String].Format("Connecting {0} ...", oServer.Server)



            ' You can genereate a log file by the following code.
            ' oSmtp.LogFileName = "ms-appdata:///local/smtp.txt";
            asyncCancel = oSmtp.SendMailAsync(oServer, oMail)
            Await asyncCancel


            textStatus.Text = "Completed"
        Catch ep As Exception
            textStatus.Text = "Error:  " + ep.Message
        End Try

        asyncCancel = Nothing
        btnSend.IsEnabled = True
        btnCancel.IsEnabled = False
    End Sub
    Private Sub btnCancel_Tapped(sender As Object, e As TappedRoutedEventArgs)
        btnCancel.IsEnabled = False
        If asyncCancel IsNot Nothing Then
            asyncCancel.Cancel()
        End If
    End Sub

#Region "HTML Editor"

    Private Sub lstFormat_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If lstFormat.SelectedIndex = 0 Then
            textBody.Visibility = Windows.UI.Xaml.Visibility.Visible
            htmlFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            htmlFrame.Width = textBody.Width
            htmlFrame.Height = textBody.Height
            htmlFrame.Margin = textBody.Margin
            If htmlInited Then
                textBody.Text = htmlEditor.InvokeScript("getText", Nothing)
            End If
            textBody.Focus(Windows.UI.Xaml.FocusState.Programmatic)
        Else
            textBody.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            htmlFrame.Visibility = Windows.UI.Xaml.Visibility.Visible
            If htmlInited Then
                htmlEditor.InvokeScript("setText", New String() {textBody.Text})
            Else
                htmlEditor.Navigate(New Uri("ms-appx-web:///htmlEditor/editor.html"))

            End If
        End If
    End Sub

    Private Sub imgFont_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If imgFont.SelectedIndex = 0 Then
            Return
        End If

        Dim font As String = DirectCast(imgFont.SelectedItem, [String])
        imgFont.SelectedIndex = 0

        htmlEditor.InvokeScript("execEditorCommand", New String() {"fontname", font})
    End Sub

    Private Sub imgSize_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If imgSize.SelectedIndex = 0 Then
            Return
        End If

        Dim size As Integer = imgSize.SelectedIndex
        imgSize.SelectedIndex = 0

        htmlEditor.InvokeScript("execEditorCommand", New String() {"fontsize", [String].Format("{0}", size)})
    End Sub

    Private Sub btnBold_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"Bold", ""})
    End Sub

    Private Sub imgItalic_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"Italic", ""})
    End Sub

    Private Sub imgUnderline_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"Underline", ""})
    End Sub

    Private Sub imgColor_Tapped(sender As Object, e As TappedRoutedEventArgs)
        lstColor.SelectedIndex = -1
        lstColor.IsDropDownOpen = True
        lstColor.Margin = imgColor.Margin
        lstColor.Visibility = Windows.UI.Xaml.Visibility.Visible
        BrushEditor(True)
    End Sub

    Private Sub lstColor_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If lstColor.SelectedIndex <> -1 Then
            Dim pi = TryCast(lstColor.SelectedItem, PropertyInfo)
            Dim c As Windows.UI.Color = DirectCast(pi.GetValue(Nothing), Windows.UI.Color)
            htmlEditor.InvokeScript("execEditorCommand", New String() {"ForeColor", [String].Format("#{0}{1}{2}", c.R.ToString("x2"), c.G.ToString("x2"), c.B.ToString("x2"))})
        End If
    End Sub

    Private Sub lstColor_DropDownClosed(sender As Object, e As Object)
        BrushEditor(False)
        lstColor.Visibility = Windows.UI.Xaml.Visibility.Collapsed
    End Sub

    Private Sub imgOrderedList_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"InsertOrderedList", ""})
    End Sub

    Private Sub imgUnorderedList_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"InsertUnorderedList", ""})
    End Sub

    Private Sub imgIndent_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"Indent", ""})
    End Sub

    Private Sub imgOutdent_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"Outdent", ""})
    End Sub

    Private Sub imgLeft_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"JustifyLeft", ""})
    End Sub

    Private Sub imgCenter_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"JustifyCenter", ""})
    End Sub

    Private Sub imgRight_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"JustifyRight", ""})
    End Sub

    Private Sub imgHr_Tapped(sender As Object, e As TappedRoutedEventArgs)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"InsertHorizontalRule", ""})
    End Sub

    Private Sub imgLink_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Dim dialog As New LinkDialog()

        Me.linkPopup = New Popup()
        Me.linkPopup.Child = dialog
        AddHandler dialog.CreateLinkRequested, AddressOf CreateLinkRequested
        AddHandler dialog.CancelRequested, AddressOf CreateLinkRequested
        Me.linkPopup.IsOpen = True

        BrushEditor(True)
    End Sub

    Private Sub CreateLinkRequested(sender As Object, e As EventArgs)
        Dim dialog As LinkDialog = DirectCast(Me.linkPopup.Child, LinkDialog)
        Dim link As String = dialog.LinkSrc
        Me.linkPopup.IsOpen = False

        BrushEditor(False)
        htmlEditor.InvokeScript("execEditorCommand", New String() {"CreateLink", link})
    End Sub

    Private Sub CancelLinkRequested(sender As Object, e As EventArgs)
        Me.linkPopup.IsOpen = False
        BrushEditor(False)
    End Sub

    Private Async Sub imgImage_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Dim openPicker As New FileOpenPicker()
        openPicker.ViewMode = PickerViewMode.List
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary
        openPicker.FileTypeFilter.Add(".jpg")
        openPicker.FileTypeFilter.Add(".png")
        openPicker.FileTypeFilter.Add(".gif")
        openPicker.FileTypeFilter.Add(".bmp")

        Dim file As StorageFile = Await openPicker.PickSingleFileAsync()
        If file Is Nothing Then
            Return
        End If

        Dim ext As String = file.Path
        Dim pos As Integer = ext.LastIndexOf("."c)
        If pos <> -1 Then
            ext = ext.Substring(pos + 1)
        End If

        Dim ct As String = "image/jpeg"
        If [String].Compare(ext, "png", StringComparison.OrdinalIgnoreCase) = 0 Then
            ct = "image/png"
        ElseIf [String].Compare(ext, "gif", StringComparison.OrdinalIgnoreCase) = 0 Then
            ct = "image/gif"
        ElseIf [String].Compare(ext, "bmp", StringComparison.OrdinalIgnoreCase) = 0 Then
            ct = "image/bmp"
        End If

        Dim f As StorageFile = Await StorageFile.GetFileFromPathAsync(file.Path)
        Dim fs As Stream = Await f.OpenStreamForReadAsync()
        Dim data As Byte() = New Byte(fs.Length - 1) {}
        Dim size As Integer = Await fs.ReadAsync(data, 0, CInt(fs.Length))
        Dim datasrc As String = System.Convert.ToBase64String(data, 0, data.Length)
        fs.Dispose()
        htmlEditor.InvokeScript("insertImage", New String() {file.Path, file.Name, datasrc, ct})
    End Sub

    Private Sub imgSize_DropDownOpened(sender As Object, e As Object)
        BrushEditor(True)
    End Sub

    Private Sub imgSize_DropDownClosed(sender As Object, e As Object)
        BrushEditor(False)
    End Sub


    Private Sub imgFont_DropDownOpened(sender As Object, e As Object)
        BrushEditor(True)
    End Sub

    Private Sub imgFont_DropDownClosed(sender As Object, e As Object)
        BrushEditor(False)
    End Sub


    Private Sub BrushEditor(b As Boolean)
        If b Then
            If rectEditor.Visibility = Windows.UI.Xaml.Visibility.Visible Then
                Dim brush As New WebViewBrush()
                brush.SourceName = "htmlEditor"
                brush.Redraw()
                rectEditor.Fill = brush
                htmlEditor.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            End If
        Else
            htmlEditor.Visibility = Windows.UI.Xaml.Visibility.Visible
            rectEditor.Fill = New SolidColorBrush(Windows.UI.Colors.Transparent)
        End If

    End Sub

    Private Sub chkHtml_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Dim b As Boolean = If((chkHtml.IsChecked.HasValue), CBool(chkHtml.IsChecked), False)
        If b Then
            editorMenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            htmlEditor.InvokeScript("OnViewHtmlSource", New String() {"true"})
        Else
            editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible
            htmlEditor.InvokeScript("OnViewHtmlSource", New String() {"false"})
        End If
    End Sub

    Private Sub htmlEditor_LoadCompleted(sender As Object, e As NavigationEventArgs)
        htmlInited = True
        htmlEditor.InvokeScript("setText", New String() {textBody.Text})
    End Sub


#End Region
End Class
