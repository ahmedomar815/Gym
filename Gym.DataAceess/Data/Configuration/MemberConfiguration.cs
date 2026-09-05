using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasOne(member => member.HealthyRecord)
            .WithOne(record => record.Member)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(member => member.Memberships)
            .WithOne(membership => membership.Member)
            .HasForeignKey(membership => membership.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(member => member.Bookings)
            .WithOne(booking => booking.Member)
            .HasForeignKey(booking => booking.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
