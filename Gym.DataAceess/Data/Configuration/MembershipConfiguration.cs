using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasOne(membership => membership.Plan)
            .WithMany(plan => plan.Memberships)
            .HasForeignKey(membership => membership.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(table => table.HasCheckConstraint(
            "CK_Membership_Dates", "[EndDate] IS NULL OR [EndDate] >= [StartDate]"));
    }
}
