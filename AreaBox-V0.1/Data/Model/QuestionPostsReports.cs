

namespace AreaBox_V0._1.Data.Model;

public partial class QuestionPostsReports
{
    public int ReportTypeId { get; set; }

    public string QpostId { get; set; }

    public virtual QuestionPosts Qpost { get; set; }

    public virtual ReportTypes ReportType { get; set; }
}