using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Repositories
{
    public class QuestionPostRepository : IRepository<QuestionPost>
    {
        private readonly Repository<QuestionPost> _repository;

        public QuestionPostRepository(Repository<QuestionPost> repository)
        {
            _repository = repository;
        }

        public async Task<List<QuestionPost>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<QuestionPost?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Add(QuestionPost entity)
        {
            _repository.Add(entity);
        }

        public void Remove(QuestionPost entity)
        {
            _repository.Remove(entity);
        }

        public void Update(QuestionPost entity)
        {
            _repository.Update(entity);
        }

        public Task SaveChangesAsync()
        {
            return _repository.SaveChangesAsync();
        }
    }
}
