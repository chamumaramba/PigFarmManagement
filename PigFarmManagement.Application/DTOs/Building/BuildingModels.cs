using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs.Building
{
   public static class BuildingModels
    {
        public record CreateBuildingRequest(
            [Required]
            [StringLength(100, MinimumLength = 2)]
            string Name,

            BuildingStatus Status,

            [Required]
            int DefaultPenType,

            [Required]
            [MinLength(1)]
            IReadOnlyList<CreatePenGroupRequest> PenGroups
        );

        public record CreatePenGroupRequest(
            PenType PenType,
            int NumberOfPens,
            int CapacityPerPen
        );

        public record UpdateBuildingRequest(
            string Name,
            BuildingStatus Status
        );

        public record BuildingResponse(
            Guid Id,
            string Name,
            string BuildingCode,
            Guid FarmId,
            BuildingStatus Status,
            int PenCount,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record BuildingSummaryResponse(
            Guid Id,
            string Name,
            BuildingStatus Status
        );
    }
}