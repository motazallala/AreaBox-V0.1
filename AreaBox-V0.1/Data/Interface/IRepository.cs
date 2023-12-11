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

        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        Task SaveChnagesAsync();
    }
}
