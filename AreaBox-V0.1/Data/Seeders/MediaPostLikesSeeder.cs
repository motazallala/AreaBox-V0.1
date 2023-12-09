using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class MediaPostLikesSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.MediaPostLikes.Any())
        {
            return;
        }

        var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
        var exUser2 = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User2");

        var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
        var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
        var mdeiaPost = dbContext.MediaPosts.FirstOrDefault(x => x.MpcategoryId == exCategories.CategoryId && x.MpcityId == exCity.CitryId);


        var newMediaPostLikes = new List<MediaPostLikes>
            {
                new MediaPostLikes
                {
                    MpostId = mdeiaPost.MpostId,
                    UserId =exUser.Id
                },
                new MediaPostLikes
                {
                    MpostId = mdeiaPost.MpostId,
                    UserId =exUser2.Id
                },

            };

        dbContext.MediaPostLikes.AddRange(newMediaPostLikes);
        dbContext.SaveChanges();
    }
}
