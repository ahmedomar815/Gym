using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Cofiguration;

public class PlayConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
       builder.Property(p => p.Name)
            .HasMaxLength(50);

        builder.Property(p => p.Description)
           .HasMaxLength(200);

        builder.Property(p=>p.Price)
            .HasPrecision(10, 2);

        builder.ToTable(tb=>tb.HasCheckConstraint("CK_Plan_DurationInDays", "[DurationInDays] Between 1 AND 365"));

        builder.HasIndex(d => d.Name).IsUnique();
    }
}
