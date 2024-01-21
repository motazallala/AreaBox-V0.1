namespace AreaBox_V0._1.Models.Dto;

public class QuestionPostCommentsDto
{

    public string QpcommentId { get; set; }

    public string QpostId { get; set; }
    public string UserId { get; set; }

    public DateTime QpcommentDate { get; set; }
    public ApplicationUserDto User { get; set; }

    public string QpcommentContent { get; set; }

    public virtual QuestionPostsDto Qpost { get; set; }
}