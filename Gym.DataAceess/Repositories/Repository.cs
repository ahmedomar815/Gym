using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAceess.Specificaiton;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gym.DataAccess.Repositories;

public class Repository<T>(GymDbContext context) : IRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet    
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? _dbSet.CountAsync(cancellationToken)
            : _dbSet.CountAsync(predicate, cancellationToken);
    }

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbSet  
            .FirstOrDefaultAsync(entity => entity.Id == id ,cancellationToken);
    }

    public Task<T?> GetEntityWithSpecificationAsync(
        Specification<T> specification,
        CancellationToken cancellationToken = default)
    {
        var query = SpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

   

    public Task<T?> GetDeletedByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbSet
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(entity => entity.Id == id && entity.IsDeleted, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

  
    public async Task<bool> ExistAsync(  Expression<Func<T, bool>> predicate,CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    
}
