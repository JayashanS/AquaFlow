using AquaFlow.DataAccess.Interfaces;
using AquaFlow.Domain.DTOs.WorkerPosition;
using AquaFlow.Domain.Interfaces;
using AutoMapper;

namespace AquaFlow.Domain.Services
{
    public class WorkerPositionService(IWorkerPositionRepository workerPositionRepository, IMapper mapper) : IWorkerPositionService
    {
        public async Task<IEnumerable<RetrieveWorkerPositionDTO>> GetWorkerPositionsAsync()
        {
            try
            {
                var workerPositions = await workerPositionRepository.GetWorkerPositionsAsync();
                return mapper.Map<IEnumerable<RetrieveWorkerPositionDTO>>(workerPositions);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in service layer while fetching worker positions.", ex);
            }
        }
    }
}