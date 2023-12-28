using AreaBox_V0._1.Models.Dto;

namespace AreaBox_V0._1.Areas.Admin.Models.ReportManagementViewModel;

public class MediaQuestionPostsReportViewModel
{
	public IEnumerable<MediaPostsReportsDto> MediaPostsReports { get; set; }
    public IEnumerable<QuestionPostsReportsDto> QuestionPostsReports { get; set; }
}
