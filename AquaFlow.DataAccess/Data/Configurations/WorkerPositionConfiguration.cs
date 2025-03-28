﻿using AquaFlow.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaFlow.DataAccess.Data.Configurations
{
    internal class WorkerPositionConfiguration : IEntityTypeConfiguration<WorkerPosition>
    {
        public void Configure(EntityTypeBuilder<WorkerPosition> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
                new WorkerPosition { Id = 1, Name = "CEO" },
                new WorkerPosition { Id = 2, Name = "Worker" },
                new WorkerPosition { Id = 3, Name = "Captain" }
            );
        }
    }
}
