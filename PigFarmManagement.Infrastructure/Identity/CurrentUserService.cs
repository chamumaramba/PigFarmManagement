using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PigFarmManagement.Application.Constants;
using PigFarmManagement.Application.Interfaces.Services;

namespace PigFarmManagement.Infrastructure.Identity
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserServices
    {
        //private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public Guid UserId
        {
            get
            {
                var userId = httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                if (!Guid.TryParse(userId, out var result))
                {
                    throw new UnauthorizedAccessException("User context is missing or invalid.");
                }

                return result;
            }
        }

        public Guid FarmId
        {
            get
            {
                var farmId = httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirstValue(CustomClaimTypes.FarmId);

                if (!Guid.TryParse(farmId, out var result))
                {
                    throw new UnauthorizedAccessException("Invalid or missing FarmId in token.");
                }

                return result;
            }
        }
    }


}