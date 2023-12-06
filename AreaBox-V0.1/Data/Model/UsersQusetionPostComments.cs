

namespace AreaBox_V0._1.Data.Model;

public class UsersQusetionPostComments
{
    public UsersQusetionPostComments()
    {
        this.QpcommentId = Guid.NewGuid().ToString();
    }

    public string QpcommentId { get; set; }

    public string UserId { get; set; }

    public virtual QuestionPostComments Qpcomment { get; set; }

    public virtual ApplicationUser User { get; set; }
}