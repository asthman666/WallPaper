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

        private readonly IOptions<DatabaseOption> _databaseOption;

        public WallPaperContext(IOptions<DatabaseOption> databaseOption, FileOption fileOption)
        {
            _databaseOption = databaseOption;
            DbPath = Path.Combine(fileOption.ImagePath, _databaseOption.Value.DatabaseName);
        }
        public string DbPath { get; private set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

}
