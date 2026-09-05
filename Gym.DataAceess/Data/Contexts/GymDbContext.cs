using Gym.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace Gym.DataAccess.Data.Contexts;

public class GymDbContext(DbContextOptions<GymDbContext> options) :DbContext(options)
{
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);

        modelBuilder.Entity<User>().OwnsOne(user => user.Address);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetAuditProperties();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        SetAuditProperties();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }



    public DbSet<Plan> Plans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<HealthyRecord> HealthyRecords { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Category> Categories { get; set; }




    private void SetAuditProperties()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastUpdatedAt = now;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = now;

                    break;
            }
        }
    }

}
