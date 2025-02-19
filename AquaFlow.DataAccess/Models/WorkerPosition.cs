using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlow.DataAccess.Models
{
    public class WorkerPosition
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
    }
}