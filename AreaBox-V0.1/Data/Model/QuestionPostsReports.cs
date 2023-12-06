

namespace AreaBox_V0._1.Data.Model;

public class QuestionPostsReports
{
    public int ReportTypeId { get; set; }

    public string QpostId { get; set; }

    public virtual QuestionPosts Qpost { get; set; }

    public virtual PostReports ReportType { get; set; }
}