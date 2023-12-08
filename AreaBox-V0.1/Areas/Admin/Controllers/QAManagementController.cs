using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.QuestionPost;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class QAManagementController : Controller
{
    private readonly IRepository<QuestionPosts> _repository;

    public QAManagementController(IRepository<QuestionPosts> repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var getAllQAPost = await _repository.GetAllAsync<QuestionPosts, QuestionPostViewModel>(new[] { "Qpcategory", "Qpcity", "Qpuser" } );
        return View(getAllQAPost);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var getQAPost = await _repository.GetByIdAsync(id);
        return View(getQAPost);
    }
}
