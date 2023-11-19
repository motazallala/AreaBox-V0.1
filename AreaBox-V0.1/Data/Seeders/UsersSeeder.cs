using AreaBox_V0._1.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data.Seeders
{
    public class UsersSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersSeeder(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {

            if (dbContext.Users.Any())
            {
                return;
            }

            var superAdminEmail = "SAdmin@gmail.com";
            var contentManagerEmail = "CManager@gmail.com";
            var technicalSupportEmail = "TSupport@gmail.com";
            var userEmail = "User@gmail.com";
            var userEmail2 = "User2@gmail.com";

            var initSuperAdmin = new ApplicationUser
            {
                FirstName = "Super",
                LastName = "Admin",
                UserName = "SuperAdmin",
                Gender = "Male",
                DOB = DateTime.Now,
                Email = superAdminEmail,

            };

            await _userManager.CreateAsync(initSuperAdmin, "Admin123@");
            await _userManager.AddToRoleAsync(initSuperAdmin, "SuperAdmin");


            var initContentManager = new ApplicationUser
            {
                FirstName = "Content",
                LastName = "Manager",
                UserName = "ContentManager",
                Gender = "Male",
                DOB = DateTime.Today,
                Email = contentManagerEmail,
            };

            await _userManager.CreateAsync(initContentManager, "Admin123@");
            await _userManager.AddToRoleAsync(initContentManager, "ContentManager");

            var initTechnicalSupport = new ApplicationUser
            {
                FirstName = "Technical",
                LastName = "Support",
                UserName = "TechnicalSupport",
                Gender = "Male",
                DOB = DateTime.Today,
                Email = technicalSupportEmail,
            };

            await _userManager.CreateAsync(initTechnicalSupport, "Admin123@");
            await _userManager.AddToRoleAsync(initTechnicalSupport, "TechnicalSupport");


            var initUser = new ApplicationUser
            {
                FirstName = "User",
                LastName = "",
                UserName = "User",
                Gender = "Male",
                DOB = DateTime.Today,
                Email = userEmail,
            };

            await _userManager.CreateAsync(initUser, "Admin123@");
            await _userManager.AddToRoleAsync(initUser, "User");

            var initUser2 = new ApplicationUser
            {
                FirstName = "User2",
                LastName = "",
                UserName = "User2",
                Gender = "Male",
                DOB = DateTime.Today,
                Email = userEmail2,
            };

            await _userManager.CreateAsync(initUser2, "Admin123@");
            await _userManager.AddToRoleAsync(initUser2, "User");



            logger.LogInformation($"Finished executing {nameof(UsersSeeder)}");
        }
    }
}
