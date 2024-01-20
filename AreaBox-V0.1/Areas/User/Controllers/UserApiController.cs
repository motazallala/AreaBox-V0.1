using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.input;
using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.Send;
using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Route("UserApi/[action]")]
[ApiController]
public class UserApiController : ControllerBase
{
    private readonly IUnitOfWork db;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILocationService location;
    public UserApiController(IUnitOfWork _db, UserManager<ApplicationUser> _userManager, ILocationService _location)
    {
        db = _db;
        userManager = _userManager;
        location = _location;
    }


    #region User Category
    [HttpGet]
    public async Task<IActionResult> GetAllUserCategories()
    {
        var userId = userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }
        var userCategoriesList = await db.UserCategories.FindAll<UserCategories, UUserCategoriesOutputDto>(e => e.UserId == userId, new[] { "Category" });


        return Ok(userCategoriesList);
    }
    [HttpPost]
    public async Task<IActionResult> AddUserCategory([FromForm] UUserCategoriesInputDto input)
    {
        var userId = userManager.GetUserId(User);
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
        var userId = userManager.GetUserId(User);
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

    #region Post Fun
    [HttpGet]
    public async Task<IActionResult> GetPageMediaPostCount(int categoryId)
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
            int resultCount = await db.MediaPosts.Count<MediaPosts>(e => e.Mpcity.CityName == loc.City && (categoryId <= 0 || e.MpcategoryId == categoryId) && !e.Mpstate);
            int pages = (int)Math.Ceiling((double)resultCount / PostConfig.PageSize);
            return Ok(pages);
        }
        return BadRequest("Error accord!!");

    }

    [HttpGet]
    public async Task<IActionResult> GetPageQuestionPostCount(int categoryId)
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
            int resultCount = await db.QuestionPosts.Count<QuestionPosts>(e => e.Qpcity.CityName == loc.City && categoryId > 0 ? e.QpcategoryId == categoryId : true);
            int pages = (int)Math.Ceiling((double)resultCount / PostConfig.PageSize);
            return Ok(pages);
        }
        return BadRequest("Error accord!!");

    }
    #endregion

    #region Saved Post Fun

    //public async Task<IActionResult> GetAllUserSavedMediaPost()
    //{
    //    var userSavedMediaPost = db.Users.FindAndFilter<ApplicationUser, ApplicationUser>(new["MediaPosts","MediaPosts.MediaPostsLikes"]);
    //}


    #endregion

    #region Location
    [HttpGet]
    public async Task<IActionResult> GetUserLocationByGeolocation()
    {
        var latitudeCookie = Request.Cookies["latitude"];
        var longitudeCookie = Request.Cookies["longitude"];

        // Initialize variables to store converted values
        double latitude;
        double longitude;
        if (double.TryParse(latitudeCookie, out latitude) && double.TryParse(longitudeCookie, out longitude))
        {
            var loc = await location.GetGeolocationObject(latitude, longitude);
            return Ok(loc);
        }
        return BadRequest("The Application can not get the your location!!");
    }
    #endregion
}
