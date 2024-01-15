using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.Send;
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
        var userCategoriesList = await db.UserCategories.FindAll<UserCategories, UUserCategoriesOutputDto>(e => e.UserId == userId);


        return Ok(userCategoriesList);
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
