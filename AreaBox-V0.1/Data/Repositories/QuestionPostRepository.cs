using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;

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

        public async Task Disable(string id, bool state)
        {
            var getMediaPost = await _db.QuestionPosts.FindAsync(id);

            if (getMediaPost != null)
            {
                getMediaPost.Qpstate = !state;
                _db.QuestionPosts.Update(getMediaPost);
            }
        }
    }
}