using Microsoft.EntityFrameworkCore;
using WallPaper.Core.Entities;

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
