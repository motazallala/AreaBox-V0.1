using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Interface
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<TViewModel>> GetAllAsync<TEntity, TViewModel>()
			 where TEntity : class
			 where TViewModel : class;


		Task<IEnumerable<TViewModel>> GetAllAsync<TEntity, TViewModel>(string[] includes = null)
				  where TEntity : class
				  where TViewModel : class;


		Task<T> GetByIdAsync(string id);

		TViewModel Find<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, string[] includes = null)
		 where TEntity : class
		 where TViewModel : class;

		Task<IEnumerable<TViewModel>> FindAll<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, string[] includes = null)
		   where TEntity : class
			where TViewModel : class;

		public Task<IEnumerable<TViewModel>> FindAndFilter<TEntity, TViewModel>(string[] includes = null,
																					int? skip = null,
																					int? take = null,
																					params Expression<Func<TEntity, bool>>[] match)
			where TEntity : class
			where TViewModel : class;

		public Task<int> Count<TEntity>(params Expression<Func<TEntity, bool>>[] match) where TEntity : class;


		void Add(T entity);

		void Update(T entity);

		void Remove(T entity);
        Task<int> Count();
        Task SaveChnagesAsync();


    }
}
