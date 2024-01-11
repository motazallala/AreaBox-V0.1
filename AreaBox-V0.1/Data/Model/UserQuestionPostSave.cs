namespace AreaBox_V0._1.Data.Model;

public class UserQuestionPostSave
{
    public string UserId { get; set; }
    public string QpostId { get; set; }

    public virtual QuestionPosts QuestionPosts { get; set; }

    public virtual ApplicationUser User { get; set; }
}
