
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IFarmRepository
    {
        Task<IReadOnlyList<Farm>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Farm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Farm?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Farm>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(Farm farm, CancellationToken cancellationToken = default);
        void Update(Farm farm);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<int> GetFarmcountAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
