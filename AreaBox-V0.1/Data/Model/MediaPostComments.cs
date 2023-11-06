

namespace AreaBox_V0._1.Data.Model;

public partial class MediaPostComments
{
    public string MpcommentId { get; set; }

    public string MpostId { get; set; }

    public DateTime MpcommnetDate { get; set; }

    public string MpcommentContent { get; set; }

    public virtual MediaPosts Mpost { get; set; }
}