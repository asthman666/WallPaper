using BingImageAsWallPaper;
using BingImageAsWallPaper.Option;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.IO;

namespace BingImageAsWallPaperTest
{
    public class BaseTest : IDisposable
    {
        public IServiceProvider serviceProvider { get; private set; }

        public BaseTest()
        {
            // Do "global" initialization here; Only called once.
            serviceProvider = Program.CreateHostBuilder(new string[] { })
                .ConfigureServices((hostContext, services) =>
                {
                    //services.Configure<FileOption>(hostContext.Configuration.GetSection("FileOption"));
                    //services.Configure<FileOption>(x => x.ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wallpaper_test"));
                    services.AddSingleton(x => new FileOption { ImagePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "wallpaper_test") });
                }
                )
                .Build().Services;
        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }
    }
}
