namespace AreaBox_V0._1.Models.Dto;

public class UserCategoriesDto
{
    public int CategoryId { get; set; }

    public string UserId { get; set; }

    public virtual CategoriesDto Category { get; set; }

    public virtual ApplicationUserDto User { get; set; }
}
