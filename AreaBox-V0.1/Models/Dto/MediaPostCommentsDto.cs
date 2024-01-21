namespace AreaBox_V0._1.Models.Dto;

public class MediaPostCommentsDto
{
    public string MpcommentId { get; set; }

    public string MpostId { get; set; }
    public string UserId { get; set; }

    public DateTime MpcommnetDate { get; set; }

    public string MpcommentContent { get; set; }
    public ApplicationUserDto User { get; set; }

    public virtual MediaPostsDto Mpost { get; set; }
}