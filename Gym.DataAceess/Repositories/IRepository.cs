using Gym.DataAccess.Models;
using Gym.DataAceess.Specificaiton;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gym.DataAccess.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<T?> GetEntityWithSpecificationAsync(
        Specification<T> specification,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(
        Specification<T> specification,
        CancellationToken cancellationToken = default);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<T?> GetDeletedByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<bool>ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
   
    void Update(T entity);

    void Delete(T entity);


}
