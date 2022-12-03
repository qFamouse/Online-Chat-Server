namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> InsertAsync(T entity);
        Task DeleteByIdAsync(int id);
        void Update(T entity);
        Task Save();
    }
}
