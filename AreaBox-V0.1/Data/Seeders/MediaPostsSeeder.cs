using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class MediaPostsSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.MediaPosts.Any())
        {
            return;
        }
        var exCategories = await dbContext.Categories.FirstOrDefaultAsync(e => e.CategoryName == "News");
        var exCity = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Amman");
        var exCity2 = await dbContext.Cities.FirstOrDefaultAsync(e => e.CityName == "Zarqa");
        var exUser = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User");
        var exUser2 = await dbContext.Users.FirstOrDefaultAsync(e => e.UserName == "User2");
        var newMediaPosts = new List<MediaPosts>
        {
            new MediaPosts
            {
                MpcategoryId  = exCategories.CategoryId,
                MpcityId = exCity.CitryId,
                Mpdate = DateTime.Now,
                MpuserId = exUser.Id,
                Mpimage = "1",
                MpshortDescription = "this is test 1",
                MplongDescription = "this is long test 1",
                Mpstate = false,

            },
            new MediaPosts
            {
                MpcategoryId  = exCategories.CategoryId,
                MpcityId = exCity2.CitryId,
                Mpdate = DateTime.Now.AddDays(1),
                MpuserId = exUser.Id,
                Mpimage = "2",
                MpshortDescription = "this is test 2",
                MplongDescription = "this is long test 2",
                Mpstate = false,
            },
            new MediaPosts
            {
                MpcategoryId  = exCategories.CategoryId,
                MpcityId = exCity.CitryId,
                Mpdate = DateTime.Now.AddDays(2),
                MpuserId = exUser2.Id,
                Mpimage = "3",
                MpshortDescription = "this is test 3",
                MplongDescription = "this is long test 3",
                Mpstate = false,
            },
        };
        dbContext.AddRange(newMediaPosts);
        dbContext.SaveChanges();
    }
}
