using BingImageAsWallPaper.Option;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImageAsWallPaper.Database
{
    public class WallPaperContext : DbContext
    {
        public DbSet<WallPaper> WallPaper { get; set; }

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

    [Keyless]
    public class WallPaper
    {
        public string ImageName { get; set; }
        public bool Favorite { get; set; }
    }
}
