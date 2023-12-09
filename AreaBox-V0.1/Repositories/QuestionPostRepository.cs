using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;

namespace AreaBox_V0._1.Repositories
{
    public class QuestionPostRepository : IQuestionPost
    {
		private readonly IRepository<QuestionPosts> _repoQuestionPosts;

		public QuestionPostRepository(IRepository<QuestionPosts> repoQuestionPosts)
        {
			_repoQuestionPosts = repoQuestionPosts;
        }

		public async Task Disable(string id, bool state)
		{
			var getMediaPost = await _repoQuestionPosts.GetByIdAsync(id);

			if (getMediaPost != null)
			{
				getMediaPost.Qpstate = !state;
				_repoQuestionPosts.Update(getMediaPost);
				await _repoQuestionPosts.SaveChnagesAsync();
			}
		}
	}
}