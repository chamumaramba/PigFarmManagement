using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IHealthRecordRepository : IRepository<HealthRecord>
    {
        Task<IReadOnlyList<HealthRecord>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<HealthRecord>> GetByTypeAsync(Guid animalId, HealthRecordType type, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<HealthRecord>> GetByBatchAsync(Guid batchId, CancellationToken cancellationToken = default);
    }
}
