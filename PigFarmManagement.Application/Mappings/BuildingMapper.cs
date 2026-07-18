using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.Building.BuildingModels;

namespace PigFarmManagement.Application.Mappings
{
    public static class BuildingMapper
    {
        public static BuildingResponse ToResponse(Building building)
        {
            return new BuildingResponse(
                building.Id,
                building.Name,
                building.Type,
                building.FarmId,
                building.Status,
                building.Pens.Count,
                building.CreatedAt,
                building.UpdatedAt
            );
        }

        public static IReadOnlyList<BuildingResponse> ToResponseList(IEnumerable<Building> buildings)
        {
            return buildings
                .Select(ToResponse)
                .ToList();
        }
    }
}