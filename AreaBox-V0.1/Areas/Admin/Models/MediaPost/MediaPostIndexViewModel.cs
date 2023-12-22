using AreaBox_V0._1.Areas.Admin.Models.Pagination;
using AreaBox_V0._1.Models.Dto;

namespace AreaBox_V0._1.Areas.Admin.Models.MediaPost;

public class MediaPostIndexViewModel : PageViewModel
{
    public IEnumerable<MediaPostsDto> mediaPosts { get; set; }
}
