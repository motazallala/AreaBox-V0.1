using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data;

public class UnitOfWork : IUnitOfWork
{
	public IUserManagementRepository Users { get; private set; }

	public ICategoriesRepository Categories { get; private set; }

	public ICitiesRepository Cities { get; private set; }

	public ICountriesRepository Countries { get; private set; }

	public IMediaPostCommentsRepository MediaPostComments { get; private set; }

	public IMediaPostLikesRepository MediaPostLikes { get; private set; }

	public IMediaPostRepository MediaPosts { get; private set; }

	public IMediaPostReportsRepository MediaPostReports { get; private set; }

	public IQuestionPostCommentsRepository QuestionPostComments { get; private set; }

	public IQuestionPostRepository QuestionPosts { get; private set; }

	public IQuestionPostReportsRepository QuestionPostsReports { get; private set; }

	public IReportTypeRepository ReportTypes { get; private set; }

	public ITechnicalReportsRepository TechnicalReports { get; private set; }

	public IUserCategoriesRepository UserCategories { get; private set; }

	public IUserMediaPostSaveRepository SavedMediaPosts { get; private set; }

	public IUserQuestionPostSaveRepository SavedQuestionPosts { get; private set; }


	private readonly AreaBoxDbContext db;
	private readonly IMapper mapper;
	private readonly UserManager<ApplicationUser> userManager;
	public UnitOfWork(AreaBoxDbContext _db, IMapper _mapper, UserManager<ApplicationUser> _userManager)
	{
		db = _db;
		mapper = _mapper;
		userManager = _userManager;
		Users = new UserManagementRepository(db, mapper, userManager);
		Categories = new CategoriesRepository(db, mapper);
		Cities = new CitiesRepository(db, mapper);
		Countries = new CountriesRepository(db, mapper);
		MediaPostComments = new MediaPostCommentsRepository(db, mapper);
		MediaPostLikes = new MediaPostLikesRepository(db, mapper);
		MediaPosts = new MediaPostRepository(db, mapper);
		MediaPostReports = new MediaPostReportsRepository(db, mapper);
		QuestionPostComments = new QuestionPostCommentsRepository(db, mapper);
		QuestionPosts = new QuestionPostRepository(db, mapper);
		QuestionPostsReports = new QuestionPostReportsRepository(db, mapper);
		ReportTypes = new ReportTypeRepository(db, mapper);
		TechnicalReports = new TechnicalReportsRepository(db, mapper);
		UserCategories = new UserCategoriesRepository(db, mapper);
        SavedMediaPosts = new UserMediaPostSaveRepository(db, mapper);
        SavedQuestionPosts = new UserQuestionPostSaveRepository(db, mapper);
	}

	public void Dispose()
	{
		db.Dispose();
	}

	public async Task<int> Save()
	{
		return await db.SaveChangesAsync();
	}
}
