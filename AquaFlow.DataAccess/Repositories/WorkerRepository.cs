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
                throw new Exception("An error occurred while saving the worker data.",ex);
            }
        }

        public async Task DeleteWorkerByIdAsync(int id)
        {
            try
            {
                var workerToBeDeleted = await context.Workers.FirstOrDefaultAsync(w => w.Id == id);
                if (workerToBeDeleted == null)
                {
                    throw new KeyNotFoundException($"Worker with Id:{id} is not found");
                }
   
                context.Remove(workerToBeDeleted);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while deleting worker",ex);
            }
        }

        public async Task<Worker> GetWorkerByIdAsync(int id)
        {
            try
            {
                var worker = await context.Workers.FirstOrDefaultAsync(w => w.Id == id);
                if (worker == null)
                {
                    throw new KeyNotFoundException($"Could not found a worker with Id:{id}");
                }
                return worker;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error occure while retrieving worker", ex); 
            }
        }

        public async Task<IEnumerable<Worker>> GetWorkersByFilterAsync(WorkerFilterOptions filterOptions)
        {
            try
            {
                if (filterOptions == null)
                {
                    throw new ArgumentNullException(nameof(filterOptions), "Filter options cannot be null.");
                }
                var query = context.Workers.AsQueryable();

                query = workerFilterHelper.ApplyNameFilter(query, filterOptions.Name);
                query = workerFilterHelper.ApplyFishFarmIdFilter(query, filterOptions.FishFarmId);
                query = workerFilterHelper.ApplyPositionIdFilter(query, filterOptions.PositionId);
                query = workerFilterHelper.ApplyCertifiedUntilDateFilter(query, filterOptions.CertifiedUntil);
                query = workerFilterHelper.ApplyPaging(query, filterOptions.PageNumber, filterOptions.PageSize);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the workers with filter options", ex);
            }
        }
        public async Task<int> GetTotalWorkerCountAsync(WorkerFilterOptions filterOptions)
        {
            try
            {
                if (filterOptions == null)
                {
                    throw new ArgumentNullException(nameof(filterOptions), "Filter options cannot be null.");
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
                throw new Exception("An error occurred while counting workers with filter options", ex);
            }

        }

        public async Task UpdateWorkerById(int id, Worker updatedWorker)
        {
            try
            {
                var worker = await context.Workers.FirstOrDefaultAsync(w => w.Id == id);
                if(worker == null)
                {
                    throw new KeyNotFoundException($"Worker with Id:{id} not found");
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
                throw new Exception("Error occure updating the worker", ex);
            }
        }

       
    }
}