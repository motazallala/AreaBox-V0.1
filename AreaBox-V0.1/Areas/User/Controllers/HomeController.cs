using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.MediaPost;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]

public class HomeController : Controller
{
    private readonly IRepository<MediaPosts> _repoMediaPost;
    private readonly IRepository<MediaPostLikes> _repoMediaPostLikes;
    private readonly IRepository<ApplicationUser> _repoUserManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public HomeController(IRepository<MediaPosts> repoMediaPost,
        IMapper mapper,
        IRepository<ApplicationUser> repoUserManager,
        UserManager<ApplicationUser> userManager,
		IRepository<MediaPostLikes> repoMediaPostLikes)
    {
        _repoMediaPost = repoMediaPost;
		_repoUserManager = repoUserManager;
        _repoMediaPostLikes = repoMediaPostLikes;
		_userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var getUsers = await _repoUserManager.GetAllAsync<MediaPosts, MediaPostViewModel>(new[] { "Mpuser", "MediaPostComments", "MediaPostsLikes" });

		return View(getUsers);
    }

    public IActionResult Info()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(MediaPostViewModel mediaPostViewModel, IFormFile file)
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
                    MpshortDescription = mediaPostViewModel.ShortDescription,
                    MplongDescription = mediaPostViewModel.LongDescription,
                    Mpstate = false
                };

                _repoMediaPost.Add(mediaPost);
                await _repoMediaPost.SaveChnagesAsync();

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
