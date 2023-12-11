﻿using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Repositories
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

        public async Task<IEnumerable<TViewModel>> GetAllAsync<TEntity, TViewModel>()
            where TEntity : class
            where TViewModel : class
        {
            var entities = await _db.Set<TEntity>().ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<TViewModel>>(entities);
            return viewModels;
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

		public void Detach<TEntity>(TEntity entity) 
            where TEntity : class
		{
			var entry = _db.Entry(entity);
			if (entry.State != EntityState.Detached)
			{
				entry.State = EntityState.Detached;
			}
		}

		public async Task SaveChnagesAsync()
		{
			await _db.SaveChangesAsync();
		}


		public TViewModel Find<TEntity, TViewModel>(Expression<Func<TEntity, bool>> match, String[] includes = null)
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
			var entities = query.SingleOrDefault(match);
			var viewModels = _mapper.Map<TViewModel>(entities);
			return viewModels;
		}

		public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _db.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return _db.Set<T>().Where(match).ToList();
        }
	}
}
