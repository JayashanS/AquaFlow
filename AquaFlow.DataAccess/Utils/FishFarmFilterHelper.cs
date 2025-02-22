using AquaFlow.DataAccess.Models;

namespace AquaFlow.DataAccess.Utils
{
    public  class FishFarmFilterHelper
    {
        public  IQueryable<FishFarm> ApplyNameFilter(IQueryable<FishFarm> query, string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(f => f.Name.Contains(name));
            }
            return query;
        }

        public  IQueryable<FishFarm> ApplyLocationFilter(IQueryable<FishFarm> query, double? topRightLat, double? topRightLng, double? bottomLeftLat, double? bottomLeftLng)
        {
            if (topRightLat.HasValue && topRightLng.HasValue && bottomLeftLat.HasValue && bottomLeftLng.HasValue)
            {
                query = query.Where(f => f.Location.Y <= topRightLat.Value &&
                                          f.Location.Y >= bottomLeftLat.Value &&
                                          f.Location.X <= topRightLng.Value &&
                                          f.Location.X >= bottomLeftLng.Value);
            }
            return query;
        }

        public  IQueryable<FishFarm> ApplyNumberOfCagesFilter(IQueryable<FishFarm> query, int? numberOfCages)
        {
            if (numberOfCages.HasValue)
            {
                query = query.Where(f => f.NumberOfCages >= numberOfCages.Value);
            }
            return query;
        }

        public  IQueryable<FishFarm> ApplyHasBargeFilter(IQueryable<FishFarm> query, bool? hasBarge)
        {
            if (hasBarge.HasValue)
            {
                query = query.Where(f => f.HasBarge == hasBarge.Value);
            }
            return query;
        }

        public  IQueryable<FishFarm> ApplyPaging(IQueryable<FishFarm> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }

}
