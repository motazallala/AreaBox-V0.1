using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.input;
using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using AreaBox_V0._1.Services;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork db;

    private readonly int PageSize = 15;
    public HomeController(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IUnitOfWork _db,
        IImageService imageService
        )
    {
        _userManager = userManager;
        _mapper = mapper;
        _imageService = imageService;
        db = _db;
    }

    public async Task<ActionResult> Index(int page = 1)
    {


        int resultCount = await db.MediaPosts.Count<MediaPosts>();
        int pages = (int)Math.Ceiling((double)resultCount / PageSize);

        int skip = PageSize * (page - 1);
        int take = PageSize;
        var resalt = await db.MediaPosts.FindAndFilter<MediaPosts, MediaPostsDto>(new[] { "Mpcity", "Mpuser", "Mpcategory", "Mpcity.Country" }, skip, take);
        var posts = new UMediaPostIndexDto
        {
            mediaPostsDtos = resalt,
            PagesCount = pages
        };


        return View(posts);
    }

    public async Task<ActionResult> GetMediaPostPartialList(int page = 1)
    {
        // Retrieve posts from your data source based on the page number
        int skip = PageSize * (page - 1);
        int take = PageSize;
        var resalt = await db.MediaPosts.FindAndFilter<MediaPosts, MediaPostsDto>(new[] { "Mpcity", "Mpuser", "Mpcategory", "Mpcity.Country" }, skip, take);

        return PartialView("_MediaPostListPartial", resalt);
    }


    public IActionResult Info()
    {
        return View();
    }

    public IActionResult AddPost()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPost([FromForm] UMediaPostInputDto postInputDto, IFormFile file)
    {
        var userId = _userManager.GetUserId(User);

        if (userId == null)
        {
            ModelState.AddModelError(string.Empty, "User is not logged in.");
        }

        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("file", "Please select a Image.");
        }

        try
        {
            string base64String = await _imageService.UploadImage(file);

            var mediaPost = new MediaPosts
            {
                MpuserId = userId,
                Mpimage = base64String,
                Mpdate = DateTime.Now,
                MpcityId = 1,
                MpcategoryId = 1,
                MpshortDescription = mediaPostsDto.ShortDescription,
                MplongDescription = mediaPostsDto.LongDescription,
                Mpstate = false
            };

            db.MediaPosts.Add(mediaPost);
            await db.Save();

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return Content($"An error occurred: {ex.Message}");
        }
    }
}
