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
        Task UpdateWorkerById(int id, Worker updatedWorker);
    }
}