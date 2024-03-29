﻿using BingImageAsWallPaper.Option;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using WallPaper.Core.Wallpaper;

namespace BingImageAsWallPaper.BackGroundService
{
    public class WallPaperBackgroundService : BackgroundService
    {
        private const int DELEYTIME = 3600 * 1000; // 1 hour

        private readonly ILogger<WallPaperBackgroundService> _logger;
        private readonly WallpaperService _wallPaper;
        private readonly IOptionsMonitor<RandomSet> _randomSetOptionDelegate;


        public WallPaperBackgroundService(ILogger<WallPaperBackgroundService> logger, WallpaperService wallPaper, IOptionsMonitor<RandomSet> randomSet)
        {
            _logger = logger;
            _wallPaper = wallPaper;
            _randomSetOptionDelegate = randomSet;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(DELEYTIME, stoppingToken);
                if (_randomSetOptionDelegate.CurrentValue.Active)
                {
                    _logger.LogWarning("set wallpaper running");
                    _wallPaper.SetRandom();
                }
            }
        }
    }
}
