using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using WallPaper.Core.Download;

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
                _logger.LogWarning("download wallpaper running");
                await _imageDownload.DownloadAll();
                await Task.Delay(DELEYTIME, stoppingToken);
            }
        }
    }
}
