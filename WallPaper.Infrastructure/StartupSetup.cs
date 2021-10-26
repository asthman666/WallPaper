using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using WallPaper.Core.Interfaces;
using WallPaper.Infrastructure.AppDbConnectionOption;

namespace WallPaper.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, string databaseMainPath)
        {
            services.Configure<DatabaseOption>(configuration.GetSection(DatabaseOption.DatabaseSection));
            services.AddSingleton(x =>
               new FileOption { ImagePath = Path.Combine(databaseMainPath, "bingwallpaper") }
            );

            var serviceProvider = services.BuildServiceProvider();
            var databaseName = serviceProvider.GetRequiredService<IOptions<DatabaseOption>>().Value.DatabaseName;
            var connectionString = Path.Combine(databaseMainPath, "bingwallpaper", databaseName);
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlite($"Data Source={connectionString}")
            );
            services.AddSingleton<IRepository, EfRepository>();
        }        
    }
}
