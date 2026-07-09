using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Application.DTOs
{
    public class FarmModels
    {
        public record CreateFarmRequest(
            string Name,
            string Location,
            string Currency,
            string TimeZone
        );

        public record FarmResponse(
            Guid Id,
            string Name,
            string Location,
            string Currency,
            string TimeZone,
            DateTime CreatedAt,
            int BuildingCount
        );

        public record UpdateFarmRequest(
            string Name,
            string Location,
            string Currency,
            string TimeZone
        );
    }
}