using AquaFlow.DataAccess.Models;
using AquaFlow.Domain.DTOs.FishFarm;
using AquaFlow.Domain.DTOs.Worker;
using AquaFlow.Domain.DTOs.WorkerPosition;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace AquaFlow.Domain.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<CreateFishFarmDTO, AquaFlow.DataAccess.Models.FishFarm>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom( src =>
                    new Point(Math.Round(src.Longitude, 4), Math.Round(src.Latitude, 4)) { SRID = 4326 }))
                .ForMember(dest => dest.PictureUrl, opt => opt.Ignore());
            CreateMap<UpdateFishFarmDTO, AquaFlow.DataAccess.Models.FishFarm>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src =>
                    new Point(Math.Round(src.Longitude, 4), Math.Round(src.Latitude, 4)) { SRID = 4326 }));
            CreateMap<AquaFlow.DataAccess.Models.FishFarm, RetrieveFishFarmDTO>()
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.X))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Y));

            CreateMap<Worker, CreateWorkerDTO>();
            CreateMap<Worker, RetrieveWorkerDTO>();

            CreateMap<WorkerPosition, CreateWorkerPositionDTO>();
            CreateMap<WorkerPosition, RetrieveWorkerPositionDTO>();

        }
    }
}