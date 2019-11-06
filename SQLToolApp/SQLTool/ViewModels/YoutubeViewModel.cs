using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using YoutubeExtractor;
using System.Threading;
using System.Windows;
using System.Diagnostics;

namespace SQLTool.ViewModels
{
    [POCOViewModel]
    public class YoutubeViewModel : BasePopupViewModel
    {
        Window main;
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _linkDownload;
        public string LinkDownload
        {
            get => _linkDownload;
            set => SetProperty(ref _linkDownload, value);
        }
        private List<String> _lstResolution;
        public List<String> lstResolution
        {
            get => _lstResolution;
            set => SetProperty(ref _lstResolution, value);
        }
        public ICommand downloadCommand { get; set; }

        public YoutubeViewModel()
        {
            downloadCommand = new RelayCommand<object>((x) => { return true; }, (x) => DownLoadCommandEvent(x));
            Title = "Download Video Youtube";
            lstResolution = new List<string> { "360", "480", "720" };
        }
        string link;
        private void DownLoadCommandEvent(object x)
        {
            IEnumerable<VideoInfo> videos = DownloadUrlResolver.GetDownloadUrls(LinkDownload);
            VideoInfo video = videos.First(p => p.VideoType == VideoType.Mp4 && p.Resolution == 720);
            if (video.RequiresDecryption)
                DownloadUrlResolver.DecryptDownloadUrl(video);
            link = System.Windows.Forms.Application.StartupPath + "" + video.Title + video.VideoExtension;
            link = link.Replace("|", "_").Replace(" ", "_");
            VideoDownloader downloader = new VideoDownloader(video, link);
            downloader.DownloadProgressChanged += DownloadProgressChanged;
            Thread thread = new Thread(() => { downloader.Execute(); }) { IsBackground = true };
            thread.Start();
        }

        private void DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            this.main.Dispatcher.Invoke(() =>
            {
                //progressBar1.Value = (int)e.ProgressPercentage;
                //lblPercent.Text = $"{string.Format("{0:0,##}", e.ProgressPercentage)}%";
                //progressBar1.Update();
                if (e.ProgressPercentage == 100)
                {
                    Process.Start(link);
                }
            });
        }
    }
}