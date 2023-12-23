using AreaBox_V0._1.Areas.Admin.Models.UserManagementDto.send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class UserManagementController : Controller
{
	private readonly IUnitOfWork db;
	public UserManagementController(IUnitOfWork _db)
	{
		db = _db;
	}
	public async Task<IActionResult> Index(int id = 1, int pageSize = 5, string? Search = null)
	{
		int skip = pageSize * (id - 1);
		int take = pageSize;
		IEnumerable<ApplicationUserDto> result;
		int resultsCount;
		if (Search != null)
		{
			result = await db.Users.FindAndFilter(x => x.UserName.Contains(Search), null, skip, take);
			resultsCount = await db.Users.CountUser(x => x.UserName.Contains(Search));
		}
		else
		{
			result = await db.Users.FindAndFilter(x => true, null, skip, take);
			resultsCount = await db.Users.CountUser(x => true);
		}

		if (pageSize <= 0)
		{
			pageSize = 5;
		}

		var pages = (int)Math.Ceiling((double)resultsCount / pageSize);
		var par = new Dictionary<string, string>();
		par.Add("id", id.ToString());
		par.Add("pageSize", pageSize.ToString());
		par.Add("Search", Search);
		var mediaPostPaged = new UserManagementIndexDto
		{
			Users = result,
			Controller = "UserManagement",
			Action = nameof(Index),
			CurrentPage = id,
			PagesCount = pages,
			paramss = par

		};
		return View(mediaPostPaged);
	}
}
