using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Infrastructure.Identity;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Common;
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
        public DbSet<HealthRecord> HealthRecords { get; set; }

        /// <summary>
        /// Returns the current user's FarmId, or Guid.Empty when no HTTP context
        /// is present (e.g. migrations, role seeding, background jobs).
        /// Queries executed with Guid.Empty will return no farm-owned records,
        /// which is the correct behaviour for unauthenticated contexts.
        /// </summary>
        public Guid CurrentFarmId
        {
            get
            {
                try
                {
                    return _currentUserServices.FarmId;
                }
                catch (UnauthorizedAccessException)
                {
                    // No authenticated HTTP context (migration, seeding, background job).
                    return Guid.Empty;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Animal relationship configuration ────────────────────────────
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

            // ── Automatic query filters for all FarmEntity subclasses ────────
            // Applies:  WHERE FarmId = @CurrentFarmId AND IsDeleted = 0
            // Any new entity that inherits FarmEntity is automatically covered.
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(FarmEntity).IsAssignableFrom(entityType.ClrType))
                    continue;

                var clrType = entityType.ClrType;

                // x
                var parameter = Expression.Parameter(clrType, "x");

                // x.FarmId
                var farmIdProperty = Expression.Property(parameter, nameof(FarmEntity.FarmId));

                // this.CurrentFarmId  (resolved at query time, not model-building time)
                var currentFarmId = Expression.Property(
                    Expression.Constant(this),
                    nameof(CurrentFarmId));

                // x.FarmId == this.CurrentFarmId
                var farmIdFilter = Expression.Equal(farmIdProperty, currentFarmId);

                // x.IsDeleted
                var isDeletedProperty = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));

                // !x.IsDeleted
                var notDeleted = Expression.Not(isDeletedProperty);

                // x.FarmId == this.CurrentFarmId && !x.IsDeleted
                var combined = Expression.AndAlso(farmIdFilter, notDeleted);

                // x => x.FarmId == this.CurrentFarmId && !x.IsDeleted
                var lambda = Expression.Lambda(combined, parameter);

                modelBuilder.Entity(clrType).HasQueryFilter(lambda);
            }

            // ── FarmId indexes for high-volume entities ──────────────────────
            // Every query is scoped by FarmId; these indexes make that fast.
            modelBuilder.Entity<Animal>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<Batch>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<Building>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<Pen>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<WeightRecord>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<FeedAllocation>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<VaccinationSchedule>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<Treatment>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<BreedingRecord>().HasIndex(x => x.FarmId);
            modelBuilder.Entity<AnimalMovement>().HasIndex(x => x.FarmId);
        }
    }
}