using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]
public class QandAController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork db;
    private readonly int PageSize = 15;
    public QandAController(IMapper mapper, UserManager<ApplicationUser> userManager, IUnitOfWork _db)
    {
        _userManager = userManager;
        _mapper = mapper;
        db = _db;
    }
    public async Task<ActionResult> Index(int page = 1)
    {
        int resultCount = await db.QuestionPosts.Count<QuestionPosts>();
        int pages = (int)Math.Ceiling((double)resultCount / PageSize);

        int skip = PageSize * (page - 1);
        int take = PageSize;
        var resalt = await db.MediaPosts.FindAndFilter<QuestionPosts, QuestionPostsDto>(new[] { "Qpcity", "Qpuser", "Qpcategory", "Qpcity.Country" }, skip, take);
        var posts = new UQuestionPostIndexDto
        {
            questionPostsDtos = resalt,
            PagesCount = pages
        };


        return View(posts);
    }

    public async Task<ActionResult> GetQuestionPostPartialList(int page = 1)
    {
        // Retrieve posts from your data source based on the page number
        int skip = PageSize * (page - 1);
        int take = PageSize;
        var resalt = await db.MediaPosts.FindAndFilter<QuestionPosts, QuestionPostsDto>(new[] { "Qpcity", "Qpuser", "Qpcategory", "Qpcity.Country" }, skip, take);

        return PartialView("_QuestionPostListPartial", resalt);
    }

    public IActionResult AddPost()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddPost(QuestionPostsDto questionPostsDto)
    {
        var userId = _userManager.GetUserId(User);

        if (userId != null)
        {

                var questionPost = new QuestionPosts
                {
                    QpuserId = userId,
                    Qpdate = DateTime.Now,
                    QpcityId = 1,
                    QpcategoryId = 1,
                    Qpdescription = questionPostsDto.Description,
                    Qpstate = false
                };

                db.QuestionPosts.Add(questionPost);
                await db.Save();

                return RedirectToAction("Index");
            }
        return Content("User not logged in or no file selected for upload.");
    }

}
