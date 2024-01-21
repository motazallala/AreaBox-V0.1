using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Models.Dto;

public class ApplicationUserDto : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime DOB { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; }
    public bool State { get; set; }
    public virtual ICollection<MediaPostsDto> MediaPosts { get; set; }
    public virtual ICollection<QuestionPostsDto> QuestionPosts { get; set; }
    public virtual ICollection<TechnicalReportsDto> TechnicalReports { get; set; }
    public virtual ICollection<MediaPostsReportsDto> MediaPostsReports { get; set; }
    public virtual ICollection<QuestionPostsReportsDto> QuestionPostsReports { get; set; }
    public virtual ICollection<MediaPostCommentsDto> MediaPostComments { get; set; }
    public virtual ICollection<QuestionPostCommentsDto> QuestionPostComments { get; set; }

}
