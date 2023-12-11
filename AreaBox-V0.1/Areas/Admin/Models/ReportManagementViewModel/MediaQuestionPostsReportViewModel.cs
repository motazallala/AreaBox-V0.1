using AreaBox_V0._1.Areas.Admin.Models.MediaPostsReport;
using AreaBox_V0._1.Areas.Admin.Models.QuestionPostReports;


namespace AreaBox_V0._1.Areas.Admin.Models.ReportManagementViewModel;

public class MediaQuestionPostsReportViewModel
{
    public IEnumerable<MediaPostsReportViewModel> MediaPostsReports { get; set; }

    public IEnumerable<QuestionPostsReportViewModel> QuestionPostsReports { get; set; }
}
