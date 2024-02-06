using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "SuperAdmin,ContentManager,TechnicalSupport")]
public class ReviewTechnicalManagementController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
