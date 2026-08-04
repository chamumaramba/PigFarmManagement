using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IAnimalMovementRepository : IRepository<AnimalMovement>
    {
        Task<IReadOnlyList<AnimalMovement>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken = default);
    }
}
