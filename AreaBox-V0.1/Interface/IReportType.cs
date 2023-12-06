namespace AreaBox_V0._1.Interface
{
    public interface IReportType
    {
        Task<T> GetPostByReportId<T>(Guid id);
    }
}
