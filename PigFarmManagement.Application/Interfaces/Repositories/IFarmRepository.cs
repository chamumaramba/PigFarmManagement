
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IFarmRepository
    {
        Task<IEnumerable<Farm>> GetAll(CancellationToken cancellationToken = default);
        Task<Farm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Farm?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task AddAsync(Farm farm, CancellationToken cancellationToken = default);
        void UpdateAsync(Farm farm);
        Task<bool> IsExistAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}