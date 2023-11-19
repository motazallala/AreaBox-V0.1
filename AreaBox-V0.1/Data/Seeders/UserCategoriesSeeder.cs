using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class UserCategoriesSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.UserCategories.Any())
        {
            return;
        }
        var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
        var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
        var exCategories2 = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "Sport");

        var newUserCategories = new List<UserCategories>
        {
            new UserCategories
            {
                UserId = exUser.Id,
                CategoryId = exCategories.CategoryId
            },
            new UserCategories
            {
                UserId = exUser.Id,
                CategoryId = exCategories2.CategoryId
            },
        };
        dbContext.AddRange(newUserCategories);
        dbContext.SaveChanges();
    }
}
