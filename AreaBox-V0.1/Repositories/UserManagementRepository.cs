using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using Microsoft.AspNetCore.Identity;

namespace AreaBox_V0._1.Repositories
{
	public class UserManagementRepository : IUserManagement
	{
		private readonly IRepository<ApplicationUser> _repoUserManager;

		public UserManagementRepository(IRepository<ApplicationUser> repoUserManager)
		{
			_repoUserManager = repoUserManager;
		}

		public async Task Disable(string id, bool state)
		{
			var getUser = await _repoUserManager.GetByIdAsync(id);

			if (getUser != null)
			{
				getUser.State = !state;
				_repoUserManager.Update(getUser);
				await _repoUserManager.SaveChnagesAsync();
			}
		}
	}
}
