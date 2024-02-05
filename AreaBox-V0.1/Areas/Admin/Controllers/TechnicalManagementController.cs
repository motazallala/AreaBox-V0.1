using AreaBox_V0._1.Areas.Admin.Models.TechnicalReportDto.send;
using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers;
[Area("Admin")]
[Route("[controller]/[action]")]
public class TechnicalManagementController : Controller
{
	private readonly IUnitOfWork db;
	public TechnicalManagementController(IUnitOfWork _db)
	{
		db = _db;
	}
	public async Task<IActionResult> Index(int id = 1, int pageSize = 5, bool SendedToSuAdmin = false, bool ReviewedBySuAdmin = false, bool ReviewCompleted = false)
	{

		if (id <= 0)
		{
			id = 1;
		}

		if (pageSize <= 0)
		{
			pageSize = 5;
		}

		int resultCount = await db.TechnicalReports.Count<TechnicalReports>();
		int pages = (int)Math.Ceiling((double)resultCount / pageSize);
		if (id > pages)
		{
			id = 1;
		}
		int skip = pageSize * (id - 1);
		int take = pageSize;
		var result = await db.TechnicalReports.FindAndFilter<TechnicalReports, TechnicalReportsDto>(new[] { "User" }, skip, take, e => e.ReportDateTime, OrderBy.Descending, e => (!SendedToSuAdmin && !ReviewedBySuAdmin && !ReviewCompleted ? true : true) &&
																																											(SendedToSuAdmin ? e.ReviewByAdmin == true : true) &&
																																											(ReviewedBySuAdmin ? e.Reviewed == true : true) &&
																																											(ReviewCompleted ? e.Complete == true : true) &&
																																											(SendedToSuAdmin && ReviewedBySuAdmin && ReviewCompleted ? e.Complete == true && e.Reviewed == true && e.ReviewByAdmin == true : true) &&
																																											(SendedToSuAdmin && ReviewedBySuAdmin ? e.Reviewed == true && e.ReviewByAdmin == true : true) &&
																																											(SendedToSuAdmin && ReviewCompleted ? e.Complete == true && e.ReviewByAdmin == true : true) &&
																																											(ReviewedBySuAdmin && ReviewCompleted ? e.Complete == true && e.Reviewed == true : true)
																																											);




		var par = new Dictionary<string, string>();
		par.Add("id", id.ToString());
		par.Add("SendedToSuAdmin", SendedToSuAdmin.ToString());
		par.Add("ReviewedBySuAdmin", ReviewedBySuAdmin.ToString());
		par.Add("ReviewCompleted", ReviewCompleted.ToString());
		par.Add("pageSize", pageSize.ToString());

		var fResult = new TechnicalReportsIndexDto
		{
			Action = nameof(Index),
			Controller = "TechnicalManagement",
			CurrentPage = id,
			PagesCount = pages,
			paramss = par,
			TechnicalReports = result,

		};
		return View(fResult);

	}
	public async Task<IActionResult> Index2()
	{
		return View();
	}
}

