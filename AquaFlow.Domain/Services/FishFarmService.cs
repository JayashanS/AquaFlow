using AquaFlow.API.Utils;
using AquaFlow.DataAccess.Filters;
using AquaFlow.DataAccess.Interfaces;
using AquaFlow.Domain.DTOs.FishFarm;
using AquaFlow.Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AquaFlow.Domain.Services
{
    public class FishFarmService(IFishFarmRepository fishFarmRepository, IMapper mapper, ILogger<FishFarmService> logger, FileUploadHelper fileUploadHelper, IWorkerRepository workerRepository) : IFishFarmService
    {
        public async Task<RetrieveFishFarmDTO> CreateFishFarmAsync(CreateFishFarmDTO fishFarmDTO)
        {
            string pictureUrl = null;

            if (fishFarmDTO.Picture != null)
            {
                pictureUrl = await fileUploadHelper.SaveFileAsync(fishFarmDTO.Picture);
            }
            try
            {
                var fishFarm = mapper.Map<AquaFlow.DataAccess.Models.FishFarm>(fishFarmDTO);
                fishFarm.PictureUrl = pictureUrl;

                var createdFishFarm = await fishFarmRepository.CreateFishFarmAsync(fishFarm);
                return mapper.Map<RetrieveFishFarmDTO>(createdFishFarm);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating fish farm.");
                throw;
            }
        }

        public async Task DeleteFishFarmByIdAsync(int id)
        {
            try
            {
                var fishFarm = await fishFarmRepository.GetFishFarmByIdAsync(id);

                if (fishFarm == null)
                {
                    throw new KeyNotFoundException($"Fish farm with ID {id} not found.");
                }

                if (!string.IsNullOrEmpty(fishFarm.PictureUrl))
                {
                    var picturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fishFarm.PictureUrl.TrimStart('/'));

                    if (File.Exists(picturePath))
                    {
                        File.Delete(picturePath); 
                    }
                }

                var usersInFishFarm = await workerRepository.GetWorkersByFishFarmIdAsync(id);
                foreach (var user in usersInFishFarm)
                {
                    if (!string.IsNullOrEmpty(user.PictureUrl))
                    {
                        var userPicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.PictureUrl.TrimStart('/'));

                        if (File.Exists(userPicturePath))
                        {
                            File.Delete(userPicturePath); 
                        }
                    }
                }

                await fishFarmRepository.DeleteFishFarmByIdAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting fish farm with id {id}", id);
                throw;
            }
        }

        public async Task<RetrieveFishFarmDTO> GetFishFarmByIdAsync(int id)
        {
            try
            {
                var fishFarm = await fishFarmRepository.GetFishFarmByIdAsync(id);
                return mapper.Map<RetrieveFishFarmDTO>(fishFarm);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving fish farm with ID {FishFarmId}.", id);
                throw;
            }
        }

        public async Task<RetrieveFishFarmWithTotalDTO> GetFishFarmsWithCountAsync(FishFarmFilterOptions filterOptions)
        {
            try
            {
                var fishFarms = await fishFarmRepository.GetFishFarmsAsync(filterOptions);
                var totalCount = await fishFarmRepository.GetTotalFishFarmsCountAsync(filterOptions);

                return new RetrieveFishFarmWithTotalDTO
                {
                    FishFarms = mapper.Map<IEnumerable<RetrieveFishFarmDTO>>(fishFarms),
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving fish farms with count.");
                throw;
            }
        }

        public async Task UpdateFishFarmByIdAsync(int id, UpdateFishFarmDTO updatedFishFarmDto)
        {
            try
            {
                var existingFishFarm = await fishFarmRepository.GetFishFarmByIdAsync(id);
                if (existingFishFarm == null)
                {
                    throw new KeyNotFoundException($"Fish farm with ID {id} not found.");
                }

                mapper.Map(updatedFishFarmDto, existingFishFarm);

                await fishFarmRepository.UpdateFishFarmByIdAsync(id, existingFishFarm);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating fish farm with ID {FishFarmId}.", id);
                throw;
            }
        }
    }
}