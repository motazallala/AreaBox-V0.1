

namespace AreaBox_V0._1.Data.Model;
public class PostReports
{
    public int PostReportId { get; set; }

    public int PostTypeId { get; set; }

    public int ReportTypeId { get; set; }

    public virtual PostType PostType { get; set; }

    public virtual ReportTypes ReportTypes { get; set; }
}