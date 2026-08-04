using PigFarmManagement.Application.Helpers;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Application.Mappings;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Enums;
using static PigFarmManagement.Application.DTOs.Batch.BatchModels;

namespace PigFarmManagement.Application.Services
{
    public class BatchService(
        IBatchRepository batchRepo,
        IAnimalRepository animalRepo,
        IFarmRepository farmRepository,
        ICurrentUserServices currentUser)
        : IBatchService
    {
        private readonly IBatchRepository _batchRepo = batchRepo;
        private readonly IAnimalRepository _animalRepo = animalRepo;
        private readonly IFarmRepository _farmRepository = farmRepository;
        private readonly ICurrentUserServices _currentUser = currentUser;

        public async Task ActivateAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var batch = await _batchRepo.GetByIdAsync(batchId, cancellationToken);
            if (batch == null)
                throw new KeyNotFoundException("Batch does not exist.");

            batch.IsDeleted = false;
            batch.Status = BatchStatus.Active;
            batch.UpdatedAt = DateTime.UtcNow;
            _batchRepo.Update(batch, cancellationToken);
            await _batchRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAnimalAsync(string batchCode, Guid animalId, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByIdAsync(animalId, cancellationToken);
            if (animal == null || animal.IsDeleted)
            {
                throw new KeyNotFoundException("Animal not found");
            }

            var batch = await _batchRepo.GetByBatchCodeAsync(batchCode, cancellationToken);
            if (batch == null || batch.IsDeleted)
            {
                throw new KeyNotFoundException("No active batch found.");
            }

            animal.BatchId = batch.Id;

            _animalRepo.Update(animal, cancellationToken);
            await _animalRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task<BatchResponse> AddBatchAsync(CreateBatchRequest request, CancellationToken cancellationToken)
        {
            var farmId = _currentUser.FarmId;
            var farm = await _farmRepository.GetByIdAsync(farmId, cancellationToken)
                ?? throw new InvalidOperationException("Farm not found.");
            var count = await _batchRepo.GetBatchCountAsync(cancellationToken);
            var batchCode = BatchCodeGenerator.Generate(farm.FarmCode, request.StartDate, count + 1);

            var batch = new Batch
            {
                BatchCode = batchCode,
                Status = request.Status,
                StartDate = request.StartDate,
                EndDate = request.EndDate,

            };

            await _batchRepo.AddAsync(batch);
            await _batchRepo.SaveChangesAsync(cancellationToken);
            return BatchMapper.ToResponse(batch);
        }

        public async Task CloseBatchAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var batch = await _batchRepo.GetByIdAsync(batchId, cancellationToken);
            if (batch == null)
               throw new KeyNotFoundException("Batch not found.");

            batch.Status = BatchStatus.Archived;
            batch.IsDeleted = true;
            _batchRepo.Update(batch);
            await _batchRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task DeactivateAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var batch = await _batchRepo.GetByIdAsync(batchId, cancellationToken);
            if (batch == null)
               throw new KeyNotFoundException("Batch not found.");

            batch.Status = BatchStatus.Active;
            batch.IsDeleted = true;
            _batchRepo.Update(batch);
            await _batchRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<BatchSummaryResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var batches = await _batchRepo.GetAllAsync(cancellationToken);

            return BatchMapper.ToSummaryResponseList(batches);
        }

        public async Task<BatchResponse?> GetBatchByIdAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var batch = await _batchRepo.GetByIdAsync(batchId, cancellationToken);
            if (batch is null)
            {
                throw new KeyNotFoundException(
                    $"Batch with code '{batchId}' was not found."
                );
            }
            return BatchMapper.ToResponse(batch);
        }

        public async Task<BatchResponse> GetByCodeAsync(string batchCode, CancellationToken cancellationToken)
        {
            var batch = await _batchRepo.GetByBatchCodeAsync(batchCode, cancellationToken);
            if (batch is null)
            {
                throw new KeyNotFoundException(
                    $"Batch with code '{batchCode}' was not found."
                );
            }
            return BatchMapper.ToResponse(batch);

        }

        public async Task<IReadOnlyList<BatchSummaryResponse>> GetByStatusAsync(BatchStatus batchStatus, CancellationToken cancellationToken)
        {
            var batches = await _batchRepo.GetByStatusAsync(batchStatus, cancellationToken);
            return BatchMapper.ToSummaryResponseList(batches);
        }

        public async Task RemoveAnimalAsync(string batchCode, Guid animalId, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByIdAsync(animalId, cancellationToken);
            if (animal == null || animal.IsDeleted)
            {
                throw new KeyNotFoundException("Animal not found");
            }

            var batch = await _batchRepo.GetByBatchCodeAsync(batchCode, cancellationToken);
            if (batch == null || batch.IsDeleted)
            {
                throw new KeyNotFoundException("No active batch found.");
            }

            if (animal.BatchId != batch.Id)
            {
                throw new InvalidOperationException($"Animal with ID {animalId} is not assigned to batch {batchCode}.");
            }

            animal.BatchId = null;

            _animalRepo.Update(animal, cancellationToken);
            await _animalRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task<BatchResponse> UpdateAsync(
            Guid batchId,
            UpdateBatchRequest request,
            CancellationToken cancellationToken
        )
        {
            var batch = await _batchRepo.GetByIdAsync(batchId, cancellationToken);

            if (batch is null)
            {
                throw new KeyNotFoundException("Batch not found.");
            }

            batch.EndDate = request.EndDate;
            batch.StartDate = request.StartDate;
            batch.Status = request.Status;
            batch.UpdatedAt = DateTime.UtcNow;

            _batchRepo.Update(batch, cancellationToken);

            await _batchRepo.SaveChangesAsync(cancellationToken);

            return BatchMapper.ToResponse(batch);
        }
    }
}