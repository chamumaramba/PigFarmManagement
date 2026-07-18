
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IFarmRepository: IRepository<Farm>
    {
        Task<Farm?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Farm>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<int> GetFarmcountAsync(CancellationToken cancellationToken = default);
    }
}
