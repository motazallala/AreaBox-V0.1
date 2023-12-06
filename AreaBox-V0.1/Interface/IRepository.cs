namespace AreaBox_V0._1.Interface
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(Guid id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task SaveChnageAsync();
    }
}
