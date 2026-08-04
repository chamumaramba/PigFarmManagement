using PigFarmManagement.Domain.Enums;
using static PigFarmManagement.Application.DTOs.HealthRecord.HealthRecordModels;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IHealthRecordService
    {
        Task<HealthRecordResponse> AddAsync(CreateHealthRecordRequest request, CancellationToken cancellationToken);

        Task<HealthRecordResponse> UpdateAsync(Guid id, UpdateHealthRecordRequest request, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<HealthRecordResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<HealthRecordSummary>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken);

        Task<IReadOnlyList<HealthRecordSummary>> GetByTypeAsync(Guid animalId, HealthRecordType type, CancellationToken cancellationToken);

        Task<IReadOnlyList<HealthRecordSummary>> GetByBatchAsync(Guid batchId, CancellationToken cancellationToken);
    }
}
