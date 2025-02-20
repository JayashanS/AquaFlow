namespace AquaFlow.DataAccess.Filters
{
    public class FishFarmFilterOptions
    {
        public string? Name { get; set; }
        public double? TopRightLat { get; set; }
        public double? TopRightLng { get; set; }
        public double? BottomLeftLat { get; set; }
        public double? BottomLeftLng { get; set; }
        public int? NumberOfCages { get; set; }
        public bool? HasBarge { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}