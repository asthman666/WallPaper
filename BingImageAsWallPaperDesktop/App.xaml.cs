using BingImageAsWallPaper;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using WallPaper.Infrastructure;

namespace BingImageAsWallPaperDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
        public IConfiguration Configuration { get; private set; }
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
            nIcon.Icon = new Icon(Path.Combine(AppContext.BaseDirectory, "wallpaper.ico"));
            nIcon.Visible = true;
            nIcon.ShowBalloonTip(3000, "WallPaper", "Set WallPaper", System.Windows.Forms.ToolTipIcon.Info);
            nIcon.DoubleClick += nIcon_DoubleClick;

            nIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            nIcon.ContextMenuStrip.Items.Add("Exit", null, this.MenuExit_Click);
            //nIcon.Click += nIcon_Click;
        }

        //private void nIcon_Click(object sender, EventArgs e)
        //{
        //    var wallPaper = _serviceProvider.GetService<Wallpaper>();
        //    wallPaper.SetRandom();
        //}

        void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void nIcon_DoubleClick(object sender, EventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
            MainWindow.WindowState = WindowState.Normal;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddLogging(configure => { configure.AddDebug(); });

            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            //services.Configure<DatabaseOption>(Configuration.GetSection(DatabaseOption.DatabaseSection));
            //services.AddEntityFrameworkSqlite().AddDbContext<WallPaperContext>();
            //services.AddSingleton(x =>
            //   new FileOption { ImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "bingwallpaper") }
            //);
            services.AddDbContext(Configuration, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            services.AddHttpClient();
            services.AddTransient<IDownloader, DownloaderService>();
            services.AddTransient<FileUtil>();
            services.AddTransient<Wallpaper>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var db = _serviceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
