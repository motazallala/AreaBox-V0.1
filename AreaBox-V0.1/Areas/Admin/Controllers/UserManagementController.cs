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
    private readonly UserManager<ApplicationUser> _userManager;
    public UserManagementController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        var getUsers = await _userManager.Users.ToListAsync();
        var userManagementViewModels = getUsers.Select(u => new UserManagementViewModel
        {
            UserID = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            Bio = u.Bio,
            DOB = u.DOB,
            Gender = u.Gender,
            ProfilePicture = u.ProfilePicture,
            State = u.State,
        });

        return View(userManagementViewModels);
    }
}
