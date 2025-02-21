using AquaFlow.DataAccess.Filters;
using AquaFlow.Domain.DTOs.Worker;

namespace AquaFlow.Domain.Interfaces
{
    public interface IWorkerService
    {
        Task<RetrieveWorkerDTO> CreateWorkerAsync(CreateWorkerDTO workerDTO);
        Task DeleteWorkerByIdAsync(int id);
        Task<RetrieveWorkerWithTotalDTO> GetWorkersByFilterAsync(WorkerFilterOptions filterOptions);
        Task<RetrieveWorkerDTO> GetWorkerByIdAsync(int id);
        Task UpdateWorkerById(int id, UpdateWorkerDTO updatedWorkerDTO);
    }
}