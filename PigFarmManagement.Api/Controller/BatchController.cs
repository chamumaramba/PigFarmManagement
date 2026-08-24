using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PigFarmManagement.Application.Common;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Application.Services;
using static PigFarmManagement.Application.DTOs.AnimalModels;
using static PigFarmManagement.Application.DTOs.Batch.BatchModels;

namespace PigFarmManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController(IBatchService batchService) : ControllerBase
    {
        private readonly IBatchService _batchService = batchService;

        [HttpPost]
        public async Task<IActionResult> CreateBatch([FromBody] CreateBatchRequest createBatchRequest, CancellationToken cancellationToken)
        {
            var batch = await _batchService.AddBatchAsync(createBatchRequest, cancellationToken);
            return Ok(ApiResponse<BatchResponse>.SuccessResponse(batch, "Batch created succesfully."));
        }
    }
}
