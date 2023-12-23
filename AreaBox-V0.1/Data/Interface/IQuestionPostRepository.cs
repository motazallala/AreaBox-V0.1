using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Interface
{
    public interface IQuestionPostRepository : IRepository<QuestionPosts>
    {
		Task<IEnumerable<QuestionPostsDto>> FindAndFilter(Expression<Func<QuestionPosts, bool>> match,
																			string[] includes = null,
																			int? cityId = null,
																			int? categoryId = null,
																			int? countryId = null,
																			int? skip = null,
																			int? take = null
																			);
		Task<int> CountQuestionPosts(Expression<Func<QuestionPosts, bool>> match,
									int? cityId = null,
									int? categoryId = null,
									int? countryId = null);
		Task Disable(string id, bool state);
    }
}
