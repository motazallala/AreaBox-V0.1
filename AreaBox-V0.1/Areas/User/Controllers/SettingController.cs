using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.Send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]
public class SettingController : Controller
{
    private readonly IUnitOfWork db;
    private readonly UserManager<ApplicationUser> userManager;

    public SettingController(UserManager<ApplicationUser> userManager, IUnitOfWork db)
    {
        this.userManager = userManager;
        this.db = db;
    }

    public IActionResult MyAccount()
    {
        return View();
    }

    public async Task<IActionResult> MyCategoryAsync()
    {
        var userId = userManager.GetUserId(User);
        if (userId == null)
        {
            return BadRequest("Log in to report the post");
        }
        var userCategoriesList = await db.UserCategories.FindAll<UserCategories, UUserCategoriesOutputDto>(e => e.UserId == userId, new[] { "Category" });
        return View(userCategoriesList);
    }
    public IActionResult MyMediaPost()
    {
        return View();
    }
    public IActionResult MyQuestionPost()
    {
        return View();
    }
    public IActionResult SavePost()
    {
        return View();
    }
    public IActionResult TechicalReport()
    {
        return View();
    }

    public IActionResult SavedMediaPost()
    {
        return View();
    }

    public IActionResult SavedQuestionPost()
    {
        return View();
    }

}
