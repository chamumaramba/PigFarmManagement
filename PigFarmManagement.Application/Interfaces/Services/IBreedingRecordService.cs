using static PigFarmManagement.Application.DTOs.BreedingRecord.BreedingRecordModels;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IBreedingRecordService
    {
        Task<BreedingRecordResponse> AddAsync(CreateBreedingRecordRequest request, CancellationToken cancellationToken);

        Task<BreedingRecordResponse> RecordPregnancyCheckAsync(Guid id, RecordPregnancyCheckRequest request, CancellationToken cancellationToken);

        Task<BreedingRecordResponse> RecordFarrowingAsync(Guid id, RecordFarrowingRequest request, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<BreedingRecordResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<BreedingRecordSummary>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken);

        Task<IReadOnlyList<BreedingRecordSummary>> GetBySowAsync(Guid sowId, CancellationToken cancellationToken);

        Task<IReadOnlyList<BreedingRecordSummary>> GetByBoarAsync(Guid boarId, CancellationToken cancellationToken);
    }
}
