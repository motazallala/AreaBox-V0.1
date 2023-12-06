using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Seeders
{
    public class ReportTypesSeeder : ISeeder
    {
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            if (dbContext.ReportTypes.Any())
            {
                return;
            }

            var newReportType = new List<ReportTypes>
            {
                new ReportTypes
                {
                    Type = "Explicit",
                    Description = "Report for containing explicit content."
                },
                new ReportTypes
                {
                    Type = "Misleading",
                    Description = "Report for with misleading information."
                },
                new ReportTypes
                {
                    Type = "Harassment",
                    Description = "Report for involving harassment."
                },
                new ReportTypes
                {
                    Type = "Violence",
                    Description = "Report for containing violent content."
                },
                new ReportTypes
                {
                    Type = "Spam",
                    Description = "Report for marked as spam."
                },

                new ReportTypes
                {
                    Type = "Offensive Language",
                    Description = "Report for containing offensive language."
                }
            };

            dbContext.ReportTypes.AddRange(newReportType);
            await dbContext.SaveChangesAsync();
        }
    }
}
