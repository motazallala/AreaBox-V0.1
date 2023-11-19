using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Seeders;

public class CountriesSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.Countries.Any())
        {
            return;
        }
        var newCountries = new List<Countries>
        {
            new Countries
            {
                CountryName = "Jordan"
            },
            new Countries
            {
                CountryName = "Usa"
            }
        };
        dbContext.Countries.AddRange(newCountries);
        dbContext.SaveChanges();
    }
}
