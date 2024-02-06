using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;
using AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.send;
using AreaBox_V0._1.Areas.User.Models.UUserCategoriesDto.Send;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]
public class SettingsController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IUnitOfWork _db;
	private readonly ILogger<EnableAuthenticatorModel> _logger;


	public SettingsController(UserManager<ApplicationUser> userManager, IUnitOfWork db, ILogger<EnableAuthenticatorModel> logger)
	{
		_userManager = userManager;
		_db = db;
		_logger = logger;

	}
	public IActionResult MyAccount()
	{
		return View();
	}

	public async Task<IActionResult> MyCategory()
	{
		var userId = _userManager.GetUserId(User);
		if (userId == null)
		{
			return BadRequest("Log in to report the post");
		}
		var userCategoriesList = await _db.UserCategories.FindAll<UserCategories, UUserCategoriesOutputDto>(e => e.UserId == userId, new[] { "Category" });
		return View(userCategoriesList);
	}

	public async Task<IActionResult> MyMediaPostAsync()
	{
		var user = await _userManager.GetUserAsync(User);
		var results = await _db.MediaPosts.FindAll<MediaPosts, UMediaPostOutputDto>(e => e.MpuserId == user.Id, new[] { "Mpcity", "Mpcategory" });
		return View(results);
	}

	public async Task<IActionResult> MyQuestionPost()
	{
		var user = await _userManager.GetUserAsync(User);
		var results = await _db.MediaPosts.FindAll<QuestionPosts, UQuestionPostOutPutDto>(e => e.QpuserId == user.Id, new[] { "Qpcity", "Qpcity.Country", "Qpcategory" });
		return View(results);
	}

	public IActionResult SavePost()
	{
		return View();
	}
	public async Task<IActionResult> TechicalReport()
	{
		var user = await _userManager.GetUserAsync(User);
		var result = await _db.TechnicalReports.FindAll<TechnicalReports, TechnicalReports>(e => e.UserId == user.Id, null);
		return View(result);
	}

	public IActionResult AddTechicalReport()
	{

		return View();
	}
	[HttpPost]
	public async Task<IActionResult> AddTechicalReportAsync(TechnicalReports newTechnicalReports)
	{

		var user = await _userManager.GetUserAsync(User);
		newTechnicalReports.ReportDateTime = DateTime.Now;
		newTechnicalReports.UserId = user.Id;
		_db.TechnicalReports.Add(newTechnicalReports);
		await _db.Save();
		return RedirectToAction(nameof(TechicalReport));

	}
	public IActionResult SavedMediaPost()
	{
		return View();
	}

	public IActionResult SavedQuestionPost()
	{
		return View();
	}

	public IActionResult PersonalData()
	{
		return View();
	}

}
