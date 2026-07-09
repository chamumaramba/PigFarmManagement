using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PigFarmManagement.Application.Common;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Application.Services;
using static PigFarmManagement.Application.DTOs.FarmModels;

namespace PigFarmManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
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


        }
    }
}