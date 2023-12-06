using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace AreaBox_V0._1.Repositories
{
    public class MediaPostRepository : IMediaPost
    {
        private AreaBoxDbContext _db;

        public MediaPostRepository(AreaBoxDbContext db)
        {
            _db = db;
        }

        public async void Disable(Guid id)
        {
            var getMediaPost = await _db.MediaPosts.FindAsync(id);

            if(getMediaPost != null)
            {
                getMediaPost.Mpstate = !getMediaPost.Mpstate;
                _db.MediaPosts.Update(getMediaPost);
                await _db.SaveChangesAsync();
            }
        }
    }
}
