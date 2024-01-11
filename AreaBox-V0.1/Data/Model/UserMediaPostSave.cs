namespace AreaBox_V0._1.Data.Model;

public class UserMediaPostSave
{
    public string UserId { get; set; }
    public string MpostId { get; set; }

    public virtual MediaPosts MediaPosts { get; set; }

    public virtual ApplicationUser User { get; set; }
}
