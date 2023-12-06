using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Seeders
{
    public class PostTypesSeeder : ISeeder
    {
        public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
        {
            if (dbContext.PostTypes.Any())
            {
                return;
            }

            var newPostType = new List<PostType>
            {
                new PostType
                {
                    Name = "MediaPost"
                },
                new PostType
                {
                    Name = "QAPost"
                }
            };
            dbContext.PostTypes.AddRange(newPostType);
            await dbContext.SaveChangesAsync();
        }
    }
}
