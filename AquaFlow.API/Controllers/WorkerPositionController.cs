
using AquaFlow.Domain.DTOs.WorkerPosition;
using AquaFlow.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AquaFlow.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WorkerPositionController(IWorkerPositionService workerPositionService, ILogger<WorkerPositionController> logger) : ControllerBase
    {
        [HttpGet("getWorkerPositions")]
        public async Task<ActionResult<RetrieveWorkerPositionDTO>> GetWorkerPositions()
        {
            try
            {
                var positions = await workerPositionService.GetWorkerPositionsAsync();
                return Ok(positions);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error in GetWorkerPositions");
                return StatusCode(500, new { Message = "Internal server error while fetching worker positions." });
            }
        }
    }
}
