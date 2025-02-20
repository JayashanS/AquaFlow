using Microsoft.AspNetCore.Http;

namespace AquaFlow.Domain.Interfaces
{
    public interface IImageProcessingService
    {
        Task<byte[]> ConvertToJpgAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080);
        bool IsImageValid(IFormFile file);
    }
}