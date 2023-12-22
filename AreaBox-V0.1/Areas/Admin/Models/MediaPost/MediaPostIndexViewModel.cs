using AreaBox_V0._1.Areas.Admin.Models.Pagination;

namespace AreaBox_V0._1.Areas.Admin.Models.MediaPost;

public class MediaPostIndexViewModel : PageViewModel
{
	public IEnumerable<MediaPostViewModel> mediaPosts { get; set; }
}
