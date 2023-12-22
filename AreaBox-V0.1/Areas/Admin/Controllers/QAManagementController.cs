using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class QAManagementController : Controller
{
    private readonly IUnitOfWork db;

    public QAManagementController(IUnitOfWork _db)
    {
        db = _db;
    }

    public async Task<IActionResult> Index()
    {

        var getAllQAPost = await db.QuestionPosts.GetAllAsync<QuestionPosts, QuestionPostsDto>(new[] { "Qpcategory", "Qpcity", "Qpuser" });
        return View(getAllQAPost);
    }

    public async Task<IActionResult> Details(string id)
    {
        var getQAPost = await db.QuestionPosts.GetByIdAsync(id);
        return View(getQAPost);
    }
}
