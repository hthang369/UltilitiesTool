using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLTool.Views
{
    /// <summary>
    /// Interaction logic for YoutubeView.xaml
    /// </summary>
    public partial class YoutubeView : UserControl
    {
        public YoutubeView()
        {
            InitializeComponent();
            lvwResult.SelectionChanged += LvwResult_SelectionChanged;
        }

        private void LvwResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //YouTubeApi.Views.SearchResult result = e.AddedItems[0] as YouTubeApi.Views.SearchResult;
            //YouTubeApi.YouTubeApiRestClient youTubeApi = (this.DataContext as ViewModels.YoutubeViewModel).youTubeApi;
            //youTubeApi.GetVideos("player", result.Id.VideoId);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<iframe ");
            //sb.Append("width='420' height='315' ");
            //sb.AppendFormat("src='{0}{1}'", "http://www.youtube.com/embed/", result.Id.VideoId);
            //sb.Append(" />");
            //string result1 =  youTubeApi.GetLinkVideo(result.Id.VideoId);
            //frmMediaSource.Navigated += FrmMediaSource_Navigated;
            //(this.DataContext as ViewModels.YoutubeViewModel).HtmlToDisplay = sb.ToString();
            //(this.DataContext as ViewModels.YoutubeViewModel).HtmlToDisplay = "<h1>abcvgđ</h1>";
            //frmMediaSource.Navigate(string.Format("http://www.youtube.com/embed/{0}", result.Id.VideoId));
            //System.IO.FileStream file = new System.IO.FileStream(System.Windows.Forms.Application.StartupPath + "\\player.html", System.IO.FileMode.Open);
            //frmMediaSource.NavigateToStream(file);
            //frmMediaSource.Address = "https://www.youtube.com/embed/cMtxUc2GtlE";
            //frmMediaSource.ObjectForScripting = true;
            //frmMediaSource.pl
            try
            {
                //var obj = YoutubeExtractor.DownloadUrlResolver.GetDownloadUrls(string.Format("youtube.com/embed/{0}", result.Id.VideoId));
            }
            catch (Exception ex)
            {

            }
        }

        private void FrmMediaSource_Navigated(object sender, NavigationEventArgs e)
        {
            //SetSilent(frmMediaSource, true); // make it silent
        }

        public static void SetSilent(WebBrowser browser, bool silent)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            // get an IWebBrowser2 from the document
            IOleServiceProvider sp = browser.Document as IOleServiceProvider;
            if (sp != null)
            {
                Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

                object webBrowser;
                sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out webBrowser);
                if (webBrowser != null)
                {
                    webBrowser.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser, new object[] { silent });
                }
            }
        }

        [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
        }
    }
}
