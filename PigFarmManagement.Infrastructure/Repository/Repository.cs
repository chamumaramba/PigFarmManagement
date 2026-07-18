using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class Repository<T>(PigFarmDbContext context) : IRepository<T>
        where T : BaseEntity
    {
        protected readonly PigFarmDbContext _context = context;


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
            return await _context.Set<T>()
                .AnyAsync(
                    x => x.Id == id && !x.IsDeleted,
                    cancellationToken);
        }


        public virtual async Task<IReadOnlyList<T>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                //.Where(x => !x.IsDeleted)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }


        public virtual async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(
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
            _context.Set<T>().Update(entity);
        }


        public virtual void Remove(T entity)
        {
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
        }
    }
}