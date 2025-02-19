using AquaFlow.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlow.DataAccess.Data.Configurations
{
    internal class FishFarmConfiguration : IEntityTypeConfiguration<FishFarm>
    {
        public void Configure(EntityTypeBuilder<FishFarm> builder)
        {
            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(f => f.Latitude)
                   .IsRequired()
                   .HasColumnType("decimal(12,4)");

            builder.Property(f => f.Longitude)
                   .IsRequired()
                   .HasColumnType("decimal(12,4)");

            builder.Property(f => f.NumberOfCages)
                   .IsRequired();

            builder.Property(f => f.HasBarge)
                   .IsRequired();

            builder.Property(f => f.PictureUrl)
                   .HasMaxLength(255);
        }
    }
}
