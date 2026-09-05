using Gym.DataAccess.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Repositories;

public class Repository<T>(GymDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _entities = context.Set<T>();

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _entities.AsNoTracking()
            .FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id, cancellationToken);
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
