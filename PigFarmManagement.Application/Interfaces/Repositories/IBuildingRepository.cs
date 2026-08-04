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
        Task<IEnumerable<Building>> GetAllBuildingByFarmIdAsync(CancellationToken cancellationToken);
        Task<bool> BuildingCodeExistsAsync(string name, CancellationToken cancellationToken);
        Task<Building?> GetBuildingByName(string name, CancellationToken cancellationToken);

    }
}