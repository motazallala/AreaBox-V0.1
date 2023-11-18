
namespace AreaBox_V0._1.Data.Model;

public partial class MediaPostLikes
{
    public string MpostId { get; set; }

    public string UserId { get; set; }

    public virtual MediaPosts Mpost { get; set; }

    public virtual ApplicationUser User { get; set; }

}