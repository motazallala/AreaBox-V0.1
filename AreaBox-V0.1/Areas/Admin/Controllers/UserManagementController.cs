using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
public class UserManagementController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
