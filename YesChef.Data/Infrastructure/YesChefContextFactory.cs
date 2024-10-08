using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Yes_Chef.Data;

namespace YesChef.Data
{
    public class YesChefContextFactory : IDesignTimeDbContextFactory<YesChefContext>
    {
        public YesChefContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<YesChefContextFactory>(optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<YesChefContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? "Server=(localdb)\\mssqllocaldb;Database=YesChef;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new YesChefContext(optionsBuilder.Options);
        }
    }
}
