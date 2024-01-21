using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Repositories
{
	public class MediaPostRepository : Repository<MediaPosts>, IMediaPostRepository
	{
		private readonly IMapper _mapper;
		private readonly AreaBoxDbContext _db;
		public MediaPostRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
		{
			_mapper = mapper;
			_db = db;
		}

		public async Task<int> CountMediaPost(Expression<Func<MediaPosts, bool>> match, int? cityId = null, int? categoryId = null, int? countryId = null)
		{
			IQueryable<MediaPosts> query = _db.MediaPosts.AsQueryable();

			if (countryId != null)
			{
				query = query.Where(e => e.Mpcity.CountryId == countryId);
			}

			if (cityId != null)
			{
				query = query.Where(e => e.MpcityId == cityId);
			}

			if (categoryId != null)
			{
				query = query.Where(match).Where(e => e.MpcategoryId == categoryId);
			}
			return await query.CountAsync();
		}

		public async Task Disable(string id, bool state)
		{

			var getMediaPost = await _db.MediaPosts.FindAsync(id);

			if (getMediaPost != null)
			{
				getMediaPost.Mpstate = !state;
				_db.MediaPosts.Update(getMediaPost);
			}
		}


		public async Task<IEnumerable<MediaPostsDto>> FindAndFilter(Expression<Func<MediaPosts, bool>> match, string[] includes = null, int? cityId = null, int? categoryId = null, int? countryId = null, int? skip = null, int? take = null)
		{
			IQueryable<MediaPosts> query = _db.MediaPosts.AsQueryable();

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			if (countryId != null)
			{
				query = query.Where(e => e.Mpcity.CountryId == countryId);
			}

			if (cityId != null)
			{
				query = query.Where(e => e.MpcityId == cityId);
			}

			if (categoryId != null)
			{
				query = query.Where(e => e.MpcategoryId == categoryId);
			}
			if (skip != null)
			{
				int skipItem = skip.Value;
				query = query.Skip(skipItem);
			}

			if (take != null)
			{
				int takeItem = take.Value;
				query = query.Take(takeItem);
			}
			var entities = await query.Where(match).ToListAsync();
			var viewModels = _mapper.Map<IEnumerable<MediaPostsDto>>(entities);
			return viewModels;
		}
	}
}
