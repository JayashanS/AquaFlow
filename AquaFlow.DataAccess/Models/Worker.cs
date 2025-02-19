using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlow.DataAccess.Models
{
    public class Worker
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string? PictureUrl { get; set; }
        public int Age { get; set; }
        public string Email { get; set; } 
        public int PositionId { get; set; } 
        public DateTime CertifiedUntil { get; set; }
        public int FishFarmId { get; set; }

        public virtual WorkerPosition Position { get; set; }
        public virtual FishFarm FishFarm { get; set; }
    }
}
