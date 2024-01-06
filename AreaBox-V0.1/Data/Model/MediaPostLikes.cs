
using System.ComponentModel.DataAnnotations;

namespace AreaBox_V0._1.Data.Model;

public class MediaPostLikes
{

    public string MpostId { get; set; }

    public string UserId { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }

    public virtual MediaPosts Mpost { get; set; }

    public virtual ApplicationUser User { get; set; }

}