using AreaBox_V0._1.Areas.Admin.Models.Pagination;
using AreaBox_V0._1.Models.Dto;
namespace AreaBox_V0._1.Areas.Admin.Models.QuestionPostDto.send
{
	public class QuestionPostIndexDto :PageViewModel
	{
		public IEnumerable<QuestionPostsDto> questionPostDtos { get; set; }
	}
}
