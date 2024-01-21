namespace AreaBox_V0._1.Areas.User.Models.UMediaPostCommentsDto.Send;

public class UMediaPostCommentsOutputDto
{
    public string CommentId { get; set; }

    public string PostId { get; set; }
    public string UserId { get; set; }

    public DateTime CommnetDate { get; set; }

    public string CommentContent { get; set; }


    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string MediaPostImage { get; set; }



}
