using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.MediaPost;
using AreaBox_V0._1.Models.QuestionPost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers
{
	[Route("AdminApi")]
	[ApiController]
	public class AdminApiController : ControllerBase
	{
		private readonly IMediaPost _mediaPost;
		private readonly IQuestionPost _questionPost;
		private readonly IRepository<MediaPosts> _repoMediaPost;
		private readonly IRepository<QuestionPosts> _repoQuestionPosts;

		public AdminApiController(
			IMediaPost mediaPost,
			IQuestionPost questionPost,
			IRepository<MediaPosts> repository,
			IRepository<QuestionPosts> repoQuestionPosts)
		{
			_mediaPost = mediaPost;
			_questionPost = questionPost;
			_repoMediaPost = repository;
			_repoQuestionPosts = repoQuestionPosts;
		}

		[HttpPost("DisableMediaPost")]
		public async Task<IActionResult> DisableMediaPost([FromForm] string id, [FromForm] string newState)
		{
			bool state = bool.Parse(newState);
			await _mediaPost.Disable(id, state);

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

			var mediaPost = _repoMediaPost.Find<MediaPosts, MediaPostViewModel>
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
			await _questionPost.Disable(id, state);

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

			var qAPost = _repoQuestionPosts.Find<QuestionPosts, QuestionPostViewModel>
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
	}
}
