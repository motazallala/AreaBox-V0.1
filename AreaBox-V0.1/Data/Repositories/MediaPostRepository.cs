using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;

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

        public async Task Disable(string id, bool state)
        {

            var getMediaPost = await _db.MediaPosts.FindAsync(id);

            if (getMediaPost != null)
            {
                getMediaPost.Mpstate = !state;
                _db.MediaPosts.Update(getMediaPost);
            }
        }
    }
}
