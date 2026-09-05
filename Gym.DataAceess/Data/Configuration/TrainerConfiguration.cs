using Gym.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.DataAccess.Data.Configuration;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {


        builder.HasOne(trainer => trainer.Category)
            .WithMany(category => category.Trainers)
            .HasForeignKey(trainer => trainer.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
