using BingImageAsWallPaper.BackGroundService;
using BingImageAsWallPaper.ImageDownload;
using BingImageAsWallPaper.Option;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BingImageAsWallPaper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var services = CreateHostBuilder(args).Build().Services;
            //var wallPaper = services.GetRequiredService<Wallpaper>();

            //if (args.Length > 0 && args[0] == "downloadimage")
            //{
            //    var downLoader = services.GetRequiredService<IDownloader>();
            //    await downLoader.DownloadAll();
            //}
            //else if (args.Length > 0 && args[0] == "setbackground")
            //{
            //    if (DateTime.Now.Hour >= 10 && DateTime.Now.Hour < 11)
            //    {
            //        wallPaper.SetNewest();
            //    }
            //    else
            //    {
            //        wallPaper.SetRandom();
            //    }
            //}
            //else
            //{
            //    var downLoader = services.GetRequiredService<IDownloader>();
            //    await downLoader.DownloadAnyOfFile();
            //    wallPaper.SetRandom();
            //}

            var host = CreateHostBuilder(args).Build();
            var services = host.Services;
            var downLoader = services.GetRequiredService<IDownloader>();
            var wallPaper = services.GetRequiredService<Wallpaper>();
            await downLoader.DownloadAnyOfFile();
            wallPaper.SetRandom();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                    {
                        logging.AddEventLog(setting =>
                        {
                            setting.SourceName = "BingImageAsWallPaper";
                        });
                    }
                )
                .ConfigureServices((hostContext, services) =>
                {
                    //services.Configure<FileOption>(hostContext.Configuration.GetSection("FileOption"));
                    //services.Configure<FileOption>(x => x.ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wallpaper_test"));
                    services.AddSingleton( x => 
                        new FileOption { ImagePath =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "bingwallpaper") }
                    );
                    services.AddHttpClient();
                    services.AddTransient<IDownloader, DownloaderService>();
                    services.AddTransient<FileUtil>();
                    services.AddTransient<Wallpaper>();
                    services.AddHostedService<WallPaperBackgroundService>();
                    services.AddHostedService<DownloadBackgroundService>();
                });
    }
}
