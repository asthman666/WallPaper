using BingImageAsWallPaper;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using Xunit;

namespace BingImageAsWallPaperTest
{
    public class DownloaderTest : IClassFixture<BaseTest>
    {       
        [Fact]
        public void FetchImageListTest()
        {
            var service = Program.CreateHostBuilder(new string[] { }).Build().Services;
            var downloader = service.GetRequiredService<IDownloader>();

            var url = downloader.FindUrl()?.url;
            Assert.True(!string.IsNullOrEmpty(url));

            // https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4
            var containId = url.Contains("id=");
            Assert.True(containId);
        }

        [Fact]
        public void DownloadImageTest()
        {
            var service = Program.CreateHostBuilder(new string[] { }).Build().Services;
            var downloader = service.GetRequiredService<IDownloader>();
            var path = downloader.Download(new BingImageAsWallPaper.Entity.ApiImageEntity { startdate = "20210602", url = "https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4" });
            Assert.True(File.Exists(path.Result));
        }
    }
}
