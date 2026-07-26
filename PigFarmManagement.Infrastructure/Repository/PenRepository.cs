using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class PenRepository(PigFarmDbContext context, ICurrentUserServices currentUserServices)
        : Repository<Pen>(context, currentUserServices), IPenRepository
    {
        public async Task<bool> ExistsInBuildingAsync(Guid buildingId, string name, CancellationToken cancellationToken)
            => await _context.Pens
            .AnyAsync(p => p.BuildingId == buildingId && p.Name == name, cancellationToken);


        public async Task<IEnumerable<Pen>> GetByBuildingAsync(Guid buildingId, CancellationToken cancellationToken)
            => await _context.Pens
            .AsNoTracking()
            .Where(p => p.BuildingId == buildingId && !p.IsDeleted)
            .ToListAsync(cancellationToken);

        public async Task<Pen?> GetByNameAsync(string name, CancellationToken cancellationToken)
            => await _context.Pens
            .AsNoTracking()
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

        public async Task<int> GetCountAsync(Guid buildingId, CancellationToken cancellationToken)
            => await _context.Pens
            .CountAsync(p => p.BuildingId == buildingId && !p.IsDeleted, cancellationToken);


        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
            => await _context.Pens
            .AnyAsync(p => p.Name == name && !p.IsDeleted, cancellationToken);
    }
}