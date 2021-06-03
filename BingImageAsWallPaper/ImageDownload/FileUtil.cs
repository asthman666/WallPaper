using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace BingImageAsWallPaper.ImageDownload
{
    public class FileUtil
    {
        private const string IMAGE_FOLDER = "bingwallpaper";

        public string GetImageName(string url)
        {
            var uri = new Uri(url);
            var queryDictionary = HttpUtility.ParseQueryString(uri.Query);
            return queryDictionary["id"];
        }

        public DirectoryInfo CreateImageFolder()
        {
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), IMAGE_FOLDER);
            return Directory.CreateDirectory(directoryPath);
        }
    }
}
