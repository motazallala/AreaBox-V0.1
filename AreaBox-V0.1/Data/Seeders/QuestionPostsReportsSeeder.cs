using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class QuestionPostsReportsSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.QuestionPostsReports.Any())
        {
            return;
        }

        var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
        var exUser2 = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User2");

        var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
        var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
        var questionPost = dbContext.QuestionPosts.FirstOrDefault(x => x.QpcategoryId == exCategories.CategoryId && x.QpcityId == exCity.CitryId);



        var postQAType = dbContext.PostTypes.FirstOrDefault(x => x.Name == "QAPost");
        var misleadingType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Misleading");
        var explicitType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Explicit");
        var postReportExplicitType = dbContext.PostReports.FirstOrDefault(x => x.PostTypeId == postQAType.PostTypeId && x.ReportTypeId == explicitType.ReportTypeId);
        var postReportMisleadingType = dbContext.PostReports.FirstOrDefault(x => x.PostTypeId == postQAType.PostTypeId && x.ReportTypeId == misleadingType.ReportTypeId);

        var newQuestionPostsReports = new List<QuestionPostsReports>
            {
                new QuestionPostsReports
                {
                    QpostId = questionPost.QpostId,
                    PostReportId = postReportExplicitType.PostReportId,
                    UserId =exUser.Id
                },
                new QuestionPostsReports
                {
                    QpostId = questionPost.QpostId,
                    PostReportId = postReportMisleadingType.PostReportId,
                    UserId =exUser2.Id
                }
            };

        dbContext.QuestionPostsReports.AddRange(newQuestionPostsReports);
        dbContext.SaveChanges();
    }
}
