using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Repositories
{
    public class QuestionPostRepository : IQuestionPost
    {
        private AreaBoxDbContext _db;

        public QuestionPostRepository(AreaBoxDbContext db)
        {
            _db = db;
        }

        public async void Disable(Guid id)
        {
            var getQAPost = await _db.QuestionPosts.FindAsync(id);

            if (getQAPost != null)
            {
                getQAPost.Qpstate = !getQAPost.Qpstate;
                _db.QuestionPosts.Update(getQAPost);
                await _db.SaveChangesAsync();
            }
        }
    }
}
