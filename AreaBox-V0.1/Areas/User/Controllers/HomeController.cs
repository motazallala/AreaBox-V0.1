using AreaBox_V0._1.Areas.User.Models.UMediaPostCommentsDto.Send;
using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.input;
using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;
using AreaBox_V0._1.Areas.User.Models.UMediaPostLikeDto.Input;
using AreaBox_V0._1.Areas.User.Models.UMediaPostReportDto.input;
using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.input;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork db;

    private readonly int PageSize = 5;
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


    [HttpGet]
    public async Task<IActionResult> GetMediaPostComments([FromForm] string mediaPostId, [FromForm] int page = 1)
    {
        if (page < 1)
        {
            page = 1;
        }
        if (mediaPostId == null)
        {
            return BadRequest("Choose post to display comment ");
        }
        var mediapost = await db.MediaPosts.CheckItemExistence<MediaPosts>(e => e.MpostId == mediaPostId);
        if (mediapost == false)
        {
            return BadRequest("the post not Exists");
        }
        int resultCount = await db.MediaPostComments.Count<MediaPostComments>();
        int pages = (int)Math.Ceiling((double)resultCount / PageSize);
        if (page > pages)
        {
            return Ok("no more posts");

        }
        int skip = PageSize * (page - 1);
        int take = PageSize;
        var resalt = await db.MediaPostComments.FindAndFilter<MediaPostComments, UMediaPostCommentsOutputDto>(new[] { "User" }, skip, take, e => e.MpostId == mediaPostId);
        return Ok(resalt);
    }


    [HttpPost]
    public async Task<IActionResult> AddPost([FromForm] UMediaPostInputDto mediaPostsDto, IFormFile image)
    {
		var userId = _userManager.GetUserId(User);

        if (userId == null)
        {
            return BadRequest("User is not logged in.");
        }

        if (image == null || image.Length == 0)
        {
            return BadRequest("Please select an Image.");
        }
        else
        {
            var fileExtension = Path.GetExtension(image.FileName).ToLower();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico" };

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Only image files are allowed.");
            }
        }

        try
        {
            string base64String = await _imageService.UploadImage(image);

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

            return Ok("Post has been added");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddReportInMediaPost([FromForm] UMediaPostReportInputDto inputReport)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }

        if (inputReport.MpostId == null || inputReport.ReportTypeId == null)
        {
            return BadRequest("Fill the information !!");
        }
        var mediaPostReport = await db.MediaPostReports.CheckItemExistence<MediaPostsReports>(e => e.UserId == userId && e.MpostId == inputReport.MpostId);
        if (mediaPostReport == true)
        {
            return BadRequest("the report Exists");
        }
        var postType = await db.PostTypes.Find<PostType, PostType>(e => e.Name == "MediaPost");
        var newReport = new PostReports
        {
            ReportTypeId = inputReport.ReportTypeId,
            PostTypeId = postType.PostTypeId
        };


        db.PostReports.Add(newReport);
        await db.Save();


        var newMediaReport = new MediaPostsReports
        {
            UserId = userId,
            MpostId = inputReport.MpostId,
            PostReportId = newReport.PostReportId,

        };

        db.MediaPostReports.Add(newMediaReport);
        await db.Save();

        return Ok("post reported");
    }

    [HttpPost]
    public async Task<IActionResult> AddUserCategory([FromForm] UUserCategoriesInputDto input)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }

        if (input.CategoryId == null)
        {
            return BadRequest("Fill the information !!");
        }

        var category = await db.Categories.CheckItemExistence<Categories>(e => e.CategoryId == input.CategoryId);
        if (category == false)
        {
            return BadRequest("the category not Exists");
        }
        var usercategory = await db.UserCategories.CheckItemExistence<UserCategories>(e => e.UserId == userId && e.CategoryId == input.CategoryId);
        if (usercategory == true)
        {
            return BadRequest("the usercategory Exists");
        }
        var newUserCategory = new UserCategories
        {
            UserId = userId,
            CategoryId = input.CategoryId

        };

        db.UserCategories.Add(newUserCategory);
        await db.Save();

        return Ok("Category has been added");

    }

    [HttpPost]
    public async Task<IActionResult> AddLikeToMediaPost([FromForm] UMediaPostLikeInputDto input)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }
        if (input.MpostId == null)
        {
            return BadRequest("Choose Post to like ");
        }

        var mediapost = await db.MediaPosts.CheckItemExistence<MediaPosts>(e => e.MpostId == input.MpostId);
        if (mediapost == false)
        {
            return BadRequest("the post not Exists");
        }
        var resalt = await db.MediaPostLikes.CheckItemExistence<MediaPostLikes>(e => e.MpostId == input.MpostId);
        var newPostLike = new MediaPostLikes
        {
            MpostId = input.MpostId,
            UserId = userId
        };

        if (resalt == false)
        {
            db.MediaPostLikes.Add(newPostLike);
            await db.Save();
            return Ok("Post Liked");
        }
        else
        {
            db.MediaPostLikes.Remove(newPostLike);
            await db.Save();
            return Ok("Post Liked Removed");
        }
    }

	/*[HttpPost]
    public async Task<IActionResult> UploadVideoAsync(IFormFile videoFile)
    {
        var userId = _userManager.GetUserId(User);
        if (videoFile == null || videoFile.Length == 0)
        {
            ViewBag.Message = "Please select a video file.";
            return View("UploadVideo");
        }

        try
        {
            using (var memoryStream = new MemoryStream())
            {
                videoFile.CopyTo(memoryStream);
                byte[] videoBytes = memoryStream.ToArray(); 
                string base64String = Convert.ToBase64String(videoBytes);


                var mediaPost = new MediaPosts
                {
                    MpuserId = userId,
                    Mpimage = "data:video/mp4;base64," + base64String,
                    Mpdate = DateTime.Now,
                    MpcityId = 1,
                    MpcategoryId = 1,
                    MpshortDescription = "dsa",
                    MplongDescription = "das",
                    Mpstate = false
                };
                db.MediaPosts.Add(mediaPost);
                await db.Save();
            }

            ViewBag.Message = "Video uploaded successfully!";
            return View("UploadVideo");
        }
        catch (Exception ex)
        {
            ViewBag.Message = $"An error occurred: {ex.Message}";
            return View("UploadVideo");
        }
    }*/
}
