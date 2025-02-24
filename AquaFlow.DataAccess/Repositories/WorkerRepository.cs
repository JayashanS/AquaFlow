using AquaFlow.DataAccess.Data;
using AquaFlow.DataAccess.Filters;
using AquaFlow.DataAccess.Interfaces;
using AquaFlow.DataAccess.Models;
using AquaFlow.DataAccess.Utils;
using Microsoft.EntityFrameworkCore;

namespace AquaFlow.DataAccess.Repositories
{
    public class WorkerRepository(AppDbContext context, WorkerFilterHelper workerFilterHelper): IWorkerRepository
    {
        public async Task<Worker> CreateWorkerAsync(Worker worker)
        {
            try
            {
                await context.Workers.AddAsync(worker);
                await context.SaveChangesAsync();
                return worker;
            }
            catch (Exception ex) 
            {           
                throw new Exception("DAL: An error occurred while saving the worker data.",ex);
            }
        }

        public async Task DeleteWorkerByIdAsync(int id)
        {
            try
            {
                var workerToBeDeleted = await context.Workers.FirstOrDefaultAsync(w => w.Id == id);
                if (workerToBeDeleted == null)
                {
                    throw new KeyNotFoundException($"DAL: Worker with Id:{id} is not found");
                }
   
                context.Remove(workerToBeDeleted);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: Error occured while deleting worker", ex);
            }
        }

        public async Task<Worker> GetWorkerByIdAsync(int id)
        {
            try
            {
                var worker = await context.Workers.FirstOrDefaultAsync(w => w.Id == id);
                if (worker == null)
                {
                    throw new KeyNotFoundException($"DAL: Could not found a worker with Id:{id}");
                }
                return worker;
            }
            catch (Exception ex) 
            {
                throw new Exception("DAL: Error occure while retrieving worker", ex); 
            }
        }

        public async Task<IEnumerable<Worker>> GetWorkersByFilterAsync(WorkerFilterOptions filterOptions)
        {
            try
            {
                if (filterOptions == null)
                {
                    throw new ArgumentNullException("DAL: Filter options cannot be null.");
                }
                var query = context.Workers.AsQueryable();

                query = workerFilterHelper.ApplyNameFilter(query, filterOptions.Name);
                query = workerFilterHelper.ApplyFishFarmIdFilter(query, filterOptions.FishFarmId);
                query = workerFilterHelper.ApplyPositionIdFilter(query, filterOptions.PositionId);
                query = workerFilterHelper.ApplyCertifiedUntilDateFilter(query, filterOptions.CertifiedUntil);
                query = workerFilterHelper.ApplyPaging(query, filterOptions.PageNumber, filterOptions.PageSize);

                var result = await query
                    .Include(w => w.FishFarm)
                    .Include(w => w.Position)
                    .Select(w => new Worker
                    {
                        Id = w.Id,
                        Name = w.Name,
                        PictureUrl = w.PictureUrl,
                        Email = w.Email,
                        Age = w.Age,
                        PositionId = w.PositionId,
                        CertifiedUntil = w.CertifiedUntil,
                        FishFarmId = w.FishFarmId,
                        FishFarmName = w.FishFarm.Name,
                        PositionName = w.Position.Name,
                    })
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: An error occurred while retrieving the workers with filter options", ex);
            }
        }
        public async Task<int> GetTotalWorkerCountAsync(WorkerFilterOptions filterOptions)
        {
            try
            {
                if (filterOptions == null)
                {
                    throw new ArgumentNullException("DAL: Filter options cannot be null.");
                }
                var query = context.Workers.AsQueryable();

                query = workerFilterHelper.ApplyNameFilter(query, filterOptions.Name);
                query = workerFilterHelper.ApplyFishFarmIdFilter(query, filterOptions.FishFarmId);
                query = workerFilterHelper.ApplyPositionIdFilter(query, filterOptions.PositionId);
                query = workerFilterHelper.ApplyCertifiedUntilDateFilter(query, filterOptions.CertifiedUntil);
                
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: An error occurred while counting workers with filter options", ex);
            }

        }

        public async Task UpdateWorkerByIdAsync(int id, Worker updatedWorker)
        {
            try
            {
                var worker = await context.Workers.FirstOrDefaultAsync(w => w.Id == id);
                if(worker == null)
                {
                    throw new KeyNotFoundException($"DAL: Worker with Id:{id} not found");
                }

                worker.Name = updatedWorker.Name;
                worker.PictureUrl = updatedWorker.PictureUrl;
                worker.Email = updatedWorker.Email;
                worker.Age = updatedWorker.Age;
                worker.PositionId = updatedWorker.PositionId;
                worker.CertifiedUntil = updatedWorker.CertifiedUntil;
                worker.FishFarmId = updatedWorker.FishFarmId;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: Error occure updating the worker", ex);
            }
        }

        public async Task<IEnumerable<Worker>> GetWorkersByFishFarmIdAsync(int id)
        {
            try
            {
                var workers = await context.Workers.Where(w => w.FishFarmId == id).ToListAsync();
                return workers;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: Error occured while finding workers", ex);
            }
        }
    }
}