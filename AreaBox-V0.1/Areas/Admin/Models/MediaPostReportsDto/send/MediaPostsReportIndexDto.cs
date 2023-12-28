using AreaBox_V0._1.Areas.Admin.Models.Pagination;
using AreaBox_V0._1.Models.Dto;

namespace AreaBox_V0._1.Areas.Admin.Models.MediaPostReportsDto.send;

public class MediaPostsReportIndexDto : PageViewModel
{
	public IEnumerable<MediaPostsReportsDto> mediaPostReports { get; set; }
}
