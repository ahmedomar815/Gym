using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.Property(plan => plan.Name).HasMaxLength(50);
        builder.Property(plan => plan.Description).HasMaxLength(200);
        builder.Property(plan => plan.Price).HasPrecision(10, 2);
        builder.HasIndex(plan => plan.Name).IsUnique();
        builder.ToTable(table => table.HasCheckConstraint(
            "CK_Plan_DurationInDays", "[DurationInDays] BETWEEN 1 AND 365"));
    }
}
