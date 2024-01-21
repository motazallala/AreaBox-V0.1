using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class QuestionPostCommentsSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.QuestionPostComments.Any())
        {
            return;
        }

        var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
        var exUser2 = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User2");

        var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
        var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
        var exCity2 = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Zarqa");
        var questionPost = dbContext.QuestionPosts.FirstOrDefault(x => x.QpcategoryId == exCategories.CategoryId && x.QpcityId == exCity.CitryId);
        var questionPost2 = dbContext.QuestionPosts.FirstOrDefault(x => x.QpcategoryId == exCategories.CategoryId && x.QpcityId == exCity2.CitryId);


        var newQuestionPostComments = new List<QuestionPostComments>
            {
                new QuestionPostComments
                {
                    QpostId = questionPost.QpostId,
                    QpcommentDate = DateTime.Now,
                    QpcommentContent = "this is test 1 for the post 1",
                    UserId = exUser.Id

                },
                new QuestionPostComments
                {
                    QpostId = questionPost.QpostId,
                    QpcommentDate = DateTime.Now,
                    QpcommentContent = "this is test 2 for the post 1",
                     UserId = exUser2.Id

                },
                new QuestionPostComments
                {
                    QpostId = questionPost2.QpostId,
                    QpcommentDate = DateTime.Now,
                    QpcommentContent = "this is test 1 for the post 2",
                    UserId = exUser.Id
                },
                new QuestionPostComments
                {
                    QpostId = questionPost2.QpostId,
                    QpcommentDate = DateTime.Now,
                    QpcommentContent = "this is test 2 for the post 2",
                    UserId = exUser2.Id
                }
            };

        dbContext.QuestionPostComments.AddRange(newQuestionPostComments);
        dbContext.SaveChanges();
    }
}
