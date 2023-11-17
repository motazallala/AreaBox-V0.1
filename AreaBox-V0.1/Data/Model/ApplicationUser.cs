using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data.Model;
public class ApplicationUser : IdentityUser
{
    public virtual ICollection<MediaPosts> MediaPosts { get; set; } = new List<MediaPosts>();

    public virtual ICollection<QuestionPosts> QuestionPosts { get; set; } = new List<QuestionPosts>();

    public virtual ICollection<TechnicalReports> TechnicalReports { get; set; } = new List<TechnicalReports>();


}
