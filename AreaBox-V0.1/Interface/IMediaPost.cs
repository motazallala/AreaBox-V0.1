namespace AreaBox_V0._1.Interface
{
	public interface IMediaPost
	{
		Task Disable(string id, bool state);
		Task Like(string id, bool state, string currentUserID);
	}
}
