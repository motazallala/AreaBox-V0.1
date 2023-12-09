

namespace AreaBox_V0._1.Data.Model;

public class QuestionPostsReports
{
    public int PostReportId { get; set; }

    public string QpostId { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    public virtual QuestionPosts Qpost { get; set; }

    public virtual PostReports PostReports { get; set; }
}