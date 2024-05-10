using Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class StartupDbExtensions
    {
        public static async void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<HotelDbContext>>();
            var blogContext = services.GetRequiredService<HotelDbContext>();

            try
            {
                var databsecrate = blogContext.Database.GetService<IDatabaseCreator>()
                   as RelationalDatabaseCreator;
                if (databsecrate != null)
                {
                    logger.LogInformation("enter databsecrate");
                    if (!databsecrate.CanConnect())
                    {

                        databsecrate.Create();
                        logger.LogInformation("enter databsecrate Create");
                    }

                    if (!databsecrate.HasTables())
                    {
                        databsecrate.CreateTables();
                        logger.LogInformation("enter databsecrate CreateTables");
                    }
                    DBInitializerSeedData.InitializeDatabase(blogContext);
                    logger.LogInformation("enter databsecrate InitializeDatabase");
                }
               
            }
            catch (Exception ex)
            {

                logger.LogError($"migration issue {ex.Message}");
            }
        }
    }
}
