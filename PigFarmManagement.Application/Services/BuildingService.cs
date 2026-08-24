using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            var farm = await _farmRepo.GetByIdAsync(farmId, cancellationToken)
                ?? throw new UnauthorizedAccessException(
                    "User is not assigned to a farm.");

            var buildingSequence = farm.LastBuildingSequence + 1;
            farm.LastBuildingSequence = buildingSequence;
            _farmRepo.Update(farm);

            var buildingCode = CodeGenerator.Building(
                farm.FarmCode,
                buildingSequence);

            if (await _repo.BuildingCodeExistsAsync(buildingCode, cancellationToken))
            {
                throw new InvalidOperationException("Building with the same code already exists");
            }

            var firstPenGroup = buildingRequest.PenGroups.FirstOrDefault()
                ?? throw new ArgumentException("At least one pen group is required.", nameof(buildingRequest));

            var building = new Building
            {
                Id = Guid.NewGuid(),
                Name = buildingRequest.Name,
                BuildingCode = buildingCode,
                DefaultPenCapacity = firstPenGroup.CapacityPerPen,
                DefaultPenType = firstPenGroup.PenType,
                Status = buildingRequest.Status,
                FarmId = farmId

            };

            foreach (var group in buildingRequest.PenGroups)
            {
                if (group.NumberOfPens <= 0 || group.CapacityPerPen <= 0)
                {
                    throw new ArgumentException(
                        "Each pen group must contain at least one pen and have a positive capacity.",
                        nameof(buildingRequest));
                }

                for (var i = 0; i < group.NumberOfPens; i++)
                {
                    building.LastPenSequence++;
                    building.NumberOfPens++;

                    building.Pens.Add(new Pen
                    {
                        Id = Guid.NewGuid(),
                        PenCode = CodeGenerator.Pen(
                            building.BuildingCode,
                            building.LastPenSequence
                        ),
                        Name = $"Pen {building.LastPenSequence:D3}",
                        BuildingId = building.Id,
                        Type = group.PenType,
                        Capacity = group.CapacityPerPen,
                        FarmId = farmId,
                    });
                }
            }


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

            _repo.Update(building);
            await _repo.SaveChangesAsync(cancellationToken);

            return BuildingMapper.ToResponse(building);
        }
    }
}