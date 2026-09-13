using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Models;
using Gym.DataAceess.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gym.DataAccess.Repositories;

internal sealed class UnitOfWork(GymDbContext context) : IUnitOfWork
{
    private readonly GymDbContext _context = context;

    private IMemberRepository? _members;
    private IBookingRepository? _bookings;
    private ISessionRepository? _sessions;
    private IRepository<Trainer>? _trainers;
    private IPlanRepository? _plans;
    private IRepository<Category>? _categories;
    private IRepository<HealthyRecord> ?_healthyRecords;
    private IDbContextTransaction? _transaction;

    public IMemberRepository Members
        => _members ??= new MemberRepository(_context);

    public IBookingRepository Bookings
        => _bookings ??= new BookingRepository(_context);

    public ISessionRepository Sessions
        => _sessions ??= new SessionRepository(_context);

    public IRepository<Trainer> Trainers
        => _trainers ??= new Repository<Trainer>(_context);

    public IRepository<Category> Categories
        => _categories ??= new Repository<Category>(_context);

    public IPlanRepository Plans
        => _plans ??= new PlanRepository(_context);

    public IRepository<HealthyRecord> HealthyRecords => _healthyRecords ??= new Repository<HealthyRecord>(_context);

    

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _transaction =
            await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task<int> CommitAsync( CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync( CancellationToken cancellationToken)
    {
        if (_transaction is null)
            throw new InvalidOperationException(
                "No active transaction.");

        await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync( CancellationToken cancellationToken)
    {
        if (_transaction is null)
            throw new InvalidOperationException(
                "No active transaction.");

        await _transaction.RollbackAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction is not null)
            await _transaction.DisposeAsync();

       
    }


}
