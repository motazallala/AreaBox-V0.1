

using AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;

namespace AreaBox_V0._1.Data.Model;

public class MediaPosts
{
    public MediaPosts()
    {
        this.MpostId = Guid.NewGuid().ToString();
    }
    public string MpostId { get; set; }

    public int MpcategoryId { get; set; }

    public int MpcityId { get; set; }

    public DateTime Mpdate { get; set; }

    public string MpuserId { get; set; }

    public string Mpimage { get; set; }

    public string MpshortDescription { get; set; }

    public string MplongDescription { get; set; }

    public int? LikeCount { get; set; } = 0;
    public int? CommentCount { get; set; } = 0;

    public bool Mpstate { get; set; }

    public virtual ICollection<MediaPostLikes> MediaPostsLikes { get; set; } = new List<MediaPostLikes>();


    public virtual ICollection<MediaPostComments> MediaPostComments { get; set; } = new List<MediaPostComments>();
    public int CommentCount => MediaPostComments.Count;

    public virtual Categories Mpcategory { get; set; }

    public virtual Cities Mpcity { get; set; }

    public virtual ApplicationUser Mpuser { get; set; }
}