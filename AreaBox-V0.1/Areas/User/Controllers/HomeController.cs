using Microsoft.AspNetCore.Mvc;
namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Info()
    {
        return View();
    }
}
