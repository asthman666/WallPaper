using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using WallPaper.Download.Core;
using WallPaper.Download.Core.Entity;
using WallPaper.SharedKernel;
using Xunit;
using Xunit.Abstractions;

namespace BingImageAsWallPaperTest
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    public class DownloaderTest : IClassFixture<BaseTest>, IDisposable
    {
        BaseTest fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        public DownloaderTest(BaseTest fixture, ITestOutputHelper testOutputHelper)
        {
            this.fixture = fixture;
            _testOutputHelper = testOutputHelper;
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
        public async void DownloadImageTest()
        {
            var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
            var path = await downloader.Download(new ApiImageEntity { startdate = "20210602", url = "https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4" });
            Assert.True(File.Exists(path));
        }

        // NOTE: The method DownloadAnyOfFile maybe download more than one images and the images maybe used by other test case, when dispose delete images. maybe cause an error
        //[Fact]
        //public async void DownloadAnyFileTest()
        //{
        //    var downloader = fixture.serviceProvider.GetRequiredService<IDownloader>();
        //    var fileUtil = fixture.serviceProvider.GetRequiredService<FileUtil>();

        //    var path = await downloader.DownloadAnyOfFile();
        //    Assert.True(!string.IsNullOrEmpty(path));
        //    _testOutputHelper.WriteLine(path);
        //    Assert.True(fileUtil.CheckFileExists(fileUtil.GetImageNameFromPath(path)));
        //}

        private void cleanImageFile()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<FileUtil>();
            var testFiles = new List<string> { "bing-20210616-OHR.BrightEye_ZH-CN6196887876_UHD.jpg",
                                               "bing-20210628-OHR.RocksSeychelles_ZH-CN0105602892_UHD.jpg",
                                               "bing-20210528-OHR.CowbirdsEgg_EN-US8103879720_UHD.jpg"
                                              };
            var files = fileUtil.GetAllImageFiles();
            foreach (var file in files)
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
