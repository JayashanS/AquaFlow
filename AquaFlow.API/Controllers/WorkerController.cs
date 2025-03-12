using AquaFlow.DataAccess.Filters;
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
                logger.LogError(ex, "API: Error creating user");
                return StatusCode(500, new { Message = "An error occurred while creating the Worker." });
            }
        }

        [HttpGet("getByFilter")]
        public async Task<ActionResult<RetrieveWorkerWithTotalDTO>> GetWorkersByFilterAsync([FromQuery] WorkerFilterOptions filterOptions)
        {
            try
            {
                var workers = await workerService.GetWorkersByFilterAsync(filterOptions);
                return Ok(workers);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: An error occure while getting workers");
                return StatusCode(500, new { Message = "An error occure while getting workers" });
            }
        }

        [HttpDelete("deleteById/{id}")]
        public async Task<ActionResult> DeleteWorkerByIdAsync(int id)
        {
            try
            {
                await workerService.DeleteWorkerByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error occured while deleting worker");
                return StatusCode(500, new { Message = "Error occured while deleting worker" });
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<RetrieveWorkerDTO>> GetWorkerByIdAsync(int id)
        {
            try
            {
                var worker = await workerService.GetWorkerByIdAsync(id);
                return Ok(worker);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: error retrieving user");
                return StatusCode(500, new { Message = "User Not Found" });
            }
        }

        [HttpPut("updateById/{id}")]
        public async Task<ActionResult> UpdateWorkerByIdAsync(int id, [FromForm] UpdateWorkerDTO updatedWorkerDTO)
        {
            try
            {
                await workerService.UpdateWorkerByIdAsync(id, updatedWorkerDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error updating worker");
                return StatusCode(500, new { Message = "An error occurred while updating the worker." });
            }
        }

        [HttpPost("checkEmail/{email}")]
        public async Task<ActionResult<bool>> DoesEmailExist(string email)
        {
            try
            {
                bool emailExists = await workerService.DoesEmailExist(email);
                return Ok(emailExists);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error finding found");
                return StatusCode(500, new { Message = "Error finding email" });
            }   
        }
    }
}