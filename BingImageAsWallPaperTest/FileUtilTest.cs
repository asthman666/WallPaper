using BingImageAsWallPaper;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Xunit;

namespace BingImageAsWallPaperTest
{
    public class FileUtilTest : IClassFixture<BaseTest>
    {
        BaseTest fixture;
        public FileUtilTest(BaseTest fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public void GetIdFromStringTest()
        {            
            var fileUtil = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            var urlString = "https://cn.bing.com/th?id=OHR.SocaCycles_ZH-CN3583247274_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4";
            Assert.Equal("OHR.SocaCycles_ZH-CN3583247274_UHD.jpg", fileUtil.GetImageName(urlString));
        }

        [Fact]
        public void CreateImageFolderTest()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            Assert.True(fileUtil.CreateImageFolder().Exists);
        }

        [Fact]
        public void CheckFileExists()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            Assert.True(fileUtil.CheckFileExists("OHR.Pilat_ZH-CN0091553547_UHD.jpg"));
        }

        [Fact]
        public void NewsetImageTest()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            Assert.Equal(Path.Combine(fileUtil.ImageFolder, "bing-20210609-OHR.AnnularEclipse_EN-US8858263866_UHD.jpg"), fileUtil.NewestImage());
        }
    }
}
