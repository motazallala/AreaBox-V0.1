
namespace AreaBox_V0._1.Data.Model;

public class TechnicalReports
{
    public int TechnicalReportId { get; set; }

    public string Type { get; set; }

    public string Details { get; set; }

    public string UserId { get; set; }

    public string UserEmail { get; set; }

    public virtual ApplicationUser User { get; set; }

    public bool? ReviewByAdmin { get; set; }
    public string? TechnicalAdminId { get; set; }

    public bool? Reviewed { get; set; }
    public string? SuperAdminId { get; set; }
    public string? ReviewNote { get; set; }
    public bool? Complete { get; set; }

    public virtual ApplicationUser? TechnicalAdmin { get; set; }
    public virtual ApplicationUser? SuperAdmin { get; set; }

}