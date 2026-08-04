using static PigFarmManagement.Application.DTOs.AnimalMovement.AnimalMovementModels;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IAnimalMovementService
    {
        Task<AnimalMovementResponse> MoveAsync(MoveAnimalRequest request, CancellationToken cancellationToken);

        Task<AnimalMovementResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<AnimalMovementSummary>> GetByAnimalAsync(Guid animalId, CancellationToken cancellationToken);
    }
}
