namespace AquaFlow.Domain.DTOs.FishFarm
{
    public class CreateFishFarmDTO
    {
        public string Name { get; set; }    
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int NumberOfCages { get; set; } 
        public bool HasBarge { get; set; }
        public string PictureUrl { get; set; }
    }
}
