using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
