using AquaFlow.DataAccess.Models;
using System.Text.RegularExpressions;

namespace AquaFlow.DataAccess.Utils
{
    public static class FishFarmFilterHelper
    {
        public static IQueryable<FishFarm> ApplyNameFilter(IQueryable<FishFarm> query, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var regex = new Regex(name, RegexOptions.IgnoreCase);
                query = query.Where(f => regex.IsMatch(f.Name));
            }
            return query;
        }

        public static IQueryable<FishFarm> ApplyLocationFilter(IQueryable<FishFarm> query, double? topRightLat, double? topRightLng, double? bottomLeftLat, double? bottomLeftLng)
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

        public static IQueryable<FishFarm> ApplyNumberOfCagesFilter(IQueryable<FishFarm> query, int? numberOfCages)
        {
            if (numberOfCages.HasValue)
            {
                query = query.Where(f => f.NumberOfCages == numberOfCages.Value);
            }
            return query;
        }

        public static IQueryable<FishFarm> ApplyHasBargeFilter(IQueryable<FishFarm> query, bool? hasBarge)
        {
            if (hasBarge.HasValue)
            {
                query = query.Where(f => f.HasBarge == hasBarge.Value);
            }
            return query;
        }

        public static IQueryable<FishFarm> ApplyPaging(IQueryable<FishFarm> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }

}
