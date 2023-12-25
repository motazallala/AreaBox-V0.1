using AreaBox_V0._1.Areas.Admin.Models.QuestionPostDto.send;
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

    public async Task<IActionResult> Index(int id = 1 , int pageSize = 5 , int? Country = null,  int? City = null ,int? Category = null , string? Search = null) 
    {
        int skip=pageSize*(id-1);
        int take=pageSize;
        int resultCount;
        IEnumerable<QuestionPostsDto> result;
        if (Search != null)
        {
            result = await db.QuestionPosts.FindAndFilter(e => e.Qptitle.Contains(Search), new[] { "Qpcategory", "Qpcity", "Qpuser", "Qpcity.Country" }, City, Category, Country, skip, take);
            resultCount = await db.QuestionPosts.CountQuestionPosts(e => e.Qptitle.Contains(Search), City, Category, Country);
        }
        else
        {
            result = await db.QuestionPosts.FindAndFilter(e => true, new[] { "Qpcategory", "Qpcity", "Qpuser", "Qpcity.Country" }, City, Category, Country, skip, take);
			resultCount = await db.QuestionPosts.CountQuestionPosts(e => true, City, Category, Country);
		}
        if(id <= 0)
        {
            id = 1;
        }

        if(pageSize <= 0) 
        {
            pageSize = 5;
        }

        int pages = (int)Math.Ceiling((double)resultCount / pageSize);
        var par = new Dictionary<string, string>();
        par.Add("id", id.ToString());
        par.Add("pageSize", pageSize.ToString());
		par.Add("Country", Country.ToString());
		par.Add("City", City.ToString());
        par.Add("Category", Category.ToString());
        par.Add("Search", Search);

        var questionPostPaging = new QuestionPostIndexDto
        {
            Action = nameof(Index),
            Controller = "QAManagement",
            CurrentPage = id,
            PagesCount = pages,
            paramss = par,
            questionPostDtos = result,

        };
            return View(questionPostPaging);
    }

    public async Task<IActionResult> Details(string id)
    {
        var getQAPost = await db.QuestionPosts.GetByIdAsync(id);
        return View(getQAPost);
    }
}
