namespace AreaBox_V0._1.Data.Interface;

public interface IUnitOfWork : IDisposable
{
	IUserManagementRepository Users { get; }
	ICategoriesRepository Categories { get; }
	ICitiesRepository Cities { get; }
	ICountriesRepository Countries { get; }
	IMediaPostCommentsRepository MediaPostComments { get; }
	IMediaPostLikesRepository MediaPostLikes { get; }

	IMediaPostRepository MediaPosts { get; }
	IMediaPostReportsRepository MediaPostReports { get; }


	IQuestionPostCommentsRepository QuestionPostComments { get; }

	IQuestionPostRepository QuestionPosts { get; }

	IQuestionPostReportsRepository QuestionPostsReports { get; }
	IReportTypeRepository ReportTypes { get; }
	ITechnicalReportsRepository TechnicalReports { get; }
	IUserCategoriesRepository UserCategories { get; }
	IUserMediaPostSaveRepository SavedMediaPosts { get; }
	IUserQuestionPostSaveRepository SavedQuestionPosts { get; }


	Task<int> Save();
}
