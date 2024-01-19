using AreaBox_V0._1.Areas.User.Models.UMediaPostCommentsDto.Input;
using AreaBox_V0._1.Areas.User.Models.UMediaPostCommentsDto.Send;
using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.input;
using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;
using AreaBox_V0._1.Areas.User.Models.UMediaPostLikeDto.Input;
using AreaBox_V0._1.Areas.User.Models.UMediaPostReportDto.input;
using AreaBox_V0._1.Areas.User.Models.UMediaPostReportTypeDto.Send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.Input;
using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.input;
using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork db;
    private readonly ILocationService location;

    private readonly int PageSize = 5;
    public HomeController(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IUnitOfWork _db,
        IImageService imageService
,
        ILocationService location)
    {
        _userManager = userManager;
        _mapper = mapper;
        _imageService = imageService;
        db = _db;
        this.location = location;
    }

    public async Task<ActionResult> Index(int page = 1)
    {

        var latitudeCookie = Request.Cookies["latitude"];
        var longitudeCookie = Request.Cookies["longitude"];

        // Initialize variables to store converted values
        double latitude;
        double longitude;

        // Try parsing the cookies into double values
        if (double.TryParse(latitudeCookie, out latitude) && double.TryParse(longitudeCookie, out longitude))
        {
            var loc = await location.GetGeolocationObject(latitude, longitude);
            await db.Countries.CheckAndInsertCountry(loc.Country);
            await db.Cities.CheckAndInsertCity(loc.City, loc.Country);
            int resultCount = await db.MediaPosts.Count<MediaPosts>(e => e.Mpcity.CityName == loc.City);
            int pages = (int)Math.Ceiling((double)resultCount / PageSize);
            int skip = PageSize * (page - 1);
            int take = PageSize;
            var resalt = await db.MediaPosts.FindAndFilter<MediaPosts, UMediaPostOutputDto>(new[] { "Mpcity", "Mpuser", "Mpcategory", "Mpcity.Country", "MediaPostsLikes" }, skip, take, e => e.Mpdate, OrderBy.Descending,
                                                                                                loc.City != null ? e => e.Mpcity.CityName == loc.City : e => true, e=>e.Mpstate == false);

            var posts = new UMediaPostIndexDto
            {
                mediaPostsDtos = resalt,
                PagesCount = pages
            };


            return View(posts);

        }
        else
        {
            var posts = new UMediaPostIndexDto();
            return View(posts);
        }
    }


    public IActionResult Info()
    {
        return View();
    }

    public IActionResult AddPost()
    {
        return View();
    }




    #region Posts Fun
    public async Task<ActionResult> GetMediaPostPartialList(int page = 1)
    {
        var geolocationInfoCookie = Request.Cookies["geolocationInfo"];
        var latitudeCookie = Request.Cookies["latitude"];
        var longitudeCookie = Request.Cookies["longitude"];

        // Initialize variables to store converted values
        double latitude;
        double longitude;

        // Try parsing the cookies into double values
        if (double.TryParse(latitudeCookie, out latitude) && double.TryParse(longitudeCookie, out longitude))
        {

            var loc = await location.GetGeolocationObject(latitude, longitude);

            int skip = PageSize * (page - 1);
            int take = PageSize;
            var resalt = await db.MediaPosts.FindAndFilter<MediaPosts, UMediaPostOutputDto>(new[] { "Mpcity", "Mpuser", "Mpcategory", "Mpcity.Country", "MediaPostsLikes" }, skip, take, e => e.Mpdate, OrderBy.Descending,
                                                                                                loc.City != null ? e => e.Mpcity.CityName == loc.City : e => true, e => e.Mpstate == false);

            return PartialView("_MediaPostListPartial", resalt);

        }
        else
        {
            return BadRequest("Failed to retrieve geolocation information");
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetMediaPostReportTypes(string mediaPostId)
    {
        if (mediaPostId == null)
        {
            return BadRequest("Choose post to report");
        }

        var mediapost = await db.MediaPosts.CheckItemExistence<MediaPosts>(e => e.MpostId == mediaPostId);
        if (mediapost == false)
        {
            return BadRequest("the post not Exists");
        }
        var resalt = await db.ReportTypes.GetAllAsync<ReportTypes, UReportTypeOutPutDto>(null);
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

        if(mediaPostsDto.ShortDescription.Length > 150)
        {
            return BadRequest("Short descripton should be less than 150");
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
        if (mediaPostsDto.CategoryId == null || mediaPostsDto.CityId == null || mediaPostsDto.ShortDescription == null || mediaPostsDto.LongDescription == null)
        {
            return BadRequest("Fill the information !!");
        }
        try
        {
            string base64String = await _imageService.UploadImage(image);

            var mediaPost = new MediaPosts
            {
                MpuserId = userId,
                Mpimage = base64String,
                Mpdate = DateTime.Now,
                MpcityId = mediaPostsDto.CityId,
                MpcategoryId = mediaPostsDto.CategoryId,
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
	public async Task<IActionResult> DeleteMediaPost([FromForm] string mediaPostId)
	{
		if (mediaPostId == null)
		{
			return BadRequest("Choose post to delete");
		}
		var isExist = await db.MediaPosts.CheckItemExistence<MediaPosts>(e => e.MpostId == mediaPostId);
		if (isExist == false)
		{
			return NotFound("The specified media post was not found.");
		}
		var itemToDelete = await db.MediaPosts.GetByIdAsync(mediaPostId);
		db.MediaPosts.Remove(itemToDelete);
		await db.Save();
		return Ok("The media post has been successfully deleted.");
	}

	[HttpPost]
    public async Task<IActionResult> AddLikeToMediaPost([FromForm] UMediaPostLikeInputDto input)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized("User is not authenticated. Log in to comment on this post!");
        }
        if (input.MpostId == null)
        {
            return BadRequest("Choose Post to like ");
        }


        var mediapost = await db.MediaPosts.GetByIdAsync(input.MpostId);

        if (mediapost == null)
        {
            return NotFound("the post not Exists");
        }
        var resalt = await db.MediaPostLikes.CheckItemExistence<MediaPostLikes>(e => e.MpostId == input.MpostId && e.UserId == userId);

        var existingLike = await db.MediaPostLikes.Find<MediaPostLikes, MediaPostLikes>(e => e.MpostId == input.MpostId && e.UserId == userId);

        var newPostLike = new MediaPostLikes
        {
            MpostId = input.MpostId,
            UserId = userId
        };

        if (resalt == false)
        {
            db.MediaPostLikes.Add(newPostLike);
            mediapost.LikeCount++;
            db.MediaPosts.Update(mediapost);
            await db.Save();
            return Ok("Post Liked");
        }
        else
        {
            db.MediaPostLikes.Remove(existingLike);

            mediapost.LikeCount--;
            db.MediaPosts.Update(mediapost);
            await db.Save();
            return Ok("Post Liked Removed");
        }
    }


	[HttpGet]
	public async Task<IActionResult> GetMediaPostDetails(string mediaPostId)
	{
		if (mediaPostId == null)
		{
			return BadRequest("Choose media post to Get the details.");
		}

		var existingMediaPost = await db.MediaPosts.GetByIdAsync(mediaPostId);

		if (existingMediaPost == null)
		{
			return NotFound("The specified question post was not found.");
		}

		var getMediaPost = new MediaPostsDto
		{
			Id = existingMediaPost.MpostId,
			UserId = existingMediaPost.MpuserId,
            Image = existingMediaPost.Mpimage,
            LongDescription = existingMediaPost.MplongDescription,
            ShortDescription = existingMediaPost.MpshortDescription,
			CategoryId = existingMediaPost.MpcategoryId,
			CityId = existingMediaPost.MpcityId,
			Date = existingMediaPost.Mpdate
		};

		return Ok(getMediaPost);
	}

	[HttpPost]
	public async Task<IActionResult> EditMediaPost([FromForm] UMediaPostEditDto mediaPostEditDto, IFormFile image)
	{
		var userId = _userManager.GetUserId(User);

		if (string.IsNullOrEmpty(userId))
		{
			return BadRequest("User is not authenticated. Please log in to continue.");
		}

		if (string.IsNullOrEmpty(mediaPostEditDto.Id))
		{
			return BadRequest("Invalid media post ID provided.");
		}

		var existingMediaPost = await db.MediaPosts.GetByIdAsync(mediaPostEditDto.Id);

		if (existingMediaPost == null)
		{
			return BadRequest("The specified media post was not found.");
		}

		if (existingMediaPost.MpuserId != userId)
		{
			return BadRequest("You are not authorized to edit this media post.");
		}


        if(image != null)
        {
			var fileExtension = Path.GetExtension(image.FileName).ToLower();
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico" };

			if (!allowedExtensions.Contains(fileExtension))
			{
				return BadRequest("Only image files are allowed.");
			}

			string base64String = await _imageService.UploadImage(image);
			existingMediaPost.Mpimage = base64String;
		}

		existingMediaPost.MpuserId = userId;
		existingMediaPost.MpostId = mediaPostEditDto.Id;
		existingMediaPost.MpcityId = mediaPostEditDto.CityId;
		existingMediaPost.MpcategoryId = mediaPostEditDto.CategoryId;
        existingMediaPost.MplongDescription = mediaPostEditDto.LongDescription;
        existingMediaPost.MpshortDescription = mediaPostEditDto.ShortDescription;

		db.MediaPosts.Update(existingMediaPost);
		await db.Save();

		return Ok("Media post updated successfully.");
	}

	#endregion


	#region Category Fun
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

    [HttpDelete]
    public async Task<IActionResult> DeleteUserCategory([FromForm] int categoryId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }
        if (categoryId == null)
        {
            return BadRequest("send the user category id");
        }
        var isExist = await db.UserCategories.CheckItemExistence<UserCategories>(e => e.UserId == userId && e.CategoryId == categoryId);
        if (isExist == false)
        {
            return Ok("the category is not exist for this user");
        }
        var itemToRemove = await db.UserCategories.Find<UserCategories, UserCategories>(e => e.UserId == userId && e.CategoryId == categoryId);
        if (itemToRemove == null)
        {
            return Ok("the category is not exist for this user");
        }
        else
        {
            return Ok("the category removed Successfully");

        }


    }

    #endregion


    #region Comments Fun
    [HttpGet]
    public async Task<IActionResult> GetMediaPostComments(string mediaPostId, int page = 1)
    {
        if (page < 1)
        {
            page = 1;
        }
        if (mediaPostId == null)
        {
            return BadRequest("Choose post to display comment");
        }
        var mediapost = await db.MediaPosts.CheckItemExistence<MediaPosts>(e => e.MpostId == mediaPostId);
        if (mediapost == false)
        {
            return BadRequest("The post not exists");
        }

        int resultCount = await db.MediaPostComments.Count<MediaPostComments>(e=>e.MpostId == mediaPostId);
        int pages = (int)Math.Ceiling((double)resultCount / PageSize);

        if (page > pages)
        {
            return Ok("no more posts");
        }

        int skip = PageSize * (page - 1);
        int take = PageSize;
        var resalt = await db.MediaPostComments.FindAndFilter<MediaPostComments, UMediaPostCommentsOutputDto>(new[] { "User", "Mpost" }, skip, take, e => e.MpcommnetDate, OrderBy.Descending, e => e.MpostId == mediaPostId);
        return Ok(resalt);
    }


    [HttpPost]
    public async Task<IActionResult> AddCommentToMediaPost([FromForm] UMediaPostCommentsInputDto input)
    {
        var userId = _userManager.GetUserId(User);

        if (userId == null)
        {
            return BadRequest("Log in to comment on this post!");
        }

        if (input.PostId == null)
        {
            return BadRequest("Choose Post to comment.");
        }

        var mediapost = await db.MediaPosts.GetByIdAsync(input.PostId);


        if (mediapost == null)
        {
            return BadRequest("The post not exists!");
        }

        var newComment = new MediaPostComments
        {
            MpostId = input.PostId,
            UserId = userId,
            MpcommentContent = input.CommentContent,
            MpcommnetDate = DateTime.Now
        };
        db.MediaPostComments.Add(newComment);
        mediapost.CommentCount++;
        db.MediaPosts.Update(mediapost);
        await db.Save();
        return Ok("Comment has been added");
    }
    #endregion

    #region Report Fun
    [HttpPost]
    public async Task<IActionResult> AddReportInMediaPost([FromForm] UMediaPostReportInputDto inputReport)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Please log in to report the post.");
        }

        if (inputReport.MpostId == null || inputReport.ReportTypeId == null)
        {
            return BadRequest("Please provide complete report details.");
        }
        var mediaPostReport = await db.MediaPostReports.CheckItemExistence<MediaPostsReports>(e => e.UserId == userId && e.MpostId == inputReport.MpostId);
        if (mediaPostReport == true)
        {
            return BadRequest("You have already reported this post.");
        }


        var newMediaReport = new MediaPostsReports
        {
            UserId = userId,
            MpostId = inputReport.MpostId,
            ReportTypeId = inputReport.ReportTypeId,

        };

        db.MediaPostReports.Add(newMediaReport);
        await db.Save();

        return Ok("Post has been successfully reported.");
    }

    #endregion

}
