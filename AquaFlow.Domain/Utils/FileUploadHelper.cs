using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using Microsoft.AspNetCore.Http;

namespace AquaFlow.API.Utils
{
    public class FileUploadHelper
    {
        private readonly string uploadsFolder;

        public FileUploadHelper()
        {
            uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "fishfarms");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                await image.SaveAsync(filePath, new JpegEncoder());
            }

            return $"/uploads/fishfarms/{fileName}";
        }
    }
}