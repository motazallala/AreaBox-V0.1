

namespace AreaBox_V0._1.Data.Model;

public partial class UsersMediaPostComments
{
    public UsersMediaPostComments()
    {
        this.MpcommentId = Guid.NewGuid().ToString();
    }
    public string MpcommentId { get; set; }

    public string UserId { get; set; }

    public virtual MediaPostComments Mpcomment { get; set; }

    public virtual ApplicationUser User { get; set; }
}