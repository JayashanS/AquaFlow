namespace AquaFlow.Domain.DTOs.FishFarm
{
    public class  RetrieveFishFarmWithTotalDTO
    {
        public IEnumerable<RetrieveFishFarmDTO> FishFarms { get; set; } = [];
        public int TotalCount { get; set; }
    }
}