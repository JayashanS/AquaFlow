using AquaFlow.API.Utils;
using AquaFlow.DataAccess.Filters;
using AquaFlow.DataAccess.Interfaces;
using AquaFlow.Domain.DTOs.FishFarm;
using AquaFlow.Domain.Interfaces;
using AutoMapper;

namespace AquaFlow.Domain.Services
{
    public class FishFarmService(IFishFarmRepository fishFarmRepository, IMapper mapper, FileUploadHelper fileUploadHelper, IWorkerRepository workerRepository) : IFishFarmService
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
                throw new Exception("SVC: Error creating fish farm",ex);
            }
        }

        public async Task DeleteFishFarmByIdAsync(int id)
        {
            try
            {
                var fishFarm = await fishFarmRepository.GetFishFarmByIdAsync(id);

                if (fishFarm == null)
                {
                    throw new KeyNotFoundException($"SVC: Fish farm with ID {id} not found.");
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
                if (usersInFishFarm.Any()) 
                {
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
                }


                await fishFarmRepository.DeleteFishFarmByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("SVC: Error deleting fish farm", ex);
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
                throw new Exception("SVC: Error retrieving fish farm", ex);
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
                throw new Exception("SVC: Error retrieving fish farms with count.",ex);
            }
        }

        public async Task UpdateFishFarmByIdAsync(int id, UpdateFishFarmDTO updatedFishFarmDto)
        {
            try
            {
                var existingFishFarm = await fishFarmRepository.GetFishFarmByIdAsync(id);
                if (existingFishFarm == null)
                {
                    throw new KeyNotFoundException($"SVC: Fish farm with ID {id} not found.");
                }
                if (updatedFishFarmDto.Picture != null)
                {
                    if (!string.IsNullOrEmpty(existingFishFarm.PictureUrl))
                    {
                        var oldPicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingFishFarm.PictureUrl.TrimStart('/'));

                        if (File.Exists(oldPicturePath))
                        {
                            File.Delete(oldPicturePath);
                        }
                    }
                    existingFishFarm.PictureUrl = await fileUploadHelper.SaveFileAsync(updatedFishFarmDto.Picture);
                }
                mapper.Map(updatedFishFarmDto, existingFishFarm);
              
                await fishFarmRepository.UpdateFishFarmByIdAsync(id, existingFishFarm);
            }
            catch (Exception ex)
            {
                throw new Exception("SVC: Error updating fish farm", ex);
            }
        }
    }
}