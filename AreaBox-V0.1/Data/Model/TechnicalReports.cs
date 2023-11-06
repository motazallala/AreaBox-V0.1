


namespace AreaBox_V0._1.Data.Model;

public partial class TechnicalReports
{
    public int TechnicalReportId { get; set; }

    public string Type { get; set; }

    public string Details { get; set; }

    public string UserId { get; set; }

    public string UserEmail { get; set; }

    public virtual ApplicationUser User { get; set; }
}