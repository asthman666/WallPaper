using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BingImageAsWallPaper.BackGroundService
{
    public class WallPaperBackgroundService : BackgroundService
    {
        private readonly ILogger<WallPaperBackgroundService> _logger;
        private readonly Wallpaper _wallPaper;

        public WallPaperBackgroundService(ILogger<WallPaperBackgroundService> logger, Wallpaper wallPaper)
        {
            _logger = logger;
            _wallPaper = wallPaper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("running");
                _wallPaper.SetRandom();
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
