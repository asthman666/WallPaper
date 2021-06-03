using BingImageAsWallPaper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BingImageAsWallPaperTest
{
    public class FileUtilTest
    {
        [Fact]
        public void GetIdFromStringTest()
        {
            var service = Program.CreateHostBuilder(new string[] { }).Build().Services;
            var fileUtil = service.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            var urlString = "https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4";
            Assert.Equal("OHR.SocaCycles_ZH-CN3583247274_UHD.jpg", fileUtil.GetImageName(urlString));
        }

        [Fact]
        public void CreateImageFolderTest()
        {
            var service = Program.CreateHostBuilder(new string[] { }).Build().Services;
            var fileUtil = service.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            Assert.True(fileUtil.CreateImageFolder().Exists);
        }
    }
}
