using AreaBox_V0._1.Areas.User.Models.UQuestionPostCommentsDto.Send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.Input;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostReportDto.Input;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]
public class QandAController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IUnitOfWork db;
	private readonly int PageSize = 15;
	public QandAController(UserManager<ApplicationUser> userManager, IUnitOfWork _db)
	{
		_userManager = userManager;
		db = _db;
	}
	public async Task<ActionResult> Index(int page = 1)
	{
		int resultCount = await db.QuestionPosts.Count<QuestionPosts>();
		int pages = (int)Math.Ceiling((double)resultCount / PageSize);

		int skip = PageSize * (page - 1);
		int take = PageSize;
		var resalt = await db.MediaPosts.FindAndFilter<QuestionPosts, QuestionPostsDto>(new[] { "Qpcity", "Qpuser", "Qpcategory", "Qpcity.Country" }, skip, take);
		var posts = new UQuestionPostIndexDto
		{
			questionPostsDtos = resalt,
			PagesCount = pages
		};


		return View(posts);
	}

	public async Task<ActionResult> GetQuestionPostPartialList(int page = 1)
	{
		// Retrieve posts from your data source based on the page number
		int skip = PageSize * (page - 1);
		int take = PageSize;
		var resalt = await db.MediaPosts.FindAndFilter<QuestionPosts, QuestionPostsDto>(new[] { "Qpcity", "Qpuser", "Qpcategory", "Qpcity.Country" }, skip, take);

		return PartialView("_QuestionPostListPartial", resalt);
	}

	public IActionResult AddPost()
	{
		return View();
	}

	#region Post Fun
	[HttpPost]
	public async Task<IActionResult> AddPost([FromForm] UQuestionPostInputDto questionPostInputDto)
	{
		var userId = _userManager.GetUserId(User);

		if (userId == null)
		{
			return BadRequest("User not logged in or no file selected for upload.");
		}
		if (questionPostInputDto.CategoryId == null || questionPostInputDto.CityId == null || questionPostInputDto.Title == null || questionPostInputDto.Description == null)
		{
			return BadRequest("Fill the information !!");
		}

		var questionPost = new QuestionPosts
		{
			QpuserId = userId,
			Qpdate = DateTime.Now,
			QpcityId = questionPostInputDto.CityId,
			QpcategoryId = questionPostInputDto.CategoryId,
			Qptitle = questionPostInputDto.Title,
			Qpdescription = questionPostInputDto.Description,
			Qpstate = false
		};

		db.QuestionPosts.Add(questionPost);
		await db.Save();

		return Ok("Post Added Successful!!!");
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteQuestionPost([FromForm] string questionPostId)
	{
		if (questionPostId == null)
		{
			return BadRequest("Choose post to delete");
		}
		var isExist = await db.QuestionPosts.CheckItemExistence<QuestionPosts>(e => e.QpostId == questionPostId);
		if (isExist == false)
		{
			return NotFound("the post not Found");
		}
		var itemToDelete = await db.QuestionPosts.GetByIdAsync(questionPostId);
		db.QuestionPosts.Remove(itemToDelete);
		await db.Save();
		return Ok("the post is deleted!");
	}

	#endregion

	#region Comment Fun
	[HttpGet]
	public async Task<IActionResult> GetQuestionPostComments([FromForm] string questionPostId, [FromForm] int page = 1)
	{
		if (page < 1)
		{
			page = 1;
		}
		if (questionPostId == null)
		{
			return BadRequest("Choose post to display comment ");
		}
		var questionPosts = await db.QuestionPosts.CheckItemExistence<QuestionPosts>(e => e.QpostId == questionPostId);
		if (questionPosts == false)
		{
			return BadRequest("the post not Exists");
		}
		int resultCount = await db.QuestionPostComments.Count<QuestionPostComments>();
		int pages = (int)Math.Ceiling((double)resultCount / PageSize);
		if (page > pages)
		{
			return Ok("no more posts");

		}
		int skip = PageSize * (page - 1);
		int take = PageSize;
		var resalt = await db.QuestionPostComments.FindAndFilter<QuestionPostComments, UQuestionPostCommentsOutputDto>(new[] { "User" }, skip, take, e => e.QpostId == questionPostId);
		return Ok(resalt);
	}

	#endregion

	#region Report Fun
	[HttpPost]
	public async Task<IActionResult> AddReportInMediaPost([FromForm] UQuestionPostReportInputDto inputReport)
	{
		var userId = _userManager.GetUserId(User);
		if (userId == null)
		{
			return BadRequest("Log in to report the post");
		}

		if (inputReport.QpostId == null || inputReport.ReportTypeId == null)
		{
			return BadRequest("Fill the information !!");
		}
		var questionPostReport = await db.QuestionPostsReports.CheckItemExistence<QuestionPostsReports>(e => e.UserId == userId && e.QpostId == inputReport.QpostId);
		if (questionPostReport == true)
		{
			return BadRequest("the report Exists");
		}
		var postType = await db.PostTypes.Find<PostType, PostType>(e => e.Name == "QAPost");
		var newReport = new PostReports
		{
			ReportTypeId = inputReport.ReportTypeId,
			PostTypeId = postType.PostTypeId
		};


		db.PostReports.Add(newReport);
		await db.Save();


		var newQuestionReport = new QuestionPostsReports
		{
			UserId = userId,
			QpostId = inputReport.QpostId,
			PostReportId = newReport.PostReportId,

		};

		db.QuestionPostsReports.Add(newQuestionReport);
		await db.Save();

		return Ok("post reported");
	}

	#endregion
}
