using AreaBox_V0._1.Areas.Admin.Models.MediaPostReportsDto.send;
using AreaBox_V0._1.Areas.Admin.Models.Pages.ReportManagement.send;
using AreaBox_V0._1.Areas.Admin.Models.QuestionPostReportsDto.send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class ReportManagementController : Controller
{
	private readonly IUnitOfWork db;

	public ReportManagementController(IUnitOfWork _db)
	{
		db = _db;
	}

	public async Task<IActionResult> Index()
	{
		var mediaPostCount = await db.MediaPosts.Count();
		var mediaPostReportCount = await db.MediaPostReports.Count();
		var questionPostCount = await db.QuestionPosts.Count();
		var questionPostReportCount = await db.QuestionPostsReports.Count();

		var MpQpReports = new ReportsStatisticDto
		{
			MediaPostReportCount = mediaPostReportCount,
			MediaPostReportPercentage = (double)((double)mediaPostReportCount / (double)mediaPostCount) * 100.0,
			QuestiomPostReportCount = questionPostReportCount,
			QuestionPostReportPercentage = (double)((double)questionPostReportCount / (double)questionPostCount) * 100.0
		};
		return View(MpQpReports);
	}
	public async Task<IActionResult> MediaPostsReports(int id = 1, int pageSize = 5)
	{

		int skip = pageSize * (id - 1);
		int take = pageSize;
		var result = await db.MediaPostReports.FindAndFilter<MediaPostsReports, MediaPostsReportsDto>(new[] { "Mpost", "PostReport", "User" }, skip, take);


		int resultCount = await db.MediaPostReports.Count<MediaPostsReports>();


		if (id <= 0)
		{
			id = 1;
		}

		if (pageSize <= 0)
		{
			pageSize = 5;
		}

		int pages = (int)Math.Ceiling((double)resultCount / pageSize);
		var par = new Dictionary<string, string>();
		par.Add("id", id.ToString());
		par.Add("pageSize", pageSize.ToString());

		var mediaPostsReportPaging = new MediaPostsReportIndexDto
		{
			Action = nameof(MediaPostsReports),
			Controller = "ReportManagement",
			CurrentPage = id,
			PagesCount = pages,
			paramss = par,
			mediaPostReports = result,

		};
		return View(mediaPostsReportPaging);
	}

	public async Task<IActionResult> QuestionPostsReports(int id = 1, int pageSize = 5)
	{
		if (id <= 0)
		{
			id = 1;
		}

		if (pageSize <= 0)
		{
			pageSize = 5;
		}
		int skip = pageSize * (id - 1);
		int take = pageSize;
		var result = await db.MediaPostReports.FindAndFilter<QuestionPostsReports, QuestionPostsReportsDto>(new[] { "Qpost", "PostReports", "User" }, skip, take);


		int resultCount = await db.MediaPostReports.Count<MediaPostsReports>();


		int pages = (int)Math.Ceiling((double)resultCount / pageSize);
		if (id >= pages)
		{
			id = 1;
		}
		var par = new Dictionary<string, string>();
		par.Add("id", id.ToString());
		par.Add("pageSize", pageSize.ToString());

		var questionPostsReportPaging = new QuestionPostsReportIndexDto
		{
			Action = nameof(QuestionPostsReports),
			Controller = "ReportManagement",
			CurrentPage = id,
			PagesCount = pages,
			paramss = par,
			questionPostsReports = result,

		};
		return View(questionPostsReportPaging);

	}
	public async Task<IActionResult> Details(string id)
	{
		var getReport = await db.PostReports.GetByIdAsync(id);
		return View(getReport);
	}

	public async Task<IActionResult> Disable(Guid id)
	{
		/*        var getReportMPost = await _reportType.GetPostByReportId<MediaPostViewModel>(id);
                var getReportQAPost = await _reportType.GetPostByReportId<QuestionPostViewModel>(id);

                if (getReportMPost != null)
                {
                    getReportMPost.State = !getReportMPost.State;
                    _repoMediaPost.Update(getReportMPost);
                }
                else if (getReportQAPost != null)
                {
                    getReportQAPost.State = !getReportQAPost.State;
                    _repoQuestionPost.Update(getReportQAPost);
                }
                else
                {
                    return NotFound("Post not found");
                }

                await _repoReportType.SaveChnageAsync();*/
		return Ok("Post state updated successfully");
	}
}
