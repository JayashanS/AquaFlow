namespace AquaFlow.DataAccess.Filters
{
    public class WorkerFilterOptions
    {
        public string? Name { get; set; }
        public int? FishFarmId { get; set; }
        public int? PositionId { get; set; } 
        public DateTime? CertifiedUntil { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}