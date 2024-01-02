using AreaBox_V0._1.Models.Dto;
using AreaBox_V0._1.Models.Pagination;

namespace AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.send
{
    public class UQuestionPostIndexDto : PageViewModel
    {
        public IEnumerable<QuestionPostsDto> questionPostsDtos { get; set; }

    }
}
