using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Seeders
{
    public interface ISeeder
    {
        public Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger);
    }
}
