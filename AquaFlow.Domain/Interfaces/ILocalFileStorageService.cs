using Microsoft.AspNetCore.Http;

namespace AquaFlow.Domain.Interfaces
{
    public interface ILocalFileStorageService
    {
        Task<string> SaveImageAsync(IFormFile file, string folder);
        Task DeleteImageAsync(string filePath);
        bool IsImageValid(IFormFile file);
    }
}