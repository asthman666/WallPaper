using BingImageAsWallPaper;
using BingImageAsWallPaper.ImageDownload;
using BingImageAsWallPaper.Option;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BingImageAsWallPaperDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddLogging(configure => { configure.AddDebug(); });

            services.AddSingleton(x =>
               new FileOption { ImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "bingwallpaper") }
            );
            services.AddHttpClient();
            services.AddTransient<IDownloader, DownloaderService>();
            services.AddTransient<FileUtil>();
            services.AddTransient<Wallpaper>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
