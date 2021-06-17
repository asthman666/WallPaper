using BingImageAsWallPaper.ImageDownload;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace BingImageAsWallPaper
{
    public class Wallpaper
    {
        private readonly FileUtil _fileUtil;

        public Wallpaper(FileUtil fileUtil)
        {
            _fileUtil = fileUtil;
        }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

        public int Set(string imagePath, Style style)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            switch (style)
            {
                case Style.Stretched:
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case Style.Centered:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case Style.Tiled:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                    break;
            }
            key.Close();
            return SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                imagePath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        // NOTE: https://stackoverflow.com/questions/2745803/how-to-get-the-name-of-the-select-wallpaper
        public (string Path, Style Style) GetWallPaper()
        {
            var wpReg = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
            var wallpaperPath = wpReg.GetValue("WallPaper").ToString();
            var WallPaperStyle = wpReg.GetValue("WallPaperStyle").ToString();
            var TileWallpaper = wpReg.GetValue("TileWallpaper").ToString();
            wpReg.Close();

            var style = Style.Stretched;
            if (WallPaperStyle == "1" && TileWallpaper == "0")
                style = Style.Centered;

            if (WallPaperStyle == "1" && TileWallpaper == "1")
                style = Style.Tiled;
            return (wallpaperPath, style);
        }

        public int SetRandom(Style style = Style.Stretched)
        {
            return Set(_fileUtil.RandomImage(), style);
        }

        public int SetNewest(Style style = Style.Stretched)
        {
            return Set(_fileUtil.NewestImage(), style);
        }
    }
}
