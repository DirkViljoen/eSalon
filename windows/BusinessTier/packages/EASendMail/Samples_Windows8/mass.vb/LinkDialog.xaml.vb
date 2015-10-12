' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class LinkDialog
    Inherits Page

    Public Event CreateLinkRequested As EventHandler
    Public Event CancelRequested As EventHandler
    Public LinkSrc As String = ""

    Public Sub New()
        Me.InitializeComponent()

        Dim bounds = Window.Current.Bounds
        Me.RootPanel.Width = bounds.Width

        Me.RootPanel.Height = bounds.Height
    End Sub
    ''' <summary>
    ''' Invoked when this page is about to be displayed in a Frame.
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.  The Parameter
    ''' property is typically used to configure the page.</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        LinkSrc = textLink.Text
        RaiseEvent CreateLinkRequested(Me, EventArgs.Empty)
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        LinkSrc = ""
        RaiseEvent CancelRequested(Me, EventArgs.Empty)
    End Sub
End Class
