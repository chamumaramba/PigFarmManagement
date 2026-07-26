using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class FarmRepository(PigFarmDbContext context, ICurrentUserServices currentUserServices)
        : Repository<Farm>(context, currentUserServices), IFarmRepository
    {
        public async Task<Farm?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Farms
                .AsNoTracking()
                .Include(f => f.Buildings)
                .FirstOrDefaultAsync(f => f.Name == name && !f.IsDeleted, cancellationToken);
        }

        public async Task<IReadOnlyList<Farm>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var identityUserId = userId.ToString();

            return await _context.Farms
                .AsNoTracking()
                .Include(f => f.Buildings)
                .Where(f => !f.IsDeleted && _context.Users.Any(u =>
                    u.Id == identityUserId && u.IsActive && u.FarmId == f.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.AnyAsync(f => f.Name == name && !f.IsDeleted, cancellationToken);
        }

        public async Task<int> GetFarmcountAsync(CancellationToken cancellationToken = default)
            => await _context.Farms.CountAsync(cancellationToken);
    }
}
