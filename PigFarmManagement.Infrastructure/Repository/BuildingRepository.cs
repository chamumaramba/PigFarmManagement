
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Entities;


using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class BuildingRepository(PigFarmDbContext context
    ) : Repository<Building>(context), IBuildingRepository
    {
        public async Task<bool> BuildingNameExistsAsync(Guid farmId, string name, CancellationToken cancellationToken)
            => await _context.Buildings
                .AnyAsync(n => n.Name == name && n.FarmId == farmId, cancellationToken);


        public async Task<IEnumerable<Building>> GetAllBuildingByFarmIdAsync(Guid farmId, CancellationToken cancellationToken)
            => await _context.Buildings
                .Where(b => b.FarmId == farmId)
                .ToListAsync(cancellationToken);

        public async Task<Building?> GetBuildingByName(Guid farmId, string name, CancellationToken cancellationToken)
         => await _context.Buildings
         .FirstOrDefaultAsync(b => b.Name == name && b.FarmId == farmId, cancellationToken);
    }
}