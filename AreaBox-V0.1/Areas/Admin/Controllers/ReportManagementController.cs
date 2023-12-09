using AreaBox_V0._1.Areas.Admin.Models.MediaPostsReport;
using AreaBox_V0._1.Areas.Admin.Models.QuestionPostsReports;
using AreaBox_V0._1.Areas.Admin.Models.ReportManagementViewModel;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class ReportManagementController : Controller
{
    private readonly IRepository<PostReports> _repoPostReports;
    private readonly IRepository<MediaPosts> _repoMediaPost;
    private readonly IRepository<QuestionPosts> _repoQuestionPost;
    private readonly IReportType _reportType;
    private readonly IRepository<MediaPostsReports> __repoMediaPostReports;

    public ReportManagementController
        (IRepository<PostReports> repository,
        IReportType reportType,
        IRepository<MediaPosts> mediaPost,
        IRepository<QuestionPosts> questionPost,
        IRepository<MediaPostsReports> repoMediaPostReports
        )
    {
        _repoPostReports = repository;
        _repoMediaPost = mediaPost;
        _repoQuestionPost = questionPost;
        _reportType = reportType;
        __repoMediaPostReports = repoMediaPostReports;

    }

    public async Task<IActionResult> Index()
    {
        var mediaPostsReports = await __repoMediaPostReports.GetAllAsync<MediaPostsReports, MediaPostsReportViewModel>(new[] { "Mpost", "PostReport" });
        var questionPostsReports = await __repoMediaPostReports.GetAllAsync<QuestionPostsReports, QuestionPostsReportViewModel>(new[] { "Qpost", "PostReports" });
        var MpQpReports = new MediaQuestionPostsReportViewModel
        {
            MediaPostsReports = mediaPostsReports,
            QuestionPostsReports = questionPostsReports
        };
        return View(MpQpReports);
    }

    public async Task<IActionResult> Details(string id)
    {
        var getReport = await _repoPostReports.GetByIdAsync(id);
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
