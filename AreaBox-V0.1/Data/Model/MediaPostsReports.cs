

namespace AreaBox_V0._1.Data.Model;

public partial class MediaPostsReports
{
    public int ReportTypeId { get; set; }

    public string MpostId { get; set; }

    public virtual MediaPosts Mpost { get; set; }

    public virtual ReportTypes ReportType { get; set; }
}