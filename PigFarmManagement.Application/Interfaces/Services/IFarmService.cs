
using static PigFarmManagement.Application.DTOs.FarmModels;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IFarmService
    {
        Task<FarmResponse> AddAsync(CreateFarmRequest request, CancellationToken cancellationToken);
        Task<FarmResponse> Update(Guid id, UpdateFarmRequest request, CancellationToken cancellationToken);
        Task<FarmResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<FarmResponse> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<FarmResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<FarmResponse>> GetUserFarmAsync(Guid userId, CancellationToken cancellationToken);
        Task DeactivateFarmAsync(Guid id, CancellationToken cancellationToken);
        Task<FarmResponse> ActivateFarmAsync(Guid id, CancellationToken cancellationToken);
    }
}