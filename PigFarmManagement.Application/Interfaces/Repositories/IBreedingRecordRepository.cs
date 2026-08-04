using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IBreedingRecordRepository : IRepository<BreedingRecord>
    {
        Task<IReadOnlyList<BreedingRecord>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<BreedingRecord>> GetBySowAsync(Guid sowId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<BreedingRecord>> GetByBoarAsync(Guid boarId, CancellationToken cancellationToken = default);
    }
}
