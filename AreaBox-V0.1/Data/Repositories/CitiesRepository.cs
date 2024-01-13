using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Repositories
{
	public class CitiesRepository : Repository<Cities>, ICitiesRepository
	{
        private readonly AreaBoxDbContext _db;
        private readonly IMapper _mapper;

        public CitiesRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
		{
            _db = db;
            _mapper = mapper;
		}

        public async Task CheckAndInsertCity(string cityName, string countryName)
        {
            var checkCity = await _db.Cities.FirstOrDefaultAsync(e => e.CityName == cityName);
            var getCountryId = await _db.Countries.FirstOrDefaultAsync(e => e.CountryName == countryName);

            if(checkCity == null)
            {
                var newCity = new Cities
                {
                    CityName = cityName,
                    CountryId = getCountryId.CountryId
                };

                await _db.Cities.AddAsync(newCity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
