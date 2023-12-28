using AreaBox_V0._1.Areas.Admin.Models.Pages.ReportManagement.send;
using AreaBox_V0._1.Areas.Admin.Models.ReportManagementViewModel;
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

  //      var mediaPostsReports = await db.MediaPostReports.GetAllAsync<MediaPostsReports, MediaPostsReportsDto>(new[] { "Mpost", "PostReport", "User" });
		//var questionPostsReports = await db.QuestionPostsReports.GetAllAsync<QuestionPostsReports, QuestionPostsReportsDto>(new[] { "Qpost", "PostReports", "User" });
		var MpQpReports = new ReportsStatisticDto
		{
            MediaPostReportCount = mediaPostReportCount,
			MediaPostReportPercentage =(double)((double)mediaPostReportCount / (double)mediaPostCount )*100.0,
            QuestiomPostReportCount = questionPostReportCount,
			QuestionPostReportPercentage=(double) ((double)questionPostReportCount / (double)questionPostCount)*100.0
        };
		return View(MpQpReports);
	}
	public async Task<IActionResult> MediaPostsReports()
	{
        var mediaPostsReports = await db.MediaPostReports.GetAllAsync<MediaPostsReports, MediaPostsReportsDto>(new[] { "Mpost", "PostReport", "User" });
        return View(mediaPostsReports);
	}
    public async Task<IActionResult> QuestionPostsReports()
    {
        var questionPostsReports = await db.QuestionPostsReports.GetAllAsync<QuestionPostsReports, QuestionPostsReportsDto>(new[] { "Qpost", "PostReports", "User" });
        return View(questionPostsReports);
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
