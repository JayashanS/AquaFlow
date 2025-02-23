using System.ComponentModel.DataAnnotations.Schema;

namespace AquaFlow.DataAccess.Models
{
    public class Worker
    {
        public int Id { get; set; } 
        public required string Name { get; set; }
        public string? PictureUrl { get; set; }
        public int Age { get; set; }
        public required string Email { get; set; } 
        public int PositionId { get; set; } 
        public DateTime CertifiedUntil { get; set; }
        public int FishFarmId { get; set; }

        [NotMapped] 
        public string FishFarmName { get; set; }

        [NotMapped]
        public string PositionName { get; set; }

        public virtual WorkerPosition Position { get; set; }
        public virtual FishFarm FishFarm { get; set; }
    }
}