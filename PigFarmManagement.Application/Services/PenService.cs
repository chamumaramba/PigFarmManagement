using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs.Pen;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Application.Mappings;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.Pen.PenModel;

namespace PigFarmManagement.Application.Services
{
    public class PenService(IPenRepository repository) : IPenService
    {
        private readonly IPenRepository _repo = repository;
        public async Task ActivateAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingPen = await _repo.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Pen not found.");

            if (!existingPen.IsDeleted)
                throw new InvalidOperationException("Pen is already active.");

            existingPen.IsDeleted = false;
            existingPen.UpdatedAt = DateTime.UtcNow;

            _repo.Update(existingPen);
            await _repo.SaveChangesAsync();
        }

        public async Task<PenResponse> AddAsync(CreatePenRequest request, CancellationToken cancellationToken)
        {
            var pen = new Pen
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Capacity = request.Capacity,
                BuildingId = request.BuildingId,
                Type = request.PenType,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _repo.AddAsync(pen, cancellationToken);
            await _repo.SaveChangesAsync();

            return PenMapper.ToResponse(pen);
        }

        public async Task DeactivateAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingPen = await _repo.GetByIdAsync(id, cancellationToken);

            if (existingPen == null)
            {
                throw new KeyNotFoundException("Pen not found.");
            }

            if (existingPen.IsDeleted)
            {
                throw new InvalidOperationException("The pen is already deactivated.");
            }

            existingPen.IsDeleted = true;
            existingPen.UpdatedAt = DateTime.UtcNow;

            _repo.Update(existingPen);
            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<PenSummaryResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var Pens = await _repo.GetAllAsync(cancellationToken);
            return PenMapper.ToResponseList(Pens);
        }

        public async Task<IEnumerable<PenSummaryResponse>> GetByBuildingAsync(Guid buildingId, CancellationToken cancellationToken)
        {
            var pens = await _repo.GetByBuildingAsync(buildingId, cancellationToken);

            return PenMapper.ToResponseList(pens);
        }

        public async Task<PenResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var pen = await _repo.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException("Building not found.");
            return PenMapper.ToResponse(pen);
        }

        public async Task<PenResponse> Update(Guid id, UpdatePenRequest request, CancellationToken cancellationToken)
        {

            var pen = await _repo.GetByIdAsync(id, cancellationToken);
            if (pen == null)
                throw new KeyNotFoundException("Pen not found.");

            if (!string.Equals(pen.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                var exists = await _repo.ExistsInBuildingAsync(
                    pen.BuildingId,
                    request.Name,
                    cancellationToken);

                if (exists)
                    throw new InvalidOperationException(
                        "A pen with the same name already exists in this building.");
            }
            pen.Name = request.Name;
            pen.Capacity = request.Capacity;
            pen.Type = request.PenType;
            pen.UpdatedAt = DateTime.UtcNow;

            _repo.Update(pen);
            await _repo.SaveChangesAsync(cancellationToken);

            return PenMapper.ToResponse(pen);
        }
    }
}