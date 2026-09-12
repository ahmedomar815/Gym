using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.UseTphMappingStrategy();

        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.HasDiscriminator<string>("Type")
            .HasValue<Member>("Member")
            .HasValue<Trainer>("Trainer");

        builder.Property<string>("Type").HasMaxLength(20);

        builder.Property(user => user.Name).HasMaxLength(100).IsRequired();
        builder.Property(user => user.Email).HasMaxLength(100).IsRequired();
        builder.Property(user => user.PhoneNumber).HasMaxLength(20).IsRequired();
        builder.HasIndex(user => user.Email).IsUnique();

        builder.OwnsOne(user => user.Address, address =>
        {
            address.Property(value => value.City).HasMaxLength(100).IsRequired();
            address.Property(value => value.Street).HasMaxLength(200).IsRequired();
        });
    }
}
