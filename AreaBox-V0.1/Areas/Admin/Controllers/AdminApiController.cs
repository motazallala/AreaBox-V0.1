using AreaBox_V0._1.Areas.Admin.Models.MediaPostsReport;
using AreaBox_V0._1.Areas.Admin.Models.QuestionPostReports;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models.MediaPost;
using AreaBox_V0._1.Models.QuestionPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Areas.Admin.Controllers
{
    [Route("AdminApi")]
	[ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IMediaPost _mediaPost;
        private readonly IQuestionPost _questionPost;
        private readonly IUserManagement _userManagement;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<MediaPosts> _repoMediaPost;
        private readonly IRepository<QuestionPosts> _repoQuestionPosts;
        private readonly IRepository<MediaPostsReports> _repoMediaPostsReports;
        private readonly IRepository<QuestionPostsReports> _repoQuestionPostsReports;
        private readonly IRepository<ApplicationUser> _repoUserManager;

        public AdminApiController(
            IMediaPost mediaPost,
            IQuestionPost questionPost,
			UserManager<ApplicationUser> userManager,
			IUserManagement userManagement,
            IRepository<MediaPosts> repository,
            IRepository<QuestionPosts> repoQuestionPosts,
            IRepository<MediaPostsReports> repoMediaPostsReports,
            IRepository<QuestionPostsReports> repoQuestionPostsReports,
            IRepository<ApplicationUser> repoUserManager)
        {
            _mediaPost = mediaPost;
            _questionPost = questionPost;
            _userManagement = userManagement;
            _userManager = userManager;
			_repoMediaPost = repository;
            _repoQuestionPosts = repoQuestionPosts;
            _repoMediaPostsReports = repoMediaPostsReports;
            _repoQuestionPostsReports = repoQuestionPostsReports;
			_repoUserManager = repoUserManager;
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

		[HttpPost("LikeMediaPost")]
		public async Task<IActionResult> LikeMediaPost([FromForm] string id, [FromForm] string newState)
		{
			bool state = bool.Parse(newState);
            var currentUserID = _userManager.GetUserId(User);
			await _mediaPost.Like(id, state, currentUserID);
			

			if (state)
			{
				return Ok(new { Message = $"Media Post han been Liked" });
			}
			else
			{
				return Ok(new { Message = $"Media Post han been Unliked" });
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




        [HttpGet("GetMediaPostReportDetails/{id}")]
        public async Task<IActionResult> GetMediaPostReportDetails(int id)
        {

            var mediaPostReports = _repoMediaPostsReports.Find<MediaPostsReports, MediaPostsReportViewModel>
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
            var user = await _repoUserManager.GetByIdAsync(id);

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
			await _userManagement.Disable(id, state);

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
