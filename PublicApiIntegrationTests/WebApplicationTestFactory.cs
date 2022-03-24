using System;
using System.Data.Common;
using System.Linq;
using DataAccess;
using DataAccess.Initialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PublicApiIntegrationTests
{
    public class WebApplicationTestFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private SqliteConnection _sqliteDbConnection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    descr => descr.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                );

                services.Remove(descriptor);

                _sqliteDbConnection = new SqliteConnection("DataSource=:memory:");
                _sqliteDbConnection.Open();

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(_sqliteDbConnection));

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<WebApplicationTestFactory<TStartup>>>();
                var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();

                try
                {
                    SampleDataInitializer.DropAndMigrateAndSeedDatabase(dbContext, loggerFactory);
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "An error occurred seeding the " +
                        $"database with test data. Error: {exception.Message}");
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _sqliteDbConnection.Close();
        }
    }
}