namespace AreaBox_V0._1.Models.Dto;

public class QuestionPostsReportsDto
{
	public int ReportTypeId { get; set; }

	public string QpostId { get; set; }

	public string UserId { get; set; }

	public ApplicationUserDto User { get; set; }

	public virtual QuestionPostsDto Qpost { get; set; }

	public virtual ReportTypesDto ReportType { get; set; }


}