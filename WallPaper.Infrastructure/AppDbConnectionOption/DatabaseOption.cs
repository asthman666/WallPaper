using Microsoft.EntityFrameworkCore;
using WallPaper.Core.Entities;

namespace WallPaper.Infrastructure.AppDbConnectionOption
{
    public class DatabaseOption
    {
        public const string DatabaseSection = "Database";
        public string DatabaseName { get; set; }
    }
}
