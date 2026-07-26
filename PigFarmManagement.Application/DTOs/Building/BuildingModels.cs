using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs.Building
{
   public static class BuildingModels
    {
        public record CreateBuildingRequest(
            string Name,
            BuildingType Type,
            BuildingStatus Status,
            int NumberOfPens
        );

        public record UpdateBuildingRequest(
            string Name,
            BuildingType Type,
            BuildingStatus Status
        );

        public record BuildingResponse(
            Guid Id,
            string Name,
            BuildingType Type,
            Guid FarmId,
            BuildingStatus Status,
            int PenCount,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record BuildingSummaryResponse(
            Guid Id,
            string Name,
            BuildingType Type,
            BuildingStatus Status
        );
    }
}