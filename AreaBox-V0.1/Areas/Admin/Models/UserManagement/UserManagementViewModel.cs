using AreaBox_V0._1.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Areas.Admin.Models.UserManagement
{
	public class UserManagementViewModel : IdentityUser
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public DateTime DOB { get; set; }
		public string? ProfilePicture { get; set; }
		public string? Bio { get; set; }
		public bool State { get; set; }
		public virtual ICollection<MediaPosts> MediaPosts { get; set; }
		public virtual ICollection<QuestionPosts> QuestionPosts { get; set; }
		public virtual ICollection<TechnicalReports> TechnicalReports { get; set; }
		public virtual ICollection<MediaPostsReports> MediaPostsReports { get; set; }
		public virtual ICollection<QuestionPostsReports> QuestionPostsReports { get; set; }
		public virtual ICollection<MediaPostComments> MediaPostComments { get; set; }
		public virtual ICollection<QuestionPostComments> QuestionPostComments { get; set; }

	}
}
