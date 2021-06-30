﻿using BingImageAsWallPaper.Option;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace BingImageAsWallPaper.ImageDownload
{
    public class FileUtil
    {
        private string imageFolder;
        public FileUtil(FileOption opt)
        {
            this.imageFolder = opt.ImagePath;
        }

        public string GetImageName(string url)
        {
            var uri = new Uri(url);
            var queryDictionary = HttpUtility.ParseQueryString(uri.Query);
            return queryDictionary["id"];
        }

        public string GetImageNameFromPath(string path)
        {
            var file = new FileInfo(path);
            var skipPrefixStart = 5;
            return file.Name.Substring(file.Name.IndexOf('-', skipPrefixStart) + 1);
        }

        public DirectoryInfo CreateImageFolder()
        {
            return Directory.CreateDirectory(imageFolder);
        }

        public DirectoryInfo GetImageFolder()
        {
            return new DirectoryInfo(imageFolder);
        }

        public string ImageFolder { get { return imageFolder; } }

        public string RandomImage()
        {            
            var files = Directory.GetFiles(imageFolder, "*.jpg");
            if (!files.Any())
                throw new FileNotFoundException("You need to download wallpaper prior to set it.");
            var rand = new Random();
            return files[rand.Next(files.Length)];
        }

        public string NewestImage()
        {
            var files = new DirectoryInfo(imageFolder).GetFiles("*.jpg").OrderByDescending(x => x.LastWriteTime);
            if (!files.Any())
                throw new FileNotFoundException("You need to download wallpaper prior to set it.");
            return files.First().FullName;
        }

        public string NextImage(string currentImage)
        {
            var files = new DirectoryInfo(imageFolder).GetFiles("*.jpg").OrderByDescending(x => x.LastWriteTime).ToArray();
            if (!files.Any())
                throw new FileNotFoundException("You need to download wallpaper prior to set it.");
            var nextIndex = 0;
            var index = 0;
            bool found = false;
            foreach ( var file in files)
            {
                if (file.FullName == currentImage)
                {
                    found = true;
                    break;
                }
                index++;
            }

            if (found)
            {
                if (index == files.Length - 1)
                {
                    nextIndex = 0;
                }
                else
                {
                    nextIndex = index+1;
                }
            }
            return files[nextIndex].FullName;
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

        public int FileNumber()
        {
            return new DirectoryInfo(imageFolder).GetFiles("*.jpg").Count();
        }

        public void RemoveFile(string filename)
        {
            File.Delete(filename);
        }
    }
}
