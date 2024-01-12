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

			var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
			var exUser2 = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User2");

			var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
			var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
			var mdeiaPost = dbContext.MediaPosts.FirstOrDefault(x => x.MpcategoryId == exCategories.CategoryId && x.MpcityId == exCity.CitryId);


			var misleadingType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Misleading");
			var explicitType = dbContext.ReportTypes.FirstOrDefault(x => x.Type == "Explicit");


			var newReportType = new List<MediaPostsReports>
						{
							new MediaPostsReports
							{
								MpostId = mdeiaPost.MpostId,
								ReportTypeId= misleadingType.ReportTypeId,
								UserId =exUser.Id
							},
							new MediaPostsReports
							{
								MpostId = mdeiaPost.MpostId,
								ReportTypeId = explicitType.ReportTypeId,
								UserId =exUser2.Id
							},

						};

			dbContext.MediaPostsReports.AddRange(newReportType);
			dbContext.SaveChanges();
		}
	}
}
