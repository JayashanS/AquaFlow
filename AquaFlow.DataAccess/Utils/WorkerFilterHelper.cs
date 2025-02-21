using AquaFlow.DataAccess.Models;

namespace AquaFlow.DataAccess.Utils
{
    public class WorkerFilterHelper
    {
        public IQueryable<Worker> ApplyNameFilter(IQueryable<Worker> query, string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return query.Where(x => x.Name.Contains(name));
            }
            return query;
        }

        public IQueryable<Worker> ApplyFishFarmIdFilter(IQueryable<Worker> query, int? id)
        {
            if (id.HasValue)
            {
                return query.Where(x => x.FishFarmId == id);
            }
            return query;
        }

        public IQueryable<Worker> ApplyPositionIdFilter(IQueryable<Worker> query, int? id)
        {
            if (id.HasValue)
            {
                return query.Where(x => x.PositionId == id);
            }
            return query;
        }

        public IQueryable<Worker> ApplyCertifiedUntilDateFilter(IQueryable<Worker> query, DateTime? date)
        {
            if (date.HasValue)
            {
                return query.Where(x => x.CertifiedUntil > date);
            }
            return query;
        }

        public  IQueryable<Worker> ApplyPaging(IQueryable<Worker> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}