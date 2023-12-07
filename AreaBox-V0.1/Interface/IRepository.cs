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


        Task<T> GetByIdAsync(Guid id);

        T Find(Expression<Func<T, bool>> match, String[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, String[] includes = null);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        Task SaveChnageAsync();
    }
}
