using AquaFlow.DataAccess.Data;
using AquaFlow.DataAccess.Interfaces;
using AquaFlow.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AquaFlow.DataAccess.Repositories
{
    public class WorkerPositionRepository(AppDbContext context) : IWorkerPositionRepository
    {
        public async Task<IEnumerable<WorkerPosition>> GetWorkerPositionsAsync()
        {
            try
            {
                return await context.WorkersPositions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL: Error fetching worker positions from the database.", ex);
            }
        }
    }
}