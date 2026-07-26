using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IBatchRepository: IRepository<Batch>
    {
        Task<Batch?> GetByBatchCodeAsync(string batchCode, CancellationToken cancellationToken);
        Task<int> GetBatchCountAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Batch>> GetByStatusAsync(BatchStatus batchStatus, CancellationToken cancellationToken);
        //Task AddAnimalToBatchAsync(Guid batchCode, Guid animalId, CancellationToken cancellationToken);
    }
}