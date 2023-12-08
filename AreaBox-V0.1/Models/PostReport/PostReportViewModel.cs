using AreaBox_V0._1.Data.Model;
using Microsoft.Build.Framework;
using System.ComponentModel;

namespace AreaBox_V0._1.Models.PostReport;

public class PostReportViewModel
{

    public int PostReportId { get; set; }

    public int PostTypeId { get; set; }

    public int ReportTypeId { get; set; }

    public virtual PostType PostType { get; set; }

    public virtual ReportTypes ReportTypes { get; set; }
}
