namespace AreaBox_V0._1.Models.Dto;

public class MediaPostLikesDto
{
    public string MpostId { get; set; }

    public string UserId { get; set; }

    public virtual MediaPostsDto Mpost { get; set; }

    public virtual ApplicationUserDto User { get; set; }

}