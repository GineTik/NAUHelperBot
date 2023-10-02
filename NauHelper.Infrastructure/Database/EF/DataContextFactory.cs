using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Telegramper.Core.Helpers.Factories.Configuration;

namespace NauHelper.Infrastructure.Database.EF
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private const string CONNECTION_STRING_NAME = "LocalConnection";

        public DataContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>();
            ConfigureOptions(options);
            return new DataContext(options.Options);
        }

        public static void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationFactory().CreateConfiguration();
            optionsBuilder
                .UseSqlServer(configuration.GetConnectionString(CONNECTION_STRING_NAME));
        }
    }
}
