namespace Gym.DataAceess.Specificaiton;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> query,
        Specification<TEntity> specification)
        where TEntity : class
    {
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        foreach (var include in specification.Includes)
        {
            query = include(query);
        }

        return query;
    }
}
