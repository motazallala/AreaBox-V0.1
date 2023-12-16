using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Interface
{
    public interface IUserManagement : IRepository<ApplicationUser>
    {
        Task Disable(string id, bool state);
    }
}
