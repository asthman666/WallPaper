using BingImageAsWallPaper.Database;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using WallPaper.Core.Entities;
using WallPaper.Infrastructure;

namespace BingImageAsWallPaper
{
    public class Wallpaper
    {
        private readonly FileUtil _fileUtil;
        private readonly AppDbContext _dbContext;

        public Wallpaper(FileUtil fileUtil, AppDbContext dbContext)
        {
            _fileUtil = fileUtil;
            _dbContext = dbContext;
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

        public int SetNext(Style style = Style.Stretched)
        {
            var (imagePath, _) = GetWallPaper();
            return Set(_fileUtil.NextImage(imagePath), style);
        }

        public bool HasFavoriteWallPaperList()
        {
            return _dbContext.WallPaper.Any();
        }

        public bool IsFavoriteWallPaper()
        {
            var (path, _) = GetWallPaper();
            var entity = _dbContext.WallPaper.Where(x => x.ImageName == path).FirstOrDefault();
            if (entity != null)
                return true;
            return false;
        }

        public void LikeCurrentWallPaper()
        {
            var (path, _) = GetWallPaper();
            if (_dbContext.WallPaper.Where(x => x.ImageName == path).FirstOrDefault() != null)
                return;

            _dbContext.Add(new WallPaperDbEntity { ImageName = path, Favorite = true });
            _dbContext.SaveChanges();
        }

        public int SetFavoriteWallPaper()
        {
            var rand = new Random();
            var wallPapers = _dbContext.WallPaper.ToList();
            if (!wallPapers.Any())
                throw new ArgumentException("There is no wallpapers in favorite list.");
            return Set(wallPapers[rand.Next(wallPapers.Count)].ImageName, Style.Stretched);
        }

        public void RemoveCurrentWallPaper()
        {
            var (path, _) = GetWallPaper();
            var entity = _dbContext.WallPaper.Where(x => x.ImageName == path).FirstOrDefault();
            if (entity != null)
            {
                _dbContext.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }
}
