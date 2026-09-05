using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class HealthyRecordConfiguration : IEntityTypeConfiguration<HealthyRecord>
{
    public void Configure(EntityTypeBuilder<HealthyRecord> builder)
    {
        builder.Property(record => record.HeightInCentimeters).HasPrecision(5, 2);
        builder.Property(record => record.WeightInKilograms).HasPrecision(5, 2);
  
  
    }
}
