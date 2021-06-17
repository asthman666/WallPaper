using BingImageAsWallPaper;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Xunit;

namespace BingImageAsWallPaperTest
{
    public class DownloaderTest : IClassFixture<BaseTest>
    {
        BaseTest fixture;
        public DownloaderTest(BaseTest fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void FindUrlListTest()
        {
            var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
            var items = downloader.FindUrlList();
            Assert.True(items.Count > 0);
        }

        [Fact]
        public void FetchImageListTest()
        {
            var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
            var url = downloader.FindUrl()?.url;
            Assert.True(!string.IsNullOrEmpty(url));

            // NOTE:
            // url format => https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4
            var containId = url.Contains("id=");
            Assert.True(containId);
        }

        [Fact]
        public void DownloadImageTest()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<FileUtil>();
            var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
            fileUtil.RemoveFile(Path.Combine(fileUtil.GetImageFolder().FullName, "bing-20210602-OHR.SocaCycles_ZH-CN3583247274_UHD.jpg"));
            var path = downloader.Download(new BingImageAsWallPaper.Entity.ApiImageEntity { startdate = "20210602", url = "https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4" });
            Assert.True(File.Exists(path.Result));
            fileUtil.RemoveFile(Path.Combine(fileUtil.GetImageFolder().FullName, "bing-20210602-OHR.SocaCycles_ZH-CN3583247274_UHD.jpg"));
        }
    }
}
