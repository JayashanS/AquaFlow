using AquaFlow.DataAccess.Models;

namespace AquaFlow.DataAccess.Interfaces
{
    public interface IWorkerPositionRepository
    {
        Task<IEnumerable<WorkerPosition>> GetWorkerPositionsAsync();
    }
}