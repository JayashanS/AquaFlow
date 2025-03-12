using AquaFlow.DataAccess.Filters;
using AquaFlow.DataAccess.Models;

namespace AquaFlow.DataAccess.Interfaces
{
    public interface IWorkerRepository
    {
        Task<Worker> CreateWorkerAsync(Worker worker);
        Task DeleteWorkerByIdAsync(int id);
        Task<IEnumerable<Worker>> GetWorkersByFilterAsync(WorkerFilterOptions filterOptions);
        Task<Worker> GetWorkerByIdAsync(int id);
        Task<int> GetTotalWorkerCountAsync(WorkerFilterOptions filetrOptions);
        Task UpdateWorkerByIdAsync(int id, Worker updatedWorker);
        Task<IEnumerable<Worker>> GetWorkersByFishFarmIdAsync(int id);
        Task<bool> DoesEmailExist(string email);
    }
}