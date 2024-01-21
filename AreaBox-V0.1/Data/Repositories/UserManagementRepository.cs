using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Repositories
{
	public class UserManagementRepository : Repository<ApplicationUser>, IUserManagementRepository
	{
		private readonly AreaBoxDbContext _db;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;

		public UserManagementRepository(AreaBoxDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager) : base(db, mapper)
		{
			_db = db;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<int> CountUser(Expression<Func<ApplicationUser, bool>> match)
		{
			IQueryable<ApplicationUser> query = _userManager.Users.AsQueryable();
			return await query.Where(match).CountAsync();
		}

		public async Task Disable(string id, bool state)
		{
			var getUser = await _db.Users.FindAsync(id);

			if (getUser != null)
			{
				getUser.State = !state;
				_db.Users.Update(getUser);
			}
		}

		public async Task<IEnumerable<ApplicationUserDto>> FindAndFilter(Expression<Func<ApplicationUser, bool>> match, string[] includes = null, int? skip = null, int? take = null)
		{
			IQueryable<ApplicationUser> query = _userManager.Users.AsQueryable();

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			if (skip != null)
			{
				int skipItem = skip.Value;
				query = query.Skip(skipItem);
			}

			if (take != null)
			{
				int takeItem = take.Value;
				query = query.Take(takeItem);
			}
			var entities = await query.Where(match).ToListAsync();
			var viewModels = _mapper.Map<IEnumerable<ApplicationUserDto>>(entities);
			return viewModels;
		}
	}
}
