using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Enums;
using PigFarmManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class HealthRecordRepository(
        PigFarmDbContext context)
        : Repository<HealthRecord>(context),
        IHealthRecordRepository
    {
        public async Task<IReadOnlyList<HealthRecord>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken = default)
            => await _context.HealthRecords
                .Where(hr => hr.AnimalId == animalId && !hr.IsDeleted)
                .ToListAsync(cancellationToken);

        public async Task<IReadOnlyList<HealthRecord>> GetByBatchAsync(Guid batchId, CancellationToken cancellationToken = default)
            => await _context.HealthRecords
                .Where(hr => hr.BatchId == batchId && !hr.IsDeleted)
                .ToListAsync(cancellationToken);

        public async Task<IReadOnlyList<HealthRecord>> GetByTypeAsync(Guid animalId, HealthRecordType type, CancellationToken cancellationToken = default)
            => await _context.HealthRecords
                .Where(hr => hr.AnimalId == animalId && hr.HealthRecordType == type && !hr.IsDeleted)
                .ToListAsync(cancellationToken);
    }
}