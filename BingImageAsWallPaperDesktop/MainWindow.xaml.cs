﻿using BingImageAsWallPaper;
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

        private void Remove_Wallpaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.RemoveCurrentWallPaper();
            FavoriteWallPaperAvailableControl();
        }

        private void Like_Wallpaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.LikeCurrentWallPaper();
            FavoriteWallPaperAvailableControl();
        }

        private void Set_Wallpaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetFavoriteWallPaper(); 
            FavoriteWallPaperAvailableControl();
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
