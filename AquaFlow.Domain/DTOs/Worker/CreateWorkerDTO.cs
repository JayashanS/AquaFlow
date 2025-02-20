namespace AquaFlow.Domain.DTOs.Worker
{
    public class CreateWorkerDTO
    {
        public string Name { get; set; }
        public string? PictureUrl { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int PositionId { get; set; }
        public DateTime CertifiedUntil { get; set; }
        public int FishFarmId { get; set; }
    }
}