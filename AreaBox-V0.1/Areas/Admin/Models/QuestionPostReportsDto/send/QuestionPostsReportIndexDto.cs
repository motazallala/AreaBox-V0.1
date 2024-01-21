using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Models.Pagination;

namespace AreaBox_V0._1.Areas.Admin.Models.QuestionPostReportsDto.send;

public class QuestionPostsReportIndexDto : PageViewModel
{
	public IEnumerable<QuestionPostsReportsDto> questionPostsReports { get; set; }

}
