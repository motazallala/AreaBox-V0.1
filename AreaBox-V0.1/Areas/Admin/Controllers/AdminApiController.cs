

using AreaBox_V0._1.Areas.Admin.Models.Countries.send;
using AreaBox_V0._1.Data.Interface;

using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers
{
	[Route("AdminApi")]
	[ApiController]
	public class AdminApiController : ControllerBase
	{

		private readonly IUnitOfWork db;

		public AdminApiController(IUnitOfWork _db)
		{
			db = _db;
		}

		[HttpPost("DisableMediaPost")]
		public async Task<IActionResult> DisableMediaPost([FromForm] string id, [FromForm] string newState)
		{
			bool state = bool.Parse(newState);
			await db.MediaPosts.Disable(id, state);
			await db.Save();

			if (state)
			{
				return Ok(new { Message = $"MediaPost has been successfully Published" });
			}
			else
			{
				return Ok(new { Message = $"MediaPost has been successfully Suspended" });
			}

		}

		[HttpGet("GetMediaPostDetails/{id}")]
		public async Task<IActionResult> GetMediaPostDetails(string id)
		{

			var mediaPost = db.MediaPosts.Find<MediaPosts, MediaPostsDto>
				(x => x.MpostId == id, new[] { "Mpcity", "Mpcategory", "Mpcity.Country", "Mpuser" });

			if (mediaPost == null)
			{
				return NotFound();
			}

			var mediaDetails = new
			{
				imageSrc = mediaPost.Image,
				name = mediaPost.User.UserName,
				email = mediaPost.User.Email,
				city = mediaPost.City.CityName,
				country = mediaPost.City.Country.CountryName,
				category = mediaPost.Category.CategoryName,
				description = mediaPost.LongDescription,
				date = mediaPost.Date,
				state = mediaPost.State
			};

			return Ok(mediaDetails);
		}

		[HttpPost("DisableQAPost")]
		public async Task<IActionResult> DisableQAPost([FromForm] string id, [FromForm] string newState)
		{
			bool state = bool.Parse(newState);
			await db.QuestionPosts.Disable(id, state);
			await db.Save();

			if (state)
			{
				return Ok(new { Message = $"QAPost has been successfully Published" });
			}
			else
			{
				return Ok(new { Message = $"QAPost has been successfully Suspended" });
			}
		}

		[HttpGet("GetQAPostDetails/{id}")]
		public async Task<IActionResult> GetQAPostDetails(string id)
		{

			var qAPost = db.QuestionPosts.Find<QuestionPosts, QuestionPostsDto>
				(x => x.QpostId == id, new[] { "Qpcity", "Qpcategory", "Qpcity.Country", "Qpuser" });

			if (qAPost == null)
			{
				return NotFound();
			}

			var mediaDetails = new
			{
				name = qAPost.User.UserName,
				email = qAPost.User.Email,
				city = qAPost.City.CityName,
				country = qAPost.City.Country.CountryName,
				category = qAPost.Category.CategoryName,
				description = qAPost.Description,
				date = qAPost.Date,
				state = qAPost.State
			};

			return Ok(mediaDetails);
		}
		[HttpGet("GetAllCountryAndCities")]
		public async Task<IActionResult> GetAllCountryAndCities()
		{
			var countryList = await db.Countries.GetAllAsync<Countries, CountriesDtoForApi>(new[] { "Cities" });


			return Ok(countryList);
		}

		[HttpGet("GetAllCategories")]
		public async Task<IActionResult> GetAllCategories()
		{
			var categoriesList = await db.Countries.GetAllAsync<Categories, CategoriesDto>();


			return Ok(categoriesList);
		}


		[HttpGet("GetMediaPostReportDetails/{id}")]
		public async Task<IActionResult> GetMediaPostReportDetails(int id)
		{

			var mediaPostReports = db.MediaPostReports.Find<MediaPostsReports, MediaPostsReportsDto>
				(x => x.PostReportId == id, new[] { "User", "Mpost", "PostReport", "PostReport.ReportTypes" });

			if (mediaPostReports == null)
			{
				return NotFound();
			}

			var mediaPostDetails = new
			{
				reportId = mediaPostReports.PostReportId,
				userName = mediaPostReports.User.UserName,
				userEmail = mediaPostReports.User.Email,
				reportType = mediaPostReports.PostReport.ReportTypes.Type,
				reportTypeDescription = mediaPostReports.PostReport.ReportTypes.Description

			};

			return Ok(mediaPostDetails);
		}

		[HttpGet("GetUserDetails/{id}")]
		public async Task<IActionResult> GetUserDetails(string id)
		{
			var user = await db.Users.GetByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			var userDetails = new
			{
				userID = user.Id,
				userName = user.UserName,
				email = user.Email,
				bio = user.Bio,
				dob = user.DOB,
				gender = user.Gender,
				profilePicture = user.ProfilePicture,
				state = user.State,
			};

			return Ok(userDetails);
		}


		[HttpPost("DisableUser")]
		public async Task<IActionResult> DisableUser([FromForm] string id, [FromForm] string newState)
		{
			bool state = bool.Parse(newState);
			await db.Users.Disable(id, state);
			await db.Save();

			if (state)
			{
				return Ok(new { Message = $"User has been successfully Activated" });
			}
			else
			{
				return Ok(new { Message = $"User has been successfully Suspended" });
			}
		}

		/*        [HttpGet("GetQAPostReportDetails/{id}")]
                public async Task<IActionResult> GetQAPostReportDetails(int id)
                {

                    var qAPostReport = _repoQuestionPostsReports.Find<QuestionPostsReports, QuestionPostsReportViewModel>
                        (x => x.PostReportId == id, new[] { "Qpost", "User", "PostReport", "PostReport.ReportTypes" });

                    if (qAPostReport == null)
                    {
                        return NotFound();
                    }

                    var qAPostDetails = new
                    {
                        reportId = qAPostReport.PostReportId,
                        userName = qAPostReport.User.UserName,
                        userEmail = qAPostReport.User.Email,
                        reportType = qAPostReport.PostReport.ReportTypes.Type,
                        reportTypeDescription = qAPostReport.PostReport.ReportTypes.Description
                    };

                    return Ok(qAPostDetails);
                }*/
	}
}
