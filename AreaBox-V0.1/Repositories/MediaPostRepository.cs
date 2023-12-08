using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace AreaBox_V0._1.Repositories
{
    public class MediaPostRepository : IMediaPost
    {
        private readonly AreaBoxDbContext _db;

        public MediaPostRepository(AreaBoxDbContext db)
        {
            _db = db;
        }

        public async Task<string> getCityById(int id)
        {
            var city = await _db.Cities.FindAsync(id);

            if(city != null)
            {
				return city.CityName;
            }

            return null;
            
		}


		public async Task<string> getCountryById(int id)
		{
			var country = await _db.Countries.FindAsync(id);

			if (country != null)
			{
				return country.CountryName;
			}

			return null;
		}

		public async Task Disable(string id)
		{
			var getMediaPost = await _db.MediaPosts.FindAsync(id);

			if (getMediaPost != null)
			{
				getMediaPost.Mpstate = !getMediaPost.Mpstate;
				_db.MediaPosts.Update(getMediaPost);
				await _db.SaveChangesAsync();
			}
		}

	}
}
