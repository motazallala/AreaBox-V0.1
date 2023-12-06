namespace AreaBox_V0._1.Data.Model
{
    public class ReportTypes
    {
        public int ReportTypeId { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<PostReports> PostReports { get; set; }
    }
}
