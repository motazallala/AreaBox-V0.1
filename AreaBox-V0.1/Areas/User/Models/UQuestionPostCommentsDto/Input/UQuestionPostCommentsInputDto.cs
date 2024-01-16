namespace AreaBox_V0._1.Areas.User.Models.UQuestionPostCommentsDto.Input
{
    public class UQuestionPostCommentsInputDto
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public DateTime CommnetDate { get; set; }
        public string CommentContent { get; set; }
    }
}
