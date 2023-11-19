using AreaBox_V0._1.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data.Seeders
{
    public class RolesSeeder : ISeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            if (dbContext.Roles.Any())
            {
                return;
            }

            var rolesNames = new List<string>
            {
                "SuperAdmin","ContentManager","TechnicalSupport","User"
            };

            foreach (var role in rolesNames)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            logger.LogInformation($"Finished executing {nameof(RolesSeeder)}");
        }
    }
}
