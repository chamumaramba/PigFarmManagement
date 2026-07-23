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
using PigFarmManagement.Application.Mappings;

namespace PigFarmManagement.Application.Services
{
    public class FarmService(IFarmRepository repo, ICurrentUserServices currentUserServices) : IFarmService
    {
        private readonly IFarmRepository _repo = repo;
        //private readonly ICurrentUserServices _currentUserServices = currentUserServices;
        public async Task<FarmResponse> AddAsync(CreateFarmRequest request, CancellationToken cancellationToken)
        {
            if (await _repo.ExistsByNameAsync(request.Name, cancellationToken))
            {
                throw new InvalidOperationException("A farm with the same name already exists.");
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
                CreatedAt = DateTime.UtcNow,
                CreatedBy = GetCurrentUserIdString()
            };

            await _repo.AddAsync(farm, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);



            return FarmMapper.ToResponse(farm);

        }

        public async Task DeactivateFarmAsync(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken);

            if (farm == null)
            {
                throw new KeyNotFoundException("Farm not found.");
            }

            farm.IsDeleted = true;
            farm.UpdatedBy = GetCurrentUserIdString();
            _repo.Update(farm);

            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task<FarmResponse> ActivateFarmAsync(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Farm not found.");

            farm.IsDeleted = false;
            farm.UpdatedBy = GetCurrentUserIdString();
            _repo.Update(farm);
            await _repo.SaveChangesAsync(cancellationToken);
            return FarmMapper.ToResponse(farm);
        }

        public async Task<IEnumerable<FarmResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var farms = await _repo.GetAllAsync(cancellationToken);

            return FarmMapper.ToResponseList(farms);
        }

        public async Task<FarmResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException("Farm not found.");
            return FarmMapper.ToResponse(farm);
        }

        public async Task<FarmResponse> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByNameAsync(name, cancellationToken) ?? throw new KeyNotFoundException("Farm not found.");
            return FarmMapper.ToResponse(farm);
        }

        public async Task<IReadOnlyList<FarmResponse>> GetUserFarmAsync(Guid userId, CancellationToken cancellationToken)
        {
            var farmUser = await _repo.GetByUserIdAsync(userId, cancellationToken);
            return farmUser.Select(FarmMapper.ToResponse).ToList();
        }

        public async Task<FarmResponse> Update(Guid id, FarmModels.UpdateFarmRequest request, CancellationToken cancellationToken)
        {
            var farm = await _repo.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException("Farm not found.");

            farm.Name = request.Name;
            farm.Location = request.Location;
            farm.Currency = request.Currency;
            farm.TimeZone = request.TimeZone;
            farm.UpdatedBy = GetCurrentUserIdString();

            _repo.Update(farm);
            await _repo.SaveChangesAsync(cancellationToken);

            return FarmMapper.ToResponse(farm);
        }

        private string GetCurrentUserIdString()
        {
            return currentUserServices.UserId.ToString();
        }

    }
}