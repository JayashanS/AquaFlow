using AquaFlow.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;

namespace AquaFlow.DataAccess.Data.Configurations
{
    internal class FishFarmConfiguration : IEntityTypeConfiguration<FishFarm>
    {
        public void Configure(EntityTypeBuilder<FishFarm> builder)
        {
            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(f => f.Location)
                    .IsRequired()
                    .HasColumnType("geography")
                    .HasPrecision(4);

            builder.Property(f => f.NumberOfCages)
                   .IsRequired();

            builder.Property(f => f.HasBarge)
                   .IsRequired();

            builder.Property(f => f.PictureUrl)
                   .HasMaxLength(255);

            builder.HasAnnotation("SqlServer:Include", new[] { "Location" });

        }
    }
}
