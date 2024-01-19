using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.Send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using System.Text.RegularExpressions;
using AreaBox_V0._1.Areas.User.Models.UserInfoDto.Input;
using AreaBox_V0._1.Services;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Route("UserApi/[action]")]
[ApiController]
public class UserApiController : ControllerBase
{
    private readonly IUnitOfWork _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageService _imageService;

    public UserApiController(IUnitOfWork db, UserManager<ApplicationUser> userManager, IImageService imageService)
    {
        _db = db;
        _userManager = userManager;
        _imageService = imageService;
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
        var userCategoriesList = await _db.UserCategories.FindAll<UserCategories, UUserCategoriesOutputDto>(e => e.UserId == userId);


        return Ok(userCategoriesList);
    }
    #endregion


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




    #region Saved Post Fun

    //public async Task<IActionResult> GetAllUserSavedMediaPost()
    //{
    //    var userSavedMediaPost = db.Users.FindAndFilter<ApplicationUser, ApplicationUser>(new["MediaPosts","MediaPosts.MediaPostsLikes"]);
    //}


    #endregion

    private bool ContainsOnlyLetters(string input)
    {
        Regex regex = new Regex("^[a-zA-Z]+$");
        return regex.IsMatch(input);
    }
}
