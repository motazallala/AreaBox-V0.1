namespace AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.Input
{
    public class UQuestionPostEditDto
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public int CityId { get; set; }

    }
}
