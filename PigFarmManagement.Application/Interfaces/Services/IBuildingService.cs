using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PigFarmManagement.Application.DTOs.Building.BuildingModels;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IBuildingService
    {
        Task<BuildingResponse> AddAsync(CreateBuildingRequest buildingRequest, CancellationToken cancellationToken);
        Task<BuildingResponse> Update(Guid id, UpdateBuildingRequest updateBuildingRequest, CancellationToken cancellationToken);
        Task<BuildingResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BuildingResponse> GetByNameAsync(Guid farmId, string name, CancellationToken cancellationToken);
        Task<IEnumerable<BuildingResponse>> GetAllAsync(Guid farmId, CancellationToken cancellationToken);
        Task DeactivateAsync(Guid id, CancellationToken cancellationToken);
        Task ActivateAsync(Guid id, CancellationToken cancellationToken);
        void Remove(Guid id, CancellationToken cancellationToken);



    }
}