using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;
using WallPaper.Core.Entities;
using WallPaper.Infrastructure.AppDbConnectionOption;

namespace WallPaper.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<WallPaperDbEntity> WallPaper { get; set; }
    }
}
