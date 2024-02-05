using AreaBox_V0._1.Areas.User.Models.UMediaPostReportTypeDto.Send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostCommentsDto.Input;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostCommentsDto.Send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.Input;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostReportDto.Input;
using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]
public class QandAController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IUnitOfWork db;
	private readonly ILocationService location;
	private readonly int PageSize = 5;
	public QandAController(UserManager<ApplicationUser> userManager, IUnitOfWork _db, ILocationService _location)
	{
		_userManager = userManager;
		db = _db;
		location = _location;
	}
	public async Task<ActionResult> Index(int page = 1, int? categoryId = null)
	{

		var latitudeCookie = Request.Cookies["latitude"];
		var longitudeCookie = Request.Cookies["longitude"];

		// Initialize variables to store converted values
		double latitude;
		double longitude;

		// Try parsing the cookies into double values
		if (double.TryParse(latitudeCookie, out latitude) && double.TryParse(longitudeCookie, out longitude))
		{
			var loc = await location.GetGeolocationObject(latitude, longitude);
			await db.Countries.CheckAndInsertCountry(loc.Country);
			await db.Cities.CheckAndInsertCity(loc.City, loc.Country);
			int resultCount = await db.QuestionPosts.Count<QuestionPosts>();
			int pages = (int)Math.Ceiling((double)resultCount / PageSize);

			int skip = PageSize * (page - 1);
			int take = PageSize;
			var resalt = await db.QuestionPosts.FindAndFilter<QuestionPosts, QuestionPostsDto>(new[] { "Qpcity", "Qpuser", "Qpcategory", "Qpcity.Country" }, skip, take, e => e.Qpdate, OrderBy.Descending,
																									   loc.City != null ? e => e.Qpcity.CityName == loc.City : e => true, categoryId > 0 ? e => e.QpcategoryId == categoryId : e => true, e => e.Qpstate == false);
			var posts = new UQuestionPostIndexDto
			{
				questionPostsDtos = resalt,
				PagesCount = pages
			};


			return View(posts);
		}
		else
		{
			return View();
		}
	}

	public async Task<ActionResult> GetQuestionPostPartialList(int page = 1, int? categoryId = null)
	{
		var latitudeCookie = Request.Cookies["latitude"];
		var longitudeCookie = Request.Cookies["longitude"];

		// Initialize variables to store converted values
		double latitude;
		double longitude;

		// Try parsing the cookies into double values
		if (double.TryParse(latitudeCookie, out latitude) && double.TryParse(longitudeCookie, out longitude))
		{
			var loc = await location.GetGeolocationObject(latitude, longitude);
			await db.Countries.CheckAndInsertCountry(loc.Country);
			await db.Cities.CheckAndInsertCity(loc.City, loc.Country);
			// Retrieve posts from your data source based on the page number
			int skip = PageSize * (page - 1);
			int take = PageSize;
			var resalt = await db.QuestionPosts.FindAndFilter<QuestionPosts, QuestionPostsDto>(new[] { "Qpcity", "Qpuser", "Qpcategory", "Qpcity.Country" }, skip, take, e => e.Qpdate, OrderBy.Descending,
																									   loc.City != null ? e => e.Qpcity.CityName == loc.City : e => true, categoryId > 0 ? e => e.QpcategoryId == categoryId : e => true, e => e.Qpstate == false);

			return PartialView("_QuestionPostListPartial", resalt);
		}
		else
		{
			return BadRequest("Failed to retrieve geolocation information");
		}
	}

	public IActionResult AddPost()
	{
		return View();
	}

	#region Post Fun
	[HttpPost]
	public async Task<IActionResult> AddPost([FromForm] UQuestionPostInputDto questionPostInputDto)
	{
		var latitudeCookie = Request.Cookies["latitude"];
		var longitudeCookie = Request.Cookies["longitude"];

		// Initialize variables to store converted values
		double latitude;
		double longitude;
		if (double.TryParse(latitudeCookie, out latitude) && double.TryParse(longitudeCookie, out longitude))
		{
			var loc = await location.GetGeolocationObject(latitude, longitude);
			var cityPost = await db.Cities.Find<Cities, Cities>(e => e.CityName == loc.City);
			var userId = _userManager.GetUserId(User);
			if (cityPost == null)
			{
				return BadRequest("User Location error");
			}
			if (userId == null)
			{
				return BadRequest("User not logged in or no file selected for upload.");
			}
			if (questionPostInputDto.CategoryId == null || questionPostInputDto.Title == null || questionPostInputDto.Description == null)
			{
				return BadRequest("Fill the information !!");
			}

			var questionPost = new QuestionPosts
			{
				QpuserId = userId,
				Qpdate = DateTime.Now,
				QpcityId = cityPost.CitryId,
				QpcategoryId = questionPostInputDto.CategoryId,
				Qptitle = questionPostInputDto.Title,
				Qpdescription = questionPostInputDto.Description,
				Qpstate = false
			};

			db.QuestionPosts.Add(questionPost);
			await db.Save();

			return Ok("Post Added Successful!!!");
		}
		else
		{
			return BadRequest("User Location error");
		}
	}

	[HttpPost]
	public async Task<IActionResult> DeleteQuestionPost([FromForm] string questionPostId)
	{
		if (questionPostId == null)
		{
			return BadRequest("Choose question to delete");
		}
		var isExist = await db.QuestionPosts.CheckItemExistence<QuestionPosts>(e => e.QpostId == questionPostId);
		if (isExist == false)
		{
			return NotFound("The specified question was not found.");
		}
		var itemToDelete = await db.QuestionPosts.GetByIdAsync(questionPostId);
		db.QuestionPosts.Remove(itemToDelete);
		await db.Save();
		return Ok("The Question has been successfully deleted.");
	}

	[HttpGet]
	public async Task<IActionResult> GetQuestionPostDetails(string questionPostId)
	{
		if (questionPostId == null)
		{
			return BadRequest("Choose question to Get the details.");
		}

		var existingQuestionPost = await db.QuestionPosts.GetByIdAsync(questionPostId);

		if (existingQuestionPost == null)
		{
			return NotFound("The specified question post was not found.");
		}

		var getQuestionPost = new QuestionPostsDto
		{
			Id = existingQuestionPost.QpostId,
			CategoryId = existingQuestionPost.QpcategoryId,
			CityId = existingQuestionPost.QpcityId,
			Date = existingQuestionPost.Qpdate,
			UserId = existingQuestionPost.QpuserId,
			Title = existingQuestionPost.Qptitle,
			Description = existingQuestionPost.Qpdescription,
		};

		return Ok(getQuestionPost);
	}

	[HttpPost]
	public async Task<IActionResult> EditQuestionPost([FromForm] UQuestionPostEditDto questionPostEditDto)
	{
		var userId = _userManager.GetUserId(User);

		if (string.IsNullOrEmpty(userId))
		{
			return BadRequest("User is not authenticated. Please log in to continue.");
		}

		if (string.IsNullOrEmpty(questionPostEditDto.Id))
		{
			return BadRequest("Invalid question post ID provided.");
		}

		var existingQuestionPost = await db.QuestionPosts.GetByIdAsync(questionPostEditDto.Id);

		if (existingQuestionPost == null)
		{
			return BadRequest("The specified question post was not found.");
		}

		if (existingQuestionPost.QpuserId != userId)
		{
			return BadRequest("You are not authorized to edit this question post.");
		}

		existingQuestionPost.QpuserId = userId;
		existingQuestionPost.QpostId = questionPostEditDto.Id;
		existingQuestionPost.QpcityId = questionPostEditDto.CityId;
		existingQuestionPost.QpcategoryId = questionPostEditDto.CategoryId;
		existingQuestionPost.Qptitle = questionPostEditDto.Title;
		existingQuestionPost.Qpdescription = questionPostEditDto.Description;

		db.QuestionPosts.Update(existingQuestionPost);
		await db.Save();

		return Ok("Question post updated successfully.");
	}


	#endregion

	#region Comment Fun
	[HttpGet]
	public async Task<IActionResult> GetQuestionPostComments(string questionPostId, int page = 1)
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
			return BadRequest("The post not exists");
		}

		int resultCount = await db.QuestionPostComments.Count<QuestionPostComments>(e => e.QpostId == questionPostId);
		int pages = (int)Math.Ceiling((double)resultCount / PostConfig.QuestionPostCommentsSize);
		if (page > pages)
		{
			return Ok("No more comments");

		}
		int skip = PageSize * (page - 1);
		int take = PageSize;
		var results = await db.QuestionPostComments.FindAndFilter<QuestionPostComments, UQuestionPostCommentsOutputDto>(new[] { "User" }, skip, take, e => e.QpcommentDate, OrderBy.Descending, e => e.QpostId == questionPostId);
		return Ok(results);
	}

	[HttpPost]
	public async Task<IActionResult> AddCommentToQuestionPost([FromForm] UQuestionPostCommentsInputDto input)
	{
		var userId = _userManager.GetUserId(User);

		if (userId == null)
		{
			return BadRequest("Log in to comment on this post!");
		}

		if (input.PostId == null)
		{
			return BadRequest("Choose Question Post to comment.");
		}

		var quetionPost = await db.QuestionPosts.GetByIdAsync(input.PostId);


		if (quetionPost == null)
		{
			return BadRequest("The post not exists!");
		}

		var newComment = new QuestionPostComments
		{
			QpostId = input.PostId,
			UserId = userId,
			QpcommentContent = input.CommentContent,
			QpcommentDate = DateTime.Now
		};

		db.QuestionPostComments.Add(newComment);
		quetionPost.CommentCount++;
		db.QuestionPosts.Update(quetionPost);
		await db.Save();
		return Ok("Comment has been added");
	}

	#endregion

	[HttpGet]
	public async Task<IActionResult> GetQuestionPostReportTypes(string questionPostId)
	{
		if (questionPostId == null)
		{
			return BadRequest("Choose post to report");
		}

		var questionPost = await db.QuestionPosts.CheckItemExistence<QuestionPosts>(e => e.QpostId == questionPostId);

		if (questionPost == false)
		{
			return BadRequest("The post not exists");
		}

		var resalt = await db.ReportTypes.GetAllAsync<ReportTypes, UReportTypeOutPutDto>();
		return Ok(resalt);
	}

	#region Report Fun
	[HttpPost]
	public async Task<IActionResult> AddReportInQuestionPost([FromForm] UQuestionPostReportInputDto inputReport)
	{
		var userId = _userManager.GetUserId(User);
		if (userId == null)
		{
			return BadRequest("Please log in to report the post.");
		}

		if (inputReport.QpostId == null || inputReport.ReportTypeId == null)
		{
			return BadRequest("Please provide complete report details.");
		}
		var questionPostReport = await db.QuestionPostsReports.CheckItemExistence<QuestionPostsReports>(e => e.UserId == userId && e.QpostId == inputReport.QpostId);
		if (questionPostReport == true)
		{
			return BadRequest("You have already reported this post.");
		}


		var newQuestionReport = new QuestionPostsReports
		{
			UserId = userId,
			QpostId = inputReport.QpostId,
			ReportTypeId = inputReport.ReportTypeId,

		};

		db.QuestionPostsReports.Add(newQuestionReport);
		await db.Save();

		return Ok("Post has been successfully reported.");
	}

	#endregion
}
