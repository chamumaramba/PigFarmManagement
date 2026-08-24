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
    public class BreedingRecordRepository(
        PigFarmDbContext context)
        : Repository<BreedingRecord>(context),
        IBreedingRecordRepository
    {
        //private readonly PigFarmDbContext _context = context;
        // private readonly ICurrentUserServices _currentUser = currentUser;
        public async Task<IReadOnlyList<BreedingRecord>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken = default)
            => await _context.BreedingRecords
                .Where(br => (br.SowId == animalId || br.BoarId == animalId) && !br.IsDeleted)
                .ToListAsync(cancellationToken);
        public async Task<IReadOnlyList<BreedingRecord>> GetByBoarAsync(Guid boarId, CancellationToken cancellationToken = default)
            => await _context.BreedingRecords
                .Where(br => br.BoarId == boarId && !br.IsDeleted)
                .ToListAsync(cancellationToken);
        public async Task<IReadOnlyList<BreedingRecord>> GetBySowAsync(Guid sowId, CancellationToken cancellationToken = default)
            => await _context.BreedingRecords
                .Where(br => br.SowId == sowId && !br.IsDeleted)
                .ToListAsync(cancellationToken);
    }
}
