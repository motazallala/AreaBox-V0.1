namespace AreaBox_V0._1.Data.Model;

public class UserCategories
{
    public int CategoryId { get; set; }

    public string UserId { get; set; }


    public virtual Categories Category { get; set; }

    public virtual ApplicationUser User { get; set; }
}
