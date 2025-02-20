using AquaFlow.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaFlow.DataAccess.Data.Configurations
{
    internal class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.Property(w => w.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(w => w.PictureUrl)
                   .HasMaxLength(255);

            builder.Property(w => w.Age)
                   .IsRequired();

            builder.Property(w => w.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasIndex(w => w.Email)
                   .IsUnique();

            builder.Property(w => w.CertifiedUntil)
                   .IsRequired();

            builder.HasOne(w => w.FishFarm)
                   .WithMany(f => f.Workers)
                   .HasForeignKey(w => w.FishFarmId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.Position)
                   .WithMany(p => p.Workers)
                   .HasForeignKey(w => w.PositionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
