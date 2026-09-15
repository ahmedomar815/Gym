using System.Linq.Expressions;

namespace Gym.DataAceess.Specificaiton;

public abstract class Specification<TEntity>
    where TEntity : class
{
    public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }

    public List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> Includes { get; } = [];

    protected void AddInclude(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}
