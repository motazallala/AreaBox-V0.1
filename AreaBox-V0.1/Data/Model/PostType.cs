namespace AreaBox_V0._1.Data.Model
{
    public class PostType
    {
        public int PostTypeId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PostReports> PostReports { get; set; }
    }
}
