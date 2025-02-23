using System.ComponentModel.DataAnnotations.Schema;

namespace AquaFlow.Domain.DTOs.Worker
{
    public class RetrieveWorkerDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? PictureUrl { get; set; }
        public int Age { get; set; }
        public required string Email { get; set; }
        public int PositionId { get; set; }
        public DateTime CertifiedUntil { get; set; }
        public int FishFarmId { get; set; }
        public required string FishFarmName { get; set; }
        public required string PositionName { get; set; }
    }
}