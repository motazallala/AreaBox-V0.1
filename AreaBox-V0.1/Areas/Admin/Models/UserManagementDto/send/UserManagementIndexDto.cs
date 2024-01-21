using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Models.Pagination;

namespace AreaBox_V0._1.Areas.Admin.Models.UserManagementDto.send;

public class UserManagementIndexDto : PageViewModel
{
	public IEnumerable<ApplicationUserDto> Users { get; set; }
}
