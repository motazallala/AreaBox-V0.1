

namespace AreaBox_V0._1.Data.Model;

public class MediaPostsReports
{
    public int PostReportId { get; set; }

    public string MpostId { get; set; }

    public virtual MediaPosts Mpost { get; set; }

    public virtual PostReports PostReport { get; set; }

}