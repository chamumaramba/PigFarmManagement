
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;


using PigFarmManagement.Infrastructure.Data;
using PigFarmManagement.Infrastructure.Identity;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class BuildingRepository(
        PigFarmDbContext context)
     : Repository<Building>(context),
     IBuildingRepository
    {
        public async Task<bool> BuildingCodeExistsAsync(string code, CancellationToken cancellationToken)
            => await _context.Buildings
                .AnyAsync(n => n.BuildingCode == code, cancellationToken);


        public async Task<IEnumerable<Building>> GetAllBuildingByFarmIdAsync(CancellationToken cancellationToken)
            => await _context.Buildings
                .ToListAsync(cancellationToken);

        public async Task<Building?> GetBuildingByName(string name, CancellationToken cancellationToken)
         => await _context.Buildings
         .FirstOrDefaultAsync(b => b.Name == name , cancellationToken);
    }
}