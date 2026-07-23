using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IBatchRepository: IRepository<Batch>
    {
        Task<Batch> GetByBatchNumber(string batchNumber, CancellationToken cancellationToken);
        //Task AddAnimalToBatchAsync(Guid batchCode, Guid animalId, CancellationToken cancellationToken);
    }
}