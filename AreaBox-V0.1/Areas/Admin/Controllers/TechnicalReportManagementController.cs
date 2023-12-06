using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("TechnicalReport/[action]")]
public class TechnicalReportManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

