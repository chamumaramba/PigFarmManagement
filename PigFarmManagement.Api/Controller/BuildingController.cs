using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PigFarmManagement.Application.Common;
using PigFarmManagement.Application.Interfaces.Services;
using static PigFarmManagement.Application.DTOs.Building.BuildingModels;

namespace PigFarmManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingController(IBuildingService buildingService) : ControllerBase
    {
        private readonly IBuildingService _buildingService = buildingService;
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var buildings = await _buildingService.GetAllAsync(cancellationToken);
            return Ok(ApiResponse<IEnumerable<BuildingResponse>>.SuccessResponse(buildings));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBuildingRequest request, CancellationToken cancellationToken)
        {
            var building = await _buildingService.AddAsync(request, cancellationToken);
            return CreatedAtRoute(
                "GetBuildingById",
                new { id = building.Id },
                building
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateBuildingRequest request, CancellationToken cancellationToken)
        {
            var building = await _buildingService.Update(id, request, cancellationToken);
            return Ok(ApiResponse<BuildingResponse>.SuccessResponse(building, "Building updated successfully"));
        }


        [HttpGet("{id:guid}", Name = "GetBuildingById")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var building = await _buildingService.GetByIdAsync(id, cancellationToken);
            return Ok(ApiResponse<BuildingResponse>.SuccessResponse(building));
        }

        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> DeactivateAsync(Guid id, CancellationToken cancellationToken)
        {
            await _buildingService.DeactivateAsync(id, cancellationToken);
            return Ok(
                ApiResponse<object?>.SuccessResponse(
                    null,
                    "Building successfully deactivated"
                )
            );
        }

        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> ActivateAsync(Guid id, CancellationToken cancellationToken)
        {
            await _buildingService.ActivateAsync(id, cancellationToken);
            return Ok(
                ApiResponse<object?>.SuccessResponse(
                    null,
                    "Building successfully activated"
                )
            );
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var building = await _buildingService.GetByNameAsync(name, cancellationToken);
            return Ok(ApiResponse<BuildingResponse>.SuccessResponse(building));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Remove(Guid id, CancellationToken cancellationToken)
        {
            _buildingService.Remove(id, cancellationToken);
            return Ok(
                ApiResponse<object?>.SuccessResponse(
                    null,
                    "Building successfully removed"
                )
            );
        }
    }
}