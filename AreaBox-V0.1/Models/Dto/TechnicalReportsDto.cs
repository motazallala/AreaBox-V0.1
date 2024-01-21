namespace AreaBox_V0._1.Models.Dto;

public class TechnicalReportsDto
{
    public int TechnicalReportId { get; set; }

    public string Type { get; set; }

    public string Details { get; set; }

    public string UserId { get; set; }

    public string UserEmail { get; set; }

    public virtual ApplicationUserDto User { get; set; }
}