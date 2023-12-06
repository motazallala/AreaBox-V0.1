using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace AreaBox_V0._1.Repositories
{
    public class MediaPostRepository : IRepository<MediaPost>
    {
        private readonly Repository<MediaPost> _repository;

        public MediaPostRepository(Repository<MediaPost> repository)
        {
            _repository = repository;
        }

        public async Task<List<MediaPost>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MediaPost?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Add(MediaPost entity)
        {
            _repository.Add(entity);
        }

        public void Remove(MediaPost entity)
        {
            _repository.Remove(entity);
        }

        public void Update(MediaPost entity)
        {
            _repository.Update(entity);
        }

        public Task SaveChangesAsync()
        {
            return _repository.SaveChangesAsync();
        }
    }
}
