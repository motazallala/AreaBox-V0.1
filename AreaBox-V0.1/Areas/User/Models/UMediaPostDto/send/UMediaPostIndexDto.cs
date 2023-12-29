using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Models.Pagination;

namespace AreaBox_V0._1.Areas.User.Models.UMediaPostDto.send;

public class UMediaPostIndexDto : PageViewModel
{
    public IEnumerable<MediaPostsDto> mediaPostsDtos { get; set; }
}
