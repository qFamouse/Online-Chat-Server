using System.Net;
using Application.Interfaces.Entities;
using Application.Interfaces.Repositories;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntity
    {
        protected OnlineChatContext DbContext { get; }
        private readonly DbSet<T> _dbSet;

        public BaseRepository(OnlineChatContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>() ?? throw new ArgumentNullException(nameof(T));
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.Id == id, cancellationToken) 
                   ?? throw new ProblemDetailsException((int)HttpStatusCode.NotFound);
        }

        public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id, cancellationToken) 
                         ?? throw new ProblemDetailsException((int) HttpStatusCode.NotFound);

            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save(CancellationToken cancellationToken = default)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken) != null;
        }
    }
}
