using AreaBox_V0._1.Areas.Admin.Models.UserManagement;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class UserManagementController : Controller
{
    private readonly IRepository<ApplicationUser> _userManager;

    public UserManagementController(IRepository<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var getUsers = await _userManager.GetAllAsync<ApplicationUser,UserManagementViewModel>();
        return View(getUsers);
    }
}
