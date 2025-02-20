namespace AquaFlow.Domain.DTOs.FishFarm
{
    public class RetrieveFishFarmDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int NumberOfCages { get; set; }
        public bool HasBarge { get; set; }
        public string PictureUrl { get; set; }
    }
}
