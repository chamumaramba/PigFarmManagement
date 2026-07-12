using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class FarmRepository : IFarmRepository
    {
        private readonly PigFarmDbContext _context;

        public FarmRepository(PigFarmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Farm farm, CancellationToken cancellationToken = default)
        {
            await _context.Farms.AddAsync(farm, cancellationToken);
        }

        public async Task<IReadOnlyList<Farm>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Farms
            .AsNoTracking()
            .Include(f => f.Building)
            .Where(f => f.IsDeleted == false)
            .ToListAsync(cancellationToken);
        }

        public async Task<Farm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Farms
                .AsNoTracking()
                .Include(f => f.Building)
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<Farm?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Farms
                .AsNoTracking()
                .Include(f => f.Building)
                .FirstOrDefaultAsync(f => f.Name == name && f.IsDeleted, cancellationToken);
        }

        public async Task<IReadOnlyList<Farm>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var identityUserId = userId.ToString();

            return await _context.Farms
                .AsNoTracking()
                .Include(f => f.Building)
                .Where(f => f.IsDeleted && _context.Users.Any(u =>
                    u.Id == identityUserId && u.IsActive && u.FarmId == f.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.AnyAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.AnyAsync(f => f.Name == name, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Farm farm)
        {
            _context.Farms.Update(farm);
        }

        public async Task<int> GetFarmcountAsync(CancellationToken cancellationToken = default)
            => await _context.Farms.CountAsync(cancellationToken);
    }
}
