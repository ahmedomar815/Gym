using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasQueryFilter(session => !session.Trainer.IsDeleted);

        builder.Property(session => session.Description).HasMaxLength(100);

        builder.HasOne(session => session.Trainer)
            .WithMany(trainer => trainer.Sessions)
            .HasForeignKey(session => session.TrainerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(session => session.Category)
            .WithMany(category => category.Sessions)
            .HasForeignKey(session => session.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(table => table.HasCheckConstraint(
            "CK_Session_Capacity", "[Capacity] BETWEEN 1 AND 25"));

        builder.ToTable(table => table.HasCheckConstraint(
            "CK_Session_Times", "[EndTime] > [StartTime]"));
    }
}
