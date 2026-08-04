using PigFarmManagement.Domain.Enums;
using static PigFarmManagement.Application.DTOs.AnimalModels;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IAnimalService
    {
        // CRUD
        Task<AnimalResponse> AddAsync(CreateAnimalRequest request, CancellationToken cancellationToken);

        Task<AnimalResponse> UpdateAsync(Guid id, UpdateAnimalRequest request, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<AnimalResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<AnimalResponse?> GetByTagNumberAsync(string tagNumber, CancellationToken cancellationToken);

        Task<IReadOnlyList<AnimalSummary>> GetAllAsync(CancellationToken cancellationToken);

        // Status & Stage Management
        Task UpdateStatusAsync(Guid id, AnimalStatus status, CancellationToken cancellationToken);

        Task UpdateProductionStageAsync(Guid id, ProductionStage stage, CancellationToken cancellationToken);

        // Filtering
        Task<IReadOnlyList<AnimalSummary>> GetByStatusAsync(AnimalStatus status, CancellationToken cancellationToken);

        Task<IReadOnlyList<AnimalSummary>> GetByProductionStageAsync(ProductionStage stage, CancellationToken cancellationToken);

        Task<IReadOnlyList<AnimalSummary>> GetByGenderAsync(Gender gender, CancellationToken cancellationToken);

        Task<IReadOnlyList<AnimalSummary>> GetByBatchAsync(Guid batchId, CancellationToken cancellationToken);
    }
}