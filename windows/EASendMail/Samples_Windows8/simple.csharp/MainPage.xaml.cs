//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2013  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// |    Project: It demonstrates how to use EASendMail to send text/html email with 
// |             c# in Windows 8 Application.
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

namespace simple.csharp
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
        private IAsyncAction asyncCancel = null;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

 #region EASendMail Event Handler
        private  void OnSecuring(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            textStatus.Text = "Securing ... ";
        }

        private void OnAuthorized(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            textStatus.Text = "Authorized";
        }


        public void OnConnected(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            textStatus.Text = "Connected";
        }

        public void OnSendingDataStream(
            object sender,
            SmtpDataStreamEventArgs  e
        )
        {
            textStatus.Text = String.Format("{0}/{1} sent", e.Sent, e.Total );
            pgBar.Maximum = e.Total;
            pgBar.Value = e.Sent;
            
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

            textBody.Text = "Hi,\r\n\r\nThis is a simple test email sent from C# Windows 8 Store Applicatoin (Metro) project.\r\nPlease do not reply.";

            imgSize.Items.Add("Size");
            for (int i = 0; i < 7; i++)
            {
                imgSize.Items.Add(String.Format("{0}", i+1));
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
            if (textFrom.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input from address!");
                await dlg.ShowAsync();
                textFrom.Text = "";
                textFrom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            if (textTo.Text.Trim().Length == 0 && textCc.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input a recipient at least!");
                await dlg.ShowAsync();
                textTo.Text = ""; textCc.Text = "";
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

            btnSend.IsEnabled = false;
            pgBar.Value = 0;

            try
            {
                SmtpClient oSmtp = new SmtpClient();

                oSmtp.Authorized +=  OnAuthorized;
                oSmtp.Connected += OnConnected;
                oSmtp.Securing +=  OnSecuring;
                oSmtp.SendingDataStream += OnSendingDataStream;

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
                oMail.To = new AddressCollection(textTo.Text);
                oMail.Cc = new AddressCollection(textCc.Text);
                oMail.Subject = textSubject.Text;

                if (lstFormat.SelectedIndex == 0)
                {
                    oMail.TextBody = textBody.Text;
                }
                else
                {
                    if (chkHtml.IsChecked.HasValue)
                    {
                        if ((bool)chkHtml.IsChecked)
                        {
                            editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
                            htmlEditor.InvokeScript("OnViewHtmlSource", new string[] { "false" });
                            chkHtml.IsChecked = true;
                        }
                    }
                    string html = htmlEditor.InvokeScript("getHtml", null);
                    html = "<html><head><meta charset=\"utf-8\" /></head><body style=\"font-family:Calibri;font-size: 15px;\">" + html + "<body></html>";
                    await oMail.ImportHtmlAsync(html,
                        Windows.ApplicationModel.Package.Current.InstalledLocation.Path,
                        ImportHtmlBodyOptions.ErrorThrowException | ImportHtmlBodyOptions.ImportLocalPictures
                        | ImportHtmlBodyOptions.ImportHttpPictures | ImportHtmlBodyOptions.ImportCss);
                }

                int count = m_atts.Count;
                for (int i = 0; i < count; i++)
                {
                    await oMail.AddAttachmentAsync(m_atts[i]);
                }
                btnCancel.IsEnabled = true;

                textStatus.Text = String.Format("Connecting {0} ...", oServer.Server);
                // You can genereate a log file by the following code.
                // oSmtp.LogFileName = "ms-appdata:///local/smtp.txt";
                asyncCancel = oSmtp.SendMailAsync(oServer, oMail);
                await asyncCancel;

                textStatus.Text = "Completed";

            }
            catch (Exception ep)
            {
                textStatus.Text = "Error:  " + ep.Message;
            }

            asyncCancel = null;
            btnSend.IsEnabled = true;
            btnCancel.IsEnabled = false;
        }
        private void btnCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            btnCancel.IsEnabled = false;
            if (asyncCancel != null)
            {
                asyncCancel.Cancel();
            }
        }

        #region HTML Editor 

        private void lstFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    textBody.Text = htmlEditor.InvokeScript("getText", null);
                }
                textBody.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
            else
            {
                textBody.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                htmlFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
                if (htmlInited)
                {
                    htmlEditor.InvokeScript("setText", new string[] { textBody.Text });
                }
                else
                {
                    htmlEditor.Navigate(new Uri("ms-appx-web:///htmlEditor/editor.html"));
                }
                
            }
        }

        private void imgFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imgFont.SelectedIndex == 0)
                return;

            string font = (String)imgFont.SelectedItem;
            imgFont.SelectedIndex = 0;

            htmlEditor.InvokeScript("execEditorCommand", new string[] { "fontname", font});
        }

        private void imgSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imgSize.SelectedIndex == 0)
                return;

            int size = imgSize.SelectedIndex;
            imgSize.SelectedIndex = 0;

            htmlEditor.InvokeScript("execEditorCommand", new string[] { "fontsize", String.Format("{0}", size) });
        }

        private void btnBold_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript( "execEditorCommand", new string[]{"Bold", "" });
        }

        private void imgItalic_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "Italic", "" });
        }

        private void imgUnderline_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "Underline", "" });
        }

        private void imgColor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lstColor.SelectedIndex = -1;
            lstColor.IsDropDownOpen = true;
            lstColor.Margin = imgColor.Margin;
            lstColor.Visibility = Windows.UI.Xaml.Visibility.Visible;
            BrushEditor(true);
        }

        private void lstColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstColor.SelectedIndex != -1)
            {
                var pi = lstColor.SelectedItem as PropertyInfo;
                Windows.UI.Color c = (Windows.UI.Color)pi.GetValue(null);
                htmlEditor.InvokeScript("execEditorCommand", new string[] { "ForeColor", 
                    String.Format("#{0}{1}{2}", c.R.ToString("x2"), c.G.ToString("x2"), c.B.ToString("x2"))});
            }
        }

        private void lstColor_DropDownClosed(object sender, object e)
        {
            BrushEditor(false);
            lstColor.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        
        private void imgOrderedList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "InsertOrderedList", "" });
        }

        private void imgUnorderedList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "InsertUnorderedList", "" });
        }

        private void imgIndent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "Indent", "" });
        }

        private void imgOutdent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "Outdent", "" });
        }

        private void imgLeft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "JustifyLeft", "" });
        }

        private void imgCenter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "JustifyCenter", "" });
        }

        private void imgRight_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "JustifyRight", "" });
        }

        private void imgHr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "InsertHorizontalRule", "" });
        }

        private void imgLink_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LinkDialog dialog = new LinkDialog();
           
            this.linkPopup = new Popup();
            this.linkPopup.Child = dialog;
            dialog.CreateLinkRequested += CreateLinkRequested;
            dialog.CancelRequested += CreateLinkRequested;
            this.linkPopup.IsOpen = true;
            
            BrushEditor(true);
        }

        private void CreateLinkRequested(object sender, EventArgs e)
        {
            LinkDialog dialog = (LinkDialog)this.linkPopup.Child;
            string link = dialog.LinkSrc;
            this.linkPopup.IsOpen = false;
            
            BrushEditor(false);
            htmlEditor.InvokeScript("execEditorCommand", new string[] { "CreateLink", link });
        }

        private void CancelLinkRequested(object sender, EventArgs e)
        {
            this.linkPopup.IsOpen = false;
            BrushEditor(false);
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
            if( String.Compare( ext, "png", StringComparison.OrdinalIgnoreCase ) == 0 )
            {
                ct = "image/png";
            }
            else if (String.Compare(ext, "gif", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/gif";
            }
            else if(String.Compare(ext, "bmp", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/bmp";
            }

            StorageFile f = await StorageFile.GetFileFromPathAsync(file.Path);
            Stream fs = await f.OpenStreamForReadAsync();
            byte [] data = new byte[fs.Length];
            int size =  await fs.ReadAsync(data, 0, (int)fs.Length);
            string datasrc = System.Convert.ToBase64String(data, 0, data.Length);
            fs.Dispose();
            htmlEditor.InvokeScript("insertImage", new string[] { file.Path, file.Name, datasrc, ct });
        }

        private void imgSize_DropDownOpened(object sender, object e)
        {
            BrushEditor(true);
        }

        private void imgSize_DropDownClosed(object sender, object e)
        {
            BrushEditor(false);
        }


        private void imgFont_DropDownOpened(object sender, object e)
        {
            BrushEditor(true);
        }

        private void imgFont_DropDownClosed(object sender, object e)
        {
            BrushEditor(false);
        }

       
        private void BrushEditor(bool b)
        {
            if (b)
            {
                if (rectEditor.Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    WebViewBrush brush = new WebViewBrush();
                    brush.SourceName = "htmlEditor";
                    brush.Redraw();
                    rectEditor.Fill = brush;
                    htmlEditor.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
            else
            {
                htmlEditor.Visibility = Windows.UI.Xaml.Visibility.Visible;
                rectEditor.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
            }

        }

        private void chkHtml_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bool b = (chkHtml.IsChecked.HasValue) ? (bool)chkHtml.IsChecked : false;
            if (b)
            {
                editorMenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                htmlEditor.InvokeScript("OnViewHtmlSource", new string[] { "true" });
            }
            else
            {
                editorMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
                htmlEditor.InvokeScript("OnViewHtmlSource", new string[] { "false" });
            }
        }

        private void htmlEditor_LoadCompleted(object sender, NavigationEventArgs e)
        {
            htmlInited = true;
            htmlEditor.InvokeScript("setText", new string[] { textBody.Text });
        }


        #endregion



   

















  

       




    }
}
