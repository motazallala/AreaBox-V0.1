

namespace AreaBox_V0._1.Data.Model;

public partial class QuestionPostComments
{
    public string QpcommentId { get; set; }

    public string QpostId { get; set; }

    public DateTime QpcommentDate { get; set; }

    public string QpcommentContent { get; set; }

    public virtual QuestionPosts Qpost { get; set; }
}