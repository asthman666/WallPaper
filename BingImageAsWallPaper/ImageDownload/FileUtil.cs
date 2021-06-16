using System;
using System.IO;
using System.Linq;
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

        public string ImageFolder { get { return imageFolder; } }

        public string RandomImage()
        {
            var rand = new Random();
            var files = Directory.GetFiles(imageFolder, "*.jpg");
            return files[rand.Next(files.Length)];
        }

        public string NewestImage()
        {
            return new DirectoryInfo(imageFolder).GetFiles("*.jpg").OrderByDescending(x => x.CreationTime).FirstOrDefault()?.FullName;
        }

        public bool CheckFileExists(string fileName)
        {
            var files = Directory.GetFiles(imageFolder, "*.jpg");

            // NOTE: fileName example: OHR.Pilat_ZH-CN0091553547_UHD.jpg

            var partfileName = fileName.Substring(0, fileName.IndexOf('_'));

            // NOTE: partfileName example: OHR.Pilat

            foreach (var file in files)
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
