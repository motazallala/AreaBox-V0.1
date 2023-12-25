using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data;

public class UnitOfWork : IUnitOfWork
{
	public IUserManagement Users { get; private set; }
	public IRepository<Categories> Categories { get; private set; }

	public IRepository<Cities> Cities { get; private set; }

	public IRepository<Countries> Countries { get; private set; }

	public IRepository<MediaPostComments> MediaPostComments { get; private set; }

	public IRepository<MediaPostLikes> MediaPostLikes { get; private set; }

	public IMediaPostRepository MediaPosts { get; private set; }

	public IMediaPostReportsRepository MediaPostReports { get; private set; }

	public IRepository<PostReports> PostReports { get; private set; }

	public IRepository<PostType> PostTypes { get; private set; }

	public IRepository<QuestionPostComments> QuestionPostComments { get; private set; }

	public IQuestionPostRepository QuestionPosts { get; private set; }

	public IRepository<QuestionPostsReports> QuestionPostsReports { get; private set; }

	public IReportTypeRepository ReportTypes { get; private set; }

	public IRepository<TechnicalReports> TechnicalReports { get; private set; }

	public IRepository<UserCategories> UserCategories { get; private set; }


	private readonly AreaBoxDbContext db;
	private readonly IMapper mapper;
	private readonly UserManager<ApplicationUser> userManager;
	public UnitOfWork(AreaBoxDbContext _db, IMapper _mapper, UserManager<ApplicationUser> _userManager)
	{
		db = _db;
		mapper = _mapper;
		userManager = _userManager;
		Users = new UserManagementRepository(db, mapper, userManager);
		Categories = new Repository<Categories>(db, mapper);
		Cities = new Repository<Cities>(db, mapper);
		Countries = new Repository<Countries>(db, mapper);
		MediaPostComments = new Repository<MediaPostComments>(db, mapper);
		MediaPostLikes = new Repository<MediaPostLikes>(db, mapper);
		MediaPosts = new MediaPostRepository(db, mapper);
		MediaPostReports = new MediaPostReportsRepository(db, mapper);
		PostReports = new Repository<PostReports>(db, mapper);
		PostTypes = new Repository<PostType>(db, mapper);
		QuestionPostComments = new Repository<QuestionPostComments>(db, mapper);
		QuestionPosts = new QuestionPostRepository(db, mapper);
		QuestionPostsReports = new Repository<QuestionPostsReports>(db, mapper);
		ReportTypes = new ReportTypeRepository(db, mapper);
		TechnicalReports = new Repository<TechnicalReports>(db, mapper);
		UserCategories = new Repository<UserCategories>(db, mapper);
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
