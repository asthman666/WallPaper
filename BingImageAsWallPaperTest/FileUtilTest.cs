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
            Assert.True(fileUtil.CheckFileExists("bing-20210528-OHR.CowbirdsEgg_EN-US8103879720_UHD.jpg"));
        }

        [Fact]
        public void NewsetImageTest()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            Assert.Equal(Path.Combine(fileUtil.ImageFolder, "bing-20210616-OHR.BrightEye_ZH-CN6196887876_UHD.jpg"), fileUtil.NewestImage());
        }

        [Fact]
        public void GetImageNameFromPathTest()
        {
            var fileUtil = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.ImageDownload.FileUtil>();
            var path = Path.Combine(fileUtil.ImageFolder, "bing-20210528-OHR.CowbirdsEgg_EN-US8103879720_UHD.jpg");
            Assert.Equal("OHR.CowbirdsEgg_EN-US8103879720_UHD.jpg", fileUtil.GetImageNameFromPath(path));
        }
    }
}
