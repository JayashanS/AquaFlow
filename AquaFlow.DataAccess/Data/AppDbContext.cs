﻿using AquaFlow.DataAccess.Data.Configurations;
using AquaFlow.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlow.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<FishFarm> FishFarms { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerPosition> WorkersPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new FishFarmConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerPositionConfiguration());
        }
    }
}