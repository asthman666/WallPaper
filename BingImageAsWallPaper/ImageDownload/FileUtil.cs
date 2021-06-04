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

        private DirectoryInfo GetImageFolder()
        {
            return new DirectoryInfo(imageFolder);
        }

        public string RandomImage()
        {
            var rand = new Random();
            var files = Directory.GetFiles(imageFolder, "*.jpg");
            return files[rand.Next(files.Length)];
        }

        public bool CheckFileExists(string fileName)
        {
            var files = Directory.GetFiles(imageFolder, "*.jpg");

            // NOTE: fileName example: OHR.Pilat_ZH-CN0091553547_UHD.jpg

            var partfileName = fileName.Substring(0, fileName.IndexOf('_'));

            // NOTE: partfileName example: OHR.Pilat

            foreach ( var file in files )
            {
                if (file.Contains(partfileName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
