using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Seeders;

public class CitiesSeeder : ISeeder
{
    public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
    {
        if (dbContext.Cities.Any())
        {
            return;
        }
        var exCountries = await dbContext.Countries.FirstOrDefaultAsync(e => e.CountryName == "Jordan");
        var newCities = new List<Cities>
        {
            new Cities
            {
                CityName ="Amman",
                CountryId = exCountries.CountryId,
            },
            new Cities
            {
                CityName ="Zarqa",
                CountryId = exCountries.CountryId,
            },
            new Cities
            {
                CityName ="Aqaba",
                CountryId = exCountries.CountryId,
            },
        };
        dbContext.AddRange(newCities);
        dbContext.SaveChanges();
    }
}