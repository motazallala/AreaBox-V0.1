using AreaBox_V0._1.Areas.Admin.Models.MediaPostReportsDto.send;
using AreaBox_V0._1.Areas.Admin.Models.TechnicalReportDto.send;
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
	public async Task<IActionResult> Index(int id = 1, int pageSize = 5)
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
        var result = await db.TechnicalReports.FindAndFilter<TechnicalReports, TechnicalReportsDto>(new[] { "User" },skip,take);




        var par = new Dictionary<string, string>();
        par.Add("id", id.ToString());
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
    }

