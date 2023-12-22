namespace AreaBox_V0._1.Models.Dto;

public class MediaPostsReportsDto
{
    public int PostReportId { get; set; }

    public string MpostId { get; set; }
    public string UserId { get; set; }

    public ApplicationUserDto User { get; set; }

    public virtual MediaPostsDto Mpost { get; set; }

    public virtual PostReportsDto PostReport { get; set; }

}