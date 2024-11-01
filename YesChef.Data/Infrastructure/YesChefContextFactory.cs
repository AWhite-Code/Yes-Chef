using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Yes_Chef.Data
{
    public class YesChefContextFactory : IDesignTimeDbContextFactory<YesChefContext>
    {
        public YesChefContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<YesChefContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not set in appsettings.json.");
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new YesChefContext(optionsBuilder.Options);
        }
    }
}
