namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    void Update(T entity);
    Task Save(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}