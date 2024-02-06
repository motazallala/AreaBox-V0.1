using AreaBox_V0._1.Areas.Admin.Models.Dashboard.send;
using AreaBox_V0._1.Areas.Admin.Models.MediaPost;
using AreaBox_V0._1.Areas.Admin.Models.Pages.ReportManagement.send;
using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Data.Repositories;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "SuperAdmin,ContentManager,TechnicalSupport")]
public class DashboardController : Controller
{
        private readonly IUnitOfWork db;
    public DashboardController(IUnitOfWork _db)
    {
        db = _db;
    }

    public async Task<IActionResult> Index()
    {
        DateTime last7DaysDate = DateTime.UtcNow.AddDays(-7);
        var mediaPostCount = await db.MediaPosts.Count<MediaPosts>(e => e.Mpdate >= last7DaysDate );
        var questionPostCount = await db.QuestionPosts.Count<QuestionPosts>(e=> e.Qpdate >= last7DaysDate);
        var userCount = await db.Users.Count();

        var analysis = new DashboardAnalysis
        {
            MediaPostCount = mediaPostCount,
            MediaPostsPercentage = Math.Ceiling(((double)mediaPostCount / (mediaPostCount + questionPostCount)) * 100.0),
            QuestionPostCount = questionPostCount,
            QuestionPostsPercentage = Math.Floor(((double)questionPostCount / (mediaPostCount + questionPostCount)) * 100.0),
            TotalPostsPercentage = Math.Floor(((double)(mediaPostCount + questionPostCount) / (mediaPostCount + questionPostCount)) * 100.0),
            UserCount = userCount,
        };
        return View(analysis);
    }

    public IActionResult Index2()
    {
        return View();
    }

}

