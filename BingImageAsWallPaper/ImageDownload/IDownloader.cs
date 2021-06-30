using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BingImageAsWallPaper.ImageDownload
{
    public interface IDownloader
    {
        public Task<string> Download(Entity.ApiImageEntity item, CancellationToken cancellationToken = default);

        public Task<Entity.ApiImageEntity> FindUrl();

        public Task<IList<Entity.ApiImageEntity>> FindUrlList();

        public Task DownloadAll();

        public Task<string[]> DownloadAllWaitDone();

        public Task<string> DownloadAnyOfFile();

        public Task<string> DownloadFirst();
    }
}
