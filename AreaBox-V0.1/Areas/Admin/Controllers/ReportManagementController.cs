﻿using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class ReportManagementController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
