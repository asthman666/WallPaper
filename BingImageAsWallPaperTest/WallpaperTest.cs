using BingImageAsWallPaper;
using Microsoft.Extensions.DependencyInjection;

namespace BingImageAsWallPaperTest
{
    public class WallpaperTest
    {
        public void SetBackGroundTest()
        {
            var service = Program.CreateHostBuilder(new string[] { }).Build().Services;
            var wallPaper = service.GetRequiredService<BingImageAsWallPaper.Wallpaper>();
            wallPaper.Set("", Wallpaper.Style.Stretched);
        }
    }
}
