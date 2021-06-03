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
        private string imageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), IMAGE_FOLDER);

        public string GetImageName(string url)
        {
            var uri = new Uri(url);
            var queryDictionary = HttpUtility.ParseQueryString(uri.Query);
            return queryDictionary["id"];
        }

        public DirectoryInfo CreateImageFolder()
        {
            return Directory.CreateDirectory(imageFolder);
        }

        public string RandomImage()
        {
            var rand = new Random();
            var files = Directory.GetFiles(imageFolder, "*.jpg");
            return files[rand.Next(files.Length)];
        }
    }
}
