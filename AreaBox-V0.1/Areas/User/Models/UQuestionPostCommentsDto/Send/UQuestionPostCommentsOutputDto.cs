


namespace AreaBox_V0._1.Areas.User.Models.UQuestionPostCommentsDto.Send;

public class UQuestionPostCommentsOutputDto
{
    public string CommentId { get; set; }

    public string PostId { get; set; }
    public string UserId { get; set; }

    public DateTime CommentDate { get; set; }

    public string CommentContent { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }

}
