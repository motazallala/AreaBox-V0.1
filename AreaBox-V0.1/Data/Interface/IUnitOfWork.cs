using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Interface;

public interface IUnitOfWork : IDisposable
{
	IUserManagement Users { get; }
	IRepository<Categories> Categories { get; }
	IRepository<Cities> Cities { get; }
	IRepository<Countries> Countries { get; }
	IRepository<MediaPostComments> MediaPostComments { get; }
	IRepository<MediaPostLikes> MediaPostLikes { get; }

	IMediaPostRepository MediaPosts { get; }
	IMediaPostReportsRepository MediaPostReports { get; }


	IRepository<PostReports> PostReports { get; }
	IRepository<PostType> PostTypes { get; }
	IRepository<QuestionPostComments> QuestionPostComments { get; }

	IQuestionPostRepository QuestionPosts { get; }

	IRepository<QuestionPostsReports> QuestionPostsReports { get; }
	IReportTypeRepository ReportTypes { get; }
	IRepository<TechnicalReports> TechnicalReports { get; }
	IRepository<UserCategories> UserCategories { get; }


	Task<int> Save();
}
