using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.MediaPost;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class MediaManagementController : Controller
{
	private readonly IMediaPost _mediaPost;
	private readonly IRepository<MediaPosts> _repository;
	private readonly UserManager<ApplicationUser> _userManager;

	public MediaManagementController(IMediaPost mediaPost, IRepository<MediaPosts> repository, IMapper mapper, UserManager<ApplicationUser> userManager)
	{
		_mediaPost = mediaPost;
		_repository = repository;
		_userManager = userManager;
	}

	public async Task<IActionResult> Index()
	{
		var getAllMediaPosts = await _repository.GetAllAsync<MediaPosts, MediaPostViewModel>(new[] { "Mpcity", "Mpuser" });

		return View(getAllMediaPosts);
	}

	public async Task<IActionResult> Details(string id)
	{
		var getMediaPost = await _repository.GetByIdAsync(id);
		return View(getMediaPost);
	}

	[HttpPost]
	public IActionResult Disable(string id)
	{
		_mediaPost.Disable(id);
		return Ok(new { Message = "Post state updated successfully" });
	}

	[HttpGet]
	public async Task<IActionResult> GetMediaDetails(string id)
	{

		var mediaPost = await _repository.GetByIdAsync(id);

		if (mediaPost == null)
		{
			return NotFound();
		}

		var user = await _userManager.FindByIdAsync(mediaPost.MpuserId);

		var mediaDetails = new
		{
			imageSrc = mediaPost.Mpimage,
			name = user.UserName,
			email = user.Email,
			city = _mediaPost.getCityById(mediaPost.MpcityId).Result,
			category = _mediaPost.getCountryById(mediaPost.MpcategoryId).Result,
			description = mediaPost.MplongDescription,
			date = mediaPost.Mpdate,
			state = mediaPost.Mpstate
		};

		return Json(mediaDetails);
	}
}
