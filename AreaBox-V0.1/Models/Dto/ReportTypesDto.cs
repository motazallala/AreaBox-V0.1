namespace AreaBox_V0._1.Models.Dto
{
    public class ReportTypesDto
    {
        public int ReportTypeId { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<PostReportsDto> PostReports { get; set; }
    }
}
