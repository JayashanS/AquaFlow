using AquaFlow.DataAccess.Data;
using AquaFlow.DataAccess.Filters;
using AquaFlow.DataAccess.Utils;
using AquaFlow.DataAccess.Interfaces;
using AquaFlow.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AquaFlow.DataAccess.Repositories
{
    public class FishFarmRepository(AppDbContext context, FishFarmFilterHelper fishFarmFilterHelper, ILogger<FishFarmRepository> logger) : IFishFarmRepository
    {
        public async Task<FishFarm> CreateFishFarmAsync(FishFarm fishFarm)
        {
            try
            {
                var createdFishFarm = (await context.FishFarms.AddAsync(fishFarm)).Entity;
                await context.SaveChangesAsync();
                return createdFishFarm;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: Error while processing the creation of fish farm", ex);
            }
        }

        public async Task DeleteFishFarmByIdAsync(int id)
        {
            try
            {
                var fishFarm = await context.FishFarms.FirstOrDefaultAsync(f => f.Id == id);
                if (fishFarm == null)
                {
                    throw new KeyNotFoundException($"DAL: Fish farm with ID {id} not found.");
                }
                context.FishFarms.Remove(fishFarm);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: An error occurred while deleting the fish farm", ex);
            }
        }

        public async Task<FishFarm> GetFishFarmByIdAsync(int id)
        {
            try
            {
                var fishFarm = await context.FishFarms.FirstOrDefaultAsync(f => f.Id == id);
                if (fishFarm == null)
                {
                    throw new KeyNotFoundException($"DAL: Fish farm with ID {id} not found.");
                }
                return fishFarm;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: An error occurred while retrieving the fish farm", ex);
            }
        }

        public async Task<IEnumerable<FishFarm>> GetFishFarmsAsync(FishFarmFilterOptions filterOptions)
        {
            try
            {
                if (filterOptions == null)
                {
                    throw new ArgumentNullException("DAL: Filter options cannot be null.");
                }
                var query = context.FishFarms.AsQueryable();

                query = fishFarmFilterHelper.ApplyNameFilter(query, filterOptions.Name);
                query = fishFarmFilterHelper.ApplyLocationFilter(query, filterOptions.TopRightLat, filterOptions.TopRightLng, filterOptions.BottomLeftLat, filterOptions.BottomLeftLng);
                query = fishFarmFilterHelper.ApplyNumberOfCagesFilter(query, filterOptions.NumberOfCages);
                query = fishFarmFilterHelper.ApplyHasBargeFilter(query, filterOptions.HasBarge);
                query = fishFarmFilterHelper.ApplyPaging(query, filterOptions.PageNumber, filterOptions.PageSize);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception ("DAL: An error occurred while retrieving the fish farms with filter options",ex);
            }
        }

        public async Task<int> GetTotalFishFarmsCountAsync(FishFarmFilterOptions filterOptions)
        {
            var query = context.FishFarms.AsQueryable();

            query = fishFarmFilterHelper.ApplyNameFilter(query, filterOptions.Name);
            query = fishFarmFilterHelper.ApplyLocationFilter(query, filterOptions.TopRightLat, filterOptions.TopRightLng, filterOptions.BottomLeftLat, filterOptions.BottomLeftLng);
            query = fishFarmFilterHelper.ApplyNumberOfCagesFilter(query, filterOptions.NumberOfCages);
            query = fishFarmFilterHelper.ApplyHasBargeFilter(query, filterOptions.HasBarge);

            return await query.CountAsync();
        }

        public async Task UpdateFishFarmByIdAsync(int id, FishFarm updatedFishFarm)
        {
            try
            {
                var fishFarm = await context.FishFarms.FirstOrDefaultAsync(f => f.Id == id);
                if (fishFarm == null)
                {
                    throw new KeyNotFoundException($"DAL: Fish farm with ID {id} not found.");
                }

                fishFarm.Name = updatedFishFarm.Name;
                fishFarm.Location = updatedFishFarm.Location;
                fishFarm.NumberOfCages = updatedFishFarm.NumberOfCages;
                fishFarm.HasBarge = updatedFishFarm.HasBarge;
                fishFarm.PictureUrl = updatedFishFarm.PictureUrl;              

                await context.SaveChangesAsync();  
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: An error occurred while updating the fish farm",ex);
            }
        }
    }
}