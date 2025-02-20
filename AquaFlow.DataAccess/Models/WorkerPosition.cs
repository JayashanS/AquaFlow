namespace AquaFlow.DataAccess.Models
{
    public class WorkerPosition
    {
        public int Id { get; set; }
        public  string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
    }
}