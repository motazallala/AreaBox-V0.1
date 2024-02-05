using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.input;
using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.Send;
using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using System.Text.RegularExpressions;
using AreaBox_V0._1.Areas.User.Models.UserInfoDto.Input;
using AreaBox_V0._1.Areas.Admin.Models.CategoriesModel.Send;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Route("UserApi/[action]")]
[ApiController]
public class UserApiController : ControllerBase
{
    private readonly IUnitOfWork _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageService _imageService;
    private readonly ILocationService _location;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserApiController(IUnitOfWork db,
        UserManager<ApplicationUser> userManager,
        IImageService imageService,
        ILocationService location,
        SignInManager<ApplicationUser> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _imageService = imageService;
        _location = location;
        _signInManager = signInManager;
    }

	[HttpGet]
	public async Task<IActionResult> GetAllCategory()
	{
		var getAllCategories = await _db.Categories.GetAllAsync<Categories, CategoriesIndexDto>();
		return Ok(getAllCategories);
	}

	#region User Category
	[HttpGet]
    public async Task<IActionResult> GetAllUserCategories()
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }
        var userCategoriesList = await _db.UserCategories.FindAll<UserCategories, UUserCategoriesOutputDto>(e => e.UserId == userId, new[] { "Category" });


        return Ok(userCategoriesList);
    }
    [HttpPost]
    public async Task<IActionResult> AddUserCategory([FromForm] int categoryId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }

        if (categoryId == null)
        {
            return BadRequest("Fill the information !!");
        }

        var category = await _db.Categories.CheckItemExistence<Categories>(e => e.CategoryId == categoryId);
        if (category == false)
        {
            return BadRequest("the category not Exists");
        }
        var usercategory = await _db.UserCategories.CheckItemExistence<UserCategories>(e => e.UserId == userId && e.CategoryId == categoryId);
        if (usercategory == true)
        {
            return BadRequest("the usercategory Exists");
        }
        var newUserCategory = new UserCategories
        {
            UserId = userId,
            CategoryId = categoryId

        };

        _db.UserCategories.Add(newUserCategory);
        await _db.Save();

        return Ok("Category has been added");

    }

    [HttpPost]
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
        var isExist = await _db.UserCategories.CheckItemExistence<UserCategories>(e => e.UserId == userId && e.CategoryId == categoryId);
        if (isExist == false)
        {
            return BadRequest("the category is not exist for this user");
        }
        var itemToRemove = await _db.UserCategories.Find<UserCategories, UserCategories>(e => e.UserId == userId && e.CategoryId == categoryId);
        if (itemToRemove == null)
        {
            return BadRequest("the category is not exist for this user");
        }

        _db.UserCategories.Remove(itemToRemove);
        await _db.Save();

        return Ok("the category removed Successfully");
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
            var loc = await _location.GetGeolocationObject(latitude, longitude);
            int resultCount = await _db.MediaPosts.Count<MediaPosts>(e => e.Mpcity.CityName == loc.City && (categoryId <= 0 || e.MpcategoryId == categoryId) && !e.Mpstate);
            int pages = (int)Math.Ceiling((double)resultCount / PostConfig.PageSize);
            return Ok(pages);
        }
        return BadRequest("Error accord!!");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUserEmail([FromForm] string newEmail)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var email = await _userManager.GetEmailAsync(user);

        if (newEmail != email)
        {
            await _userManager.SetEmailAsync(user, newEmail);

            await _userManager.UpdateAsync(user);

            return Ok("Email successfully changed.");
        }

        return BadRequest("Your email is not changed.");
    }


    [HttpPost]
    public async Task<IActionResult> ChangeUserInfo([FromForm] UserInfoInputDto userInfo)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (userInfo != null)
        {
            if (!string.IsNullOrEmpty(userInfo.FirstName) && !ContainsOnlyLetters(userInfo.FirstName))
            {
                return BadRequest("First Name must be a valid name without any numbers or special characters.");
            }

            if (!string.IsNullOrEmpty(userInfo.LastName) && !ContainsOnlyLetters(userInfo.LastName))
            {
                return BadRequest("Last Name must be a valid name without any numbers or special characters.");
            }

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.Gender = userInfo.Gender;
            user.PhoneNumber = userInfo.PhoneNumber;
            user.Bio = userInfo.Bio;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("User info successfully changed.");
            }
        }

        return BadRequest("User info is not changed.");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUserImage([FromForm] IFormFile image)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (image != null)
        {
            var base64 = await _imageService.UploadImage(image);
            user.ProfilePicture = base64;
            await _userManager.UpdateAsync(user);

            return Ok("User profile successfully changed.");
        }

        return BadRequest("User profile is not changed.");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUserImage([FromServices] IWebHostEnvironment webHostEnvironment)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var defaultAvatarPath = Path.Combine(webHostEnvironment.WebRootPath, "userhome", "media", "default-avatar.jpg");

        var defaultAvatar = await _imageService.UploadLocalImage(defaultAvatarPath);

        user.ProfilePicture = defaultAvatar;

        await _userManager.UpdateAsync(user);

        return Ok("Profile picture has been removed!");
    }



    [HttpPost]
    public async Task<IActionResult> ChangeUserPassword([FromForm] string oldPassword, [FromForm] string newPassword, [FromForm] string confirmPassword)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(user, oldPassword);

        if (!isOldPasswordCorrect)
        {
            return BadRequest("Old password is incorrect.");
        }

        if (newPassword != confirmPassword)
        {
            return BadRequest("Passwords do not match.");
        }

        var isNewPasswordSameAsOld = await _userManager.CheckPasswordAsync(user, newPassword);

        if (isNewPasswordSameAsOld)
        {
            return BadRequest("New password cannot be the same as the old password.");
        }

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (result.Succeeded)
        {
            return Ok("Password changed successfully.");
        }

        return BadRequest("Failed to change password. Please check the provided information.");
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
            var loc = await _location.GetGeolocationObject(latitude, longitude);
            int resultCount = await _db.QuestionPosts.Count<QuestionPosts>(e => e.Qpcity.CityName == loc.City && categoryId > 0 ? e.QpcategoryId == categoryId : true);
            int pages = (int)Math.Ceiling((double)resultCount / PostConfig.PageSize);
            return Ok(pages);
        }
        return BadRequest("Error accord!!");

    }
    #endregion

    #region Saved Post Fun

    //public async Task<IActionResult> GetAllUserSavedMediaPost()
    //{
    //    var userSavedMediaPost = _db.Users.FindAndFilter<ApplicationUser, ApplicationUser>(new["MediaPosts","MediaPosts.MediaPostsLikes"]);
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
            var loc = await _location.GetGeolocationObject(latitude, longitude);
            return Ok(loc);
        }
        return BadRequest("The Application can not get the your location!!");
    }
    #endregion


    #region Personal Data
    [HttpPost]
    public async Task<IActionResult> DownloadPersonalData()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var personalData = new Dictionary<string, string>();
        var personalDataProps = typeof(ApplicationUser).GetProperties().Where(
                        prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
        foreach (var p in personalDataProps)
        {
            personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
        }

        var logins = await _userManager.GetLoginsAsync(user);
        foreach (var l in logins)
        {
            personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
        }

        personalData.Add($"Authenticator Key", await _userManager.GetAuthenticatorKeyAsync(user));

        Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
        return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUserData([FromForm] string password)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        bool RequirePassword = await _userManager.HasPasswordAsync(user);

        if (RequirePassword)
        {
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return BadRequest("Incorrect password");
            }
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            BadRequest($"Unexpected error occurred deleting user.");
        }

        await _signInManager.SignOutAsync();
        return Ok("We're sorry to see you go. Your account has been successfully deleted.");
    }


    #endregion

    #region TwoFactorAuthentication

    [HttpGet]
    public async Task<IActionResult> GetTwoFactorAuthentication()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
        var Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
        var IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
        var RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

        var result = new
        {
            HasAuthenticator,
            Is2faEnabled,
            IsMachineRemembered,
            RecoveryCodesLeft
        };

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> ForgetTwoFactorAuthentication()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await _signInManager.ForgetTwoFactorClientAsync();

        return Ok("The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.");
    }

    #endregion

    public bool ContainsOnlyLetters(string input)
    {
        Regex regex = new Regex("^[a-zA-Z]+$");
        return regex.IsMatch(input);
    }
}
