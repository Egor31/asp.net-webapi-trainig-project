using System;
using System.Data.Common;
using System.IO;
using DataAccess;
using DataAccess.Initialization;
using DataAccess.Migrations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace DataAccessTests
{
    public class InMemoryDbConextFactory : IDisposable
    {
        private DbConnection _sqliteDbConnection;
        private ApplicationDbContext _sqliteDbContext;

        public ApplicationDbContext CreateContext()
        {
            if (_sqliteDbConnection == null)
            {
                _sqliteDbConnection = new SqliteConnection("DataSource=:memory:");
                _sqliteDbConnection.Open();
                var sqliteDbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(_sqliteDbConnection).EnableSensitiveDataLogging().Options;
                _sqliteDbContext = new ApplicationDbContext(sqliteDbContextOptions);
                SampleDataInitializer.DropAndMigrateAndSeedDatabase(_sqliteDbContext, new NullLoggerFactory());
            }
            return _sqliteDbContext;
        }

        public void Dispose()
        {
            if (_sqliteDbConnection != null)
            {
                _sqliteDbConnection.Dispose();
                _sqliteDbConnection = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}