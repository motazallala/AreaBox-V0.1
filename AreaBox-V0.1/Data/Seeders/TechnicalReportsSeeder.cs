using AreaBox_V0._1.Data.Model;
namespace AreaBox_V0._1.Data.Seeders
{
    public class TechnicalReportsSeeder : ISeeder
    {
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            if (dbContext.TechnicalReports.Any())
            {
                return;
            }

            var users = dbContext.Users.ToList();

            var initialReports = new List<TechnicalReports>
            {
                new TechnicalReports
                {
                    Type = "Issue",
                    ReportDateTime = DateTime.Now,
                    Details = "Something is not working",
                    UserId = users.FirstOrDefault()?.Id,
                    UserEmail = users.FirstOrDefault()?.Email
                },
                new TechnicalReports
                {
                    Type = "Bug",
                    ReportDateTime = DateTime.Now,
                    Details = "Application crashes on startup",
                    UserId = users.LastOrDefault()?.Id,
                    UserEmail = users.LastOrDefault()?.Email
                },
            };

            dbContext.TechnicalReports.AddRange(initialReports);
            await dbContext.SaveChangesAsync();
        }
    }
}
