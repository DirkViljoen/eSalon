
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

    Private m_total As Integer = 0
    Private m_success As Integer = 0
    Private m_failed As Integer = 0
    ''' <summary>
    ''' Invoked when this page is about to be displayed in a Frame.
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.  The Parameter
    ''' property is typically used to configure the page.</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)
    
    End Sub

    Public Class RecipientData
        Implements System.ComponentModel.INotifyPropertyChanged
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements _
            INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
        End Sub

        Public Sub New(addr As String, s As String, index As Integer)
            m_addr = addr
            m_index = index

            m_status = s
        End Sub
        Private m_addr As String = ""
        Private m_status As String = ""
        Private m_index As Integer = 0

        Public Property Address() As String
            Get
                Return m_addr
            End Get
            Set(value As String)

                If Me.m_addr <> value Then
                    Me.m_addr = value
                    Me.OnPropertyChanged("Address")
                End If
            End Set
        End Property

        Public Property Index() As Integer
            Get
                Return m_index
            End Get
            Set(value As Integer)
                If Me.m_index <> value Then
                    Me.m_index = value
                    Me.OnPropertyChanged("Index")
                End If
            End Set
        End Property

        Public Property Status() As String
            Get
                Return m_status
            End Get
            Set(value As String)
                If Me.m_status <> value Then
                    Me.m_status = value
                    Me.OnPropertyChanged("Status")
                End If
            End Set
        End Property

        Public ReadOnly Property Width() As Double
            Get
                Return Windows.UI.Xaml.Window.Current.Bounds.Width
            End Get
        End Property
    End Class

#Region "EASendMail Event Handler"
    Private Sub OnSecuring(sender As Object, e As SmtpStatusEventArgs)
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = CInt(oSmtp.Tag)
        UpdateRecipientItem(index, "Securing ... ")
    End Sub

    Private Sub OnAuthorized(sender As Object, e As SmtpStatusEventArgs)
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = CInt(oSmtp.Tag)
        UpdateRecipientItem(index, "Authorized")
    End Sub


    Public Sub OnConnected(sender As Object, e As SmtpStatusEventArgs)
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = CInt(oSmtp.Tag)
        UpdateRecipientItem(index, "Connected")
    End Sub

    Public Sub OnSendingDataStream(sender As Object, e As SmtpDataStreamEventArgs)
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = CInt(oSmtp.Tag)
        UpdateRecipientItem(index, [String].Format("{0}/{1} sent", e.Sent, e.Total))
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

        textBody.Text = "Hi," & vbCr & vbLf & vbCr & vbLf & "This is a simple test email sent from VB.NET Windows 8 Store Applicatoin (Metro) project with multiple threads." & vbCr & vbLf & "Please do not reply."

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
        gridCompose.Visibility = Windows.UI.Xaml.Visibility.Visible
        gridStatus.Visibility = Windows.UI.Xaml.Visibility.Collapsed

        m_total = 0
        m_success = 0
        m_failed = 0
        If textFrom.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input from address!")
            Await dlg.ShowAsync()
            textFrom.Text = ""
            textFrom.Focus(Windows.UI.Xaml.FocusState.Programmatic)
            Return
        End If

        If textTo.Text.Trim(vbCr & vbLf & " " & vbTab.ToCharArray()).Length = 0 Then
            Dim dlg As New MessageDialog("Please input a recipient at least!")
            Await dlg.ShowAsync()
            textTo.Text = ""
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

        If chkHtml.IsChecked.HasValue Then
            If CBool(chkHtml.IsChecked) Then
                editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible
                Await htmlEditor.InvokeScriptAsync("OnViewHtmlSource", New String() {"false"})
                chkHtml.IsChecked = True
            End If
        End If

        btnSend.IsEnabled = False
        lstRecipients.Items.Clear()

        m_cts = New CancellationTokenSource()

        Dim ar As String() = textTo.Text.Trim(vbCr & vbLf & " " & vbTab.ToCharArray()).Split(vbLf.ToCharArray())
        Dim arTo As List(Of String) = New List(Of String)()
        Dim n As Integer = 0
        For i As Integer = 0 To ar.Length - 1
            Dim addr As String = ar(i).Trim(vbCr & vbLf & " " & vbTab.ToCharArray())
            If addr.Length > 0 Then

                arTo.Add(addr)
            End If
        Next

        n = arTo.Count
        ar = arTo.ToArray()
        m_total = n
        textStatus.Text = [String].Format("Total {0}, success: {1}, failed {2}", m_total, m_success, m_failed)

        Dim arTask As New List(Of Task)()

        gridStatus.Height = 650
        gridStatus.Width = Windows.UI.Xaml.Window.Current.Bounds.Width
        gridStatus.Margin = New Thickness(0, 0, 0, 0)

        gridCompose.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        btnClose.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        gridStatus.Visibility = Windows.UI.Xaml.Visibility.Visible

        btnCancel.IsEnabled = True

        n = 0
        For i As Integer = 0 To ar.Length - 1
            Dim maxThreads As Integer = CInt(sdThreads.Value)
            While arTask.Count >= maxThreads
                Dim arT As Task() = arTask.ToArray()
                Dim taskFinished As Task = Await Task.WhenAny(arT)
                arTask.Remove(taskFinished)
                textStatus.Text = [String].Format("Total {0}, success: {1}, failed {2}", m_total, m_success, m_failed)
            End While

            Dim addr As String = ar(i).Trim(vbCr & vbLf & " " & vbTab.ToCharArray())
            lstRecipients.Items.Add(New RecipientData(addr, "Queued", n + 1))
            Dim index As Integer = n
            If m_cts.Token.IsCancellationRequested Then
                n += 1
                UpdateRecipientItem(index, "Operation was cancelled!")
                Continue For
            End If

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
            ' oMail.Headers.RemoveKey("From");
            oMail.[To] = New AddressCollection(addr)
            oMail.Subject = textSubject.Text

            Dim bodyText As String = ""
            Dim htmlBody As Boolean = False
            If lstFormat.SelectedIndex = 0 Then
                bodyText = textBody.Text
            Else
                bodyText = Await htmlEditor.InvokeScriptAsync("getHtml", Nothing)
                htmlBody = True
            End If

            Dim count As Integer = m_atts.Count
            Dim atts As String() = New String(count - 1) {}
            For x As Integer = 0 To count - 1
                atts(x) = m_atts(x)
            Next

            Dim subtask As Task = Task.Factory.StartNew(
                Sub()
                    SubmitMail(oServer, oMail, atts, bodyText, htmlBody, index).Wait()
                End Sub)

            arTask.Add(subtask)
            n += 1

        Next


        If arTask.Count > 0 Then
            Await Task.WhenAll(arTask.ToArray())
        End If

        textStatus.Text = [String].Format("Total {0}, success: {1}, failed {2}", m_total, m_success, m_failed)

        btnSend.IsEnabled = True
        btnCancel.IsEnabled = False
        btnClose.Visibility = Windows.UI.Xaml.Visibility.Visible
    End Sub

    Private Sub UpdateRecipientItem(index As Integer, status As String)
        If Me.Dispatcher.HasThreadAccess Then
            Dim data = TryCast(lstRecipients.Items(index), RecipientData)
            data.Status = status
            Return
        End If

        Me.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            Sub()
                Dim data = TryCast(lstRecipients.Items(index), RecipientData)
                data.Status = status
            End Sub).AsTask().Wait()

    End Sub

    Private Async Function SubmitMail(oServer As SmtpServer,
                                      oMail As SmtpMail,
                                      atts As String(),
                                      bodyText As String, htmlBody As Boolean, index As Integer) As Task

        Dim oSmtp As SmtpClient = Nothing
        Try
            oSmtp = New SmtpClient()
            AddHandler oSmtp.Authorized, AddressOf OnAuthorized
            AddHandler oSmtp.Connected, AddressOf OnConnected
            AddHandler oSmtp.Securing, AddressOf OnSecuring
            AddHandler oSmtp.SendingDataStream, AddressOf OnSendingDataStream

            UpdateRecipientItem(index, "Preparing ...")

            If Not htmlBody Then
                oMail.TextBody = bodyText
            Else
                Dim html As String = bodyText
                html = "<html><head><meta charset=""utf-8"" /></head><body style=""font-family:Calibri;font-size: 15px;"">" & html & "<body></html>"
                Await oMail.ImportHtmlAsync(html, Windows.ApplicationModel.Package.Current.InstalledLocation.Path, ImportHtmlBodyOptions.ErrorThrowException Or ImportHtmlBodyOptions.ImportLocalPictures Or ImportHtmlBodyOptions.ImportHttpPictures Or ImportHtmlBodyOptions.ImportCss)
            End If

            Dim count As Integer = atts.Length
            For i As Integer = 0 To count - 1
                Await oMail.AddAttachmentAsync(atts(i))
            Next


            UpdateRecipientItem(index, [String].Format("Connecting {0} ...", oServer.Server))
            oSmtp.Tag = index

            ' You can genereate a log file by the following code.
            ' oSmtp.LogFileName = "ms-appdata:///local/smtp.txt"
            Dim asyncObject As IAsyncAction = oSmtp.SendMailAsync(oServer, oMail)
            m_cts.Token.Register(Sub()
                                     asyncObject.Cancel()
                                 End Sub)

            Await asyncObject

            Interlocked.Increment(m_success)

            UpdateRecipientItem(index, "Completed")
        Catch ep As Exception
            oSmtp.Close()
            Dim errDescription As String = ep.Message
            UpdateRecipientItem(index, errDescription)
            Interlocked.Increment(m_failed)
        End Try

    End Function
    Private Sub btnCancel_Tapped(sender As Object, e As TappedRoutedEventArgs)
        btnCancel.IsEnabled = False
        If m_cts IsNot Nothing Then
            m_cts.Cancel(True)
        End If
    End Sub

    Private Sub btnClose_Tapped(sender As Object, e As TappedRoutedEventArgs)
        gridStatus.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        gridCompose.Visibility = Windows.UI.Xaml.Visibility.Visible
        lstRecipients.Items.Clear()
    End Sub

#Region "HTML Editor"

    Private Async Sub lstFormat_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If lstFormat.SelectedIndex = 0 Then
            textBody.Visibility = Windows.UI.Xaml.Visibility.Visible
            htmlFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            htmlFrame.Width = textBody.Width
            htmlFrame.Height = textBody.Height
            htmlFrame.Margin = textBody.Margin
            If htmlInited Then
                textBody.Text = Await htmlEditor.InvokeScriptAsync("getText", Nothing)
            End If
            textBody.Focus(Windows.UI.Xaml.FocusState.Programmatic)
        Else
            textBody.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            htmlFrame.Visibility = Windows.UI.Xaml.Visibility.Visible
            If htmlInited Then
                Await htmlEditor.InvokeScriptAsync("setText", New String() {textBody.Text})
            Else
                htmlEditor.Navigate(New Uri("ms-appx-web:///htmlEditor/editor.html"))

            End If
        End If
    End Sub

    Private Async Sub imgFont_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If imgFont.SelectedIndex = 0 Then
            Return
        End If

        Dim font As String = DirectCast(imgFont.SelectedItem, [String])
        imgFont.SelectedIndex = 0

        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"fontname", font})
    End Sub

    Private Async Sub imgSize_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If imgSize.SelectedIndex = 0 Then
            Return
        End If

        Dim size As Integer = imgSize.SelectedIndex
        imgSize.SelectedIndex = 0

        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"fontsize", [String].Format("{0}", size)})
    End Sub

    Private Async Sub btnBold_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"Bold", ""})
    End Sub

    Private Async Sub imgItalic_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"Italic", ""})
    End Sub

    Private Async Sub imgUnderline_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"Underline", ""})
    End Sub

    Private Sub imgColor_Tapped(sender As Object, e As TappedRoutedEventArgs)
        lstColor.SelectedIndex = -1
        lstColor.IsDropDownOpen = True
        lstColor.Margin = imgColor.Margin
        lstColor.Visibility = Windows.UI.Xaml.Visibility.Visible
    End Sub

    Private Async Sub lstColor_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If lstColor.SelectedIndex <> -1 Then
            Dim pi = TryCast(lstColor.SelectedItem, PropertyInfo)
            Dim c As Windows.UI.Color = DirectCast(pi.GetValue(Nothing), Windows.UI.Color)
            Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"ForeColor", [String].Format("#{0}{1}{2}", c.R.ToString("x2"), c.G.ToString("x2"), c.B.ToString("x2"))})
        End If
    End Sub

    Private Sub lstColor_DropDownClosed(sender As Object, e As Object)
        lstColor.Visibility = Windows.UI.Xaml.Visibility.Collapsed
    End Sub

    Private Async Sub imgOrderedList_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"InsertOrderedList", ""})
    End Sub

    Private Async Sub imgUnorderedList_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"InsertUnorderedList", ""})
    End Sub

    Private Async Sub imgIndent_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"Indent", ""})
    End Sub

    Private Async Sub imgOutdent_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"Outdent", ""})
    End Sub

    Private Async Sub imgLeft_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"JustifyLeft", ""})
    End Sub

    Private Async Sub imgCenter_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"JustifyCenter", ""})
    End Sub

    Private Async Sub imgRight_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"JustifyRight", ""})
    End Sub

    Private Async Sub imgHr_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"InsertHorizontalRule", ""})
    End Sub

    Private Sub imgLink_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Dim dialog As New LinkDialog()

        Me.linkPopup = New Popup()
        Me.linkPopup.Child = dialog
        AddHandler dialog.CreateLinkRequested, AddressOf CreateLinkRequested
        AddHandler dialog.CancelRequested, AddressOf CreateLinkRequested
        Me.linkPopup.IsOpen = True

    End Sub

    Private Async Sub CreateLinkRequested(sender As Object, e As EventArgs)
        Dim dialog As LinkDialog = DirectCast(Me.linkPopup.Child, LinkDialog)
        Dim link As String = dialog.LinkSrc
        Me.linkPopup.IsOpen = False
        Await htmlEditor.InvokeScriptAsync("execEditorCommand", New String() {"CreateLink", link})
    End Sub

    Private Sub CancelLinkRequested(sender As Object, e As EventArgs)
        Me.linkPopup.IsOpen = False
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
        fs.Dispose()
        Dim datasrc As String = System.Convert.ToBase64String(data, 0, data.Length)
        Await htmlEditor.InvokeScriptAsync("insertImage", New String() {file.Path, file.Name, datasrc, ct})
    End Sub

    

    Private Async Sub chkHtml_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Dim b As Boolean = If((chkHtml.IsChecked.HasValue), CBool(chkHtml.IsChecked), False)
        If b Then
            editorMenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            Await htmlEditor.InvokeScriptAsync("OnViewHtmlSource", New String() {"true"})
        Else
            editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible
            Await htmlEditor.InvokeScriptAsync("OnViewHtmlSource", New String() {"false"})
        End If
    End Sub

    Private Async Sub htmlEditor_NavigationCompleted(sender As WebView, args As WebViewNavigationCompletedEventArgs) Handles htmlEditor.NavigationCompleted
        htmlInited = True
        Await htmlEditor.InvokeScriptAsync("setText", New String() {textBody.Text})
    End Sub
#End Region


End Class
