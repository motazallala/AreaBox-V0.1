using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Interface
{
    public interface IReportTypeRepository : IRepository<ReportTypes>
    {
        Task<T> GetPostByReportId<T>(Guid id);
    }
}
