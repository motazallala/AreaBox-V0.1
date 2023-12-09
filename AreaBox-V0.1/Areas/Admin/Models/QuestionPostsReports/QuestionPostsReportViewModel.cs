using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Areas.Admin.Models.QuestionPostsReports;

public class QuestionPostsReportViewModel
{
    public int ReportTypeId { get; set; }

    public string QpostId { get; set; }

    public virtual QuestionPosts Qpost { get; set; }

    public virtual PostReports ReportType { get; set; }
}
