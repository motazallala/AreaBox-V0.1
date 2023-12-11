using AreaBox_V0._1.Areas.Admin.Models.MediaPostsReport;
using AreaBox_V0._1.Areas.Admin.Models.QuestionPostsReports;
using AreaBox_V0._1.Areas.Admin.Models.ReportManagementViewModel;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
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
        var mediaPostsReports = await db.MediaPosts.GetAllAsync<MediaPostsReports, MediaPostsReportViewModel>(new[] { "Mpost", "PostReport", "User" });
        var questionPostsReports = await db.QuestionPostsReports.GetAllAsync<QuestionPostsReports, QuestionPostsReportViewModel>(new[] { "Qpost", "PostReports", "User" });
        var MpQpReports = new MediaQuestionPostsReportViewModel
        {
            MediaPostsReports = mediaPostsReports,
            QuestionPostsReports = questionPostsReports
        };
        return View(MpQpReports);
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
