using AreaBox_V0._1.Areas.Admin.Models.UserManagementDto.send;
using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
[Authorize(Roles = "SuperAdmin,ContentManager,TechnicalSupport")]
public class UserManagementController : Controller
{
	private readonly IUnitOfWork db;
	public UserManagementController(IUnitOfWork _db)
	{
		db = _db;
	}
	public async Task<IActionResult> Index(int id = 1, int pageSize = 5, string? SearchType = null, string? Search = null)
	{
		int skip = pageSize * (id - 1);
		int take = pageSize;
		IEnumerable<ApplicationUserDto> result;
		int resultsCount;
		result = await db.Users.FindAndFilter<ApplicationUser, ApplicationUserDto>(null, skip, take, e => e.UserName, OrderBy.Ascending,
																					SearchType == "phonenumber" && Search != null ? x => x.PhoneNumber.Contains(Search) : x => true,
																					SearchType == "email" && Search != null ? x => x.Email.Contains(Search) : x => true,
																					SearchType == "username" && Search != null ? x => x.UserName.Contains(Search) : x => true);

		resultsCount = await db.Users.Count<ApplicationUser>(SearchType == "phonenumber" ? x => x.PhoneNumber.Contains(Search) : x => true,
																					SearchType == "email" && Search != null ? x => x.Email.Contains(Search) : x => true,
																					SearchType == "username" && Search != null ? x => x.UserName.Contains(Search) : x => true);

		if (pageSize <= 0)
		{
			pageSize = 5;
		}

		var pages = (int)Math.Ceiling((double)resultsCount / pageSize);
		var par = new Dictionary<string, string>();
		par.Add("id", id.ToString());
		par.Add("pageSize", pageSize.ToString());
		par.Add("Search", Search);
		par.Add("SearchType", SearchType);
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
