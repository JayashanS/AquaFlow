namespace AquaFlow.DataAccess.Interfaces
{
    public interface IWorkerRepository
    {
        Task CreateWorkerAsync();
        Task DeleteWorkerByIdAsync(int id);
        Task GetWorkersAsync();
        Task GetWorkerByIdAsync(int id);
        Task UpdateWorkerById(int id);
    }
}