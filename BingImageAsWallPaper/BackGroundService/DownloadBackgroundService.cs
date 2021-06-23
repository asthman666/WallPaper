using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BingImageAsWallPaper.ImageDownload;

namespace BingImageAsWallPaper.BackGroundService
{
    public class DownloadBackgroundService : BackgroundService
    {
        private const int DELEYTIME = 6 * 3600 * 1000; // 6 hour

        private readonly IDownloader _imageDownload;

        public DownloadBackgroundService(IDownloader downloader)
        {
            _imageDownload = downloader;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _imageDownload.DownloadAll();
                await Task.Delay(DELEYTIME, stoppingToken);
            }
        }
    }
}
