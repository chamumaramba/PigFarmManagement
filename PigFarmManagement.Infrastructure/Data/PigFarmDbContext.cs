using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Infrastructure.Identity;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;
using PigFarmManagement.Application.Interfaces.Services;

namespace PigFarmManagement.Infrastructure.Data
{
    public class PigFarmDbContext(
        DbContextOptions<PigFarmDbContext> options,
        ICurrentUserServices currentUserServices)
        : IdentityDbContext<ApplicationUser,
        ApplicationRole,
        string>(options)
    {
        private readonly ICurrentUserServices _currentUserServices = currentUserServices;
        public DbSet<FeedType> FeedTypes { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Pen> Pens { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<FeedAllocation> FeedAllocations { get; set; }
        public DbSet<FeedProgram> FeedPrograms { get; set; }
        public DbSet<VaccinationSchedule> VaccinationSchedules { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<WeightRecord> WeightRecords { get; set; }
        public DbSet<BreedingRecord> BreedingRecords { get; set; }
        public DbSet<AnimalMovement> AnimalMovements { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public Guid CurrentFarmId => _currentUserServices.FarmId;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasOne(a => a.Sow)
                    .WithMany()
                    .HasForeignKey(a => a.SowId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Boar)
                    .WithMany()
                    .HasForeignKey(a => a.BoarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Batch)
                    .WithMany(b => b.Animals)
                    .HasForeignKey(a => a.BatchId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(a => a.BreedingRecords)
                    .WithOne(b => b.Animal)
                    .HasForeignKey(b => b.AnimalId);
            });

            modelBuilder.Entity<Building>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<Pen>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<Animal>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<HealthRecord>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<BreedingRecord>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<FeedAllocation>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<FeedProgram>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<Treatment>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<VaccinationSchedule>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<WeightRecord>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<AnimalMovement>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);

            modelBuilder.Entity<Batch>()
                .HasQueryFilter(x => x.FarmId == CurrentFarmId);
        }
    }
}