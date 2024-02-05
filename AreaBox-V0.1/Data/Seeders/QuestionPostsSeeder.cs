using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class QuestionPostsSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.QuestionPosts.Any())
        {
            return;
        }
        var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
        var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
        var exCity2 = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Zarqa");
        var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
        var exUser2 = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User2");
        var newQuestionPosts = new List<QuestionPosts>
        {
            new QuestionPosts
            {
                QpcategoryId  = exCategories.CategoryId,
                QpcityId = exCity.CitryId,
                Qpdate = DateTime.Now,
                QpuserId = exUser.Id,
                Qptitle = "this is test 1",
                Qpdescription = "this is long test 1",
                Qpstate = false,

            },
            new QuestionPosts
            {
                QpcategoryId  = exCategories.CategoryId,
                QpcityId = exCity2.CitryId,
                Qpdate = DateTime.Now.AddDays(-1),
                QpuserId = exUser.Id,
                Qptitle = "this is test 2",
                Qpdescription = "this is long test 2",
                Qpstate = false,
            },
            new QuestionPosts
            {
                QpcategoryId  = exCategories.CategoryId,
                QpcityId = exCity.CitryId,
                Qpdate = DateTime.Now.AddDays(-2),
                QpuserId = exUser2.Id,
                Qptitle = "this is test 3",
                Qpdescription = "this is long test 3",
                Qpstate = false,
            },
        };
        dbContext.AddRange(newQuestionPosts);
        dbContext.SaveChanges();
    }
}
