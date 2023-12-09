using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Seeders
{
    public class PostReportsSeeder : ISeeder
    {
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            if (dbContext.PostReports.Any())
            {
                return;
            }

            var postMPType = dbContext.PostTypes.FirstOrDefault(x => x.Name == "MediaPost");
            var postQAType = dbContext.PostTypes.FirstOrDefault(x => x.Name == "QAPost");
            var explicitType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Explicit");
            var misleadingType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Misleading");
            var HarassmentType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Harassment");
            var newReportType = new List<PostReports>
            {
                new PostReports
                {
                    PostTypeId = postMPType.PostTypeId,
                    ReportTypeId = explicitType.ReportTypeId

                },
                new PostReports
                {
                    PostTypeId = postMPType.PostTypeId,
                    ReportTypeId = misleadingType.ReportTypeId

                },
                new PostReports
                {
                    PostTypeId = postQAType.PostTypeId,
                    ReportTypeId = explicitType.ReportTypeId
                },
                new PostReports
                {
                    PostTypeId = postQAType.PostTypeId,
                    ReportTypeId = misleadingType.ReportTypeId
                }
            };

            dbContext.PostReports.AddRange(newReportType);
            await dbContext.SaveChangesAsync();
        }
    }
}
