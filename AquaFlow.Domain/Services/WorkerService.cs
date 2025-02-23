using AquaFlow.API.Utils;
using AquaFlow.DataAccess.Filters;
using AquaFlow.DataAccess.Interfaces;
using AquaFlow.DataAccess.Models;
using AquaFlow.Domain.DTOs.Worker;
using AquaFlow.Domain.Interfaces;
using AutoMapper;

namespace AquaFlow.Domain.Services
{
    public class WorkerService(IWorkerRepository workerRepository, IMapper mapper, FileUploadHelper fileUploadHelper) : IWorkerService
    {
        public async Task<RetrieveWorkerDTO> CreateWorkerAsync(CreateWorkerDTO workerDTO)
        {
            string pictureUrl = null;
            if(workerDTO.Picture != null)
            {
                pictureUrl = await fileUploadHelper.SaveFileAsync(workerDTO.Picture);
            }
            try
            {
                var worker = mapper.Map<Worker>(workerDTO);
                worker.PictureUrl = pictureUrl;

                var createdWorker = await workerRepository.CreateWorkerAsync(worker);
                return mapper.Map<RetrieveWorkerDTO>(createdWorker);
            }
            catch (Exception ex) 
            {
                throw new Exception("SVC: Internal server error",ex);
            }
              
        }

        public async Task DeleteWorkerByIdAsync(int id)
        {
            try
            {
                await workerRepository.DeleteWorkerByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("SVC: Internal server error", ex);
            }
        }

        public async Task<RetrieveWorkerDTO> GetWorkerByIdAsync(int id)
        {
            try
            {
                var worker = await workerRepository.GetWorkerByIdAsync(id);
                return mapper.Map<RetrieveWorkerDTO>(worker);
            }
            catch (Exception ex)
            {
                throw new Exception("SVC: Internal server error", ex);
            }
        }

        public async Task<RetrieveWorkerWithTotalDTO> GetWorkersByFilterAsync(WorkerFilterOptions filterOptions)
        {
            try
            {
                int count = await workerRepository.GetTotalWorkerCountAsync(filterOptions);
                var workers = await workerRepository.GetWorkersByFilterAsync(filterOptions);

                return new RetrieveWorkerWithTotalDTO
                {
                    Workers = mapper.Map<IEnumerable<RetrieveWorkerDTO>>(workers),
                    TotalCount = count

                };
            }
            catch (Exception ex)
            {
                throw new Exception("SVC: Internal server error", ex);
            }
        }

        public Task UpdateWorkerById(int id, UpdateWorkerDTO updatedWorkerDTO)
        {
            throw new NotImplementedException();
        }
    }
}