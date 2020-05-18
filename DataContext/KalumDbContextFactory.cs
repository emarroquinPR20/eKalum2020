using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace kalum2020_v1.DataContext
{
    public class KalumDbContextFactory : IDesignTimeDbContextFactory<KalumDbContext>
    {
        public KalumDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Json")
            .Build();
            var optionBuilder = new DbContextOptionsBuilder<KalumDbContext>();            
            optionBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new KalumDbContext(optionBuilder.Options); 
        }
    }
}