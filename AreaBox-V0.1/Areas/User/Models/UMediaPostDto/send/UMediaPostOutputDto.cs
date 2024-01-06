namespace AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;

public class UMediaPostOutputDto
{

	public string Id { get; set; }

	public int CategoryId { get; set; }

	public int CityId { get; set; }

	public DateTime Date { get; set; }

	public string UserId { get; set; }
	public string UserName { get; set; }
	public string Image { get; set; }

	public string ShortDescription { get; set; }

	public string LongDescription { get; set; }

	public bool State { get; set; }
	public virtual ICollection<UMediaPostOutputDto> MediaPostsLikes { get; set; }

	public string UserProfilePicture { get; set; }
	public string CountryName { get; set; }
	public string CityName { get; set; }
	public string CategoryName { get; set; }

	public int CountPostLike { get; set; }
	public int CountMediaPostComments { get; set; }
}
