using AreaBox_V0._1.Consts;
using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AreaBoxDbContext _db;
		private readonly IMapper _mapper;

		public Repository(AreaBoxDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<IEnumerable<TViewModel>> GetAllAsync<TEntity, TViewModel>(string[] includes = null)
			where TEntity : class
			where TViewModel : class
		{
			IQueryable<TEntity> query = _db.Set<TEntity>();

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			var entities = await query.ToListAsync();
			var viewModels = _mapper.Map<IEnumerable<TViewModel>>(entities);

			return viewModels;
		}

		public async Task<T> GetByIdAsync(string id)
		{
			return await _db.Set<T>().FindAsync(id);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _db.Set<T>().FindAsync(id);
		}

		public void Add(T entity)
		{
			_db.Set<T>().Add(entity);
		}

		public void Remove(T entity)
		{
			_db.Set<T>().Remove(entity);
		}

		public void Update(T entity)
		{
			_db.Set<T>().Update(entity);
		}




		public async Task<TViewModel> Find<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, string[] includes = null)
			 where TEntity : class
			 where TViewModel : class
		{
			IQueryable<TEntity> query = _db.Set<TEntity>();

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			var entities = await query.SingleOrDefaultAsync(match);
			var viewModels = _mapper.Map<TViewModel>(entities);
			return viewModels;
		}

		public async Task<IEnumerable<TViewModel>> FindAll<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, string[] includes = null)
			where TEntity : class
			 where TViewModel : class
		{
			IQueryable<TEntity> query = _db.Set<TEntity>();

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			var entities = await query.Where(match).ToListAsync();
			var viewModels = _mapper.Map<IEnumerable<TViewModel>>(entities);
			return viewModels;
		}

		public async Task<IEnumerable<TViewModel>> FindAndFilter<TEntity, TViewModel>(string[] includes = null, int? skip = null, int? take = null, Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, params Expression<Func<TEntity, bool>>[] match)
			where TEntity : class
			where TViewModel : class
		{
			IQueryable<TEntity> query = _db.Set<TEntity>();

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			if (match != null)
			{
				foreach (var item in match)
				{

					query = query.Where(item);

				}
			}
			if (orderBy != null)
			{
				if (orderByDirection == OrderBy.Ascending)
					query = query.OrderBy(orderBy);
				else
					query = query.OrderByDescending(orderBy);

			}
			if (skip.HasValue)
			{
				query = query.Skip(skip.Value);
			}

			if (take.HasValue)
			{
				query = query.Take(take.Value);
			}
			var entities = await query.ToListAsync();
			var viewModels = _mapper.Map<IEnumerable<TViewModel>>(entities);
			return viewModels;
		}

		public async Task<int> Count<TEntity>(params Expression<Func<TEntity, bool>>[] match) where TEntity : class
		{
			IQueryable<TEntity> query = _db.Set<TEntity>();

			if (match != null)
			{
				foreach (var item in match)
				{
					if (item != null)
					{
						query = query.Where(item);
					}
				}
			}

			return await query.CountAsync();

		}

		public async Task<int> Count()
		{
			return await _db.Set<T>().CountAsync();
		}

		public async Task<bool> CheckItemExistence<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class
		{
			IQueryable<TEntity> query = _db.Set<TEntity>();
			return await query.Where(match).AsNoTracking().AnyAsync();
		}

		public void Attach(T entity)
		{
			_db.Attach(entity);
		}
	}
}
