using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IBuildingRepository
    {
        Task<Building?> GetBuildingAsync(Guid id, CancellationToken cancellationToken);

        void UpdateBuilding(Building building);

        Task<IEnumerable<Building>> GetAllBuildingByFarmIdAsync(Guid farmId, CancellationToken cancellationToken);
        Task AddBuildingAsync(Building building, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<bool> BuildingExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> BuildingNameExistsAsync(Guid farmId, string name, CancellationToken cancellationToken);

    }
}