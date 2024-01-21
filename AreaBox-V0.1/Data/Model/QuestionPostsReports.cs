

namespace AreaBox_V0._1.Data.Model;

public class QuestionPostsReports
{

	public string QpostId { get; set; }

	public string UserId { get; set; }
	public int ReportTypeId { get; set; }

	public ApplicationUser User { get; set; }

	public virtual QuestionPosts Qpost { get; set; }

	public virtual ReportTypes ReportType { get; set; }

}