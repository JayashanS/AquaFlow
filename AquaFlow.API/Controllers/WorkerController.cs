using AquaFlow.Domain.DTOs.FishFarm;
using AquaFlow.Domain.DTOs.Worker;
using AquaFlow.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AquaFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController(IWorkerService workerService, ILogger<WorkerController> logger) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<RetrieveWorkerDTO>> CreateWorkerAsync([FromForm] CreateWorkerDTO workerDTO)
        {
            try
            {
                var createdWorker = await workerService.CreateWorkerAsync(workerDTO);
                return Ok(createdWorker);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating user");
                return StatusCode(500, new { Message = "An error occurred while creating the Worker." });
            }
        }
    }
}
