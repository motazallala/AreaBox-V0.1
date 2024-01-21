namespace AreaBox_V0._1.Areas.User.Models.UMediaPostDto.input;

public class UMediaPostInputDto
{
    public int CategoryId { get; set; }

    public int CityId { get; set; }

    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public string Image { get; set; }

    public string ShortDescription { get; set; }

    public string LongDescription { get; set; }
}
