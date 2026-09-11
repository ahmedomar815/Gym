using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gym.DataAccess.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<T?> GetDeletedByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<bool>ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?>GetByIdIncludingAsync(int id, CancellationToken cancellationToken = default,params Expression<Func<T, Object>>[]includes);
    void Update(T entity);

    void Delete(T entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
