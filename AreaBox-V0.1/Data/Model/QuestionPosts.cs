

namespace AreaBox_V0._1.Data.Model;

public partial class QuestionPosts
{
    public QuestionPosts()
    {
        this.QpostId = Guid.NewGuid().ToString();
    }
    public string QpostId { get; set; }

    public int QpcategoryId { get; set; }

    public int QpcityId { get; set; }

    public DateTime Qpdate { get; set; }

    public string QpuserId { get; set; }

    public string Qptitle { get; set; }

    public string Qpdescription { get; set; }

    public bool Qpstate { get; set; }

    public virtual Categories Qpcategory { get; set; }

    public virtual Cities Qpcity { get; set; }

    public virtual ApplicationUser Qpuser { get; set; }

    public virtual ICollection<QuestionPostComments> QuestionPostComments { get; set; } = new List<QuestionPostComments>();
}