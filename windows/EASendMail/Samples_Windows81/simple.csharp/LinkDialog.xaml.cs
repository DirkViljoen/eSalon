using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace simple.csharp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LinkDialog : Page
    {
        public event EventHandler CreateLinkRequested = null;
        public event EventHandler CancelRequested = null;
        public string LinkSrc = "";
        public LinkDialog()
        {
            this.InitializeComponent();
            
            var bounds = Window.Current.Bounds;
            this.RootPanel.Width = bounds.Width;
            this.RootPanel.Height = bounds.Height;

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LinkSrc = textLink.Text;
            if (this.CreateLinkRequested != null)
            {
                this.CreateLinkRequested(this, EventArgs.Empty);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LinkSrc = "";
            if (this.CancelRequested != null)
            {
                this.CancelRequested(this, EventArgs.Empty);
            }
        }
    }
}
