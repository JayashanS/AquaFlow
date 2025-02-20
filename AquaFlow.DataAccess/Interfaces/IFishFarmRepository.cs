using AquaFlow.DataAccess.Filters;
using AquaFlow.DataAccess.Models;

namespace AquaFlow.DataAccess.Interfaces
{
    public interface IFishFarmRepository
    {
        Task CreateFishFarmAsync(FishFarm fishFarm);
        Task DeleteFishFarmByIdAsync(int id);
        Task<IEnumerable<FishFarm>> GetFishFarmsAsync(FishFarmFilterOptions filterOptions);
        Task<int> GetTotalFishFarmsCountAsync(FishFarmFilterOptions filterOptions);
        Task<FishFarm> GetFishFarmByIdAsync(int id);
        Task UpdateFishFarmByIdAsync(int id, FishFarm updatedFishFarm);
    }
}