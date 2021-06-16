using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BingImageAsWallPaper.ImageDownload
{
    public class DownloaderService : IDownloader
    {
        private const string BING_API = "https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=10&nc=1612409408851&pid=hp&FORM=BEHPTB&uhd=1&uhdwidth=3840&uhdheight=2160";
        private const string BING_URL = "https://cn.bing.com";
        private const string FILE_PREFIX = "bing";

        private readonly HttpClient _httpClient;
        private readonly FileUtil _fileUtil;
        public DownloaderService(HttpClient httpClient, FileUtil fileUtil)
        {
            _httpClient = httpClient;
            _fileUtil = fileUtil;
        }

        public async Task<string> Download(Entity.ApiImageEntity item)
        {
            var fileName = _fileUtil.GetImageName(item.url);
            var directoryInfo = _fileUtil.CreateImageFolder();
            var path = Path.Combine(directoryInfo.FullName, $"{FILE_PREFIX}-{item.startdate}-{fileName}");

            if (_fileUtil.CheckFileExists(fileName))
                return path;

            var uri = new Uri(item.url);
            var imageData = await _httpClient.GetByteArrayAsync(uri);
            await File.WriteAllBytesAsync(path, imageData);
            return path;
        }

        public IList<Entity.ApiImageEntity> FindUrlList()
        {
            var responseText = _httpClient.GetStringAsync(BING_API).Result;
            var regex = new Regex(@"""images"":(?<images>\[.+\])");

            Match m = regex.Match(responseText);
            if (m.Success)
            {
                var result = JsonSerializer.Deserialize<List<Entity.ApiImageEntity>>(m.Result("${images}"));
                result.ForEach(x => x.url = BING_URL + x.url);
                return result;
            }

            return null;
        }

        public async Task DownloadAll()
        {
            var items = FindUrlList();
            foreach (var item in items)
            {
                await Download(item);
            }
        }

        public async Task<string> DownloadFirst()
        {
            var item = FindUrl();
            return await Download(item);
        }

        public Entity.ApiImageEntity FindUrl()
        {
            return FindUrlList().FirstOrDefault();
        }


    }
}
