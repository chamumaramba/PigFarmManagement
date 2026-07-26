using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class Repository<T>(PigFarmDbContext context, ICurrentUserServices currentUserServices) : IRepository<T>
        where T : BaseEntity
    {
        protected readonly PigFarmDbContext _context = context;
        protected readonly ICurrentUserServices _currentUserServices = currentUserServices;

        public virtual async Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await _context.Set<T>()
                .AddAsync(entity, cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();
            if (typeof(FarmEntity).IsAssignableFrom(typeof(T)))
            {
                var farmId = GetFarmId();
                query = query.Where(x => EF.Property<Guid>(x, "FarmId") == farmId);
            }

            return await query.AnyAsync(
                x => x.Id == id && !x.IsDeleted,
                cancellationToken);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();
            if (typeof(FarmEntity).IsAssignableFrom(typeof(T)))
            {
                var farmId = GetFarmId();
                query = query.Where(x => EF.Property<Guid>(x, "FarmId") == farmId);
            }

            return await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();
            if (typeof(FarmEntity).IsAssignableFrom(typeof(T)))
            {
                var farmId = GetFarmId();
                query = query.Where(x => EF.Property<Guid>(x, "FarmId") == farmId);
            }

            return await query.FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
        }

        public virtual async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void Update(
            T entity,
            CancellationToken cancellationToken = default)
        {
            if (typeof(FarmEntity).IsAssignableFrom(typeof(T)))
            {
                var farmEntity = (FarmEntity)(object)entity;
                if (farmEntity.FarmId != GetFarmId())
                    throw new UnauthorizedAccessException("Entity does not belong to current farm.");
            }
            _context.Set<T>().Update(entity);
        }

        public virtual void Remove(T entity)
        {
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
        }

        private Guid GetFarmId()
        {
            return _currentUserServices.FarmId;
        }
    }
}