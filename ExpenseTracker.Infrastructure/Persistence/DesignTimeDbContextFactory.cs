using ExpenseTracker.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExpenseTrackerDbContext>
    {
        public ExpenseTrackerDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ExpenseTracker.Api"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ExpenseTrackerDb");

            // Build options
            var optionsBuilder = new DbContextOptionsBuilder<ExpenseTrackerDbContext>();
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 42))
            );

            Console.WriteLine($">>> Using connection string: {connectionString}");

            return new ExpenseTrackerDbContext(optionsBuilder.Options);    
        }
    }
}
