
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Entities;


using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class BuildingRepository(PigFarmDbContext context) : IBuildingRepository
    {
        private readonly PigFarmDbContext _context = context;

        public async Task AddBuildingAsync(Building building, CancellationToken cancellationToken)
            => await _context.Buildings.AddAsync(building, cancellationToken);

        public async Task<bool> BuildingExistsAsync(Guid id, CancellationToken cancellationToken)
            => await _context.Buildings.AnyAsync(b => b.Id == id, cancellationToken);

        public async Task<bool> BuildingNameExistsAsync(Guid farmId, string name, CancellationToken cancellationToken)
            => await _context.Buildings
                .AnyAsync(n => n.Name == name && n.FarmId == farmId, cancellationToken);



        public async Task<IEnumerable<Building>> GetAllBuildingByFarmIdAsync(Guid farmId, CancellationToken cancellationToken)
            => await _context.Buildings
                .Where(b => b.FarmId == farmId)
                .ToListAsync(cancellationToken);

        public async Task<Building?> GetBuildingAsync(Guid id, CancellationToken cancellationToken)
            => await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);

        public void UpdateBuilding(Building building)
            => _context.Buildings.Update(building);

    }
}