using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.MediaPost;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class MediaManagementController : Controller
{
    private readonly IMediaPost _mediaPost;
    private readonly IRepository<MediaPosts> _repository;

    public MediaManagementController(IMediaPost mediaPost, IRepository<MediaPosts> repository, IMapper mapper)
    {
        _mediaPost = mediaPost;
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var getAllMediaPosts = await _repository.GetAllAsync<MediaPosts, MediaPostViewModel>(new[] { "Mpcity", "Mpuser" });

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
