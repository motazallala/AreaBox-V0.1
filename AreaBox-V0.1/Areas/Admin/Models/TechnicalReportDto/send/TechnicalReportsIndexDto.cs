using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Models.Pagination;

namespace AreaBox_V0._1.Areas.Admin.Models.TechnicalReportDto.send
{
    public class TechnicalReportsIndexDto : PageViewModel
    {
        public IEnumerable<TechnicalReportsDto> TechnicalReports { get; set; }
    }
}
