namespace AquaFlow.Domain.DTOs.FishFarm
{
    public class RetrieveFishFarmDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int NumberOfCages { get; set; }
        public bool HasBarge { get; set; }
        public required string PictureUrl { get; set; }
    }
}
