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
    public class FarmController(IFarmService farmService) : ControllerBase
    {
        private readonly IFarmService _farmService = farmService;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateFarmRequest request, CancellationToken cancellationToken)
        {
            var farm = await _farmService.AddAsync(request, cancellationToken);
            return CreatedAtAction(
                nameof(GetById),
                new { id = farm.Id },
                ApiResponse<FarmResponse>.SuccessResponse(
                    farm,
                    "Farm created successfully"
                )
            );
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin, FarmManager")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _farmService.GetByIdAsync(id, cancellationToken);

            return Ok(
                ApiResponse<FarmResponse>.SuccessResponse(farm)
            );
        }

        [HttpPatch("{id:guid}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken){
            await _farmService.DeactivateFarmAsync(id, cancellationToken);
            return Ok(
                ApiResponse<object?>.SuccessResponse(
                    null,
                    "Farm successfully deactivated"
                )
            );
        }

        [HttpPatch("{id:guid}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
        {
            var farm = await _farmService.ActivateFarmAsync(id, cancellationToken);
            return Ok(ApiResponse<object?>.SuccessResponse(
                farm,
                "Farm succesfully activated."
            ));

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var farms = await _farmService.GetAllAsync(cancellationToken);

            return Ok(
                ApiResponse<IEnumerable<FarmResponse>>
                    .SuccessResponse(farms)
            );
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin, FarmManager")]
        public async Task<IActionResult> Update(Guid id, UpdateFarmRequest request, CancellationToken cancellationToken)
        {
            var updatedFarm = await _farmService.Update(id, request, cancellationToken);
            return Ok(ApiResponse<FarmResponse>.SuccessResponse(
                updatedFarm,
                "Farm succesfully updated."
            ));

        }

        [HttpGet("name/{name}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FarmByName(string name, CancellationToken cancellationToken)
        {
            var farm = await _farmService.GetByNameAsync(name, cancellationToken);
            if (farm is null)
            {
                return NotFound(ApiResponse<FarmResponse>.ErrorResponse(
                    null,
                    "Farm not found."));
            }

            return Ok(ApiResponse<FarmResponse>.SuccessResponse(
                farm
            ));
        }
    }
}
