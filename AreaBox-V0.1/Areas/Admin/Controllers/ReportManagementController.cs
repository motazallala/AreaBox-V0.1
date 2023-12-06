using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.MediaPost;
using AreaBox_V0._1.Models.QuestionPost;
using AreaBox_V0._1.Models.ReportType;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class ReportManagementController : Controller
{
    private readonly IRepository<PostReports> _repoReportType;
    private readonly IRepository<MediaPostViewModel> _repoMediaPost;
    private readonly IRepository<QuestionPostViewModel> _repoQuestionPost;
    private readonly IReportType _reportType;

    public ReportManagementController
        (IRepository<PostReports> repository,
        IReportType reportType,
        IRepository<MediaPostViewModel> mediaPost,
        IRepository<QuestionPostViewModel> questionPost
        )
    {
        _repoReportType = repository;
        _repoMediaPost = mediaPost;
        _repoQuestionPost = questionPost;
        _reportType = reportType;
    }

    public async Task<IActionResult> Index()
    {
        var getAllReports = await _repoReportType.GetAllAsync();
        return View(getAllReports);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var getReport = await _repoReportType.GetByIdAsync(id);
        return View(getReport);
    }

    public async Task<IActionResult> Disable(Guid id)
    {
        var getReportMPost = await _reportType.GetPostByReportId<MediaPostViewModel>(id);
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

        await _repoReportType.SaveChnageAsync();
        return Ok("Post state updated successfully");
    }
}
