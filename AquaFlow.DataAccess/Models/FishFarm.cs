using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlow.DataAccess.Models
{
    public class FishFarm
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int NumberOfCages { get; set; }
        public bool HasBarge { get; set; }
        public string PictureUrl { get; set; }

        public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();

    }
}
