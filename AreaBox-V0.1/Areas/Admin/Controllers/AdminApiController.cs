using AreaBox_V0._1.Areas.Admin.Models.MediaPostsReport;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.MediaPost;
using AreaBox_V0._1.Models.QuestionPost;
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

            var mediaPost = db.MediaPosts.Find<MediaPosts, MediaPostViewModel>
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

            var qAPost = db.QuestionPosts.Find<QuestionPosts, QuestionPostViewModel>
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

            var mediaPostReports = db.MediaPostsReports.Find<MediaPostsReports, MediaPostsReportViewModel>
                (x => x.PostReportId == id, new[] { "User", "Mpost", "PostReport", "PostReport.ReportTypes" });

            if (mediaPostReports == null)
            {
                return NotFound();
            }

            var mediaDetails = new
            {
                reportId = mediaPostReports.PostReportId,
                userName = mediaPostReports.User.UserName,
                userEmail = mediaPostReports.User.Email,
                reportType = mediaPostReports.PostReport.ReportTypes.Type,
                reportTypeDescription = mediaPostReports.PostReport.ReportTypes.Description

            };

            return Ok(mediaDetails);
        }

        /*        [HttpGet("GetQAPostReportDetails/{id}")]
                public async Task<IActionResult> GetQAPostReportDetails(int id)
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
                }*/
    }
}
