using AreaBox_V0._1.Consts;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Interface
{
	public interface IRepository<T> where T : class
	{


		Task<IEnumerable<TViewModel>> GetAllAsync<TEntity, TViewModel>(string[] includes = null)
				  where TEntity : class
				  where TViewModel : class;


		Task<T> GetByIdAsync(string id);

		Task<TViewModel> Find<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, string[] includes = null)
		 where TEntity : class
		 where TViewModel : class;

		Task<IEnumerable<TViewModel>> FindAll<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, string[] includes = null)
		   where TEntity : class
			where TViewModel : class;

		Task<IEnumerable<TViewModel>> FindAndFilter<TEntity, TViewModel>(string[] includes = null,
																				   int? skip = null,
																				   int? take = null,
																				   Expression<Func<TEntity, object>> orderBy = null,
																				   string orderByDirection = OrderBy.Ascending,
																				   params Expression<Func<TEntity, bool>>[] match)
		   where TEntity : class
		   where TViewModel : class;


		Task<bool> CheckItemExistence<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class;
		Task<int> Count<TEntity>(params Expression<Func<TEntity, bool>>[] match) where TEntity : class;


		void Add(T entity);

		void Update(T entity);

		void Remove(T entity);
		Task<int> Count();


	}
}
