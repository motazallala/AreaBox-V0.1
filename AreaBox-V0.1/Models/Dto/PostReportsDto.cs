namespace AreaBox_V0._1.Models.Dto;
public class PostReportsDto
{
    public int PostReportId { get; set; }

    public int PostTypeId { get; set; }

    public int ReportTypeId { get; set; }

    public virtual PostTypeDto PostType { get; set; }

    public virtual ReportTypesDto ReportTypes { get; set; }
}