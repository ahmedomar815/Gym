using Gym.DataAccess.Models;
using Gym.DataAceess.Repositories;

namespace Gym.DataAccess.Repositories;

public  interface IUniteOfWork:IAsyncDisposable
{
  public IMemberRepository Members { get; }
    public IBookingRepository Bookings { get; }
    public ISessionRepository Sessions { get; }
    public ITrainerRepository Trainers { get; }
    public ICategoryRepository Categories { get; }
    public  IPlanRepository Plans { get; }
    public IRepository<HealthyRecord> HealthyRecords {  get;  }
    Task<int> CommitAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
}
