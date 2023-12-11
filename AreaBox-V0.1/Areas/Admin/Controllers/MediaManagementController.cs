using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.MediaPost;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class MediaManagementController : Controller
{
    private readonly IUnitOfWork db;

    public MediaManagementController(IUnitOfWork _db)
    {
        db = _db;
    }

    public async Task<IActionResult> Index()
    {
        var getAllMediaPosts = await db.MediaPosts.GetAllAsync<MediaPosts, MediaPostViewModel>(new[] { "Mpcity", "Mpuser", "Mpcategory" });
        return View(getAllMediaPosts);
    }

    public async Task<IActionResult> Details(string id)
    {
        var getMediaPost = await db.MediaPosts.GetByIdAsync(id);
        return View(getMediaPost);
    }

}
