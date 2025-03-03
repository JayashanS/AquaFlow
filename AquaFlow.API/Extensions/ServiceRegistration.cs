﻿using AquaFlow.DataAccess.Interfaces;
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
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IWorkerPositionRepository, WorkerPositionRepository>();
            
            services.AddScoped<IFishFarmService, FishFarmService>();
            services.AddScoped<IWorkerService, WorkerService>();  
            services.AddScoped<IWorkerPositionService , WorkerPositionService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") 
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


        }
    }
}