using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Data.Model;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime DOB { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; }
    public bool State { get; set; }
    public virtual ICollection<MediaPosts> MediaPosts { get; set; } = new List<MediaPosts>();
    public virtual ICollection<QuestionPosts> QuestionPosts { get; set; } = new List<QuestionPosts>();
    public virtual ICollection<TechnicalReports> TechnicalReports { get; set; } = new List<TechnicalReports>();


}
