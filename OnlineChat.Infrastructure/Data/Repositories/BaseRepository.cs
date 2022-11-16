using Microsoft.EntityFrameworkCore;
using OnlineChat.Core.Entities;
using OnlineChat.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntity
    {
        protected OnlineChatContext DbContext { get; }
        private readonly DbSet<T> _dbSet;

        public BaseRepository(OnlineChatContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context), "DB Context is null");
            _dbSet = context.Set<T>() ?? throw new ArgumentNullException(nameof(T), "DbSet is null");
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
