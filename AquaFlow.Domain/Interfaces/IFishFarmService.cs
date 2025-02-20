using AquaFlow.DataAccess.Filters;
using AquaFlow.Domain.DTOs.FishFarm;

namespace AquaFlow.Domain.Interfaces
{
    public interface IFishFarmService
    {
        Task<RetrieveFishFarmWithTotalDTO> GetFishFarmsWithCountAsync(FishFarmFilterOptions filterOptions);
        Task<RetrieveFishFarmDTO> GetFishFarmByIdAsync(int id);
        Task<RetrieveFishFarmDTO> CreateFishFarmAsync(CreateFishFarmDTO fishFarm);
        Task UpdateFishFarmByIdAsync(int id, UpdateFishFarmDTO updatedFishFarm);
        Task DeleteFishFarmByIdAsync(int id);
    }
}