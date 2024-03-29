﻿using BingImageAsWallPaper.BackGroundService;
using BingImageAsWallPaper.Option;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using WallPaper.Infrastructure;
using WallPaper.Core.Download;
using WallPaper.SharedKernel;
using WallPaper.Core.Wallpaper;

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
            //var services = host.Services;
            //var downLoader = services.GetRequiredService<IDownloader>();
            //var wallPaper = services.GetRequiredService<Wallpaper>();
            //await downLoader.DownloadAnyOfFile();
            //wallPaper.SetRandom();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<WallPaperContext>();
            //    await db.Database.EnsureCreatedAsync();
            //}
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
                    services.Configure<RandomSet>(hostContext.Configuration.GetSection("RandomSet"));
                    //services.AddSingleton(x =>
                    //   new FileOption { ImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "bingwallpaper") }
                    //);

                    services.AddDbContext(hostContext.Configuration, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

                    //services.Configure<DatabaseOption>(hostContext.Configuration.GetSection(DatabaseOption.DatabaseSection));
                    //services.AddEntityFrameworkSqlite().AddDbContext<WallPaperContext>();
                    services.AddHttpClient();
                    services.AddTransient<IDownloader, DownloaderService>();
                    services.AddTransient<FileUtil>();
                    services.AddTransient<WallpaperService>();
                    services.AddHostedService<WallPaperBackgroundService>();
                    services.AddHostedService<DownloadBackgroundService>();
                });
    }
}
