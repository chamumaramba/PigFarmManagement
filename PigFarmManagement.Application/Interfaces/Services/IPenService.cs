using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.Pen.PenModel;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IPenService
    {
        Task<PenResponse> AddAsync(CreatePenRequest request, CancellationToken cancellationToken);

        Task<PenResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<PenSummaryResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<IEnumerable<PenSummaryResponse>> GetByBuildingAsync(Guid buildingId, CancellationToken cancellationToken);

        Task<PenResponse> Update(Guid id, UpdatePenRequest request, CancellationToken cancellationToken);

        Task ActivateAsync(Guid id, CancellationToken cancellationToken);

        Task DeactivateAsync(Guid id, CancellationToken cancellationToken);
    }
}