using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.FarmModels;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IPenRepository:IRepository<Pen>
    {
        Task<Pen?> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Pen>> GetByBuildingAsync(Guid buildingId, CancellationToken cancellationToken);
        Task<int> GetCountAsync(Guid buildingId, CancellationToken cancellationToken);
        Task<bool> ExistsInBuildingAsync(Guid buildingId, string name, CancellationToken cancellationToken);
    }
}