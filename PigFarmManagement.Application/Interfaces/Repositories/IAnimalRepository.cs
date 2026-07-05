using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IAnimalRepository
    {

        Task<Animal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Animal?> GetByTagNumberAsync(string tagNumber, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Animal animal, CancellationToken cancellationToken = default);
        void Update(Animal animal);
        void Remove(Animal animal);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}