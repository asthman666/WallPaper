using BingImageAsWallPaper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
