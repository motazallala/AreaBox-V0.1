using System.Linq.Expressions;

namespace AreaBox_V0._1.Interface
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

		TViewModel Find<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, String[] includes = null)
		 where TEntity : class
		 where TViewModel : class;

		IEnumerable<T> FindAll(Expression<Func<T, bool>> match, String[] includes = null);

		void Add(T entity);

		void Update(T entity);

		void Remove(T entity);

		void Detach<TEntity>(TEntity entity) where TEntity : class;

		Task SaveChnagesAsync();
	}
}
