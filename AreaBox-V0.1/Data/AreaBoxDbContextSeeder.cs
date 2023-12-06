using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Data.Seeders;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data
{
    public class AreaBoxDbContextSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AreaBoxDbContextSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            var seeders = new List<ISeeder>
            {
                new RolesSeeder(_roleManager),
                new UsersSeeder(_userManager),
                new CategoriesSeeder(),
                new UserCategoriesSeeder(),
                new CountriesSeeder(),
                new CitiesSeeder(),
                new MediaPostsSeeder(),
                new QuestionPostsSeeder(),
                new PostTypesSeeder(),
                new ReportTypesSeeder(),
                new PostReportsSeeder(),
                new MediaPostReportsSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, logger);
            }

            logger.LogInformation("Finished executing seeders");
        }
    }
}
