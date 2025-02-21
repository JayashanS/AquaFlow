namespace AquaFlow.Domain.DTOs.Worker
{
    public class RetrieveWorkerWithTotalDTO
    {
        public IEnumerable<RetrieveWorkerDTO> Workers { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
