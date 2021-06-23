using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BingImageAsWallPaper.ImageDownload;
using Microsoft.Extensions.Logging;

namespace BingImageAsWallPaper.BackGroundService
{
    public class DownloadBackgroundService : BackgroundService
    {
        private const int DELEYTIME = 6 * 3600 * 1000; // 6 hour

        private readonly IDownloader _imageDownload;
        private readonly ILogger<DownloadBackgroundService> _logger;

        public DownloadBackgroundService(ILogger<DownloadBackgroundService> logger, IDownloader downloader)
        {
            _imageDownload = downloader;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("download wallpaper running");
                await _imageDownload.DownloadAll();
                await Task.Delay(DELEYTIME, stoppingToken);
            }
        }
    }
}
