using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Areas.Admin.Models.MediaPostsReport;

public class MediaPostsReportViewModel
{
    public int PostReportId { get; set; }

    public string MpostId { get; set; }
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    public virtual MediaPosts Mpost { get; set; }

    public virtual PostReports PostReport { get; set; }
}
