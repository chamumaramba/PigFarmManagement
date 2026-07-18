using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.FarmModels;

namespace PigFarmManagement.Application.Mappings
{
    public class FarmMapper
    {
        public static FarmResponse ToResponse(Farm farm)
        {
            return new FarmResponse(
                farm.Id,
                farm.Name,
                farm.Location,
                farm.FarmCode,
                farm.Currency,
                farm.TimeZone,
                farm.CreatedAt,
                farm.Building.Count,
                farm.IsDeleted
            );
        }

        public static IReadOnlyList<FarmResponse> ToResponseList(
            IEnumerable<Farm> farms)
        {
            return farms
                .Select(ToResponse)
                .ToList();
        }
    }
}