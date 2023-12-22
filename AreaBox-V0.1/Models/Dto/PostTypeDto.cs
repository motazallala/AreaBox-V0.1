namespace AreaBox_V0._1.Models.Dto
{
    public class PostTypeDto
    {
        public int PostTypeId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PostReportsDto> PostReports { get; set; }
    }
}
