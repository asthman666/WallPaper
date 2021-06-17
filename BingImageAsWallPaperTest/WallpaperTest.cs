using BingImageAsWallPaper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BingImageAsWallPaperTest
{
    public class WallpaperTest : IClassFixture<BaseTest>
    {
        BaseTest fixture;
        public WallpaperTest(BaseTest fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void SetBackGroundTest()
        {
            var wallPaper = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.Wallpaper>();
            var (wallPaperFile, style) = wallPaper.GetWallPaper(); // get original wallpaper

            var result = wallPaper.SetNewest();
            Assert.True(result != 0);

            wallPaper.Set(wallPaperFile, style); // revert to original
        }

        [Fact]
        public void GetWallPaperTest()
        {
            var wallPaper = fixture.serviceProvider.GetRequiredService<BingImageAsWallPaper.Wallpaper>();
            var (wallPaperFile, _) = wallPaper.GetWallPaper();
            Assert.NotNull(wallPaperFile);
            Assert.True(wallPaperFile.Length > 0);
        }
    }
}
