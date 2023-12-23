using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Repositories
{
    public class QuestionPostRepository : Repository<QuestionPosts>, IQuestionPostRepository
    {
        private readonly IMapper _mapper;
        private readonly AreaBoxDbContext _db;
        public QuestionPostRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
        {
            _mapper = mapper;
            _db = db;
        }

		public async Task<int> CountQuestionPosts(Expression<Func<QuestionPosts, bool>> match, int? cityId = null, int? categoryId = null, int? countryId = null)
		{
            IQueryable<QuestionPosts> query = _db.QuestionPosts.AsQueryable();
            if (cityId != null)
            {
                query = query.Where(e => e.QpcityId == cityId);
            }
            if (categoryId != null)
            {
                query = query.Where(e => e.QpcategoryId == categoryId);
            }
            if (countryId != null)
            {
                query = query.Where(e => e.Qpcity.CountryId == countryId).Where(match);
            }
            return await query.CountAsync(); 
		}

		public async Task Disable(string id, bool state)
        {
            var getMediaPost = await _db.QuestionPosts.FindAsync(id);

            if (getMediaPost != null)
            {
                getMediaPost.Qpstate = !state;
                _db.QuestionPosts.Update(getMediaPost);
            }
        }

		public async Task<IEnumerable<QuestionPostsDto>> FindAndFilter(Expression<Func<QuestionPosts, bool>> match, string[] includes = null, int? cityId = null, int? categoryId = null, int? countryId = null, int? skip = null, int? take = null)
		{
			IQueryable<QuestionPosts> query = _db.QuestionPosts.AsQueryable();
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query=query.Include(include);
                }
            }
			if (cityId != null)
			{
				query = query.Where(e => e.QpcityId == cityId);
			}
			if (categoryId != null)
			{
				query = query.Where(e => e.QpcategoryId == categoryId);
			}
			if (countryId != null)
			{
				query = query.Where(e => e.Qpcity.CountryId == countryId);
			}
            if (skip != null)
            {
                int skipItem=skip.Value;
                query = query.Skip(skipItem);
            }
            if(take != null)
            {
                int takeItem=take.Value;
                query = query.Take(takeItem);
            }
            var entits = await query.Where(match).ToArrayAsync();
			return _mapper.Map<IEnumerable<QuestionPostsDto>>(entits);
		}
	}
}