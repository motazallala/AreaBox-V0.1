using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Interface
{
	public interface IUserManagementRepository : IRepository<ApplicationUser>
	{
		Task<IEnumerable<ApplicationUserDto>> FindAndFilter(Expression<Func<ApplicationUser, bool>> match,
																			string[] includes = null,
																			int? skip = null,
																			int? take = null
																			);
		Task<int> CountUser(Expression<Func<ApplicationUser, bool>> match);
		Task Disable(string id, bool state);
	}
}
