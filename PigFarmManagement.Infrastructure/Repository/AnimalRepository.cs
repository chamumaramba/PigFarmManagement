
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;
namespace PigFarmManagement.Infrastructure.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly PigFarmDbContext _context;
        public AnimalRepository(PigFarmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Animal animal, CancellationToken cancellationToken = default)
        {
            await _context.Animals.AddAsync(animal, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Animals.AnyAsync(a => a.Id == id, cancellationToken);
        }

        public Task<Animal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Animals
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public Task<Animal?> GetByTagNumberAsync(string tagNumber, CancellationToken cancellationToken = default)
        {
            return _context.Animals
                .FirstOrDefaultAsync(a => a.TagNumber == tagNumber, cancellationToken);
        }

        public void Remove(Animal animal)
        {
            _context.Animals.Remove(animal);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Animal animal)
        {
            _context.Animals.Update(animal);
        }
    }
}
