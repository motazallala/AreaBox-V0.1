using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Data.Seeders;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data
{
    public class AreaBoxDbContextSeeder
    {
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            var seeders = new List<ISeeder>
            {
                new RolesSeeder(),
                new UsersSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, logger);
            }

            logger.LogInformation("Finished executing seeders");
        }
    }
}
