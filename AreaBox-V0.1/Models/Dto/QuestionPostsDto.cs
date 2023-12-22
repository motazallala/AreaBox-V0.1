namespace AreaBox_V0._1.Models.Dto;

public class QuestionPostsDto
{
    public string Id { get; set; }

    public int CategoryId { get; set; }

    public int CityId { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool State { get; set; }

    public virtual CategoriesDto Category { get; set; }

    public virtual CitiesDto City { get; set; }

    public virtual ApplicationUserDto User { get; set; }

    public virtual ICollection<QuestionPostCommentsDto> QuestionPostComments { get; set; } = new List<QuestionPostCommentsDto>();

}