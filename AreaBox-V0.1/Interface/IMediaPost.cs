namespace AreaBox_V0._1.Interface
{
    public interface IMediaPost
    {
        Task<string> getCityById(int id);
        Task<string> getCountryById(int id);
		Task Disable(string id);
    }
}
