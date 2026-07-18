using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs.Building;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Application.Mappings;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.Building.BuildingModels;

namespace PigFarmManagement.Application.Services
{
    public class BuildingService(IBuildingRepository repo) : IBuildingService
    {
        private readonly IBuildingRepository _repo = repo;
        public async Task ActivateAsync(Guid id, CancellationToken cancellationToken)
        {
            var building = await _repo.GetByIdAsync(id, cancellationToken);
            if (building == null)
            {
                throw new KeyNotFoundException("Building not found.");
            }

            building.IsDeleted = false;
            _repo.Update(building);
            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task<BuildingResponse> AddAsync(CreateBuildingRequest buildingRequest, CancellationToken cancellationToken)
        {
            if (await _repo.BuildingNameExistsAsync(buildingRequest.FarmId, buildingRequest.Name, cancellationToken))
            {
                throw new InvalidOperationException("Building with the same name already exists");
            }
            var building = new Building
            {
                Id = Guid.NewGuid(),
                Name = buildingRequest.Name,
                Status = buildingRequest.Status,
                Type = buildingRequest.Type,
                FarmId = buildingRequest.FarmId

            };

            await _repo.AddAsync(building, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            return BuildingMapper.ToResponse(building);
        }

        public async Task DeactivateAsync(Guid id, CancellationToken cancellationToken)
        {
            var building =  await _repo.GetByIdAsync(id, cancellationToken);
            if (building == null)
               throw new InvalidOperationException("Building does not exist");
            building.IsDeleted = true;
            _repo.Update(building);
            await _repo.SaveChangesAsync(cancellationToken);

        }

        public async Task<IEnumerable<BuildingResponse>> GetAllAsync(Guid farmId, CancellationToken cancellationToken)
        {
            var buildings = await _repo.GetAllBuildingByFarmIdAsync(farmId, cancellationToken);
            return BuildingMapper.ToResponseList(buildings);
        }

        public async Task<BuildingResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var building = await _repo.GetByIdAsync(id, cancellationToken);
            if (building == null)
                throw new KeyNotFoundException("Building not found.");

            return BuildingMapper.ToResponse(building);
        }

        public async Task<BuildingResponse> GetByNameAsync(Guid farmId, string name, CancellationToken cancellationToken)
        {
            var building = await _repo.GetBuildingByName(farmId, name, cancellationToken)
                ?? throw new KeyNotFoundException($"Building '{name}' not found on the specified farm.");

            return BuildingMapper.ToResponse(building);
        }

        public void Remove(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<BuildingResponse> Update(Guid id, UpdateBuildingRequest updateBuildingRequest, CancellationToken cancellationToken)
        {
            var building = await _repo.GetByIdAsync(id, cancellationToken);
            if (building == null)
               throw new KeyNotFoundException("Buiding not found.");

            building.Name = updateBuildingRequest.Name;
            building.Status = updateBuildingRequest.Status;
            building.Type = updateBuildingRequest.Type;

            _repo.Update(building);
            await _repo.SaveChangesAsync(cancellationToken);

            return BuildingMapper.ToResponse(building);
        }
    }
}