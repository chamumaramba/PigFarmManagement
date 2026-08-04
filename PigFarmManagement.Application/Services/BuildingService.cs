using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs.Building;
using PigFarmManagement.Application.Helpers;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Application.Mappings;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.Building.BuildingModels;

namespace PigFarmManagement.Application.Services
{
    public class BuildingService(
        IBuildingRepository repo,
        IFarmRepository farmRepo,
        ICurrentUserServices currentUser)
        : IBuildingService
    {
        private readonly IBuildingRepository _repo = repo;
        private readonly IFarmRepository _farmRepo = farmRepo;
        private readonly ICurrentUserServices _currentUser = currentUser;
        public async Task ActivateAsync(Guid id, CancellationToken cancellationToken)
        {
            //var farmId = _currentUserServices.FarmId;
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
            var farmId = _currentUser.FarmId;
        if (await _repo.BuildingCodeExistsAsync(buildingRequest.BuildingCode, cancellationToken))
            {
                throw new InvalidOperationException("Building with the same code already exists");
            }

            var farm = await _farmRepo.GetByIdAsync(farmId, cancellationToken)
                ?? throw new UnauthorizedAccessException(
                    "User is not assigned to a farm.");

            var buildingSequence = farm.LastBuildingSequence + 1;
            farm.LastBuildingSequence = buildingSequence;
            _farmRepo.Update(farm);

            var buildingCode = BuildingCodeGenerator.Generate(
                farm.FarmCode,
                buildingSequence);

            var building = new Building
            {
                Id = Guid.NewGuid(),
                Name = buildingRequest.Name,
                BuildingCode = buildingCode,
                Status = buildingRequest.Status,
                Type = buildingRequest.Type,
                FarmId = farmId

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

        public async Task<IEnumerable<BuildingResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var buildings = await _repo.GetAllAsync(cancellationToken);
            return BuildingMapper.ToResponseList(buildings);
        }

        public async Task<BuildingResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var building = await _repo.GetByIdAsync(id, cancellationToken);
            if (building == null)
                throw new KeyNotFoundException("Building not found.");

            return BuildingMapper.ToResponse(building);
        }

        public async Task<BuildingResponse> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var building = await _repo.GetBuildingByName(name, cancellationToken)
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