using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

namespace PigFarmManagement.Infrastructure.Repository
{
    public class FarmRepository : IFarmRepository
    {
        private readonly PigFarmDbContext _context;

        public FarmRepository(PigFarmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Farm farm, CancellationToken cancellationToken = default)
        {
            await _context.Farms.AddAsync(farm, cancellationToken);
        }

        public async Task<IEnumerable<Farm>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Farms
            .Where(f => f.IsActive)
            .ToListAsync(cancellationToken);
        }

        public async Task<Farm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<Farm?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.FirstOrDefaultAsync(f => f.Name == name, cancellationToken);
        }

        public async Task<bool> IsExistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.AnyAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Farms.AnyAsync(f => f.Name == name, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void UpdateAsync(Farm farm)
        {
            _context.Farms.Update(farm);
        }
    }
}