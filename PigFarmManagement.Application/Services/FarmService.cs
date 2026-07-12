using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs;
using PigFarmManagement.Application.Helpers;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.FarmModels;

namespace PigFarmManagement.Application.Services
{
    public class FarmService(IFarmRepository repo) : IFarmService
    {
        private readonly IFarmRepository _repo = repo;
        public async Task<FarmResponse> CreateAsync(CreateFarmRequest request, CancellationToken cancellationToken)
        {
            if (await _repo.ExistsByNameAsync(request.Name, cancellationToken))
            {
                throw new InvalidOperationException("A farm with this name already exists.");
            }
            var farmCount = await _repo.GetFarmcountAsync(cancellationToken);
            var farmcode = FarmCodeGenerator.GenerateFarmCode(request.Name, farmCount + 1);

            var farm = new Farm

            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                FarmCode = farmcode,
                Location = request.Location,
                Currency = request.Currency,
                TimeZone = request.TimeZone,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(farm, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);



            return MapToFarmResponse(farm);

        }

        public async Task DeactivateFarmAsync(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken);

            if (farm == null)
            {
                throw new KeyNotFoundException("Farm not found.");
            }

            farm.IsDeleted = true;
            _repo.Update(farm);

            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task<FarmResponse> ActivateFarmAsync(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken);
            if (farm == null)
            {
                throw new KeyNotFoundException("Farm not found.");
            }

            farm.IsDeleted = false;
            _repo.Update(farm);
            await _repo.SaveChangesAsync(cancellationToken);
            return MapToFarmResponse(farm);
        }

        public async Task<IEnumerable<FarmResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var farms = await _repo.GetAllAsync(cancellationToken);
            return farms.Select(MapToFarmResponse).ToList();
        }

        public async Task<FarmResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException("Farm not found.");
            return MapToFarmResponse(farm);
        }

        public async Task<FarmResponse> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByNameAsync(name, cancellationToken) ?? throw new KeyNotFoundException("Farm not found.");
            return MapToFarmResponse(farm);
        }

        public async Task<IReadOnlyList<FarmResponse>> GetUserFarmAsync(Guid userId, CancellationToken cancellationToken)
        {
            var farmUser = await _repo.GetByUserIdAsync(userId, cancellationToken);
            return farmUser.Select(MapToFarmResponse).ToList();
        }

        public async Task<FarmResponse> UpdateAsync(Guid id, FarmModels.UpdateFarmRequest request, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException("Farm not found.");

            farm.Name = request.Name;
            farm.Location = request.Location;
            farm.Currency = request.Currency;
            farm.TimeZone = request.TimeZone;

            _repo.Update(farm);
            await _repo.SaveChangesAsync(cancellationToken);

            return MapToFarmResponse(farm);
        }


        private FarmResponse MapToFarmResponse(Farm farm)
        {
            return new FarmResponse(
                farm.Id,
                farm.Name,
                farm.FarmCode,
                farm.Location,
                farm.Currency,
                farm.TimeZone,
                farm.CreatedAt,
                farm.Building?.Count ?? 0
            );
        }
    }
}