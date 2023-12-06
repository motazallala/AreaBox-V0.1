using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.MediaPost;
using AreaBox_V0._1.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class MediaManagementController : Controller
{
    private readonly IMediaPost _mediaPost;
    private readonly IRepository<MediaPostViewModel> _repository;

    public MediaManagementController(IMediaPost mediaPost, IRepository<MediaPostViewModel> repository)
    {
        _mediaPost = mediaPost;
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var getAllMediaPosts = await _repository.GetAllAsync();
        return View(getAllMediaPosts);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var getMediaPost = await _repository.GetByIdAsync(id);
        return View(getMediaPost);
    }

    public IActionResult Disable(Guid id)
    {
        _mediaPost.Disable(id);
        return Ok();
    }

}
