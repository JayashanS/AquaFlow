namespace AquaFlow.DataAccess.Interfaces
{
    public interface IFishFarmRepository
    {
        Task CreateFishFarmAsync();
        Task DeleteFishFarmByIdAsync(int id);
        Task GetFishFarmsAsync();
        Task GetFishFarmByIdAsync(int id);
        Task UpdateFishFarmByIdAsync(int id);
    }
}