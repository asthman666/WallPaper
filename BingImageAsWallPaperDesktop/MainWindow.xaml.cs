using BingImageAsWallPaper;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows;

namespace BingImageAsWallPaperDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> _logger;
        private readonly Wallpaper _wallpaper;
        private readonly IDownloader _downloader;
        private readonly FileUtil _fileUtil;
        public MainWindow(ILogger<MainWindow> logger, Wallpaper wallpaper, IDownloader downloader, FileUtil fileUtil)
        {
            InitializeComponent();
            _logger = logger;
            _wallpaper = wallpaper;
            _downloader = downloader;
            _fileUtil = fileUtil;

            if (_fileUtil.FileNumber() > 0)
            {
                NextWallpaper.IsEnabled = true;
                NewestWallpaper.IsEnabled = true;
                RandomWallpaper.IsEnabled = true;
            }

            FavoriteWallPaperAvailableControl();
        }

        private void Set_Random_Wapper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetRandom();
            FavoriteWallPaperAvailableControl();
        }

        private void Set_Newest_Wallpaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetNewest();
            FavoriteWallPaperAvailableControl();
        }

        private void FavoriteWallPaperAvailableControl()
        {
            if (_wallpaper.HasFavoriteWallPaperList())
            {
                RemoveWallpaper.IsEnabled = true;
                SetWallpaper.IsEnabled = true;
            }
            else
            {
                RemoveWallpaper.IsEnabled = false;
                SetWallpaper.IsEnabled = false;
            }

            if (_wallpaper.IsFavoriteWallPaper())
            {
                LikeWallpaper.IsEnabled = false;
            }
            else
            {
                LikeWallpaper.IsEnabled = true;
            }
        }

        private async void Remove_Wallpaper(object sender, RoutedEventArgs e)
        {
            await _wallpaper.RemoveCurrentWallPaper();
            FavoriteWallPaperAvailableControl();
        }

        private async void Like_Wallpaper(object sender, RoutedEventArgs e)
        {
            await _wallpaper.LikeCurrentWallPaper();
            FavoriteWallPaperAvailableControl();
        }

        private void Set_Wallpaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetFavoriteWallPaper();
            FavoriteWallPaperAvailableControl();
        }

        private void Open_Wallpaper_Folder(object sender, RoutedEventArgs e)
        {
            _fileUtil.OpenFileFolder();
        }

        private async void Download_Bing_Picture(object sender, RoutedEventArgs e)
        {
            DownloadPicture.IsEnabled = false;
            await _downloader.DownloadAllWaitDone();
            DownloadPicture.IsEnabled = true;
            NextWallpaper.IsEnabled = true;
            NewestWallpaper.IsEnabled = true;
            RandomWallpaper.IsEnabled = true;
        }

        private void Set_Next_Wallpaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetNext();
            FavoriteWallPaperAvailableControl();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            base.OnClosing(e);
        }
    }
}
