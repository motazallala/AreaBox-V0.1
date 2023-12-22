namespace AreaBox_V0._1.Models.Dto;

public class MediaPostsDto
{

    public string Id { get; set; }

    public int CategoryId { get; set; }

    public int CityId { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public string Image { get; set; }

    public string ShortDescription { get; set; }

    public string LongDescription { get; set; }

    public bool State { get; set; }

    public virtual ICollection<MediaPostLikesDto> MediaPostLikes { get; set; } = new List<MediaPostLikesDto>();
    public int PostLike => MediaPostLikes.Count;

    public virtual ICollection<MediaPostCommentsDto> MediaPostComments { get; set; } = new List<MediaPostCommentsDto>();


    public virtual CategoriesDto Category { get; set; }

    public virtual CitiesDto City { get; set; }

    public virtual ApplicationUserDto User { get; set; }



}