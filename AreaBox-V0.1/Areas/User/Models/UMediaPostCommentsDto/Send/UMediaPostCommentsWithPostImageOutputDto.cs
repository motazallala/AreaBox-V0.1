namespace AreaBox_V0._1.Areas.User.Models.UMediaPostCommentsDto.Send;

public class UMediaPostCommentsWithPostImageOutputDto
{
	public IEnumerable<UMediaPostCommentsOutputDto> Comments { get; set; }
	public string MediaPostImage { get; set; }
}
