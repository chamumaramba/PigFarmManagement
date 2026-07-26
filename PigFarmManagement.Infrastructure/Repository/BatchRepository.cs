using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Enums;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class BatchRepository(
        PigFarmDbContext context,
        ICurrentUserServices currentUser)
        : Repository<Batch>(context, currentUser),
        IBatchRepository
    {
        /// <summary>
        /// Returns the count of batches for the current farm.
        /// FarmId scoping is handled automatically by the DbContext global query filter.
        /// </summary>
        public async Task<int> GetBatchCountAsync(CancellationToken cancellationToken)
        {
            return await _context.Batches.CountAsync(cancellationToken);
        }

        public async Task<Batch?> GetByBatchCodeAsync(string batchCode, CancellationToken cancellationToken)
        {
            return await _context.Batches.FirstOrDefaultAsync(b => b.BatchCode == batchCode, cancellationToken);
        }

        public async Task<IReadOnlyList<Batch>> GetByStatusAsync(BatchStatus status, CancellationToken cancellationToken)
        {
            return await _context.Batches
                .Where(b => b.Status == status)
                .ToListAsync(cancellationToken);
        }
    }
}