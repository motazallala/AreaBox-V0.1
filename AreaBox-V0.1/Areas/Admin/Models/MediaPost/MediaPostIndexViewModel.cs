using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Models.Pagination;

namespace AreaBox_V0._1.Areas.Admin.Models.MediaPost;

public class MediaPostIndexViewModel : PageViewModel
{
    public IEnumerable<MediaPostsDto> mediaPosts { get; set; }
}
