using Microsoft.AspNetCore.Http;

namespace AquaFlow.Domain.DTOs.Worker
{
    public class CreateWorkerDTO
    {
        public required string Name { get; set; }
        public IFormFile? Picture { get; set; }
        public int Age { get; set; }
        public required string Email { get; set; }
        public int PositionId { get; set; }
        public DateTime CertifiedUntil { get; set; }
        public int FishFarmId { get; set; }
    }
}