using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs.Batch;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Enums;
using static PigFarmManagement.Application.DTOs.Batch.BatchModels;

namespace PigFarmManagement.Application.Services
{
    public class BatchService : IBatchService
    {
        public Task ActivateAsync(Guid batchId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddAnimalAsync(Guid batchCode, Guid animalId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BatchResponse> AddBatchAsync(CreateBatchRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CloseBatchAsync(Guid batchId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeactivateAsync(Guid batchId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<BatchSummaryResponse>> GetAllAsync(Guid farmId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BatchResponse> GetBatchByIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BatchResponse> GetByCodeAsync(string batchCode, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<BatchSummaryResponse>> GetByStatusAsync(BatchStatus batchStatus, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAnimalAsync(Guid batchCode, Guid animalId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BatchResponse> UpdateAsync(Guid batchId, UpdateBatchRequest request, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }
    }
}