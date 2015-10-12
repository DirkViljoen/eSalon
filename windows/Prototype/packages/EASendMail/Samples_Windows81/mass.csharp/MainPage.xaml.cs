//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2013  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// |    Project: It demonstrates how to use EASendMail to send text/html email with 
// |             c# + multiple threads in Windows 8 Application.
// |
// |    File: Form1 : implementation file
// |
// |    Author: Ivan Lui ( ivan@emailarchitect.net )
//  ===============================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using System.Reflection;
using EASendMailRT;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace mass.csharp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }



        private Popup linkPopup;
        private CancellationTokenSource m_cts = null;
        private List<string> m_atts = new List<string>();
        private bool htmlInited = false;
        private int m_total = 0;
        private int m_success = 0;
        private int m_failed = 0;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public class RecipientData : System.ComponentModel.INotifyPropertyChanged
        {
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }

            public RecipientData(string addr, string s, int index )
            {
                m_addr = addr;
                m_index = index;
                m_status = s;

            }
            private  string m_addr = "";
            private  string m_status = "";
            private int m_index = 0;

            public string Address
            {
                get
                {
                    return m_addr;
                }
                set
                {
                    
                    if (this.m_addr != value)
                    {
                        this.m_addr = value;
                        this.OnPropertyChanged("Address");
                    }
                }
            }
            
            public int Index
            {
                get
                {
                    return m_index;
                }
                set
                {
                    if (this.m_index != value)
                    {
                        this.m_index = value;
                        this.OnPropertyChanged("Index");
                    }
                }
            }

            public string Status
            {
                get
                {
                    return m_status;
                }
                set
                {
                    if (this.m_status != value)
                    {
                        this.m_status = value;
                        this.OnPropertyChanged("Status");
                    }
                }
            }

            public double Width
            {
                get
                {
                    return Windows.UI.Xaml.Window.Current.Bounds.Width;
                }
            }
        }

        #region EASendMail Event Handler
        private void OnSecuring(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            SmtpClient oSmtp = sender as SmtpClient;
            int index = (int)oSmtp.Tag;
            UpdateRecipientItem( index,  "Securing ... " );
        }

        private void OnAuthorized(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            SmtpClient oSmtp = sender as SmtpClient;
            int index = (int)oSmtp.Tag;
            UpdateRecipientItem(index, "Authorized");
        }


        public void OnConnected( 
            object sender,
            SmtpStatusEventArgs e
        )
        {
            SmtpClient oSmtp = sender as SmtpClient;
            int index = (int)oSmtp.Tag;
            UpdateRecipientItem(index, "Connected");
        }

        public void OnSendingDataStream(
            object sender,
            SmtpDataStreamEventArgs e
        )
        {
            SmtpClient oSmtp = sender as SmtpClient;
            int index = (int)oSmtp.Tag;
            UpdateRecipientItem(index, String.Format("{0}/{1} sent", e.Sent, e.Total));
        }

        #endregion

        private void chkAuth_Click(object sender, RoutedEventArgs e)
        {
            bool? b = chkAuth.IsChecked;
            if (!b.HasValue)
            {
                return;
            }

            textUser.IsEnabled = (bool)chkAuth.IsChecked;
            textPassword.IsEnabled = (bool)chkAuth.IsChecked;

        }


        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            lstProtocols.Items.Add("SMTP Protocol - Recommended");
            lstProtocols.Items.Add("Exchange Web Service - 2007/2010/2013");
            lstProtocols.Items.Add("Exchange WebDAV - 2000/2003");

            lstProtocols.SelectedIndex = 0;

            lstFormat.Items.Add("Body Format: Text/Plain");
            lstFormat.Items.Add("Body Format: Text/Html");
            lstFormat.SelectedIndex = 0;

            textBody.Text = "Hi,\r\n\r\nThis is a simple test email sent from C# Windows 8 Store Applicatoin (Metro) project with multiple threads.\r\nPlease do not reply.";

            imgSize.Items.Add("Size");
            for (int i = 0; i < 7; i++)
            {
                imgSize.Items.Add(String.Format("{0}", i + 1));
            }

            imgSize.SelectedIndex = 0;

            imgFont.Items.Add("Arial");
            imgFont.Items.Add("Calibri");
            imgFont.Items.Add("Comic Sans MS");
            imgFont.Items.Add("Courier New");
            imgFont.Items.Add("Times New Roman");
            imgFont.Items.Add("Tahoma");
            imgFont.Items.Add("Verdana");
            imgFont.Items.Add("Segoe UI");

            imgFont.SelectedIndex = 0;

            var colors = typeof(Windows.UI.Colors).GetTypeInfo().DeclaredProperties;
            foreach (var item in colors)
            {
                lstColor.Items.Add(item);
            }

            lstColor.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            textFrom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private async void btnAtts_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add("*");

            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            if (files.Count == 0)
                return;

            string s = "";
            foreach (StorageFile file in files)
            {
                s += file.Name;
                s += ";";
                m_atts.Add(file.Path);
            }

            textAtts.Text += s;
        }

        private void btnClear_Tapped(object sender, TappedRoutedEventArgs e)
        {
            m_atts.Clear();
            textAtts.Text = "";
        }

        private async void btnSend_Tapped(object sender, TappedRoutedEventArgs e)
        {
            gridCompose.Visibility = Windows.UI.Xaml.Visibility.Visible;
            gridStatus.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            m_total = 0; m_success = 0; m_failed = 0;
            if (textFrom.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input from address!");
                await dlg.ShowAsync();
                textFrom.Text = "";
                textFrom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            if (textTo.Text.Trim("\r\n \t".ToCharArray()).Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input a recipient at least!");
                await dlg.ShowAsync();
                textTo.Text = "";
                textTo.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            if (textServer.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input server address!");
                await dlg.ShowAsync();
                textServer.Text = "";
                textServer.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }


            bool bAuth = (chkAuth.IsChecked.HasValue) ? (bool)chkAuth.IsChecked : false;
            if (bAuth)
            {
                if (textUser.Text.Trim().Length == 0)
                {
                    MessageDialog dlg = new MessageDialog("Please input user name!");
                    await dlg.ShowAsync();
                    textUser.Text = "";
                    textUser.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                    return;
                }

                if (textPassword.Password.Trim().Length == 0)
                {
                    MessageDialog dlg = new MessageDialog("Please input password!");
                    await dlg.ShowAsync();
                    textPassword.Password = "";
                    textPassword.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                    return;
                }
            }

            if (chkHtml.IsChecked.HasValue)
            {
                if ((bool)chkHtml.IsChecked)
                {
                    editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    await htmlEditor.InvokeScriptAsync("OnViewHtmlSource", new string[] { "false" });
                    chkHtml.IsChecked = true;
                }
            }

            btnSend.IsEnabled = false;
            lstRecipients.Items.Clear();

            m_cts = new CancellationTokenSource();

            List<Task> arTask = new List<Task>();

            gridStatus.Height = 650;
            gridStatus.Width = Windows.UI.Xaml.Window.Current.Bounds.Width;
            gridStatus.Margin = new Thickness(0, 0, 0, 0);

            gridCompose.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            btnClose.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            gridStatus.Visibility = Windows.UI.Xaml.Visibility.Visible;

          
            string[] ar = textTo.Text.Trim("\r\n \t".ToCharArray()).Split("\n".ToCharArray());
            List<string> arTo = new List<string>();
           
            for (int i = 0; i < ar.Length; i++)
            {
                string addr = ar[i].Trim("\r\n \t".ToCharArray());
                if (addr.Length > 0)
                {
                    arTo.Add(addr);
                }
            }

            int n = arTo.Count;
            ar = arTo.ToArray();
            
            m_total = n;
            textStatus.Text = String.Format("Total {0}, success: {1}, failed {2}",
                m_total, m_success, m_failed);

            btnCancel.IsEnabled = true;
           
            n = 0;
            for (int i = 0; i < ar.Length; i++)
            {
                int maxThreads = (int)sdThreads.Value;
                while (arTask.Count >= maxThreads)
                {
                    Task[] arT = arTask.ToArray();
                    Task taskFinished = await Task.WhenAny(arT);
                    arTask.Remove(taskFinished);
                    textStatus.Text = String.Format("Total {0}, success: {1}, failed {2}",
                    m_total, m_success, m_failed);
                }


                string addr = ar[i];
                int index = n;
                lstRecipients.Items.Add(new RecipientData(addr, "Queued", n + 1));
                if (m_cts.Token.IsCancellationRequested)
                {
                    n++;
                    UpdateRecipientItem(index, "Operation was cancelled!");
                    continue;
                }

                SmtpServer oServer = new SmtpServer(textServer.Text);
                bool bSSL = (chkSSL.IsChecked.HasValue) ? (bool)chkSSL.IsChecked : false;
                if (bSSL)
                {
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                }

                oServer.Protocol = (ServerProtocol)lstProtocols.SelectedIndex;
                if (bAuth)
                {
                    oServer.User = textUser.Text;
                    oServer.Password = textPassword.Password;
                }

                // For evaluation usage, please use "TryIt" as the license code, otherwise the 
                // "Invalid License Code" exception will be thrown. However, the trial object only can be used 
                // with developer license

                // For licensed usage, please use your license code instead of "TryIt", then the object
                // can used with published windows store application.
                SmtpMail oMail = new SmtpMail("TryIt");

                oMail.From = new MailAddress(textFrom.Text);

                // If your Exchange Server is 2007 and used Exchange Web Service protocol, please add the following line;
                // oMail.Headers.RemoveKey("From");
                oMail.To = new AddressCollection(addr);
                oMail.Subject = textSubject.Text;

                string bodyText = "";
                bool htmlBody = false;
                if (lstFormat.SelectedIndex == 0)
                {
                    bodyText = textBody.Text;
                }
                else
                {
                    bodyText = await htmlEditor.InvokeScriptAsync("getHtml", null);
                    htmlBody = true;
                }

                int count = m_atts.Count;
                string[] atts = new string[count];
                for (int x = 0; x < count; x++)
                {
                    atts[x] = m_atts[x];
                }

                Task task = Task.Factory.StartNew(() => SubmitMail(oServer, oMail, atts, bodyText, htmlBody, index).Wait());
                arTask.Add(task);
                n++;
            }


            if (arTask.Count > 0)
            {
                await Task.WhenAll(arTask.ToArray());
            }

            textStatus.Text = String.Format("Total {0}, success: {1}, failed {2}",
                   m_total, m_success, m_failed);

            btnSend.IsEnabled = true;
            btnCancel.IsEnabled = false;
            btnClose.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void UpdateRecipientItem(int index, string status)
        {
            if (this.Dispatcher.HasThreadAccess)
            {
                var data = lstRecipients.Items[index] as RecipientData;
                data.Status = status;
                return;
            }

            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>{
                        var data = lstRecipients.Items[index] as RecipientData;
                        data.Status = status;
                    }).AsTask().Wait();

        }

        private async Task SubmitMail(
             SmtpServer oServer, SmtpMail oMail, string[] atts,
             string bodyText, bool htmlBody, int index )
        {
           
            SmtpClient oSmtp = null;
            try
            {
                oSmtp = new SmtpClient();
               // oSmtp.TaskCancellationToken = m_cts.Token;
               // oSmtp.Authorized += new SmtpClient.OnAuthorizedEventHandler(OnAuthorized);
                oSmtp.Connected += OnConnected;
              //  oSmtp.Securing += new SmtpClient.OnSecuringEventHandler(OnSecuring);
                //oSmtp.SendingDataStream +=
                  //  new SmtpClient.OnSendingDataStreamEventHandler(OnSendingDataStream);

                UpdateRecipientItem(index, "Preparing ...");
               
                if ( !htmlBody )
                {
                    oMail.TextBody = bodyText;
                }
                else
                {
                    string html = bodyText;
                    html = "<html><head><meta charset=\"utf-8\" /></head><body style=\"font-family:Calibri;font-size: 15px;\">" + html + "<body></html>";
                    await oMail.ImportHtmlAsync(html,
                        Windows.ApplicationModel.Package.Current.InstalledLocation.Path,
                        ImportHtmlBodyOptions.ErrorThrowException | ImportHtmlBodyOptions.ImportLocalPictures
                        | ImportHtmlBodyOptions.ImportHttpPictures | ImportHtmlBodyOptions.ImportCss);
                }

                int count = atts.Length;
                for (int i = 0; i < count; i++)
                {
                    await oMail.AddAttachmentAsync(atts[i]);
                }


                UpdateRecipientItem(index, String.Format("Connecting {0} ...", oServer.Server));
                oSmtp.Tag = index;

                // You can genereate a log file by the following code.
                // oSmtp.LogFileName = "ms-appdata:///local/smtp.txt";

                IAsyncAction asynCancelSend = oSmtp.SendMailAsync(oServer, oMail);
                m_cts.Token.Register(() => asynCancelSend.Cancel());
                await asynCancelSend;

                Interlocked.Increment(ref m_success);
                UpdateRecipientItem(index, "Completed");

            }
           catch (Exception ep)
            {
                oSmtp.Close();
                string errDescription = ep.Message;
                UpdateRecipientItem(index, errDescription);
                Interlocked.Increment(ref m_failed);
            }

        }
        

        private void btnCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            btnCancel.IsEnabled = false;
            if (m_cts != null)
            {
                m_cts.Cancel(true);
            }
           
        }

        #region HTML Editor

        private async void lstFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFormat.SelectedIndex == 0)
            {
                textBody.Visibility = Windows.UI.Xaml.Visibility.Visible;
                htmlFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                htmlFrame.Width = textBody.Width;
                htmlFrame.Height = textBody.Height;
                htmlFrame.Margin = textBody.Margin;
                if (htmlInited)
                {
                    textBody.Text = await htmlEditor.InvokeScriptAsync("getText", null);
                }
                textBody.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
            else
            {
                textBody.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                htmlFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
                if (htmlInited)
                {
                    await htmlEditor.InvokeScriptAsync("setText", new string[] { textBody.Text });
                }
                else
                {
                    htmlEditor.Navigate(new Uri("ms-appx-web:///htmlEditor/editor.html"));
                }

            }
        }

        private async void imgFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imgFont.SelectedIndex == 0)
                return;

            string font = (String)imgFont.SelectedItem;
            imgFont.SelectedIndex = 0;

            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "fontname", font });
        }

        private async void imgSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imgSize.SelectedIndex == 0)
                return;

            int size = imgSize.SelectedIndex;
            imgSize.SelectedIndex = 0;

            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "fontsize", String.Format("{0}", size) });
        }

        private async void btnBold_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "Bold", "" });
        }

        private async void imgItalic_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "Italic", "" });
        }

        private async void imgUnderline_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "Underline", "" });
        }

        private void imgColor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lstColor.SelectedIndex = -1;
            lstColor.IsDropDownOpen = true;
            lstColor.Margin = imgColor.Margin;
            lstColor.Visibility = Windows.UI.Xaml.Visibility.Visible;
            
        }

        private async void lstColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstColor.SelectedIndex != -1)
            {
                var pi = lstColor.SelectedItem as PropertyInfo;
                Windows.UI.Color c = (Windows.UI.Color)pi.GetValue(null);
                await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "ForeColor", 
                    String.Format("#{0}{1}{2}", c.R.ToString("x2"), c.G.ToString("x2"), c.B.ToString("x2"))});
            }
        }

        private void lstColor_DropDownClosed(object sender, object e)
        {
            
            lstColor.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private async void imgOrderedList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "InsertOrderedList", "" });
        }

        private async void imgUnorderedList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "InsertUnorderedList", "" });
        }

        private async void imgIndent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "Indent", "" });
        }

        private async void imgOutdent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "Outdent", "" });
        }

        private async void imgLeft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "JustifyLeft", "" });
        }

        private async void imgCenter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "JustifyCenter", "" });
        }

        private async void imgRight_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "JustifyRight", "" });
        }

        private async void imgHr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "InsertHorizontalRule", "" });
        }

        private void imgLink_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LinkDialog dialog = new LinkDialog();

            this.linkPopup = new Popup();
            this.linkPopup.Child = dialog;
            dialog.CreateLinkRequested += CreateLinkRequested;
            dialog.CancelRequested += CreateLinkRequested;
            this.linkPopup.IsOpen = true;

        }

        private async void CreateLinkRequested(object sender, EventArgs e)
        {
            LinkDialog dialog = (LinkDialog)this.linkPopup.Child;
            string link = dialog.LinkSrc;
            this.linkPopup.IsOpen = false;
            await htmlEditor.InvokeScriptAsync("execEditorCommand", new string[] { "CreateLink", link });
        }

        private void CancelLinkRequested(object sender, EventArgs e)
        {
            this.linkPopup.IsOpen = false;
        }

        private async void imgImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".gif");
            openPicker.FileTypeFilter.Add(".bmp");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file == null)
                return;

            string ext = file.Path;
            int pos = ext.LastIndexOf('.');
            if (pos != -1)
            {
                ext = ext.Substring(pos + 1);
            }

            string ct = "image/jpeg";
            if (String.Compare(ext, "png", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/png";
            }
            else if (String.Compare(ext, "gif", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/gif";
            }
            else if (String.Compare(ext, "bmp", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/bmp";
            }

            StorageFile f = await StorageFile.GetFileFromPathAsync(file.Path);
            Stream fs = await f.OpenStreamForReadAsync();
            byte[] data = new byte[fs.Length];
            int size = await fs.ReadAsync(data, 0, (int)fs.Length);
            string datasrc = System.Convert.ToBase64String(data, 0, data.Length);
            fs.Dispose();
            await htmlEditor.InvokeScriptAsync("insertImage", new string[] { file.Path, file.Name, datasrc, ct });
        }

       
        private async void chkHtml_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bool b = (chkHtml.IsChecked.HasValue) ? (bool)chkHtml.IsChecked : false;
            if (b)
            {
                editorMenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                await htmlEditor.InvokeScriptAsync("OnViewHtmlSource", new string[] { "true" });
            }
            else
            {
                editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
                await htmlEditor.InvokeScriptAsync("OnViewHtmlSource", new string[] { "false" });
            }
        }


        private async void htmlEditor_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            htmlInited = true;
            await htmlEditor.InvokeScriptAsync("setText", new string[] { textBody.Text });
        }

        #endregion

        private void btnClose_Tapped(object sender, TappedRoutedEventArgs e)
        {
            gridStatus.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            gridCompose.Visibility = Windows.UI.Xaml.Visibility.Visible;
            lstRecipients.Items.Clear();
        }






























    }
}
