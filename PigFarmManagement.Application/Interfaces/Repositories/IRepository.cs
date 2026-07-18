using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);

        void Update(
            T entity,
            CancellationToken cancellationToken = default);

        void Remove(T entity);
    }
}