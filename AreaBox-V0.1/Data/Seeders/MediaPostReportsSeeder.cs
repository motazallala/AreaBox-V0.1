using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders
{
    public class MediaPostReportsSeeder : ISeeder
    {
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            if (dbContext.MediaPostsReports.Any())
            {
                return;
            }

            var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
            var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
            var exCity2 = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Zarqa");
            var mdeiaPost = dbContext.MediaPosts.FirstOrDefault(x => x.MpcategoryId == exCategories.CategoryId && x.MpcityId == exCity.CitryId);
            var mdeiaPost2 = dbContext.MediaPosts.FirstOrDefault(x => x.MpcategoryId == exCategories.CategoryId && x.MpcityId == exCity2.CitryId);

            var postMPType = dbContext.PostTypes.FirstOrDefault(x => x.Name == "MediaPost");
            var postQAType = dbContext.PostTypes.FirstOrDefault(x => x.Name == "QAPost");
            var misleadingType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Misleading");
            var explicitType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Explicit");
            var postReport = dbContext.PostReports.FirstOrDefault(x => x.PostTypeId == postMPType.PostTypeId && x.ReportTypeId == explicitType.ReportTypeId);
            var postReport2 = dbContext.PostReports.FirstOrDefault(x => x.PostTypeId == postQAType.PostTypeId && x.ReportTypeId == misleadingType.ReportTypeId);

            var newReportType = new List<MediaPostsReports>
            {
                new MediaPostsReports
                {
                    MpostId = mdeiaPost.MpostId,
                    PostReportId = postReport.PostReportId
                },
                new MediaPostsReports
                {
                    MpostId = mdeiaPost2.MpostId,
                    PostReportId = postReport2.PostReportId
                }
            };

            dbContext.MediaPostsReports.AddRange(newReportType);
            await dbContext.SaveChangesAsync();
        }
    }
}
