using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PigFarmManagement.Application.Common;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Infrastructure.Identity;
using static PigFarmManagement.Application.DTOs.FarmModels;

namespace PigFarmManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = AppRoles.Admin)]
    public class FarmController : ControllerBase
    {
        private readonly IFarmService _farmService;

        public FarmController(IFarmService farmService)
        {
            _farmService = farmService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateFarmRequest request, CancellationToken cancellationToken)
        {
            var farm = await _farmService.CreateAsync(request, cancellationToken);
            return Ok(
                ApiResponse<FarmResponse>.SuccessResponse(
                farm,
                "Farm created successfully"
            ));
        }

        [HttpPost("deactivate")]
        public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken){
            await _farmService.DeactivateFarmAsync(id, cancellationToken);
            return Ok(
                ApiResponse<object?>.SuccessResponse(
                    null,
                    "Farm successfully deactivated"
                )
            );
        }
    }
}
