using BingImageAsWallPaper.Option;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;
using WallPaper.Core.Entities;

namespace BingImageAsWallPaper.Database
{
    public class WallPaperContext : DbContext
    {
        public DbSet<WallPaperDbEntity> WallPaper { get; set; }

        private readonly string DbPath;

        public WallPaperContext(IOptions<DatabaseOption> databaseOption, FileOption fileOption)
        {
            DbPath = Path.Combine(fileOption.ImagePath, databaseOption.Value.DatabaseName);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

}
