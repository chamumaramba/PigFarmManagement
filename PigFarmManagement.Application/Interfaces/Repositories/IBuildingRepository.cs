using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IBuildingRepository: IRepository<Building>
    {
        Task<IEnumerable<Building>> GetAllBuildingByFarmIdAsync(Guid farmId, CancellationToken cancellationToken);
        Task<bool> BuildingNameExistsAsync(Guid farmId, string name, CancellationToken cancellationToken);
        Task<Building?> GetBuildingByName(Guid farmId, string name, CancellationToken cancellationToken);

    }
}