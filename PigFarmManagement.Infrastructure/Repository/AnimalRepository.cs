using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        public Task AddAsync(Animal animal, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Animal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => Task.FromResult<Animal?>(null);

        public Task<Animal?> GetByTagNumberAsync(string tagNumber, CancellationToken cancellationToken = default)
            => Task.FromResult<Animal?>(null);

        public void Remove(Animal animal)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}
