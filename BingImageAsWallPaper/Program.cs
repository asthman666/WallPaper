using BingImageAsWallPaper.ImageDownload;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BingImageAsWallPaper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = CreateHostBuilder(args).Build().Services;
            var wallPaper = services.GetRequiredService<Wallpaper>();

            if (args[0] == "downloadimage")
            {
                var downLoader = services.GetRequiredService<IDownloader>();
                await downLoader.DownloadAll();
            }
            else if (args[0] == "setbackground")
            {
                wallPaper.SetRandom();
            } 
            else
            {
                var downLoader = services.GetRequiredService<IDownloader>();
                await downLoader.DownloadAll();
                wallPaper.SetRandom();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddTransient<IDownloader, DownloaderService>();
                    services.AddTransient<FileUtil>();
                    services.AddTransient<Wallpaper>();
                });
    }
}
