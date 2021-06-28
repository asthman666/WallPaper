using BingImageAsWallPaper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public MainWindow(ILogger<MainWindow> logger, Wallpaper wallpaper)
        {
            InitializeComponent();
            _logger = logger;
            _wallpaper = wallpaper;
        }

        private void Random_Set_WallPaper(object sender, RoutedEventArgs e)
        {
            _wallpaper.SetRandom();
        }
    }
}
