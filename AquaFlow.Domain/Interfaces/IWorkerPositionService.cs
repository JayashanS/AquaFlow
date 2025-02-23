using AquaFlow.Domain.DTOs.WorkerPosition;

namespace AquaFlow.Domain.Interfaces
{
    public interface IWorkerPositionService
    {
        Task<IEnumerable<RetrieveWorkerPositionDTO>> GetWorkerPositionsAsync();
    }
}