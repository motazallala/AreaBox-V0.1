using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;

namespace AreaBox_V0._1.Data.Repositories
{
	public class QuestionPostCommentsRepository : Repository<QuestionPostComments>,IQuestionPostCommentsRepository
	{
		public QuestionPostCommentsRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
		{
		}
	}
}
