using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Repositories
{
	public class CountriesRepository : Repository<Countries>,ICountriesRepository
	{
        private readonly AreaBoxDbContext _db;
        private readonly IMapper _mapper;

        public CountriesRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
		{
            _db = db;
            _mapper = mapper;
		}

        public async Task CheckAndInsertCountry(string countryName)
        {
            var checkCountry = await _db.Countries.FirstOrDefaultAsync(e => e.CountryName == countryName);

            if (checkCountry == null)
            {
                var newCountry= new Countries
                {
                    CountryName = countryName,
                };

                await _db.Countries.AddAsync(newCountry);
                await _db.SaveChangesAsync();
            }
        }
    }
}
