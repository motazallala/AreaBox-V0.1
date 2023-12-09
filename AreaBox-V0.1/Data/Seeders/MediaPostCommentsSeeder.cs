using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class MediaPostCommentsSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.MediaPostComments.Any())
        {
            return;
        }

        var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
        var exUser2 = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User2");

        var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
        var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
        var exCity2 = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Zarqa");
        var mdeiaPost = dbContext.MediaPosts.FirstOrDefault(x => x.MpcategoryId == exCategories.CategoryId && x.MpcityId == exCity.CitryId);
        var mdeiaPost2 = dbContext.MediaPosts.FirstOrDefault(x => x.MpcategoryId == exCategories.CategoryId && x.MpcityId == exCity2.CitryId);


        var newMediaPostComments = new List<MediaPostComments>
            {
                new MediaPostComments
                {
                    MpostId = mdeiaPost.MpostId,
                    MpcommnetDate = DateTime.Now,
                    MpcommentContent = "this is test 1 for the post 1",
                    UserId = exUser.Id

                },
                new MediaPostComments
                {
                    MpostId = mdeiaPost.MpostId,
                    MpcommnetDate = DateTime.Now,
                    MpcommentContent = "this is test 2 for the post 1",
                     UserId = exUser2.Id

                },
                new MediaPostComments
                {
                    MpostId = mdeiaPost2.MpostId,
                    MpcommnetDate = DateTime.Now,
                    MpcommentContent = "this is test 1 for the post 2",
                    UserId = exUser.Id
                },
                new MediaPostComments
                {
                    MpostId = mdeiaPost2.MpostId,
                    MpcommnetDate = DateTime.Now,
                    MpcommentContent = "this is test 2 for the post 2",
                    UserId = exUser2.Id
                }
            };

        dbContext.MediaPostComments.AddRange(newMediaPostComments);
        dbContext.SaveChanges();
    }
}
