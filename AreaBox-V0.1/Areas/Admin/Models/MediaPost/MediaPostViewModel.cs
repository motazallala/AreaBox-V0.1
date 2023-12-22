using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Areas.Admin.Models.MediaPost
{
	public class MediaPostViewModel
	{
		public Guid Id { get; set; }

		public int CategoryId { get; set; }

		public int CityId { get; set; }

		public DateTime Date { get; set; }

		public string UserId { get; set; }

		public string Image { get; set; }

		public string ShortDescription { get; set; }

		public string LongDescription { get; set; }

		public bool State { get; set; }

		public virtual ICollection<MediaPostLikes> MediaPostLikes { get; set; } = new List<MediaPostLikes>();

		public int PostLike => MediaPostLikes.Count;

		public virtual ICollection<MediaPostComments> MediaPostComments { get; set; } = new List<MediaPostComments>();

		public virtual Categories Category { get; set; }

		public virtual Cities City { get; set; }

		public virtual ApplicationUser User { get; set; }
	}
}
