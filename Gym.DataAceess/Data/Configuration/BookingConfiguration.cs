using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasOne(booking => booking.Session)
            .WithMany(session => session.Bookings)
            .HasForeignKey(booking => booking.SessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(booking => new
        {
            booking.MemberId,
            booking.SessionId
        })
         .IsUnique()
         .HasFilter("[IsDeleted] = 0");
    }
}
