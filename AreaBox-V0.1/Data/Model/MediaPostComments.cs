﻿

namespace AreaBox_V0._1.Data.Model;

public class MediaPostComments
{
    public MediaPostComments()
    {
        this.MpcommentId = Guid.NewGuid().ToString();
    }
    public string MpcommentId { get; set; }

    public string MpostId { get; set; }
    public string UserId { get; set; }

    public DateTime MpcommnetDate { get; set; }

    public string MpcommentContent { get; set; }
    public ApplicationUser User { get; set; }

    public virtual MediaPosts Mpost { get; set; }
}