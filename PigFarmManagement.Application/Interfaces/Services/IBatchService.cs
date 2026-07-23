using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Enums;
using static PigFarmManagement.Application.DTOs.Batch.BatchModels;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IBatchService
    {
        Task AddAnimalAsync(Guid batchCode, Guid animalId, CancellationToken cancellationToken);
        Task RemoveAnimalAsync(Guid batchCode, Guid animalId, CancellationToken cancellationToken);
        Task<BatchResponse> AddBatchAsync(CreateBatchRequest request, CancellationToken cancellationToken);
        Task<BatchResponse> UpdateAsync(Guid batchId, UpdateBatchRequest request, CancellationToken cancelToken);
        Task<BatchResponse> GetBatchByIdAsync(Guid batchId, CancellationToken cancellationToken);
        //Task<BatchResponse> GetBatchByNameAsync(string name, CancellationToken cancellationToken);
        Task CloseBatchAsync(Guid batchId, CancellationToken cancellationToken);
        Task<IReadOnlyList<BatchSummaryResponse>> GetAllAsync(Guid farmId, CancellationToken cancellationToken);
        Task<IReadOnlyList<BatchSummaryResponse>> GetByStatusAsync(BatchStatus batchStatus, CancellationToken cancellationToken);
        Task DeactivateAsync(Guid batchId, CancellationToken cancellationToken);

        Task    ActivateAsync(Guid batchId, CancellationToken cancellationToken);
        Task<BatchResponse> GetByCodeAsync(string batchCode, CancellationToken cancellationToken);

    }
}