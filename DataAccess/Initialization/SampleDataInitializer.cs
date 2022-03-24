using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using DataAccess.Migrations;

namespace DataAccess.Initialization
{
    public static class SampleDataInitializer
    {
        public static void DropAndMigrateAndSeedDatabase(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            // I'm using logger factory because I cannot inject logger into static class, such a sad story =(
            // Have no idea why it is works
            var logger = loggerFactory.CreateLogger(MethodBase.GetCurrentMethod().DeclaringType);

            context.Database.EnsureDeleted();
            context.Database.Migrate();
            logger.LogInformation("Database cleared");
            
            context.Students.AddRange(SampleData.Students);
            context.SaveChanges();
            context.Teachers.AddRange(SampleData.Teachers);
            context.SaveChanges();
            context.Subjects.AddRange(SampleData.Subjects);
            context.SaveChanges();
            context.RollBook.AddRange(SampleData.RollBook);
            context.SaveChanges();
            context.HomeWorks.AddRange(SampleData.HomeWorks);
            context.SaveChanges();
            logger.LogInformation("Database seeded with data");
        }
    }
}