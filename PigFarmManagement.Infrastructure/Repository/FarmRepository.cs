using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class FarmRepository(PigFarmDbContext context) :Repository<Farm>(context), IFarmRepository
    {
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

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.AnyAsync(f => f.Name == name, cancellationToken);
        }

        public async Task<int> GetFarmcountAsync(CancellationToken cancellationToken = default)
            => await _context.Farms.CountAsync(cancellationToken);
    }
}
