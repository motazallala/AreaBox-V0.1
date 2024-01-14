using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]
public class SettingController : Controller
{
    public IActionResult MyAccount()
    {
        return View();
    }

    public IActionResult MyCategory()
    {
        return View();
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
