using BingImageAsWallPaper;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace BingImageAsWallPaperTest
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    public class DownloaderTest : IClassFixture<BaseTest>, IDisposable
    {
        BaseTest fixture;
        public DownloaderTest(BaseTest fixture)
        {
            this.fixture = fixture;
            cleanImageFile();
        }

        [Fact]
        public async void FindUrlListTest()
        {
            var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
            var items = await downloader.FindUrlList();
            Assert.True(items.Count > 0);
        }

        [Fact]
        public async void FetchImageListTest()
        {
            var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
            var item = await downloader.FindUrl();
            Assert.NotNull(item);
            var url = item.url;
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
            var path = downloader.Download(new BingImageAsWallPaper.Entity.ApiImageEntity { startdate = "20210602", url = "https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4" });
            Assert.True(File.Exists(path.Result));
        }

        [Fact]
        public void DownloadAnyFileTest()
        {
            var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
            var path = downloader.DownloadAnyOfFile();
            Assert.True(File.Exists(path.Result));
        }

        private void cleanImageFile()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<FileUtil>();
            var testFiles = new List<string> { "bing-20210616-OHR.BrightEye_ZH-CN6196887876_UHD.jpg", 
                                               "bing-20210527-OHR.ICanHearIt_EN-US7945824197_UHD.jpg", 
                                               "bing-20210528-OHR.CowbirdsEgg_EN-US8103879720_UHD.jpg" 
                                              };
            var files = Directory.GetFiles(fileUtil.ImageFolder, "*.jpg");
            foreach ( var file in files )
            {
                var fileInfo = new FileInfo(file);
                if (!testFiles.Contains(fileInfo.Name))
                {
                    fileUtil.RemoveFile(Path.Combine(fileUtil.GetImageFolder().FullName, file));
                }
            }
        }

        public void Dispose()
        {
            cleanImageFile();
        }
    }
}
