using AquaFlow.DataAccess.Interfaces;
using AquaFlow.DataAccess.Repositories;
using AquaFlow.Domain.Interfaces;
using AquaFlow.Domain.Services;

namespace AquaFlow.API.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
           
            services.AddScoped<IFishFarmRepository, FishFarmRepository>();

            
            services.AddScoped<IFishFarmService, FishFarmService>();

        }
    }
}