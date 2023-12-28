using AreaBox_V0._1.Areas.Admin.Models.MediaPost;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
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

	public async Task<IActionResult> Index(int id = 1, int pageSize = 5, int? Country = null, int? City = null, int? Category = null, string? ss = null)
	{
		int skip = pageSize * (id - 1);
		int take = pageSize;
		IEnumerable<MediaPostsDto> getAllMediaPosts;
		int resultsCount;

		getAllMediaPosts = await db.MediaPosts.FindAndFilter<MediaPosts, MediaPostsDto>(new[] { "Mpcity", "Mpuser", "Mpcategory", "Mpcity.Country" }, skip, take,
																					ss != null ? e => e.MplongDescription.Contains(ss) : e => true,
																					City != null ? e => e.MpcityId == City : e => true,
																					Category != null ? e => e.MpcategoryId == Category : e => true,
																					Country != null ? e => e.Mpcity.CountryId == Country : e => true);

		resultsCount = await db.MediaPosts.Count<MediaPosts>(ss != null ? e => e.MplongDescription.Contains(ss) : e => true,
																					City != null ? e => e.MpcityId == City : e => true,
																					Category != null ? e => e.MpcategoryId == Category : e => true,
																					Country != null ? e => e.Mpcity.CountryId == Country : e => true);


		if (pageSize <= 0)
		{
			pageSize = 5;
		}

		var pages = (int)Math.Ceiling((double)resultsCount / pageSize);
		var par = new Dictionary<string, string>();
		par.Add("id", id.ToString());
		par.Add("pageSize", pageSize.ToString());
		par.Add("Country", Country.ToString());
		par.Add("City", City.ToString());
		par.Add("Category", Category.ToString());
		par.Add("ss", ss);
		var mediaPostPaged = new MediaPostIndexViewModel
		{
			mediaPosts = getAllMediaPosts,
			Controller = "MediaManagement",
			Action = nameof(Index),
			CurrentPage = id,
			PagesCount = pages,
			paramss = par

		};
		return View(mediaPostPaged);
	}

	public async Task<IActionResult> Details(string id)
	{
		var getMediaPost = await db.MediaPosts.GetByIdAsync(id);
		return View(getMediaPost);
	}

}
