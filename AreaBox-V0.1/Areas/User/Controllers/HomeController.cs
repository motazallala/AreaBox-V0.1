using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]

public class HomeController : Controller
{
    private readonly IRepository<MediaPosts> _repoMediaPost;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork db;
    public HomeController(IRepository<MediaPosts> repoMediaPost, IMapper mapper, UserManager<ApplicationUser> userManager, IUnitOfWork _db)
    {
        _repoMediaPost = repoMediaPost;
        _userManager = userManager;
        _mapper = mapper;
        db = _db;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Info()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(MediaPostsDto mediaPostsDto, IFormFile file)
    {
        var userId = _userManager.GetUserId(User);

        if (userId != null)
        {
            if (file != null && file.Length > 0)
            {
                string base64String = ConvertToBase64(file);

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
        }
        return Content("User not logged in or no file selected for upload.");
    }

    private string ConvertToBase64(IFormFile file)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            return Convert.ToBase64String(bytes);
        }
    }
}
