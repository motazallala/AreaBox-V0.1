using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;

namespace AreaBox_V0._1.Areas.User.Models.UMediaPostReportTypeDto.Send
{
	public class UReportTypeOutPutDto
	{
		public int ReportTypeId { get; set; }

		public string Type { get; set; }

		public string? Description { get; set; }

	}
}
