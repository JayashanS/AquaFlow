﻿using AquaFlow.DataAccess.Filters;
using AquaFlow.Domain.DTOs.FishFarm;
using AquaFlow.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AquaFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishFarmController(IFishFarmService fishFarmService, ILogger<FishFarmController> logger) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<RetrieveFishFarmDTO>> CreateFishFarmAsync([FromForm] CreateFishFarmDTO createFishFarmDTO)
        {
            try
            {
                var createdFishFarm = await fishFarmService.CreateFishFarmAsync(createFishFarmDTO);
                return Ok(createdFishFarm);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error creating fish farm.");
                return StatusCode(500, new { Message = "An error occurred while creating the fish farm." });
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult> GetFishFarmByIdAsync(int id)
        {
            try
            {
                var fishFarm = await fishFarmService.GetFishFarmByIdAsync(id);
                return Ok(fishFarm);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error retrieving fish farm with ID {FishFarmId}.", id);
                return NotFound(new { Message = $"Fish farm with ID {id} not found." });
            }
        }

        [HttpGet("getByFilter")]
        public async Task<ActionResult<RetrieveFishFarmWithTotalDTO>> GetFishFarmsWithCountAsync([FromQuery] FishFarmFilterOptions filterOptions)
        {
            try
            {
                var fishFarmsWithCount = await fishFarmService.GetFishFarmsWithCountAsync(filterOptions);
                return Ok(fishFarmsWithCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error retrieving fish farms with count.");
                return StatusCode(500, new { Message = "An error occurred while retrieving the fish farms." });
            }
        }       

        [HttpPut("updateById/{id}")]
        public async Task<ActionResult> UpdateFishFarmByIdAsync(int id, [FromForm] UpdateFishFarmDTO updatedFishFarmDto)
        {
            try
            {
                await fishFarmService.UpdateFishFarmByIdAsync(id, updatedFishFarmDto);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error updating fish farm with ID {FishFarmId}.", id);
                return StatusCode(500, new { Message = "An error occurred while updating the fish farm." });
            }
        }

        [HttpDelete("deleteById/{id}")]
        public async Task<ActionResult> DeleteFishFarmByIdAsync(int id)
        {
            try
            {
                await fishFarmService.DeleteFishFarmByIdAsync(id);
                return NoContent();
            } 
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error deleting fish farm with ID {FishFarmId}.", id);
                return StatusCode(500, new { Message = "An error occurred while deleting the fish farm." });
            }
        }
    }
}