using Cursus_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cursus_API.Helper
{
    public static class MigrationHelper
    {
        //public static WebApplication MigrationDBHelper(this WebApplication webApp)
        //{
        //    using (var scope = webApp.Services.CreateScope())
        //    {
        //        using (var appContext = scope.ServiceProvider.GetRequiredService<LMS_CursusDbContext>())
        //        {
        //            try
        //            {
        //                appContext.Database.Migrate();
        //            }
        //            catch
        //            {
        //                throw;
        //            }
        //        }
        //        //using (var appContext = scope.ServiceProvider.GetRequiredService<LMS_CursusAWSDbContext>())
        //        //{
        //        //    try
        //        //    {
        //        //        appContext.Database.Migrate();
        //        //    }
        //        //    catch
        //        //    {
        //        //        throw;
        //        //    }
        //        //}

        //    }
        //    return webApp;



        //}
        public static WebApplication MigrateDatabases(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                MigrateDatabase<LMS_CursusDbContext>(services);
                //  MigrateDatabase<LMS_CursusAWSDbContext>(services);
                //MigrateDatabase<LMS_CursusAzureDbContext>(services);
            }
            return webApp;
        }

        private static void MigrateDatabase<TContext>(IServiceProvider services) where TContext : DbContext
        {
            try
            {
                var context = services.GetRequiredService<TContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<TContext>>();
                logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}.");
                throw;
            }
        }
    }
}