using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace BingImageAsWallPaper.ImageDownload
{
    public class FileUtil
    {
        public string GetImageName(string url)
        {
            var uri = new Uri(url);
            var queryDictionary = HttpUtility.ParseQueryString(uri.Query);
            return queryDictionary["id"];
        }
    }
}
