using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public class WallPaperDbEntity : BaseEntity
    {
        public string ImageName { get; set; }
        public bool Favorite { get; set; }
    }
}
