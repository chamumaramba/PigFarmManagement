using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class AnimalRepository(PigFarmDbContext context, ICurrentUserServices currentUserServices)
        : Repository<Animal>(context, currentUserServices), IAnimalRepository
    {
        //private readonly PigFarmDbContext _context = context;

        public async Task<Animal?> GetByTagNumberAsync(
            string tagNumber,
            CancellationToken cancellationToken = default)
        {
            return await _context.Animals
                .FirstOrDefaultAsync(
                    a => a.TagNumber == tagNumber &&
                         !a.IsDeleted,
                    cancellationToken);
        }
    }
}