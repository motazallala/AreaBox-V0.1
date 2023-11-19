using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Seeders;

public class CategoriesSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.Categories.Any())
        {
            return;
        }
        var newCategories = new List<Categories>
        {
            new Categories
            {
                CategoryName = "News"
            },
            new Categories
            {
                CategoryName = "Sport"
            }
        };
        dbContext.Categories.AddRange(newCategories);
        dbContext.SaveChanges();
    }
}
