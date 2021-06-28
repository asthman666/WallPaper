using BingImageAsWallPaper;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        public MainWindow(ILogger<MainWindow> logger, Wallpaper wallpaper, IDownloader downloader)
        {
            InitializeComponent();
            _logger = logger;
            _wallpaper = wallpaper;
            _downloader = downloader;
        }

        private void Set_Random_Wapper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetRandom();
        }

        private void Set_Newest_Wallpaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetNewest();
        }

        private async void Download_Bing_Picture(object sender, RoutedEventArgs e)
        {
            await _downloader.DownloadAll();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            base.OnClosing(e);
        }
    }
}
