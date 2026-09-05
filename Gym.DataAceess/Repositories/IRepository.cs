namespace Gym.DataAccess.Repositories;

public interface IRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    void Update(T entity);

    void Delete(T entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
