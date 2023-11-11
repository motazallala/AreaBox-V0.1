﻿

namespace AreaBox_V0._1.Data.Model;

public partial class MediaPosts
{
    public string MpostId { get; set; }

    public int MpcategoryId { get; set; }

    public int MpcityId { get; set; }

    public DateTime Mpdate { get; set; }

    public string MpuserId { get; set; }

    public string Mpimage { get; set; }

    public string MpshortDescription { get; set; }

    public string MplongDescription { get; set; }

    public bool Mpstate { get; set; }

    public virtual ICollection<MediaPostComments> MediaPostComments { get; set; } = new List<MediaPostComments>();

    public virtual Categories Mpcategory { get; set; }

    public virtual Cities Mpcity { get; set; }

    public virtual ApplicationUser Mpuser { get; set; }
}